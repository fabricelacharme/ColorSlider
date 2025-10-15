using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ColorSliderTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void colorSlider7_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = colorSlider7.Value.ToString();
        }

        private void colorSlider5_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = colorSlider5.Value.ToString();
        }

        private void colorSlider8_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = colorSlider8.Value.ToString();
        }
    }
}
