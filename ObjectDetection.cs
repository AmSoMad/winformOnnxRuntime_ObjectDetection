using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using ZXing;

namespace OnnxProject
{
    public partial class ObjectDetection : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        //COCO 클래스 80개
        private static string[] cocoClasses = new string[]
        {
            "person","bicycle","car","motorcycle","airplane","bus","train","truck","boat","trafficlight","firehydrant","stopsign","parkingmeter","bench","bird","cat","dog","horse","sheep","cow","elephant","bear","zebra","giraffe","backpack","umbrella","handbag","tie","suitcase","frisbee","skis","snowboard","sportsball","kite","baseballbat","baseballglove","skateboard","surfboard","tennisracket","bottle","wineglass","cup","fork","knife","spoon","bowl","banana","apple","sandwich","orange","broccoli","carrot","hotdog","pizza","donut","cake","chair","couch","pottedplant","bed","diningtable","toilet","tv","laptop","mouse","remote","keyboard","cellphone","microwave","oven","toaster","sink","refrigerator","book","clock","vase","scissors","teddybear","hairdrier","toothbrush"
        };
         

        // 마지막 창 상태 저장
        private FormWindowState lastWindowState;

        private ToolTip formToolTip = new ToolTip();

        private PictureBox pictureBox;

        // 전역 또는 클래스 멤버 변수로 선언
        private InferenceSession session;
        private string inputName;
        private string outputName;
        private List<Tuple<RectangleF, string, float>> rectData;

        // 캡쳐 중인지 여부
        bool isCapture = false;

        public ObjectDetection()
        {
            InitializeComponent();
            init();
            //리소스에 저장된 yolov5s.onnx 추가
            session = new InferenceSession(Properties.Resources.yolov5s); // 모델의 경로

            //session = new InferenceSession("./Models/yolov5s.onnx"); // 모델의 경로
            inputName = session.InputMetadata.Keys.FirstOrDefault();
            outputName = session.OutputMetadata.Keys.FirstOrDefault();
            rectData = new List<Tuple<RectangleF, string, float>>(); // 수정된 부분
        }

        private void init()
        {
            this.TransparencyKey = Color.Transparent;
            this.TopMost = true;
            tooltipSetting();
        }

        private void tooltipSetting()
        {
            formToolTip.SetToolTip(this.closeBtn, "닫기");
            formToolTip.SetToolTip(this.fullScreenBtn, "전체화면");
            formToolTip.SetToolTip(this.normalScreenBtn, "기본화면");
            formToolTip.SetToolTip(this.captureBtn, "캡쳐");
            formToolTip.SetToolTip(this.captureStopBtn, "캡쳐 중지");

        }

