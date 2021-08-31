using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public Dictionary<String, TextBox> textboxes = new Dictionary<string, TextBox>();
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


                    RemoveOldValues("hair");


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

                                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
                                    this.tableLayoutPanel1.Controls.Add(picture,0, row);
                                    this.tableLayoutPanel1.Controls.Add(textbox, 1, row);
                                    this.tableLayoutPanel1.RowCount += 1;
                                    row += 1;

                                    textboxes.Add(textbox.Name, textbox);

                                    


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

        private void RemoveOldValues(string value)
        {
            foreach (KeyValuePair<String, TextBox> entry in textboxes)
            {
                if (entry.Key.Contains(value))
                {
                    textboxes.Remove(entry.Key);
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


        private void GetHair()
        {
            ArrayList arlist = new ArrayList();
            foreach (KeyValuePair<String, TextBox> entry in textboxes)
            {
                if (entry.Key.Contains("hair"))
                {
                    var value = entry.Value.Text;
                    arlist.Add(value);
                }


            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {



           

        }
    }
}
