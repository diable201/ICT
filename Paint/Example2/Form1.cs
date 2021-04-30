using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example2
{
    enum Tool
    {
        Line,
        Rectangle,
        Pen,
        Triangle,
        Eraser,
        DummyFill,
        Fill,
        Pallete,
        Ellipse
    }
    public partial class Form1 : Form
    {
        Bitmap bitmap = default(Bitmap);
        Graphics graphics = default(Graphics);
        Pen pen = new Pen(Color.Black, 1);
        Point prevPoint = default(Point);
        Point currentPoint = default(Point);
        bool isMousePressed = false;
        Tool currentTool = Tool.Pen;
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            
            
            pictureBox1.Image = bitmap;
            graphics.Clear(Color.White);
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            button3.Select();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png";
            saveFileDialog1.Title = "Save an Image File";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case 4:
                        bitmap.Save(fs,
                            System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if (openFileDialog1.ShowDialog() == DialogResult.OK)
           {
                bitmap = Bitmap.FromFile(openFileDialog1.FileName) as Bitmap;
                pictureBox1.Image = bitmap;
                graphics = Graphics.FromImage(bitmap);
           }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Line;
        }


        Rectangle GetMRectangle(Point pPoint, Point cPoint)
        {
            return new Rectangle
            {
                X = Math.Min(pPoint.X, cPoint.X),
                Y = Math.Min(pPoint.Y, cPoint.Y),
                Width = Math.Abs(pPoint.X - cPoint.X),
                Height = Math.Abs(pPoint.Y - cPoint.Y)
            };
        }

        private void button9_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Pallete;
        }

        private Color GetColorAt(Point point)
        {
            Color pixelColor = bitmap.GetPixel(point.X, point.Y);
            return pixelColor;
        }

        Point[] Triangle(Point fPoint, Point sPoint)
        {
            Point mid = new Point
            {
                X = fPoint.X,
                Y = sPoint.Y
            };
            Point[] points = new Point[3] { fPoint, sPoint, mid };
            return points;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Location.ToString();
            if (isMousePressed)
            {
                switch (currentTool)
                {
                    case Tool.Line:
                        currentPoint = e.Location;
                        break;
                    case Tool.Rectangle:
                        currentPoint = e.Location;
                        break;
                    case Tool.Ellipse:
                        currentPoint = e.Location;
                        break;
                    case Tool.Pen:
                        prevPoint = currentPoint;
                        currentPoint = e.Location;
                        graphics.DrawLine(pen, prevPoint, currentPoint);
                        break;
                    case Tool.Triangle:
                        currentPoint = e.Location;
                        break;
                    case Tool.Eraser:                       
                        currentPoint = e.Location;
                        graphics.FillRectangle(new SolidBrush(Color.White), GetMRectangle(new Point(currentPoint.X - 10, currentPoint.Y - 10), new Point(currentPoint.X + 10, currentPoint.Y + 10)));
                        pictureBox1.Refresh();
                        break;
                    case Tool.DummyFill:
                        break;
                    case Tool.Fill:
                        break;
                    case Tool.Pallete:
                        textBox1.ForeColor = GetColorAt(currentPoint);
                        textBox1.BackColor = GetColorAt(currentPoint);
                        pen.Color = GetColorAt(currentPoint);
                        break;
                    default:
                        break;
                }                             
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            prevPoint = e.Location;
            currentPoint = e.Location;
            isMousePressed = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;
            
            switch (currentTool)
            {
                case Tool.Line:
                    graphics.DrawLine(pen, prevPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    //graphics.DrawEllipse(Pens.Red, 50, 50, 100, 100);                 
                    graphics.DrawRectangle(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Ellipse:
                    graphics.DrawEllipse(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Pen:                   
                    break;
                case Tool.Triangle:
                    graphics.DrawPolygon(pen, Triangle(prevPoint, currentPoint));
                    break;
                case Tool.Eraser:
                    graphics.FillRectangle(new SolidBrush(Color.White), GetMRectangle(new Point(currentPoint.X - 10, currentPoint.Y - 10), new Point(currentPoint.X + 10, currentPoint.Y + 10)));
                    pictureBox1.Refresh();
                    break;
                case Tool.DummyFill:
                    currentPoint = e.Location;
                    bitmap = Utils.Fill(bitmap, currentPoint, bitmap.GetPixel(e.X, e.Y), pen.Color);
                    graphics = Graphics.FromImage(bitmap);
                    pictureBox1.Image = bitmap;
                    pictureBox1.Refresh();
                    break;
                case Tool.Fill:
                    MapFill mf = new MapFill();
                    mf.Fill(graphics, currentPoint, pen.Color, ref bitmap);
                    graphics = Graphics.FromImage(bitmap);
                    pictureBox1.Image = bitmap;
                    pictureBox1.Refresh();
                    break;
                case Tool.Pallete:
                    if (e.Button == MouseButtons.Left)
                    {
                        textBox1.ForeColor = GetColorAt(currentPoint);
                        textBox1.BackColor = GetColorAt(currentPoint);
                        pen.Color = GetColorAt(currentPoint);
                    }                       
                    break;
                default:
                    break;
            }
            prevPoint = e.Location;
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            switch (currentTool)
            {
                case Tool.Line:
                    e.Graphics.DrawLine(pen, prevPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    //e.Graphics.DrawEllipse(pen, GetMRectangle(prevPoint, currentPoint));
                    e.Graphics.DrawRectangle(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Ellipse:
                    e.Graphics.DrawEllipse(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Pen:
                    break;
                case Tool.Triangle:
                    e.Graphics.DrawPolygon(pen, Triangle(prevPoint, currentPoint));
                    break;
                case Tool.DummyFill:     
                    break;
                case Tool.Fill:
                    break;
                case Tool.Eraser:
                    break;
                case Tool.Pallete:
                    break;
                default:
                    break;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Rectangle;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Pen;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Triangle;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = textBox1.ForeColor;
            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.ForeColor = MyDialog.Color;
                textBox1.BackColor = MyDialog.Color;
                pen.Color = MyDialog.Color;
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Eraser;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            currentTool = Tool.DummyFill;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Fill;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Ellipse;
        }
    }
}
