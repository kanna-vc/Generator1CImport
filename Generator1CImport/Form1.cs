using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator1CImport
{
    public partial class Form1 : Form
    {
        private LayoutConstructor constructor = new LayoutConstructor();
        private string[] Requisites = new string[6];
        private string[] TypeReq = new string[6];
        private string[] Line = new string[6];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OnlyNumber(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateData();
            string Result;
            Result = constructor.Header();
            Result += constructor.Head(StartLane.Text, DirectoryText.Text);
            for (int i = 0; i < Convert.ToInt32(NumberText.Text); i++)
            {
                Result += constructor.Body(Requisites[i], TypeReq[i], Line[i]);
            }
            Result += constructor.Footer();
            if (DataConvert.Checked)
            {
                Result += constructor.DataConvert();
            }

            ResultBox.Text = Result;
        }

        private void UpdateData()
        {
            Requisites[0] = Requisites_0.Text;
            Requisites[1] = Requisites_1.Text;
            Requisites[2] = Requisites_2.Text;
            Requisites[3] = Requisites_3.Text;
            Requisites[4] = Requisites_4.Text;
            Requisites[5] = Requisites_5.Text;

            TypeReq[0] = TypeReq_0.Text;
            TypeReq[1] = TypeReq_1.Text;
            TypeReq[2] = TypeReq_2.Text;
            TypeReq[3] = TypeReq_3.Text;
            TypeReq[4] = TypeReq_4.Text;
            TypeReq[5] = TypeReq_5.Text;

            Line[0] = Line_0.Text;
            Line[1] = Line_1.Text;
            Line[2] = Line_2.Text;
            Line[3] = Line_3.Text;
            Line[4] = Line_4.Text;
            Line[5] = Line_5.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ResultBox.Text);
        }
    }
}
