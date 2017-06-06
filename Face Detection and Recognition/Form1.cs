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

            //Load of previus trainned faces and labels for each image
            try
            {
                //Load of previus trainned faces and labels for each image
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
                //MessageBox.Show(e.ToString());
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
                    Form2 frm = new Form2();
                    frm.Show();
                }
            }
            
                // Converting the master image to a bitmap
                Bitmap masterImage = (Bitmap)pictureBox1.Image;

            try
            {
                // Normalizing it to grayscale
                normalizedImage = new Image<Bgr, Byte>(masterImage);
            }
            catch (System.NullReferenceException)
            {
                return;
            }

                MCvAvgComp[][] facesDetected = normalizedImage.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(Width / 8, Height / 8));
                //Action for each element detected
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    t = t + 1;
                    result = normalizedImage.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    //draw the face detected in the 0th (gray) channel with blue color
                    normalizedImage.Draw(f.rect, new Bgr(Color.Red), 2);

                    if (trainingImages.ToArray().Length != 0)
                    {
                        //TermCriteria for face recognition with numbers of trained images like maxIteration
                        MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);

                        //Eigen face recognizer
                        EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), 10000, ref termCrit);

                        name = recognizer.Recognize(result).Label;

                        //Draw the label for each face detected and recognized
                        label3.Text = names;

                    }

                    NamePersons[t - 1] = name;
                    NamePersons.Add("");
                }
                t = 0;

                //Names concatenation of persons recognized
                for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
                {
                    names = names + NamePersons[nnn] + ", ";
                }
                //Show the faces procesed and recognized
                pictureBox1.Image = normalizedImage.ToBitmap();
                buttonCheck = true;

                names = "";
                label3.Text = name;
                //Clear the list(vector) of names
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

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveImage_Click(object sender, EventArgs e)
        {
                
                //Trained face counter
                ContTrain = ContTrain + 1;

                Bitmap masterImage2 = (Bitmap)pictureBox1.Image;
                // Normalizing it to grayscale
                Image<Gray, Byte> gray = new Image<Gray, Byte>(masterImage2);

            if (buttonCheck == false)
            {
                //Get a gray frame from capture device
                gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            }
                //Face Detector
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

            //Action for each element detected
            foreach (MCvAvgComp f in facesDetected[0])
            {


                //resize face detected image for force to compare the same size with the 
                //test image with cubic interpolation type method
                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                labels.Add(textBox2.Text);


                //Show face added in gray scale
                pictureBox2.Image = TrainedFace.ToBitmap();

                //Write the number of triained faces in a file text for further load
                File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.ToArray().Length.ToString() + "%");

                //Write the labels of triained faces in a file text for further load
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
                //Initialize the capture device
                grabber = new Capture();
                grabber.QueryFrame();
                //Initialize the FrameGraber event
                timer1.Start();
                filming.Text = "หยุด";
                buttonCheck = true;
            }
            else
            {
                timer1.Stop();
                filming.Text = "ถ่ายภาพ";
                grabber.Dispose();
                //currentFrame = null;
                buttonCheck = false;
                
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            NamePersons.Add("");

            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().Resize(640, 360, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            //Convert it to Grayscale
            gray = currentFrame.Convert<Gray, Byte>();
            //Face Detector
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(Width / 8, Height / 8));

            //Action for each element detected
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //draw the face detected in the 0th (gray) channel with blue color
                currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);


                if (trainingImages.ToArray().Length != 0)
                {
                    //TermCriteria for face recognition with numbers of trained images like maxIteration
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);

                    //Eigen face recognizer
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), 3000, ref termCrit);

                    try
                    {
                        name = recognizer.Recognize(result).Label;
                    }
                    catch (System.NullReferenceException){}
                    

                    //Draw the label for each face detected and recognized
                    //currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.LightGreen));

                }

                NamePersons[t - 1] = name;
                NamePersons.Add("");


            }
            t = 0;

            //Names concatenation of persons recognized
            for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
            {
                names = names + NamePersons[nnn];
            }
            //Show the faces procesed and recognized
            pictureBox1.Image = currentFrame.ToBitmap();
            pictureBox1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

            names = "";
            label3.Text = name;
            //Clear the list(vector) of names
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
