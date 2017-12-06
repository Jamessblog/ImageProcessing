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
    public partial class AlgorithmChoice : Form
    {
        public static bool IsUseBackLight=true;
        public static bool IsUseSideLight=false;
        public static bool IsUseAlgorithmChoice=false;

        public AlgorithmChoice()
        {
            InitializeComponent();
        }

        //private void btn_Threshold_Click(object sender, EventArgs e)
        //{
        //    IsUseThreshold = true;
        //    IsUseAlgorithmChoice = true;
        //    this.Close();
        //}

        //private void btn_RegionGrowing_Click(object sender, EventArgs e)
        //{
        //    IsUseThreshold = false;
        //    IsUseAlgorithmChoice = true;
        //    this.Close();
        //}

        private void checkBox_backlight_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_sidelight.Checked == true && checkBox_backlight.Checked == true)
            {
                checkBox_sidelight.Checked = false;
            }
            if (checkBox_backlight.Checked == true)
            {
                IsUseBackLight = true;
            }
            if (checkBox_backlight.Checked == false)
            {
                IsUseBackLight = false;
            }
        }
        private void checkBox_sidelight_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_backlight.Checked == true && checkBox_sidelight.Checked == true)
            {
                checkBox_backlight.Checked = false;
            }
            if (checkBox_sidelight.Checked == true)
            {
                IsUseSideLight = true;
            }
            if (checkBox_sidelight.Checked == false)
            {
                IsUseSideLight = false;
            }
        }
        private void btn_modelset_Click(object sender, EventArgs e)
        {
            ModelSetting frmMS = new ModelSetting();
            frmMS.ShowDialog();
        }

        private void btn_StartAnalysis_Click(object sender, EventArgs e)
        {
            IsUseAlgorithmChoice = true;
            this.Close();
        }

    }
}
