using Graphs.Сode;
using System;
using System.Windows.Forms;

namespace Graphs
{
    public partial class Form2 : System.Windows.Forms.Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    HelpClass.Weight = int.Parse(textBox1.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введите корректные данные");
            }
            Close();
        }
    }
}
