using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_App
{
    public partial class ucQuestion : UserControl
    {
        public ucQuestion()
        {
            InitializeComponent();
        }
        public string NoiDungCauHoi
        {
            get => lblQuestionContent.Text;
            set => lblQuestionContent.Text = value;
        }

        public int SoSao
        {
            get
            {
                if (radioButton1.Checked) return 1;
                if (radioButton2.Checked) return 2;
                if (radioButton3.Checked) return 3;
                if (radioButton4.Checked) return 4;
                if (radioButton5.Checked) return 5;

                return 0;
            }

            set
            {
                switch (value)
                {
                    case 1: radioButton1.Checked = true; break;
                    case 2: radioButton2.Checked = true; break;
                    case 3: radioButton3.Checked = true; break;
                    case 4: radioButton4.Checked = true; break;
                    case 5: radioButton5.Checked = true; break;
                }
            }
        }
    }
}