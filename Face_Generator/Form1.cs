using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Face_Generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadSaclar_Click(object sender, EventArgs e)
        {
            //this.Controls.Add()

            this.groupBox1.Controls.Clear();

            using (var opnDlg = new OpenFileDialog()) //ANY dialog
            {
                opnDlg.RestoreDirectory = true;
                opnDlg.Multiselect = true;
                opnDlg.Title = "Please Select Source File(s) for Conversion";
                opnDlg.Filter = "Png Files (*.png)|*.png";
                //opnDlg.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx|CSV Files (*.csv)|*.csv"
                System.IO.Stream myStream;
                if (opnDlg.ShowDialog() == DialogResult.OK)
                {

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
                                        Name = "pictureBox",
                                        Size = new Size(100, 100),
                                        Location = new Point(10, 10),
                                        SizeMode = PictureBoxSizeMode.StretchImage,
                                        Image = Image.FromStream(myStream)

                                    };

                                    var picture2 = new PictureBox
                                    {
                                        Name = "pictureBox2",
                                        Size = new Size(100, 100),
                                        Location = new Point(10, 120),
                                        SizeMode = PictureBoxSizeMode.StretchImage,
                                        Image = Image.FromStream(myStream)

                                    };
                                    this.groupBox1.Controls.Add(picture);
                                    this.groupBox1.Controls.Add(picture2);
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

 
    }
}
