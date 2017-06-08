using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        bool RecordingCheck = false;
        VideoCapture camCapture;
        Image<Bgr, Byte> NormalImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadButton_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(RecordingCheck == true)
            {
                timer1.Stop();
                RecordButton.Text = "Record";
                camCapture.Dispose();
                RecordingCheck = false;
            }
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Load(openFileDialog1.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (System.ArgumentException)
                {
                    MessageBox.Show("Cannot load this image", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            Bitmap LoadedImage = (Bitmap)pictureBox1.Image;
            NormalImage = new Image<Bgr, Byte>(LoadedImage);
            Image<Gray, Byte> ConvertImage = NormalImage.Convert<Gray, Byte>();
            Image<Gray, Byte> ProcessedImage = ConvertImage.ThresholdBinary(new Gray(110), new Gray(255));
            pictureBox2.Image = ProcessedImage.ToBitmap();
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void RecordButton_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (RecordingCheck == false)
            {
                NormalImage = null;
                camCapture = new VideoCapture();
                timer1.Start();
                RecordButton.Text = "Stop";
                RecordingCheck = true;
            }
            else
            {
                timer1.Stop();
                RecordButton.Text = "Record";
                camCapture.Dispose();
                NormalImage = null;
                RecordingCheck = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            NormalImage = camCapture.QueryFrame().ToImage<Bgr, Byte>();
            pictureBox1.Image = NormalImage.ToBitmap();
            pictureBox1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            Image<Gray, Byte> ConvertImage = NormalImage.Convert<Gray, Byte>();
            Image<Gray, Byte> ProcessedImage = ConvertImage.ThresholdBinary(new Gray(80), new Gray(255));
            pictureBox2.Image = ProcessedImage.ToBitmap();
            pictureBox2.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
