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
    internal enum Tool
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
        private Bitmap _bitmap = default(Bitmap);
        private Graphics _graphics = default(Graphics);
        private Pen _pen = new Pen(Color.Black, 1);
        private Point _prevPoint = default(Point);
        private Point _currentPoint = default(Point);
        private bool _isMousePressed = false;
        private Tool _currentTool = Tool.Pen;
        public Form1()
        {
            InitializeComponent();
            _bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            pictureBox1.Image = _bitmap;
            _graphics.Clear(Color.White);
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
                        _bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case 2:
                        _bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 3:
                        _bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case 4:
                        _bitmap.Save(fs,
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
                _bitmap = Bitmap.FromFile(openFileDialog1.FileName) as Bitmap;
                pictureBox1.Image = _bitmap;
                _graphics = Graphics.FromImage(_bitmap);
           }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _currentTool = Tool.Line;
        }


        private Rectangle GetMRectangle(Point pPoint, Point cPoint)
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
            _currentTool = Tool.Pallete;
        }

        private Color GetColorAt(Point point)
        {
            Color pixelColor = _bitmap.GetPixel(point.X, point.Y);
            return pixelColor;
        }

        private Point[] Triangle(Point fPoint, Point sPoint)
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
            if (_isMousePressed)
            {
                switch (_currentTool)
                {
                    case Tool.Line:
                        _currentPoint = e.Location;
                        break;
                    case Tool.Rectangle:
                        _currentPoint = e.Location;
                        break;
                    case Tool.Ellipse:
                        _currentPoint = e.Location;
                        break;
                    case Tool.Pen:
                        _prevPoint = _currentPoint;
                        _currentPoint = e.Location;
                        _graphics.DrawLine(_pen, _prevPoint, _currentPoint);
                        break;
                    case Tool.Triangle:
                        _currentPoint = e.Location;
                        break;
                    case Tool.Eraser:                       
                        _currentPoint = e.Location;
                        _graphics.FillRectangle(new SolidBrush(Color.White), 
                            GetMRectangle(new Point(_currentPoint.X - 10, _currentPoint.Y - 10), 
                            new Point(_currentPoint.X + 10, _currentPoint.Y + 10)));
                        pictureBox1.Refresh();
                        break;
                    case Tool.DummyFill:
                        break;
                    case Tool.Fill:
                        break;
                    case Tool.Pallete:
                        textBox1.ForeColor = GetColorAt(_currentPoint);
                        textBox1.BackColor = GetColorAt(_currentPoint);
                        _pen.Color = GetColorAt(_currentPoint);
                        break;
                    default:
                        break;
                }                             
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _prevPoint = e.Location;
            _currentPoint = e.Location;
            _isMousePressed = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _isMousePressed = false;
            
            switch (_currentTool)
            {
                case Tool.Line:
                    _graphics.DrawLine(_pen, _prevPoint, _currentPoint);
                    break;
                case Tool.Rectangle:
                    //graphics.DrawEllipse(Pens.Red, 50, 50, 100, 100);                 
                    _graphics.DrawRectangle(_pen, GetMRectangle(_prevPoint, _currentPoint));
                    break;
                case Tool.Ellipse:
                    _graphics.DrawEllipse(_pen, GetMRectangle(_prevPoint, _currentPoint));
                    break;
                case Tool.Pen:                   
                    break;
                case Tool.Triangle:
                    _graphics.DrawPolygon(_pen, Triangle(_prevPoint, _currentPoint));
                    break;
                case Tool.Eraser:
                    _graphics.FillRectangle(new SolidBrush(Color.White), GetMRectangle(new Point(_currentPoint.X - 10, _currentPoint.Y - 10), new Point(_currentPoint.X + 10, _currentPoint.Y + 10)));
                    pictureBox1.Refresh();
                    break;
                case Tool.DummyFill:
                    _currentPoint = e.Location;
                    _bitmap = Utils.Fill(_bitmap, _currentPoint, _bitmap.GetPixel(e.X, e.Y), _pen.Color);
                    _graphics = Graphics.FromImage(_bitmap);
                    pictureBox1.Image = _bitmap;
                    pictureBox1.Refresh();
                    break;
                case Tool.Fill:
                    MapFill mf = new MapFill();
                    mf.Fill(_graphics, _currentPoint, _pen.Color, ref _bitmap);
                    _graphics = Graphics.FromImage(_bitmap);
                    pictureBox1.Image = _bitmap;
                    pictureBox1.Refresh();
                    break;
                case Tool.Pallete:
                    if (e.Button == MouseButtons.Left)
                    {
                        textBox1.ForeColor = GetColorAt(_currentPoint);
                        textBox1.BackColor = GetColorAt(_currentPoint);
                        _pen.Color = GetColorAt(_currentPoint);
                    }                       
                    break;
                case Tool.Rectangle:
                    break;
                default:
                    break;
            }
            _prevPoint = e.Location;
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            switch (_currentTool)
            {
                case Tool.Line:
                    e.Graphics.DrawLine(_pen, _prevPoint, _currentPoint);
                    break;
                case Tool.Rectangle:
                    //e.Graphics.DrawEllipse(pen, GetMRectangle(prevPoint, currentPoint));
                    e.Graphics.DrawRectangle(_pen, GetMRectangle(_prevPoint, _currentPoint));
                    break;
                case Tool.Ellipse:
                    e.Graphics.DrawEllipse(_pen, GetMRectangle(_prevPoint, _currentPoint));
                    break;
                case Tool.Pen:
                    break;
                case Tool.Triangle:
                    e.Graphics.DrawPolygon(_pen, Triangle(_prevPoint, _currentPoint));
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
            _currentTool = Tool.Rectangle;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            _currentTool = Tool.Pen;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _currentTool = Tool.Triangle;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog myDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            myDialog.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            myDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            myDialog.Color = textBox1.ForeColor;
            // Update the text box color if the user clicks OK 
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.ForeColor = myDialog.Color;
                textBox1.BackColor = myDialog.Color;
                _pen.Color = myDialog.Color;
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _currentTool = Tool.Eraser;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _currentTool = Tool.DummyFill;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _currentTool = Tool.Fill;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            _pen.Width = trackBar1.Value;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            _currentTool = Tool.Ellipse;
        }
    }
}
