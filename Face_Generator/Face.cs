using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Face_Generator
{
    class Face
    {
        public PictureBox Picture { get; set; }
        public TextBox textBox { get; set; }
        public int index { get; set; }

        public int GetCount()
        {
            return Int32.Parse(textBox.Text);
        }
        public void SetCount(int count)
        {
            textBox.Text = count.ToString();
        }
    }
}
