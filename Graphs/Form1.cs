using Graphs.Сode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Graphs
{
    public partial class Form1 : Form
    {
        DrawG G;
        List<Vertex> V;
        List<Edge> E;
        int[,] AMatrix; //матрица смежности
        int[,] IMatrix; //матрица инцидентности

        int selected1; //выбранные вершины, для соединения линиями
        int selected2;
        int startV;
        int finishV;
        List<string> catalogCycles;
        public Form1()
        {
            InitializeComponent();
            V = new List<Vertex>();
            G = new DrawG(picture.Width, picture.Height);
            E = new List<Edge>();
            picture.Image = G.GetBitmap();
            label1.Text = "Таблица смежности\n";
            label2.Text = "Таблица инцендентности\n";
        }

        private void Path_Button_Click(object sender, EventArgs e)
        {
            Path_Button.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, E);
            picture.Image = G.GetBitmap();
            selected1 = -1;
            selected2 = -1;
        }
        private void selectButton_Click(object sender, EventArgs e)
        {
            Path_Button.Enabled = true;
            selectButton.Enabled = false;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, E);
            picture.Image = G.GetBitmap();
            selected1 = -1;
        }

        private void drawVertexButton_Click(object sender, EventArgs e)
        {
            Path_Button.Enabled = true;
            drawVertexButton.Enabled = false;
            selectButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, E);
            picture.Image = G.GetBitmap();
        }

        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            Path_Button.Enabled = true;
            drawEdgeButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, E);
            picture.Image = G.GetBitmap();
            selected1 = -1;
            selected2 = -1;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Path_Button.Enabled = true;
            deleteButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, E);
            picture.Image = G.GetBitmap();
        }

        private void deleteALLButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            Path_Button.Enabled = true;
            const string message = "Вы действительно хотите полностью удалить граф?";
            const string caption = "Удаление";
            var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MBSave == DialogResult.Yes)
            {
                V.Clear();
                E.Clear();
                G.clearSheet();
                HelpClass.Size = 0;
                HelpClass.AdjMatrix = new int[HelpClass.Size, HelpClass.Size];
                picture.Image = G.GetBitmap();
                listBoxMatrix.Items.Clear();
                label1.Text = "";
                label2.Text = "";
            }
        }   

        private void picture_MouseClick(object sender, MouseEventArgs e)
        {
            createAdjAndOut();
            if(Path_Button.Enabled==false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < V.Count; i++)
                    {
                        if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                        {
                            if (selected1 == -1)
                            {
                                G.drawSelectedVertex(V[i].x, V[i].y);
                                selected1 = i;
                                startV = i;
                                picture.Image = G.GetBitmap();
                                break;
                            }

                            if (selected2 == -1)
                            {

                                G.drawSelectedVertex(V[i].x, V[i].y);
                                selected1 = -1;
                                selected2 = -1;
                                finishV = i;                                
                                picture.Image = G.GetBitmap();
                                break;
                            }
                            
                        }
                    }                   
                    //foreach (var vertex in V)
                    //{
                    //    if (vertex.)
                    //    {

                    //    }
                    //}
                }
                if (e.Button == MouseButtons.Right)
                {
                    if ((selected1 != -1) &&
                        (Math.Pow((V[selected1].x - e.X), 2) + Math.Pow((V[selected1].y - e.Y), 2) <= G.R * G.R))
                    {
                        G.drawVertex(V[selected1].x, V[selected1].y, (selected1 + 1).ToString());
                        selected1 = -1;
                        picture.Image = G.GetBitmap();
                    }
                }
            }
            if (selectButton.Enabled == false)
            {
                for (int i = 1; i <= V.Count; i++)
                {
                    if (Math.Pow((V[i - 1].x - e.X), 2) + Math.Pow((V[i - 1].y - e.Y), 2) <= G.R * G.R)
                    {
                        if (selected1 != -1)
                        {
                            selected1 = -1;
                            G.clearSheet();
                            G.drawALLGraph(V, E);
                            picture.Image = G.GetBitmap();
                        }
                        if (selected1 == -1)
                        {
                            G.drawSelectedVertex(V[i - 1].x, V[i - 1].y);
                            selected1 = i - 1;
                            picture.Image = G.GetBitmap();
                            createAdjAndOut();
                            int degree = 0;
                            for (int j = 0; j < V.Count; j++)
                                degree += AMatrix[selected1, j];
                            string deg = $"deg({((char)('A' + i - 1)).ToString()})= {degree}";
                            G.drawPow(e.X, e.Y, deg);
                            picture.Image = G.GetBitmap();
                            break;
                        }
                    }
                    //Point point1 = new Point(e.X, e.Y);
                    //Point point2 = new Point((V[i].x + V[i + 1].x) / 2, (V[i].y + V[i + 1].y) / 2);
                    //InterSect interSect = InterSect.NoPoint;
                    //InterSect res = new Circle(point1, 10) * new Circle(point2, 10);
                    if (E.Count == i)
                    {
                        for (int j = 0; j < E.Count; j++)
                        {
                            if (AMatrix[E[j].v1, E[j].v2] == 1)
                            {
                                if (new Circle(new GPoint(e.X, e.Y), 10) *
                            new Circle(new GPoint((V[E[j].v1].x + V[E[j].v2].x) / 2, (V[E[j].v1].y + V[E[j].v2].y) / 2), 10)
                            != InterSect.NoPoint)
                                {
                                    Form2 newForm = new Form2();
                                    newForm.ShowDialog();
                                    E[j].weight = HelpClass.Weight;
                                    G.EditEdge(V[E[j].v1], V[E[j].v2], E[j], j);
                                    G.clearSheet();
                                    G.drawALLGraph(V, E);
                                    picture.Image = G.GetBitmap();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            //нажата кнопка "выбрать вершину", ищем степень вершины
            if (selectButton.Enabled == false)
            {
                for (int i = 1; i <= V.Count; i++)
                {
                    if (Math.Pow((V[i - 1].x - e.X), 2) + Math.Pow((V[i - 1].y - e.Y), 2) <= G.R * G.R)
                    {
                        if (selected1 != -1)
                        {
                            selected1 = -1;
                            G.clearSheet();
                            G.drawALLGraph(V, E);
                            picture.Image = G.GetBitmap();
                        }
                        if (selected1 == -1)
                        {
                            G.drawSelectedVertex(V[i - 1].x, V[i - 1].y);
                            selected1 = i - 1;
                            picture.Image = G.GetBitmap();
                            createAdjAndOut();
                            int degree = 0;
                            for (int j = 0; j < V.Count; j++)
                                degree += AMatrix[selected1, j];
                            string deg = $"deg({((char)('A'+i-1)).ToString()})= {degree}";
                            G.drawPow(e.X, e.Y, deg);
                            picture.Image = G.GetBitmap();
                            break;
                        }
                       
                    }
                    //Point point1 = new Point(e.X, e.Y);
                    //Point point2 = new Point((V[i].x + V[i + 1].x) / 2, (V[i].y + V[i + 1].y) / 2);
                    //InterSect interSect = InterSect.NoPoint;
                    //InterSect res = new Circle(point1, 10) * new Circle(point2, 10);
                    if (E.Count == i)
                    {
                        for (int j = 0; j < E.Count; j++)
                        {
                            if (AMatrix[E[j].v1, E[j].v2] == 1)
                            {
                                if (new Circle(new GPoint(e.X, e.Y), 10) *
                            new Circle(new GPoint((V[E[j].v1].x + V[E[j].v2].x) / 2, (V[E[j].v1].y + V[E[j].v2].y) / 2), 10)
                            != InterSect.NoPoint)
                                {
                                    Form2 newForm = new Form2();
                                    newForm.ShowDialog();
                                    E[j].weight = HelpClass.Weight;
                                    G.EditEdge(V[E[j].v1], V[E[j].v2], E[j], j);
                                    G.clearSheet();
                                    G.drawALLGraph(V, E);
                                    picture.Image = G.GetBitmap();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            //нажата кнопка "рисовать вершину"
            if (drawVertexButton.Enabled == false)
            {
                V.Add(new Vertex(e.X, e.Y));
                HelpClass.Size++;
                G.drawVertex(e.X, e.Y, V.Count.ToString());
                picture.Image = G.GetBitmap();
            }
            //нажата кнопка "рисовать ребро"
            if (drawEdgeButton.Enabled == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < V.Count; i++)
                    {
                        if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                        {
                            if (selected1 == -1)
                            {
                                G.drawSelectedVertex(V[i].x, V[i].y);
                                selected1 = i;
                                picture.Image = G.GetBitmap();
                                break;
                            }

                            if (selected2 == -1)
                            {

                                G.drawSelectedVertex(V[i].x, V[i].y);
                                selected2 = i;
                                Form2 newForm = new Form2();
                                newForm.ShowDialog();
                                E.Add(new Edge(selected1, selected2, HelpClass.Weight));
                                G.drawEdge(V[selected1], V[selected2], E[E.Count - 1], E.Count - 1);
                                selected1 = -1;
                                selected2 = -1;
                                picture.Image = G.GetBitmap();
                                break;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if ((selected1 != -1) &&
                        (Math.Pow((V[selected1].x - e.X), 2) + Math.Pow((V[selected1].y - e.Y), 2) <= G.R * G.R))
                    {
                        G.drawVertex(V[selected1].x, V[selected1].y, (selected1 + 1).ToString());
                        selected1 = -1;
                        picture.Image = G.GetBitmap();
                    }
                }
            }
            //нажата кнопка "удалить элемент"
            if (deleteButton.Enabled == false)
            {
                bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                //ищем, возможно была нажата вершина
                for (int i = 0; i < V.Count; i++)
                {
                    if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                    {
                        for (int j = 0; j < E.Count; j++)
                        {
                            if ((E[j].v1 == i) || (E[j].v2 == i))
                            {
                                E.RemoveAt(j);
                                j--;
                            }
                            else
                            {
                                if (E[j].v1 > i) E[j].v1--;
                                if (E[j].v2 > i) E[j].v2--;
                            }
                        }
                        V.RemoveAt(i);
                        flag = true;
                        break;
                    }
                }
                //ищем, возможно было нажато ребро
                if (!flag)
                {
                    for (int i = 0; i < E.Count; i++)
                    {
                        if (E[i].v1 == E[i].v2) //если это петля
                        {
                            if ((Math.Pow((V[E[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E[i].v1].y - G.R - e.Y), 2) <= ((G.R + 2) * (G.R + 2))) &&
                                (Math.Pow((V[E[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E[i].v1].y - G.R - e.Y), 2) >= ((G.R - 2) * (G.R - 2))))
                            {
                                E.RemoveAt(i);
                                flag = true;
                                break;
                            }
                        }
                        else //не петля
                        {
                            if (((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) <= (e.Y + 4) &&
                                ((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) >= (e.Y - 4))
                            {
                                if ((V[E[i].v1].x <= V[E[i].v2].x && V[E[i].v1].x <= e.X && e.X <= V[E[i].v2].x) ||
                                    (V[E[i].v1].x >= V[E[i].v2].x && V[E[i].v1].x >= e.X && e.X >= V[E[i].v2].x))
                                {
                                    E.RemoveAt(i);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                //если что-то было удалено, то обновляем граф на экране
                if (flag)
                {
                    G.clearSheet();
                    G.drawALLGraph(V, E);
                    picture.Image = G.GetBitmap();
                }
            }
            createAdjAndOut();
        }
        //Матрица смежности
        private void createAdjAndOut()
        {
            //if (label1.Text != "")
            //    label1.Text = "";
            AMatrix = new int[V.Count, V.Count];
            G.fillAdjacencyMatrix(V.Count, E, AMatrix);
            //listBoxMatrix.Items.Clear();
            string sOut = "    ";
            for (int i = 0; i < V.Count; i++)
                sOut += (i + 1) + " ";
            sOut += "\n";
            label1.Text = sOut;
            //listBoxMatrix.Items.Add(sOut);
            for (int i = 0; i < V.Count; i++)
            {
                sOut = (i + 1) + " | ";
                for (int j = 0; j < V.Count; j++)
                    sOut += AMatrix[i, j] + " ";
                sOut += "\n";

                label1.Text += sOut;
                //listBoxMatrix.Items.Add(sOut);
            }
        }
        //Матрица инциндентности
        private void createIncAndOut()
        {
            if (E.Count > 0)
            {
                //if (label2.Text != "")
                //    label2.Text = "";
                IMatrix = new int[V.Count, E.Count];
                G.fillIncidenceMatrix(V.Count, E, IMatrix);
                //listBoxMatrix.Items.Clear();
                string sOut = "    ";
                for (int i = 0; i < E.Count; i++)
                    sOut += (char)('A' + i) + " ";
                sOut += "\n";
                label2.Text = sOut;
                listBoxMatrix.Items.Add(sOut);
                for (int i = 0; i < V.Count; i++)
                {
                    sOut = (i + 1) + " | ";
                    for (int j = 0; j < E.Count; j++)
                        sOut += IMatrix[i, j] + " ";
                    sOut += "\n";
                    label2.Text += sOut;
                    listBoxMatrix.Items.Add(sOut);
                }
            }
            else
            {
                listBoxMatrix.Items.Clear();
                label2.Text = "";
            }
        }
        private void DFSchain(int u, int endV, List<Edge> E, int[] color, string s)
        {
            //вершину не следует перекрашивать, если u == endV (возможно в нее есть несколько путей)
            if (u != endV)
                color[u] = 2;
            else
            {
                listBoxMatrix.Items.Add(s);
                label2.Text += s+"\n";
                return;
            }
            for (int w = 0; w < E.Count; w++)
            {
                if (color[E[w].v2] == 1 && E[w].v1 == u)
                {
                    DFSchain(E[w].v2, endV, E, color, s + "-" + ((char)('A'+E[w].v2)).ToString());
                    color[E[w].v2] = 1;
                }
                else if (color[E[w].v1] == 1 && E[w].v2 == u)
                {
                    DFSchain(E[w].v1, endV, E, color, s + "-" + ((char)('A'+E[w].v1)).ToString());
                    color[E[w].v1] = 1;
                }
            }
        }
        private void chainsSearch()
        {
            int[] color = new int[V.Count];
            for (int i = 0; i < V.Count - 1; i++)
                for (int j = i + 1; j < V.Count; j++)
                {
                    for (int k = 0; k < V.Count; k++)
                        color[k] = 1;
                    DFSchain(i, j, E, color, ((char)('A' + i)).ToString());
                    //поскольку в C# нумерация элементов начинается с нуля, то
                    //для удобочитаемости результатов в строку передаем i + 1
                }
        }
        private void DFSchainOne(int u, int endV, List<Edge> E, int[] color, string s)
        {
            //вершину не следует перекрашивать, если u == endV (возможно в нее есть несколько путей)
            int count = 0;
            if (u != endV)
                color[u] = 2;
            else
            {
                s += " " + count;
                listBoxMatrix.Items.Add(s);
                label2.Text += s + "\n";
                return;
            }
            for (int w = 0; w < E.Count; w++)
            {
                if (color[E[w].v2] == 1 && E[w].v1 == u)
                {
                    DFSchain(E[w].v2, endV, E, color, s + "-" + ((char)('A' + E[w].v2)).ToString());
                    color[E[w].v2] = 1;
                    count++;
                }
                else if (color[E[w].v1] == 1 && E[w].v2 == u)
                {
                    DFSchain(E[w].v1, endV, E, color, s + "-" + ((char)('A' + E[w].v1)).ToString());
                    color[E[w].v1] = 1;
                    count++;
                }
            }
        }
        private void chainsSearchOne(int start,int end)
        {
            int[] color = new int[V.Count];
                for (int j = start + 1; j < V.Count; j++)
                {
                    for (int k = 0; k < V.Count; k++)
                        color[k] = 1;
                    DFSchainOne(start, j, E, color, ((char)('A' + start)).ToString());
                    //поскольку в C# нумерация элементов начинается с нуля, то
                    //для удобочитаемости результатов в строку передаем i + 1
                }
        }
        private void DFScycle(int u, int endV, List<Edge> E, int[] color, int unavailableEdge, List<int> cycle)
        {
            //если u == endV, то эту вершину перекрашивать не нужно, иначе мы в нее не вернемся, а вернуться необходимо
            
            if (u != endV)
                color[u] = 2;
            else
            {
                if (cycle.Count >= 2)
                {
                    cycle.Reverse();
                    string s = ((char)('A'+cycle[0]-1)).ToString();
                    for (int i = 1; i < cycle.Count; i++)
                        s += "-" + ((char)('A' + cycle[i])).ToString();
                    bool flag = false; //есть ли палиндром для этого цикла графа в листбоксе?
                    for (int i = 0; i < catalogCycles.Count; i++)
                        if (catalogCycles.Count.ToString() == s)
                        {
                            flag = true;
                            break;
                        }
                    if (!flag)
                    {
                        cycle.Reverse();
                        s = ((char)('A' + cycle[0]-1)).ToString();
                        for (int i = 1; i < cycle.Count; i++)
                            s += "-" + ((char)('A'+cycle[i]-1)).ToString();
                        catalogCycles.Add(s);
                    }
                    return;
                }
            }
            for (int w = 0; w < E.Count; w++)
            {
                if (w == unavailableEdge)
                    continue;
                if (color[E[w].v2] == 1 && E[w].v1 == u)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(E[w].v2 + 1);
                    DFScycle(E[w].v2, endV, E, color, w, cycleNEW);
                    color[E[w].v2] = 1;
                }
                else if (color[E[w].v1] == 1 && E[w].v2 == u)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(E[w].v1 + 1);
                    DFScycle(E[w].v1, endV, E, color, w, cycleNEW);
                    color[E[w].v1] = 1;
                }
            }
            
            
        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    listBoxMatrix.Items.Clear();
        //    chainsSearch();
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    listBoxMatrix.Items.Clear();
        //    catalogCycles = new List<string>();
        //    cyclesSearch();
        //    foreach (var item in catalogCycles)
        //    {
        //        listBoxMatrix.Items.Add(item);
        //    }
        //}
       
        private void cyclesSearch()
        {
            int[] color = new int[V.Count];
            for (int i = 0; i < V.Count; i++)
            {
                for (int k = 0; k < V.Count; k++)
                    color[k] = 1;
                List<int> cycle = new List<int>();
                //поскольку в C# нумерация элементов начинается с нуля, то для
                //удобочитаемости результатов поиска в список добавляем номер i + 1
                cycle.Add(i+1);
                DFScycle(i, i, E, color, -1, cycle);
            }
        }
        private string[] ToConvertStringArr(string obj)
        {
            return obj.Split('\n').Select(x => x.ToString()).ToArray();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int numer = comboBox1.SelectedIndex+1;
            switch (numer)
            {
                case 1:
                    {
                        listBoxMatrix.Items.Clear();
                        int n = V.Count;
                        int num = 0; 
                        int count = 0;
                        string res = "Sigma\n";
                        createAdjAndOut();
                        G.clearSheet();
                        G.drawALLGraph(V, E);
                        picture.Image = G.GetBitmap();
                        AMatrix = new int[V.Count, V.Count];
                        G.fillAdjacencyMatrix(V.Count, E, AMatrix);
                        

                        foreach (var item in V)
                        {
                            int x = item.x;
                            int y = item.y;
                            int degree = 0;
                            for (int j = 0; j < n; j++)
                            {
                                degree += AMatrix[count, j];
                                num += AMatrix[count, j];
                            }
                            string deg = $"deg({((char)('A' + count)).ToString()})= {degree}";
                            res += $"deg({((char)('A' + count)).ToString()})={degree}\n + \n";
                            G.drawPow(x, y, deg);
                            picture.Image = G.GetBitmap();
                            count++;
                        }

                        res += num.ToString() + "\n";
                        string R = E.Count.ToString();
                        res += "Всего ребер " + R + " ,значит сумма степеней всех вершина равна удвоенному" +
                            " количеству ребер\n 2*" + R + "= " + num.ToString() + "\n";
                        ;
                        if ((n * (n - 1)) / 2 == E.Count)
                        {
                            res += "Граф является полным";
                        }
                        listBoxMatrix.Items.Add(num);
                        listBoxMatrix.Items.AddRange(ToConvertStringArr(res));
                        createIncAndOut();

                    }
                    break;
                case 2:
                    {
                        int count1 = 0;
                        int count2 = 0;
                        AMatrix = new int[V.Count, V.Count];
                        G.fillAdjacencyMatrix(V.Count, E, AMatrix);
                        var sum = 0;
                        for (int i = 0; i < V.Count; i++)
                        {
                            int temp = 0;
                            sum=0;
                            for (int j = 0; j < V.Count; j++)
                            {
                                sum += AMatrix[i, j];
                                temp += AMatrix[i, j];
                            }
                            if (sum % 2 != 0)
                                count1++;
                            else
                                count2++;
                            string deg = $"deg({((char)('A' + i)).ToString()})= {temp}";
                            G.drawPow(V[i].x, V[i].y, deg);
                            picture.Image = G.GetBitmap();

                        }
                        string result1 = $"Количество четных степеней:{count2}\n";
                        result1 += $"Количество нечетных степеней:{count1}\nИх сумма {count1 + count2}";
                        listBoxMatrix.Items.AddRange(ToConvertStringArr(result1));
                        break;

                    }
                    
                case 3:
                    {
                        listBoxMatrix.Items.Clear();
                        int subgraphcount = 0;
                        for (int i = 1; i <= E.Count; i++)
                        {
                            subgraphcount += F(E.Count) / (F(i) * F(E.Count - i));
                        }
                        string result = "Количество подграфов в графе равно:\n " + subgraphcount;
                        listBoxMatrix.Items.AddRange(ToConvertStringArr(result));
                        break;
                    }
                case 4:
                    {
                        listBoxMatrix.Items.Clear();
                        int size = 2;
                        int i = 0;
                        chainsSearchOne(startV,finishV);
                        char startchar = (char)('A' + startV);
                        char endchar = (char)('A' + finishV);
                        List<string> paths = new List<string>();
                        foreach (var item in listBoxMatrix.Items)
                        {
                            string[] words = item.ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                            string word = "";
                            for (int j = 0; j < words.Length; j++)
                            {
                                word += words[j];
                            }
                            int index = word.IndexOf(endchar);
                            if (index==-1)
                            {
                                continue;
                            }
                            if (word[index] == endchar)
                            {
                                paths.Add(word);
                                continue;
                            }
                        }
                        string m = paths[0];
                        listBoxMatrix.Items.Clear();
                        for (int j = 0; j < paths.Count; j++)
                        {
                            if (paths[j].Length<m.Length)
                            {
                                m = paths[j];
                            }
                        }
                        listBoxMatrix.Items.Add(m);
                        break;
                    }
                case 5:
                    {
                        int n = V.Count;
                        int count = 0;
                        bool check = false;
                        listBoxMatrix.Items.Clear();
                        foreach (var item in V)
                        {
                            int x = item.x;
                            int y = item.y;
                            int degree = 0;
                            for (int j = 0; j < n; j++)
                            {
                                degree += AMatrix[count, j];

                            }
                            if (degree == 0)
                            {
                                check = true;
                                break;
                            }
                            //string deg = $"deg({((char)('A' + count)).ToString()})= {degree}";
                            //res += $"deg({((char)('A' + count)).ToString()})={degree}\n + \n";
                            //G.drawPow(x, y, deg);
                            //picture.Image = G.GetBitmap();
                            count++;
                        }                        
                        string result = check? "Не является связным": "Является связным";
                        listBoxMatrix.Items.Add(result);
                        break;
                    }
                case 6:
                    break;
                    
                case 8:
                    {
                        listBoxMatrix.Items.Clear();
                        catalogCycles = new List<string>();
                        int z = 0;
                        cyclesSearch();
                        foreach (var item in catalogCycles)
                        {
                            z++;
                            listBoxMatrix.Items.Add(item);
                        }
                        listBoxMatrix.Items.Add("Количество циклов: "+z);
                        break;
                    }
                    
                case 9:
                    {
                        int r = V.Count;
                        string reуs = "";
                        if ((r * (r - 1)) / 2 == E.Count)
                        {
                            reуs = "Граф является полным";
                        }
                        else
                            reуs = "Граф не является полным";
                        listBoxMatrix.Items.Add(reуs);
                        break;
                    }
                    
                default:
                    break;
            }
        }

        
        private int F(int count)
        {
            int temp = 1;
            for (int i = count; i > 0; i--)
            {
                temp *= i;
            }
            return temp;
        }

        private int [,] FillAMatrix(int [,]arr,int Vcount)
        {
           int[,] Matrix = new int[Vcount, Vcount];

            for (int i = 0; i < Vcount; i++)
            {
                for (int j = 0; j < Vcount; j++)
                {
                    Matrix[i, j] = arr[i, j];
                }
            }
            return Matrix;
        }

        private void Create_Graphs_Button_Click(object sender, EventArgs e)
        {
            V.Clear();
            E.Clear();
            G.clearSheet();
            picture.Image = G.GetBitmap();
            listBoxMatrix.Items.Clear();
            label1.Text = "";
            label2.Text = "";
            Form3 newForm = new Form3();
            newForm.ShowDialog();
            int Vcount = HelpClass.Size;
            AMatrix = new int[Vcount, Vcount];
            AMatrix= FillAMatrix(HelpClass.AdjCopy(), Vcount);
            Generate();
            ToBuildPath(Vcount);
        }

        private void ToBuildPath(int Vcount)
        {
            int count = 1;
            var z = Math.Floor((decimal)AMatrix.Length / 2);
            int[,] CopyMatrix=new int[Vcount,Vcount];
            CopyMatrix=FillAMatrix(AMatrix, Vcount);
            for (int i = 0; i < Vcount; i++)
            {
                if (AMatrix[i, i] > 0)
                {
                    E.Add(new Edge(i, i, CopyMatrix[i,i]));
                    G.drawEdge(V[i], V[i], E[E.Count - 1], E.Count - 1);
                    picture.Image = G.GetBitmap();
                    CopyMatrix[i, i] = 0;
                }
                for (int j = 1; j < Vcount; j++)
                {
                    if (AMatrix[i, j] > 0 && count <z&&CopyMatrix[i,j]>0&&CopyMatrix[j,i]>0)
                    {
                        E.Add(new Edge(i, j, CopyMatrix[i, j]));
                        G.drawEdge(V[i], V[j], E[E.Count - 1], E.Count - 1);
                        picture.Image = G.GetBitmap();
                        CopyMatrix[i, j] = 0;
                        CopyMatrix[j, i] = 0;
                        count++;
                    }
                }
            }
        }

        private void Generate()
        {
            Random rand = new Random();
            for (int i = 0; i < HelpClass.Size; i++)
            {
                int x = rand.Next(0, 400);
                int y = rand.Next(0, 300);

                V.Add(new Vertex(x, y));
                G.drawVertex(x, y, V.Count.ToString());
                picture.Image = G.GetBitmap();
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            V.Clear();
            E.Clear();
            G.clearSheet();
            picture.Image = G.GetBitmap();
            listBoxMatrix.Items.Clear();
            label1.Text = "";
            label2.Text = "";
            
            Generate();
            ToBuildPath(V.Count);

        }

       
    }
}
