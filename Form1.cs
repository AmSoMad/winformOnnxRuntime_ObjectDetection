using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnnxProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.panel1.Controls.Add(new ObjectDetection());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = Image.FromFile(@"C:\Users\user\Desktop\test.jpg");
            
        }
    }
}
