using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Face_Generator
{
    public partial class Form1 : Form
    {
        public ArrayList hairList = new ArrayList();
        public ArrayList faceList = new ArrayList();

        //public Dictionary<String, TextBox> textboxes = new Dictionary<string, TextBox>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadSaclar_Click(object sender, EventArgs e)
        {
            
            using (var opnDlg = new OpenFileDialog()) //ANY dialog
            {

                opnDlg.RestoreDirectory = true;
                opnDlg.Multiselect = true;
                opnDlg.Title = "Saçları Seçiniz";
                opnDlg.Filter = "Png Files (*.png)|*.png";
                System.IO.Stream myStream;
                if (opnDlg.ShowDialog() == DialogResult.OK)
                {

                    this.tableLayoutPanel1.Controls.Clear();
                    this.tableLayoutPanel1.RowCount = 0;


                   // RemoveOldValues("hair");
                    hairList.Clear();


                    int row = 0;
                    foreach (String file in opnDlg.FileNames)
                    {

                        
                        try
                        {
                            if ((myStream = opnDlg.OpenFile()) != null)
                            {
                                using (myStream)
                                {


                                    var picture = new PictureBox
                                    {
                                        Name = "hair_"+row,
                                        Size = new Size(100, 100), 
                                        SizeMode = PictureBoxSizeMode.StretchImage,
                                        Image = Image.FromFile(file)

                                    };

                                    var textbox = new TextBox()
                                    {
                                        Name="hair_count_"+row

                                    };


                                    Hair hair = new Hair()
                                    {
                                        Picture = picture,
                                        textBox = textbox,
                                        index = row
                                    };

                                    hairList.Add(hair);

                                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
                                    this.tableLayoutPanel1.Controls.Add(picture,0, row);
                                    this.tableLayoutPanel1.Controls.Add(textbox, 1, row);
                                    this.tableLayoutPanel1.RowCount += 1;
                                    row += 1;

                                  //  textboxes.Add(textbox.Name, textbox);

                                    


                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                        }
                    }
                    
                }
            }
        }

 
        private void btnUploadFaces_Click(object sender, EventArgs e)
        {
            using (var opnDlg = new OpenFileDialog()) //ANY dialog
            {

                opnDlg.RestoreDirectory = true;
                opnDlg.Multiselect = true;
                opnDlg.Title = "Yüzleri Seçiniz";
                opnDlg.Filter = "Png Files (*.png)|*.png";
                System.IO.Stream myStream;
                if (opnDlg.ShowDialog() == DialogResult.OK)
                {

                    this.layoutFace.Controls.Clear();
                    this.layoutFace.RowCount = 0;

                    faceList.Clear();
                    int row = 0;
                    foreach (String file in opnDlg.FileNames)
                    {


                        try
                        {
                            if ((myStream = opnDlg.OpenFile()) != null)
                            {
                                using (myStream)
                                {


                                    var picture = new PictureBox
                                    {
                                        Name = "face_"+row,
                                        Size = new Size(100, 100),
                                        SizeMode = PictureBoxSizeMode.StretchImage,
                                        Image = Image.FromFile(file)

                                    };

                                    var textbox = new TextBox()
                                    {
                                        Name = "face_count_" + row,
                                        
                                    };
                                    Face hair = new Face()
                                    {
                                        Picture = picture,
                                        textBox = textbox,
                                        index = row
                                    };

                                    faceList.Add(hair);

                                    this.layoutFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
                                    this.layoutFace.Controls.Add(picture, 0, row);
                                    this.layoutFace.Controls.Add(textbox, 1, row);
                                    this.layoutFace.RowCount += 1;
                                    row += 1;


                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                        }
                    } 

                }
            }

        }

        private static int GetRandomHair(ArrayList sac_list)
        {
            bool available = false;
            foreach (Hair hair in sac_list)
            {
                int sac = Int32.Parse(hair.textBox.Text);
                if (sac > 0)
                {
                    available = true;
                }
            }

            if (available)
            {
                Random r = new Random();
                int rInt = r.Next(0, sac_list.Count);

                Hair hair = (Hair) sac_list[rInt];
                int value = hair.GetCount();
                if (value > 0)
                {
                    hair.SetCount(value - 1);
                    return rInt;
                }
                else
                {
                    return GetRandomHair(sac_list);
                }


            }
            else
            {
                return -1;
            }



        }

        private void btnStart_Click(object sender, EventArgs e)
        {


            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {



                    /*
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                    */


                    var map = new Dictionary<int, int[]>();

                    int index = 0;
                    foreach (Face face in faceList)
                    {

                        map.Add(index, new int[hairList.Count]);

                        int loopVariable = Int32.Parse(face.textBox.Text);
                        while (loopVariable > 0)
                        {
                            int hair = GetRandomHair(hairList);
                            if (hair != -1)
                            {
                                map[index][hair]++;
                            }
                            loopVariable--;
                        }

                        index += 1;
                    }
                    
                    int generatedImageCount = 0;
                    int fileNameIndex = 0;
                    foreach (KeyValuePair<int, int[]> entry in map)
                    {

                        Face face = (Face)faceList[entry.Key];

                        Console.WriteLine("Face " + entry.Key);
                        int indexx = 0;
                        int total = 0;
                        foreach (int value in entry.Value)
                        {
                            if (indexx == 0)
                                Console.Write("Yesil    : ");
                            if (indexx == 1)
                                Console.Write("Turuncu  : ");
                            if (indexx == 2)
                                Console.Write("Kırmızı  : ");
                            if (indexx == 3)
                                Console.Write("Sarı     : ");
                            if (indexx == 4)
                                Console.Write("Siyah    : ");
                            Console.WriteLine(value + " ");

                            Hair hair = (Hair)hairList[indexx];

                            // ----- Merge Image ---

                            PictureBox faceImage = face.Picture;
                            PictureBox hairImage = hair.Picture;
                            // value ise ilgili yuz ve sactan kactane uretilecegini belirtir.


                            for(int i = 0; i < value; i++)
                            {
                                var target = new Bitmap(faceImage.Image.Width, faceImage.Image.Height, PixelFormat.Format32bppArgb);
                           
                                Graphics g = Graphics.FromImage(target);
                                g.CompositingMode = CompositingMode.SourceOver;

                                g.DrawImage(faceImage.Image, 0, 0);
                                g.DrawImage(hairImage.Image, 0, 0); 

                                target.Save(fbd.SelectedPath+"/output"+ fileNameIndex + ".png", ImageFormat.Png);
                                fileNameIndex++;
                            }


                            total += value;
                            generatedImageCount += value;
                            indexx++;
                            
                        }
                        
                        Console.WriteLine("Toplam sac miktarı: " + total);
                        Console.WriteLine("--------------");


                    }
                    System.Windows.Forms.MessageBox.Show("Generated File Count: " + generatedImageCount, "Message");
                }
            }

            


        }
    }
}