        // 창 크기 변경을 위한 코드
        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }

            base.WndProc(ref m);
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // 창 이동을 위한 코드
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            if (this is Form)
            {
                ((Form)this).Close();
                return;
            }
                
            if (this.Parent is Form)
                ((Form)this.Parent).Close();
            else if (this.Parent.Parent is Form)
                ((Form)this.Parent.Parent).Close();
            else if (this.Parent.Parent.Parent is Form)
                ((Form)this.Parent.Parent.Parent).Close();
        }

        private void fullScreenBtn_Click(object sender, EventArgs e)
        {
            ToggleFullScreen(true); // 전체화면으로 전환
            fullScreenBtn.Visible = false; // 전체화면 버튼 숨김
            normalScreenBtn.Visible = true; // 원래 크기 버튼 표시
        }

        private void normalScreenBtn_Click(object sender, EventArgs e)
        {
            ToggleFullScreen(false); // 원래 크기로 복원
            fullScreenBtn.Visible = true; // 전체화면 버튼 표시
            normalScreenBtn.Visible = false; // 원래 크기 버튼 숨김
        }

        // 전체화면 또는 원래 크기로 전환하는 함수
        private void ToggleFullScreen(bool isFullScreen)
        {
            Form parentForm = FindParentForm(this); // 부모 폼을 찾음
            if (parentForm != null) // 부모 폼이 있는 경우
            {
                if (isFullScreen) // 전체화면으로 전환
                {
                    lastWindowState = parentForm.WindowState; // 현재 창 상태를 저장
                    parentForm.WindowState = FormWindowState.Maximized; // 전체화면으로 설정
                }
                else // 원래 크기로 복원
                {
                    parentForm.WindowState = lastWindowState; // 저장된 창 상태로 복원
                    parentForm.Width = 300; // 폼의 너비를 800으로 설정
                    parentForm.Height = 300; // 폼의 높이를 600으로 설정
                }
            }
        }

        // 주어진 컨트롤의 부모 폼을 찾는 함수
        private Form FindParentForm(Control control)
        {
            if (control is Form) // 현재 컨트롤이 Form인 경우
            {
                return (Form)control;
            }
            else if (control.Parent != null) // 부모 컨트롤이 있는 경우
            {
                return FindParentForm(control.Parent); // 재귀적으로 부모 폼을 찾음
            }
            return null; // 부모 폼이 없는 경우 null 반환
        }

        private void screenMinimizedBtn_Click(object sender, EventArgs e)
        {
            // 창을 작업 표시줄로 보냄 (최소화)
            this.WindowState = FormWindowState.Minimized;
        }

        private void captureBtn_Click(object sender, EventArgs e)
        {
            isCapture = true;
            IconChange();
            DeskTopCapture();
        }

        private void IconChange()
        {
            // 캡쳐여부에 따라 이미지 변경
            if (isCapture)
            {
                captureBtn.Visible = false;
                captureStopBtn.Visible = true;
            }
            else
            {
                captureBtn.Visible = true;
                captureStopBtn.Visible = false;
            }
        }

        /// <summary>
        /// Desktop 캡쳐 함수
        /// </summary>
        private void DeskTopCapture()
        {

            capturePanel.Controls.Clear();

            this.pictureBox = new PictureBox();
            this.pictureBox.Size = capturePanel.Size;
            this.pictureBox.Location = new Point(0, 0);
            this.pictureBox.BackColor = Color.White;
            capturePanel.Controls.Add(this.pictureBox);

            // capturePanel의 스크린 좌표 구하기
            Point screenPoint = capturePanel.PointToScreen(Point.Empty);

            // 스크린 캡처 로직
            Bitmap bmp = new Bitmap(capturePanel.Width, capturePanel.Height);

            using (Graphics g = Graphics.FromImage(bmp as Image))
            {
                g.CopyFromScreen(screenPoint.X, screenPoint.Y, 0, 0, capturePanel.Size);
            }
            // PictureBox에 완성된 이미지를 설정합니다.
            //pictureBox.Image = bmp;
            OnnxRuntime_ObjectDetect(bmp);
            this.TransparencyKey = Color.LimeGreen;
        }

        private void captureStopBtn_Click(object sender, EventArgs e)
        {
            isCapture = false;
            IconChange();
            this.TransparencyKey = Color.Transparent;
            // 캡쳐 중지 플래그 설정
            

            // PictureBox 컨트롤을 찾아 Image를 null로 설정하고 메모리 해제
            foreach (Control control in capturePanel.Controls)
            {
                if (control is PictureBox)
                {
                    this.pictureBox = (PictureBox)control;
                    if (this.pictureBox.Image != null)
                    {
                        this.pictureBox.Image.Dispose(); // 메모리 해제
                        this.pictureBox.Image = null;    // Image를 null로 설정
                    }
                }
            }

            // 필요하다면 PictureBox 컨트롤 자체를 제거
            // capturePanel.Controls.Clear();
        }

        private void PanelResizeing()
        {
            //사이즈 변경시 panelSizeLabel에 사이즈 표시
            string width = capturePanel.Width.ToString();
            string height = capturePanel.Height.ToString();
            panelSizeLabel.Text = width + " x " + height;
            //panelSizeLabel.Text = this.MinimumSize.Width.ToString() + this.MinimumSize.Height.ToString();
        }

        private void ObjectDetection_Resize(object sender, EventArgs e)
        {
            PanelResizeing();
        }

        private void ObjectDetection_KeyDown(object sender, KeyEventArgs e)
        {
            //스페이스바 누르면 캡쳐
            if (e.KeyCode == Keys.Space)
            {
                if (isCapture)
                {
                    captureStopBtn_Click(sender, e);
                }
                else
                {
                    captureBtn_Click(sender, e);
                }
            }

        }

        /// <summary>
        /// ONNX 모델을 활용하여 객체 검출을 수행하는 함수
        /// </summary>
        private void OnnxRuntime_ObjectDetect(Image inputImage)
        {
            // 입력 이미지를 리사이즈하고 전처리
            Bitmap resizedBitmap = ResizeAndPreprocessImage(inputImage, 640, 640);

            // 이미지를 텐서 형식으로 변환
            var tensorInput = PreprocessImage(resizedBitmap, 640, 640);

            // 기존의 경계 상자 데이터를 클리어
            this.rectData.Clear();

            // 객체 검출 추론 실행
            RunInference(tensorInput);

            // 경계 상자가 그려진 비트맵 생성
            Bitmap bboxBitmap = GenerateBoundingBoxBitmap(inputImage);

            // 원본 이미지에 경계 상자가 그려진 이미지 저장
            Bitmap bboxBitmap_origin = DrawBoundingBoxOnOriginalImage(inputImage, rectData);

            // PictureBox 이미지 업데이트
            pictureBox.Image = bboxBitmap;
        }

        // 입력 이미지를 리사이즈하고 전처리하는 함수
        private Bitmap ResizeAndPreprocessImage(Image inputImage, int width, int height)
        {
            return imageResize((Bitmap)inputImage, width, height);
        }

        // ONNX 모델 추론을 실행하고 출력 텐서를 얻는 함수
        private void RunInference(DenseTensor<float> runs)
        {
            var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor<float>(inputName, runs) };
            using (var results = session.Run(inputs))
            {
                var resultTensor = results.First(r => r.Name == this.outputName).AsTensor<float>();
                ProcessResults(resultTensor.ToArray());
            }
        }

        // 출력 텐서를 처리하여 경계 상자와 신뢰 점수 추출
        private void ProcessResults(float[] result)
        {
            for (int i = 0; i < result.Length; i += 85)
            {
                float objConfidence = result[i + 4];
                if (objConfidence > 0.5)
                {
                    CollectHighConfidenceData(result, i);
                }
            }
        }

        // 높은 신뢰도를 가진 경계 상자 데이터를 수집
        private void CollectHighConfidenceData(float[] result, int index)
        {
            float x = result[index];
            float y = result[index + 1];
            float width = result[index + 2];
            float height = result[index + 3];
            float maxScore = result.Skip(index + 5).Take(80).Max();
            int classIndex = result.Skip(index + 5).Take(80).ToList().IndexOf(maxScore);

            if (maxScore > 0.5)
            {
                RectangleF rect = new RectangleF(x - width / 2, y - height / 2, width, height);
                string className = cocoClasses[classIndex];
                rectData.Add(new Tuple<RectangleF, string, float>(rect, className, maxScore));
            }
        }

        // 경계 상자가 그려진 새로운 비트맵을 생성
        private Bitmap GenerateBoundingBoxBitmap(Image originalImage)
        {
            // 중복된 경계 상자 제거
            List<Tuple<RectangleF, string, float>> nonDuplicateRectData = RemoveDuplicates(rectData);
            Bitmap BBox_bitmap = new Bitmap(originalImage.Width, originalImage.Height);

            //배경 투명하게 설정하기
            for (int x = 0; x < BBox_bitmap.Width; x++)
            {
                for (int y = 0; y < BBox_bitmap.Height; y++)
                {
                    BBox_bitmap.SetPixel(x, y, Color.LimeGreen); // 투명한 픽셀로 설정
                }
            }

            // 경계 상자의 크기 조정을 위한 스케일링 인자 계산
            float xScale = (float)originalImage.Width / 640;
            float yScale = (float)originalImage.Height / 640;

            Graphics graphics_BBox = Graphics.FromImage(BBox_bitmap);
            foreach (var data in nonDuplicateRectData)
            {
                DrawBoundingBoxAndText(graphics_BBox, data, xScale, yScale);
            }

            return BBox_bitmap;
        }

        // 원본 이미지에 경계 상자가 그려진 이미지를 생성
        private Bitmap DrawBoundingBoxOnOriginalImage(Image originalImage, List<Tuple<RectangleF, string, float>> nonDuplicateRectData)
        {
            Bitmap bmp = new Bitmap(originalImage);
            Graphics graphics = Graphics.FromImage(bmp);
            float xScale = (float)originalImage.Width / 640;
            float yScale = (float)originalImage.Height / 640;

            foreach (var data in nonDuplicateRectData)
            {
                RectangleF rect = new RectangleF(data.Item1.X * xScale, data.Item1.Y * yScale, data.Item1.Width * xScale, data.Item1.Height * yScale); ;
                string className = data.Item2;
                float maxScore = data.Item3;

                graphics.DrawRectangle(new Pen(Color.Red, 2), Rectangle.Round(rect));

                string text = $"{className}";
                Font font = new Font("Arial", 14);
                PointF point = new PointF(rect.X, rect.Y - 20);

                graphics.DrawString(text, font, Brushes.AliceBlue, point);
            }

            return bmp;
        }

        // 그래픽 객체에 경계 상자와 관련 텍스트를 그림
        private void DrawBoundingBoxAndText(Graphics graphics, Tuple<RectangleF, string, float> data, float xScale, float yScale)
        {
            RectangleF scaledRect = new RectangleF(data.Item1.X * xScale, data.Item1.Y * yScale, data.Item1.Width * xScale, data.Item1.Height * yScale);
            graphics.DrawRectangle(new Pen(Color.Red, 2), Rectangle.Round(scaledRect));

            string text = $"클래스: {data.Item2}, 점수: {data.Item3}";
            Font font = new Font("Arial", 14);
            PointF point = new PointF(scaledRect.X, scaledRect.Y - 20);
            graphics.DrawString(text, font, Brushes.AliceBlue, point);
        }


        private DenseTensor<float> PreprocessImage(Bitmap bmp, int width, int height)
        {
            Bitmap resized = new Bitmap(bmp, new Size(width, height));
            var tensor = new DenseTensor<float>(new[] { 1, 3, width, height });
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var color = resized.GetPixel(x, y);
                    tensor[0, 0, y, x] = color.R / 255.0f;
                    tensor[0, 1, y, x] = color.G / 255.0f;
                    tensor[0, 2, y, x] = color.B / 255.0f;
                }
            }
            return tensor;
        }


        /// <summary>
        /// 중복제거 함수
        /// </summary>
        /// <param name="rectangles"></param>
        /// <returns></returns>
        private List<Tuple<RectangleF, string, float>> RemoveDuplicates(List<Tuple<RectangleF, string, float>> rectData)
        {
            List<Tuple<RectangleF, string, float>> nonDuplicateRectData = new List<Tuple<RectangleF, string, float>>();

            foreach (var data1 in rectData)
            {
                bool isDuplicate = false;
                foreach (var data2 in nonDuplicateRectData)
                {
                    RectangleF rect1 = data1.Item1;
                    RectangleF rect2 = data2.Item1;

                    float center1_x = rect1.X + rect1.Width / 2;
                    float center1_y = rect1.Y + rect1.Height / 2;
                    float center2_x = rect2.X + rect2.Width / 2;
                    float center2_y = rect2.Y + rect2.Height / 2;

                    float distance = (float)Math.Sqrt(Math.Pow(center1_x - center2_x, 2) + Math.Pow(center1_y - center2_y, 2));

                    if (distance < 50)
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    nonDuplicateRectData.Add(data1);
                }
            }

            return nonDuplicateRectData;
        }

        /// <summary>
        /// 640x640으로 이미지 크기 조정
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Bitmap imageResize(Bitmap originalImage, int newWidth, int newHeight)
        {
            Bitmap resizedImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }
            return resizedImage;
        }

        /// <summary>
        /// 현재 결과가 보여진 capturePanel을 이미지로 저장
        /// 이미지 저장 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBtn_Click(object sender, EventArgs e)
        {
            //capturePanel의 스크린 좌표 구하기
            Point screenPoint = capturePanel.PointToScreen(Point.Empty);

            // 스크린 캡처 로직
            Bitmap bmp = new Bitmap(capturePanel.Width, capturePanel.Height);

            using (Graphics g = Graphics.FromImage(bmp as Image))
            {
                g.CopyFromScreen(screenPoint.X, screenPoint.Y, 0, 0, capturePanel.Size);
            }

            // 이미지 저장
            // 파일명 Default로 
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG|*.png|JPG|*.jpg|BMP|*.bmp";
            saveFileDialog.Title = "이미지 저장";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                FileStream fs = (FileStream)saveFileDialog.OpenFile();
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        bmp.Save(fs, ImageFormat.Png);
                        break;
                    case 2:
                        bmp.Save(fs, ImageFormat.Jpeg);
                        break;
                    case 3:
                        bmp.Save(fs, ImageFormat.Bmp);
                        break;
                }
                fs.Close();
            }

        }

        private void barcodeScanBtn_Click(object sender, EventArgs e)
        {
            // Task로 비동기로 바코드 스캔을 실행
            Task.Run(() =>
            {
                BarcodeReader barcodeReader = new BarcodeReader();
                string scannedBarcode = string.Empty;

                while (string.IsNullOrEmpty(scannedBarcode))
                {
                    // capturePanel의 스크린 좌표와 크기를 가져옴
                    Point screenPoint = Point.Empty;
                    Size screenSize = Size.Empty;
                    this.Invoke((MethodInvoker)delegate
                    {
                        screenPoint = capturePanel.PointToScreen(Point.Empty);
                        screenSize = capturePanel.Size;
                    });
                    // capturePanel의 위치와 크기에 해당하는 데스크탑 영역을 캡쳐
                    using (Bitmap captureBitmap = CaptureDesktopArea(screenPoint, screenSize))
                    {
                        if (captureBitmap != null)
                        {
                            var result = barcodeReader.Decode(captureBitmap);
                            if (result != null)
                            {
                                scannedBarcode = result.Text;
                                // 읽은 바코드 출력
                                Console.WriteLine($"Decoded barcode text: {result.Text}");
                                Console.WriteLine($"Barcode format: {result.BarcodeFormat}");
                            }
                            else
                            {
                                Console.WriteLine("Could not decode image.");
                            }
                        }
                    }

                    // 스레드 지연 (옵션)
                    System.Threading.Thread.Sleep(100);
                }

                // 스캔된 바코드를 scanBarcodeTextBox에 추가
                this.Invoke((MethodInvoker)delegate
                {
                    scanBarcodeTextBox.Text += scannedBarcode;
                });

                // Task 종료
                return;
            });
        }

        private Bitmap CaptureDesktopArea(Point location, Size size)
        {
            Bitmap captureBitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(captureBitmap))
            {
                g.CopyFromScreen(location.X, location.Y, 0, 0, size);
            }
            return captureBitmap;
        }
    }
}
