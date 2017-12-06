using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoPickingSys
{
    public partial class ModelSetting : Form
    {
        public static bool IsUseSmooth = true; public static bool _IsUseSmooth = true;
        public static bool IsUseSharpen = false; public static bool _IsUseSharpen = false;
        public static bool IsUseExpend = false; public static bool _IsUseExpend = false;
        public static bool IsUseShrk = false; public static bool _IsUseShrk = false;
        public static bool IsOpenOperation = false; public static bool _IsOpenOperation = false;
        public static bool IsCloseOperation = true; public static bool _IsCloseOperation = true;
        public static bool IsUseSoble = true; public static bool _IsUseSoble = true;
        public static bool IsUseCanny = false; public static bool _IsUseCanny = false;
        public static bool IsUseThreshold = false; public static bool _IsUseThreshold = false;
        

        public ModelSetting()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
                IsUseSmooth = true;
            if (checkBox1.Checked == false)
                IsUseSmooth = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
                IsUseSharpen = true;
            if (checkBox2.Checked == false)
                IsUseSharpen = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                IsUseShrk = true;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
            }
            if (checkBox3.Checked == false)
                IsUseShrk = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                IsUseExpend = true;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
            }
            if (checkBox4.Checked == false)
                IsUseExpend = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                IsOpenOperation = true;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox6.Checked = false;
            }
            if (checkBox5.Checked == false)
                IsOpenOperation = false;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                IsCloseOperation = true;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            if (checkBox6.Checked == false)
                IsCloseOperation = false;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                IsUseSoble = true;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
            }
            if (checkBox7.Checked == false)
                IsUseSoble = false;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                IsUseCanny = true;
                checkBox7.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
            }
            if (checkBox8.Checked == false)
                IsUseCanny = false;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked == true)
            {
                IsUseThreshold = true;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
            }
            if (checkBox10.Checked == false)
                IsUseThreshold = false;
        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox28.Checked == true)
                _IsUseSmooth = true;
            if (checkBox28.Checked == false)
                _IsUseSmooth = false;
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox27.Checked == true)
                _IsUseSharpen = true;
            if (checkBox27.Checked == false)
                _IsUseSharpen = false;
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox26.Checked == true)
                _IsUseShrk = true;
            if (checkBox26.Checked == false)
                _IsUseShrk = false;
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox25.Checked == true)
                _IsUseExpend = true;
            if (checkBox25.Checked == false)
                _IsUseExpend = false;
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox24.Checked == true)
            {
                _IsOpenOperation = true;
                checkBox26.Checked = false;
                checkBox25.Checked = false;
            }
            if (checkBox24.Checked == false)
                _IsOpenOperation = false;
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox23.Checked == true)
            {
                _IsCloseOperation = true;
                checkBox26.Checked = false;
                checkBox25.Checked = false;
            }
            if (checkBox23.Checked == false)
                _IsCloseOperation = false;
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked == true)
            {
                _IsUseSoble = true;
                checkBox21.Checked = false;
                checkBox20.Checked = false;
                checkBox19.Checked = false;
            }
            if (checkBox22.Checked == false)
                _IsUseSoble = false;
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked == true)
            {
                _IsUseCanny = true;
                checkBox22.Checked = false;
                checkBox20.Checked = false;
                checkBox19.Checked = false;
            }
            if (checkBox21.Checked == false)
                _IsUseCanny = false;
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked == true)
            {
                _IsUseThreshold = true;
                checkBox22.Checked = false;
                checkBox21.Checked = false;
                checkBox20.Checked = false;
            }
            if (checkBox19.Checked == false)
                _IsUseThreshold = false;
        }


    }
}
