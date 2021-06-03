using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BaseballGame
{
    public partial class Form2 : Form
    {
        Form1 fm1;
        public Form2(Form1 _form)
        {
            InitializeComponent();
            fm1 = _form;

            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int max = (int)numericUpDown1.Value;
            fm1.max = max;
            fm1.textBox3.Text = "규칙 : " + max + "번 안으로 정답 맞추면 성공!!! 아니면 실패!!!";
            Close();
        }
    }
}
