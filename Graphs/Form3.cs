using Graphs.Сode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphs
{
    public partial class Form3 : System.Windows.Forms.Form
    {
        public int CountGraph { get; set; }
        public int[,] MatrixA;
        public Form3()
        {
            InitializeComponent();
            tableLayoutPanel1.RowCount = 0;
            tableLayoutPanel1.ColumnCount = 0;
        }

        private void Column_Box_TextChanged(object sender, EventArgs e)
        {
            if (Column_Box.Text != "")
            {
                tableLayoutPanel1.Controls.Clear();
                CountGraph = int.Parse(Column_Box.Text);
                tableLayoutPanel1.ColumnCount = CountGraph;
            }
        }
        private void FillTable()
        {

            List<List<int>> arr = new List<List<int>>();
            for (int i = 0; i < CountGraph; i++)
            {
                for (int j = 0; j < CountGraph; j++)
                {
                    TextBox textBox = new TextBox()
                    {
                        Width = 20,
                        Height = 20,
                        Text = "0",
                    };                   
                    tableLayoutPanel1.Controls.Add(textBox, i, j);
                    
                }
            }
            
            MatrixA = new int[CountGraph, CountGraph];
            ToAddValue();
        }
        private void ToAddValue()
            {
            List<Point> list = new List<Point>();
            foreach (var item in tableLayoutPanel1.Controls.OfType<TextBox>())
            {
                list.Add(item.Location);
            };          
            
            Point[,] location = new Point[CountGraph, CountGraph];
            for (int i = 0; i < CountGraph; i++)
            {
                for (int j = 0; j < CountGraph; j++)
                {
                    location[j, i] = list[i];
                }
            }


            for (int j = 0; j < CountGraph; j++)
            {

                Point point2 = new Point(0, location[0, j].Y+18);
                Label label1 = new Label()
                {
                    Location = point2,
                    Text = ((char)('A'+j)).ToString(),
                    Width = 10,
                    Height = 15
                };
                panel1.Controls.Add(label1);
                Point point = new Point(14 + j * 26, 0);
                Label label = new Label()
                {
                    Location = point,
                    Text = ((char)('A' + j)).ToString(),
                    Width = 10,
                    Height=15
                    
                };
                panel1.Controls.Add(label);
            }

        }
        private void Create_Box_Click(object sender, EventArgs e)
        {
            FillTable();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            button1.Visible = true;
        }
        private void ShowMatrix()
        {
            string res = "";
            for (int i = 0; i < CountGraph; i++)
            {
                for (int j = 0; j < CountGraph; j++)
                {
                    res += MatrixA[i, j].ToString();
                }
                res += "\n";
            }
            MessageBox.Show(res);
        }
        private void FillMatrix()
        {
            List<TextBox> list1 = new List<TextBox>();
            list1 = tableLayoutPanel1.Controls.OfType<TextBox>().ToList();

            int z = 0;
            HelpClass.Size = CountGraph;
            HelpClass.AdjMatrix = new int[CountGraph, CountGraph];
            for (int i = 0; i < CountGraph; i++)
            {
                for (int j = 0; j < CountGraph; j++)
                {
                    MatrixA[j, i] = int.Parse(list1[z].Text);
                    HelpClass.AdjMatrix[j, i] = int.Parse(list1[z].Text);
                    z++;
                }
            }


        }
        private void Clear_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillMatrix();
            ShowMatrix();
            Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
