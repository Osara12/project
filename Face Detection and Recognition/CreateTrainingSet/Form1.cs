using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;

namespace CreateTrainingSet
{
    public partial class Form1 : Form
    {
        Image<Bgr, Byte> currentFrame, normalizedImage;
        Capture grabber;
        HaarCascade face = new HaarCascade("haarcascade_frontalface_default.xml");
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;
        bool buttonCheck = false;

        public Form1()
        {
            InitializeComponent();
            try
            {
                string Labelsinfo = File.ReadAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt");
                string[] Labels = Labelsinfo.Split('%');
                NumLabels = Convert.ToInt16(Labels[0]);
                ContTrain = NumLabels;
                string LoadFaces;
                for (int tf = 1; tf < NumLabels + 1; tf++)
                {
                    LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces/" + LoadFaces));
                    labels.Add(Labels[tf]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ฐานข้อมูลว่างเปล่า กรุณาเพิ่มข้อมูล", "โหลดฐานข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void loadImage_Click_1(object sender, EventArgs e)
        {   
            if(buttonCheck == true)
            {
                timer1.Stop();
                filming.Text = "ถ่ายภาพ";
                grabber.Dispose();
                buttonCheck = false;
            }
            NamePersons.Add("");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox1.Text = openFileDialog1.FileName;
                    pictureBox1.Load(openFileDialog1.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (System.ArgumentException)
                {
                    MessageBox.Show("ไมาสามารถใช้ภาพนี้ได้", "เกิดข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
                Bitmap masterImage = (Bitmap)pictureBox1.Image;
            try
            {
                normalizedImage = new Image<Bgr, Byte>(masterImage);
            }
            catch (System.NullReferenceException)
            {
                return;
            }
                MCvAvgComp[][] facesDetected = normalizedImage.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(Width / 8, Height / 8));
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    t = t + 1;
                    result = normalizedImage.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    normalizedImage.Draw(f.rect, new Bgr(Color.Red), 2);
                    if (trainingImages.ToArray().Length != 0)
                    {
                        MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);
                        EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), 10000, ref termCrit);
                        name = recognizer.Recognize(result).Label;
                        label3.Text = names;
                    }
                    NamePersons[t - 1] = name;
                    NamePersons.Add("");
                }
                t = 0;
                for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
                {
                    names = names + NamePersons[nnn] + ", ";
                }
                pictureBox1.Image = normalizedImage.ToBitmap();
                buttonCheck = true;
                names = "";
                label3.Text = name;
                NamePersons.Clear();
                pictureBox2.InitialImage = null;
                if (name != null)
                {
                try
                {
                    pictureBox2.Load("C:/faceRecognized/" + name + ".jpg");
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (System.IO.FileNotFoundException) { label3.Text = "ไม่พบรูปในฐานข้อมูล"; }
            }
        }

        private void Exit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveImage_Click(object sender, EventArgs e)
        {
                ContTrain = ContTrain + 1;
                Bitmap masterImage2 = (Bitmap)pictureBox1.Image;
                Image<Gray, Byte> gray = new Image<Gray, Byte>(masterImage2);
            if (buttonCheck == false)
            {
                gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            }
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
            foreach (MCvAvgComp f in facesDetected[0])
            {
                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                labels.Add(textBox2.Text);
                pictureBox2.Image = TrainedFace.ToBitmap();
                File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.ToArray().Length.ToString() + "%");
                for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                {
                    trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/TrainedFaces/face" + i + ".bmp");
                    File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", labels.ToArray()[i - 1] + "%");
                }
                MessageBox.Show("บันทึกข้อมูลคุณ " + textBox2.Text + " เรียบร้อย", "Training OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void filming_Click(object sender, EventArgs e)
        {
            if (buttonCheck == false)
            {
                grabber = new Capture();
                grabber.QueryFrame();
                timer1.Start();
                filming.Text = "หยุด";
                buttonCheck = true;
            }
            else
            {
                timer1.Stop();
                filming.Text = "ถ่ายภาพ";
                grabber.Dispose();
                buttonCheck = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NamePersons.Add("");
            currentFrame = grabber.QueryFrame().Resize(640, 360, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            gray = currentFrame.Convert<Gray, Byte>();
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(Width / 8, Height / 8));
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);
                if (trainingImages.ToArray().Length != 0)
                {
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), 3000, ref termCrit);
                    try
                    {
                        name = recognizer.Recognize(result).Label;
                    }
                    catch (System.NullReferenceException){}
                }
                NamePersons[t - 1] = name;
                NamePersons.Add("");
            }
            t = 0;
            for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
            {
                names = names + NamePersons[nnn];
            }
            pictureBox1.Image = currentFrame.ToBitmap();
            pictureBox1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            names = "";
            label3.Text = name;
            NamePersons.Clear();
            pictureBox2.InitialImage = null;
            if (name != null)
            {
                try
                {
                    pictureBox2.Load("C:/faceRecognized/" + name + ".jpg");
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (System.IO.FileNotFoundException) { label3.Text = "ไม่พบรูปในฐานข้อมูล"; }
            }
        }
    }
}
