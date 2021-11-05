using System.Collections.Generic;
using System.Drawing;

namespace Graphs
{
    class DrawG
    {
        Bitmap bitmap;
        Pen blackPen;
        Pen redPen;
        Pen darkGoldPen;
        Graphics gr;
        Font fo;
        Brush br;
        PointF point;
        public int R = 20; //радиус окружности вершины

        public DrawG(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap);
            clearSheet();
            blackPen = new Pen(Color.Black);
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            redPen.Width = 2;
            darkGoldPen = new Pen(Color.DarkGoldenrod);
            darkGoldPen.Width = 2;
            fo = new Font("Arial", 15);
            br = Brushes.Black;
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void clearSheet()
        {
            gr.Clear(Color.White);
        }

        public void drawVertex(int x, int y, string number)
        {
            gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackPen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - 9, y - 9);
            gr.DrawString(number, fo, br, point);
        }
        public void drawPow(int x, int y, string number)
        {
            point = new PointF(x - 8, y - 40);
            gr.DrawString(number, new Font("Arial", 8), new SolidBrush(Color.Black), point);
        }

        public void drawSelectedVertex(int x, int y)
        {
            gr.DrawEllipse(redPen, (x - R), (y - R), 2 * R, 2 * R);
        }

        public void drawEdge(Vertex V1, Vertex V2, Edge E, int numberE)
        {
            if (E.v1 == E.v2)
            {
                gr.DrawArc(darkGoldPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else
            {
                gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                string res = ((char)('a' + numberE)).ToString();
                res += $"({E.weight})";
                gr.DrawString(res, fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
                drawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
            }
        }
        public void EditEdge(Vertex V1, Vertex V2, Edge E, int numberE)
        {
            point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
            string res = ((char)('a' + numberE)).ToString();
            res += $"({E.weight})";
            gr.DrawString(res, fo, br, point);
            drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            drawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
        }
        public void drawALLGraph(List<Vertex> V, List<Edge> E)
        {
            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].v1 == E[i].v2)
                {
                    gr.DrawArc(darkGoldPen, (V[E[i].v1].x - 2 * R), (V[E[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V[E[i].v1].x - (int)(2.75 * R), V[E[i].v1].y - (int)(2.75 * R));
                    string res = ((char)('a' + i)).ToString();
                    res += $"({E[i].weight})";
                    gr.DrawString(res, fo, br, point);
                }
                else
                {
                    gr.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
                    point = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
                    string res = ((char)('a' + i)).ToString();
                    res += $"({E[i].weight})";
                    gr.DrawString(res, fo, br, point);
                }
            }
            //рисуем вершины
            for (int i = 0; i < V.Count; i++)
            {
                drawVertex(V[i].x, V[i].y, (i + 1).ToString());
            }
        }

        //заполняет матрицу смежности
        public void fillAdjacencyMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < numberV; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, E[i].v2] = 1;
                matrix[E[i].v2, E[i].v1] = 1;
            }
        }

        //заполняет матрицу инцидентности
        public void fillIncidenceMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < E.Count; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, i] = 1;
                matrix[E[i].v2, i] = 1;
            }
        }
        private void DFSchain(int u, int endV, List<Edge> E, int[] color, string s)
        {
            //вершину не следует перекрашивать, если u == endV (возможно в нее есть несколько путей)
            if (u != endV)
                color[u] = 2;
            else
            {
                //listBoxMatrix.Items.Add(s);
                return;
            }
            for (int w = 0; w < E.Count; w++)
            {
                if (color[E[w].v2] == 1 && E[w].v1 == u)
                {
                    DFSchain(E[w].v2, endV, E, color, s + "-" + (E[w].v2 + 1).ToString());
                    color[E[w].v2] = 1;
                }
                else if (color[E[w].v1] == 1 && E[w].v2 == u)
                {
                    DFSchain(E[w].v1, endV, E, color, s + "-" + (E[w].v1 + 1).ToString());
                    color[E[w].v1] = 1;
                }
            }
        }

    }
}
