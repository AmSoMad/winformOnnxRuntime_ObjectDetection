namespace OnnxProject
{
    partial class ObjectDetection
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopPanel = new System.Windows.Forms.Panel();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.panelSizeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.capturePanel = new System.Windows.Forms.Panel();
            this.scanBarcodeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.captureStopBtn = new System.Windows.Forms.Button();
            this.barcodeScanBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.screenMinimizedBtn = new System.Windows.Forms.Button();
            this.normalScreenBtn = new System.Windows.Forms.Button();
            this.fullScreenBtn = new System.Windows.Forms.Button();
            this.captureBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TopPanel.Controls.Add(this.barcodeScanBtn);
            this.TopPanel.Controls.Add(this.label3);
            this.TopPanel.Controls.Add(this.label2);
            this.TopPanel.Controls.Add(this.scanBarcodeTextBox);
            this.TopPanel.Controls.Add(this.saveBtn);
            this.TopPanel.Controls.Add(this.sizeLabel);
            this.TopPanel.Controls.Add(this.panelSizeLabel);
            this.TopPanel.Controls.Add(this.screenMinimizedBtn);
            this.TopPanel.Controls.Add(this.normalScreenBtn);
            this.TopPanel.Controls.Add(this.fullScreenBtn);
            this.TopPanel.Controls.Add(this.captureStopBtn);
            this.TopPanel.Controls.Add(this.captureBtn);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Controls.Add(this.closeBtn);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(600, 85);
            this.TopPanel.TabIndex = 0;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            // 
            // sizeLabel
            // 
            this.sizeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sizeLabel.Font = new System.Drawing.Font("나눔스퀘어_ac Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.sizeLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sizeLabel.Location = new System.Drawing.Point(384, 61);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(38, 23);
            this.sizeLabel.TabIndex = 8;
            this.sizeLabel.Text = "size";
            this.sizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelSizeLabel
            // 
            this.panelSizeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.panelSizeLabel.Font = new System.Drawing.Font("나눔스퀘어_ac Bold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelSizeLabel.Location = new System.Drawing.Point(428, 59);
            this.panelSizeLabel.Name = "panelSizeLabel";
            this.panelSizeLabel.Size = new System.Drawing.Size(160, 23);
            this.panelSizeLabel.TabIndex = 7;
            this.panelSizeLabel.Text = "800 x 800";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(108)))), ((int)(((byte)(93)))));
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("굴림", 2F);
            this.label1.Location = new System.Drawing.Point(4, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(593, 1);
            this.label1.TabIndex = 1;
            // 
            // capturePanel
            // 
            this.capturePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.capturePanel.BackColor = System.Drawing.Color.White;
            this.capturePanel.Location = new System.Drawing.Point(5, 88);
            this.capturePanel.Margin = new System.Windows.Forms.Padding(1);
            this.capturePanel.Name = "capturePanel";
            this.capturePanel.Size = new System.Drawing.Size(589, 408);
            this.capturePanel.TabIndex = 1;
            // 
            // scanBarcodeTextBox
            // 
            this.scanBarcodeTextBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.scanBarcodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scanBarcodeTextBox.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.scanBarcodeTextBox.Location = new System.Drawing.Point(103, 8);
            this.scanBarcodeTextBox.Name = "scanBarcodeTextBox";
            this.scanBarcodeTextBox.Size = new System.Drawing.Size(262, 29);
            this.scanBarcodeTextBox.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("나눔스퀘어_ac Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(12, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 29);
            this.label2.TabIndex = 11;
            this.label2.Text = "시리얼번호";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.IndianRed;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("나눔스퀘어_ac Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(103, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(262, 2);
            this.label3.TabIndex = 12;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // captureStopBtn
            // 
            this.captureStopBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.captureStopBtn.BackgroundImage = global::OnnxProject.Properties.Resources.Unavailable;
            this.captureStopBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.captureStopBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.captureStopBtn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.captureStopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.captureStopBtn.Location = new System.Drawing.Point(54, 46);
            this.captureStopBtn.Name = "captureStopBtn";
            this.captureStopBtn.Size = new System.Drawing.Size(43, 38);
            this.captureStopBtn.TabIndex = 3;
            this.captureStopBtn.TabStop = false;
            this.captureStopBtn.UseVisualStyleBackColor = false;
            this.captureStopBtn.Visible = false;
            this.captureStopBtn.Click += new System.EventHandler(this.captureStopBtn_Click);
            // 
            // barcodeScanBtn
            // 
            this.barcodeScanBtn.BackgroundImage = global::OnnxProject.Properties.Resources.Barcode_Reader;
            this.barcodeScanBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.barcodeScanBtn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.barcodeScanBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.barcodeScanBtn.Location = new System.Drawing.Point(106, 47);
            this.barcodeScanBtn.Name = "barcodeScanBtn";
            this.barcodeScanBtn.Size = new System.Drawing.Size(43, 36);
            this.barcodeScanBtn.TabIndex = 13;
            this.barcodeScanBtn.UseVisualStyleBackColor = true;
            this.barcodeScanBtn.Click += new System.EventHandler(this.barcodeScanBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.BackgroundImage = global::OnnxProject.Properties.Resources.Save;
            this.saveBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saveBtn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBtn.Location = new System.Drawing.Point(155, 47);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(43, 36);
            this.saveBtn.TabIndex = 9;
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // screenMinimizedBtn
            // 
            this.screenMinimizedBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.screenMinimizedBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.screenMinimizedBtn.BackgroundImage = global::OnnxProject.Properties.Resources.Subtract;
            this.screenMinimizedBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.screenMinimizedBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.screenMinimizedBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.screenMinimizedBtn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.screenMinimizedBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.screenMinimizedBtn.Location = new System.Drawing.Point(456, 3);
            this.screenMinimizedBtn.Name = "screenMinimizedBtn";
            this.screenMinimizedBtn.Size = new System.Drawing.Size(43, 38);
            this.screenMinimizedBtn.TabIndex = 6;
            this.screenMinimizedBtn.TabStop = false;
            this.screenMinimizedBtn.UseVisualStyleBackColor = false;
            this.screenMinimizedBtn.Click += new System.EventHandler(this.screenMinimizedBtn_Click);
            // 
            // normalScreenBtn
            // 
            this.normalScreenBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.normalScreenBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.normalScreenBtn.BackgroundImage = global::OnnxProject.Properties.Resources.Normal_Screen;
            this.normalScreenBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.normalScreenBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.normalScreenBtn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.normalScreenBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.normalScreenBtn.Location = new System.Drawing.Point(505, 3);
            this.normalScreenBtn.Name = "normalScreenBtn";
            this.normalScreenBtn.Size = new System.Drawing.Size(43, 38);
            this.normalScreenBtn.TabIndex = 5;
            this.normalScreenBtn.TabStop = false;
            this.normalScreenBtn.UseVisualStyleBackColor = false;
            this.normalScreenBtn.Visible = false;
            this.normalScreenBtn.Click += new System.EventHandler(this.normalScreenBtn_Click);
            // 
            // fullScreenBtn
            // 
            this.fullScreenBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fullScreenBtn.BackgroundImage = global::OnnxProject.Properties.Resources.Fit_to_Width;
            this.fullScreenBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.fullScreenBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.fullScreenBtn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.fullScreenBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fullScreenBtn.Location = new System.Drawing.Point(505, 3);
            this.fullScreenBtn.Name = "fullScreenBtn";
            this.fullScreenBtn.Size = new System.Drawing.Size(43, 38);
            this.fullScreenBtn.TabIndex = 4;
            this.fullScreenBtn.TabStop = false;
            this.fullScreenBtn.UseVisualStyleBackColor = true;
            this.fullScreenBtn.Click += new System.EventHandler(this.fullScreenBtn_Click);
            // 
            // captureBtn
            // 
            this.captureBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.captureBtn.BackgroundImage = global::OnnxProject.Properties.Resources.Camera;
            this.captureBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.captureBtn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.captureBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.captureBtn.Location = new System.Drawing.Point(5, 46);
            this.captureBtn.Name = "captureBtn";
            this.captureBtn.Size = new System.Drawing.Size(43, 38);
            this.captureBtn.TabIndex = 2;
            this.captureBtn.TabStop = false;
            this.captureBtn.UseVisualStyleBackColor = false;
            this.captureBtn.Click += new System.EventHandler(this.captureBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.closeBtn.BackgroundImage = global::OnnxProject.Properties.Resources.Close;
            this.closeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.closeBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.closeBtn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Location = new System.Drawing.Point(554, 3);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(43, 38);
            this.closeBtn.TabIndex = 0;
            this.closeBtn.TabStop = false;
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // ObjectDetection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.CancelButton = this.captureStopBtn;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(this.capturePanel);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ObjectDetection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.LimeGreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ObjectDetection_KeyDown);
            this.Resize += new System.EventHandler(this.ObjectDetection_Resize);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button captureStopBtn;
        private System.Windows.Forms.Button captureBtn;
        private System.Windows.Forms.Button fullScreenBtn;
        private System.Windows.Forms.Button normalScreenBtn;
        private System.Windows.Forms.Panel capturePanel;
        private System.Windows.Forms.Button screenMinimizedBtn;
        private System.Windows.Forms.Label panelSizeLabel;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox scanBarcodeTextBox;
        private System.Windows.Forms.Button barcodeScanBtn;
    }
}
