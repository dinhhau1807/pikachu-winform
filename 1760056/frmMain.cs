using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1760056
{
    public partial class frmMain : Form
    {
        /*
            O O O O O O O O O O O O O O O O
            O X X X X X X X X X X X X X X O
            O X X X X X X X X X X X X X X O
            O X X X X X X X X X X X X X X O
            O X X X X X X X X X X X X X X O
            O X X X X X X X X X X X X X X O
            O X X X X X X X X X X X X X X O
            O O O O O O O O O O O O O O O O 
        */

        // Size
        const int startX = 80, startY = 80;
        const int sizeWidth = 70, sizeHeight = 70;
        const int margin = 5;

        // Private fields
        Random rd = new Random();
        PictureBox firstClick = null, secondClick = null;
        Color defaultColor;
        List<Image> imagesList = new List<Image>();
        const int rows = 6 + 2, cols = 14 + 2;
        List<List<PictureBox>> matrix;

        // Countdown
        const long secondsToWait = 6 * 60 + 30; // 6:30
        long timeRunning = secondsToWait;
        Timer time;

        // Draw lines
        PictureBox canvas = new PictureBox();
        Graphics g;
        Pen pen = new Pen(Color.DeepSkyBlue, 9F);
        Line line;

        public frmMain()
        {
            InitializeComponent();
            LoadImage();
            Initials();
            this.Controls.Add(canvas);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            canvas.Dock = DockStyle.Fill;
            canvas.BackgroundImageLayout = ImageLayout.Stretch;
            canvas.BackgroundImage = Image.FromFile(@"..\..\Resources\pokemon-background.jpg");
            panelTime.Parent = canvas;

            defaultColor = this.BackColor;
            this.Icon = new Icon(@"..\..\Resources\pokemon.ico");

            lblTimer.BackColor = Color.Transparent;
            panelTime.BackColor = Color.Transparent;

            this.WindowState = FormWindowState.Maximized;

            time = new Timer { Interval = 1000 };
            time.Tick += Time_Tick;

            time.Start();
        }

        #region Apperance
        private void Time_Tick(object sender, EventArgs e)
        {
            timeRunning--;

            if (timeRunning <= 0)
            {
                time.Stop();
                progressTime.Value = progressTime.Minimum;
                lblTimer.Text = "00:00";

                // Check if gamer is winner or loser
                CheckStatus();
            }

            progressTime.Value = (int)timeRunning;
            lblTimer.Text = string.Format("{0:D2}:{1:D2}", timeRunning / 60, timeRunning % 60);
        }

        /// <summary>
        /// Check if gamer is winner or loser
        /// </summary>
        private void CheckStatus()
        {
            bool check = true;

            foreach (var list in matrix)
            {
                foreach (var btn in list)
                {
                    if (btn.Visible)
                    {
                        check = false;
                        break;
                    }
                }
            }

            LockGame(true);

            if (check)
            {
                DialogResult answer = MessageBox.Show("Bạn đã thắng!\nBạn có muốn chơi lại?",
                                                       "Xin chúc mừng",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Question);

                if (answer == DialogResult.Yes)
                {
                    tròChơiMớiToolStripMenuItem.PerformClick();
                }
            }
            else
            {
                DialogResult answer = MessageBox.Show("Bạn đã thua!\nBạn có muốn chơi lại?",
                                                       "Xin chia buồn",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Error);

                if (answer == DialogResult.Yes)
                {
                    tròChơiMớiToolStripMenuItem.PerformClick();
                }
            }
        }

        private void tròChơiMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            time.Stop();
            timeRunning = secondsToWait;
            NewGame();
            time.Start();
        }

        private void toànMànHìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.TopMost == false)
            {
                var tmpImage = this.BackgroundImage;
                this.BackgroundImage = null;

                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;

                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

                this.BackgroundImage = tmpImage;
            }
            else
            {
                var tmpImage = this.BackgroundImage;
                this.BackgroundImage = null;

                this.TopMost = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Maximized;

                this.BackgroundImage = tmpImage;
            }
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Họ tên: Nguyễn Đình Hậu\nMSSV: 1760056");
        }

        /// <summary>
        /// Handle lock game
        /// </summary>
        /// <param name="check"></param>
        private void LockGame(bool check)
        {
            if (check)
            {
                time.Stop();

                foreach (var list in matrix)
                {
                    foreach (var btn in list)
                    {
                        btn.Enabled = false;
                    }
                }
            }
            else
            {
                foreach (var list in matrix)
                {
                    foreach (var btn in list)
                    {
                        btn.Enabled = true;
                    }
                }

                time.Start();
            }
        }

        /// <summary>
        /// Initial images for game
        /// </summary>
        private void LoadImage()
        {
            Image image = null;

            DirectoryInfo di = new DirectoryInfo("../../Resources/Characters");
            List<FileInfo> files = di.GetFiles().ToList();
            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].Extension == ".db")
                {
                    files.RemoveAt(i);
                    break;
                }
            }
            // Add image to List images
            foreach (var file in files)
            {
                image = Image.FromFile(file.FullName);
                imagesList.Add(image);
            }
        }

        /// <summary>
        /// Create button for form, call method RandomImage to generate image for button
        /// </summary>
        private void Initials()
        {
            progressTime.Maximum = (int)secondsToWait;
            progressTime.Value = progressTime.Maximum;

            PictureBox image = null;
            int x = startX, y = startY;
            matrix = new List<List<PictureBox>>();

            for (int i = 0; i < rows; i++)
            {
                var images = new List<PictureBox>();
                for (int j = 0; j < cols; j++)
                {
                    int index = rd.Next(imagesList.Count - 1);

                    image = new PictureBox();
                    image.Size = new Size(sizeWidth, sizeHeight);
                    image.Location = new Point(x, y);
                    image.BorderStyle = BorderStyle.None;
                    image.BackgroundImageLayout = ImageLayout.Stretch;

                    image.Click += new EventHandler(img_Click);

                    this.Controls.Add(image);
                    images.Add(image);

                    x += sizeWidth + margin;
                }
                x = startX;
                y += sizeHeight + margin;

                matrix.Add(images);
            }

            RandomImage();
            SetDisableButtonEdgeOfGrid();
        }

        /// <summary>
        /// Return the game to its original state
        /// </summary>
        private void NewGame()
        {
            LockGame(false);

            btnRadom.Text = "Random: 1";
            btnRadom.Enabled = true;

            progressTime.Maximum = (int)secondsToWait;
            progressTime.Value = progressTime.Maximum;

            firstClick = secondClick = null;

            for (int i = 1; i < matrix.Count - 1; i++)
            {
                for (int j = 1; j < matrix[i].Count - 1; j++)
                {
                    matrix[i][j].Visible = true;
                    matrix[i][j].BackColor = defaultColor;
                }
            }

            RandomImage();
        }

        /// <summary>
        /// Handle click event for every image in game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void img_Click(object sender, EventArgs e)
        {
            var image = sender as PictureBox;
            image.BackColor = Color.LightSalmon;

            if (firstClick == null)
            {
                firstClick = image;
                return;
            }

            if (secondClick == null)
            {
                secondClick = image;

                // Handle if two buttons clicked
                if ((int)firstClick.Tag == (int)secondClick.Tag && firstClick != secondClick)
                {
                    if ((line = checkTwoImages(firstClick, secondClick)) != null)
                    {
                        // Draw line graphics
                        using (g = canvas.CreateGraphics())
                        {
                            g.DrawLines(pen, line.Points.ToArray());
                        }
                        DrawLineImages(line, firstClick, secondClick);

                        // Hide two images
                        firstClick.Visible = false;
                        secondClick.Visible = false;

                        // Refresh state
                        canvas.Refresh();
                        firstClick = secondClick = null;
                    }
                    else
                    {
                        firstClick.BackgroundImage = imagesList[(int)firstClick.Tag];
                        firstClick.BackColor = defaultColor;
                        secondClick.BackgroundImage = imagesList[(int)secondClick.Tag];
                        secondClick.BackColor = defaultColor;

                        firstClick = secondClick = null;
                    }
                }
                else
                {
                    firstClick.BackgroundImage = imagesList[(int)firstClick.Tag];
                    firstClick.BackColor = defaultColor;
                    secondClick.BackgroundImage = imagesList[(int)secondClick.Tag];
                    secondClick.BackColor = defaultColor;

                    firstClick = secondClick = null;
                }


                // Check status if gamer is winner
                if (matrix.All(l => l.All(b => b.Visible == false)))
                {
                    CheckStatus();
                }

                return;
            }
        }

        /// <summary>
        /// Random images for buttons in form after loaded
        /// </summary>
        private void RandomImage()
        {
            int times, random, numbers;
            Image image;

            List<PictureBox> clone = new List<PictureBox>();
            for (int i = 1; i < matrix.Count - 1; i++)
            {
                for (int j = 1; j < matrix[i].Count - 1; j++)
                {
                    clone.Add(matrix[i][j]);
                }
            }

            while (clone.Count > 0)
            {
                times = rd.Next(0, 3);
                random = rd.Next(0, imagesList.Count - 1);

                image = imagesList[random];
                numbers = times * 2;

                int index;
                for (int i = 0; i < numbers; i++)
                {
                    if (clone.Count > 0)
                    {
                        index = rd.Next(0, clone.Count - 1);

                        clone[index].BackgroundImage = image;
                        clone[index].Tag = random;
                        clone.RemoveAt(index);
                    }
                }
            }
        }

        /// <summary>
        /// Set disable for all button place at edge of grid
        /// </summary>
        private void SetDisableButtonEdgeOfGrid()
        {
            List<PictureBox> clone = new List<PictureBox>();
            for (int i = 0; i < matrix.Count; i++)
            {
                if (i == 0 || i == matrix.Count - 1)
                {
                    for (int j = 0; j < matrix[i].Count; j++)
                    {
                        clone.Add(matrix[i][j]);
                    }
                }
                else
                {
                    clone.Add(matrix[i][0]);
                    clone.Add(matrix[i][cols - 1]);
                }
            }

            foreach (var pics in clone)
            {
                pics.Visible = false;
            }
        }

        /// <summary>
        /// Random 1 time with images is visible = true in matrix
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRadom_Click(object sender, EventArgs e)
        {
            int times, random, numbers;
            Image image;

            List<PictureBox> clone = new List<PictureBox>();
            for (int i = 1; i < matrix.Count - 1; i++)
            {
                for (int j = 1; j < matrix[i].Count - 1; j++)
                {
                    if (matrix[i][j].Visible)
                    {
                        clone.Add(matrix[i][j]);
                    }
                }
            }

            while (clone.Count > 0)
            {
                times = rd.Next(0, 3);
                random = rd.Next(0, imagesList.Count - 1);

                image = imagesList[random];
                numbers = times * 2;

                int index;
                for (int i = 0; i < numbers; i++)
                {
                    if (clone.Count > 0)
                    {
                        index = rd.Next(0, clone.Count - 1);

                        clone[index].BackgroundImage = image;
                        clone[index].Tag = random;
                        clone.RemoveAt(index);
                    }
                }
            }

            btnRadom.Text = "Random: 0";
            btnRadom.Enabled = false;
        }

        #endregion

        #region DrawLines
        /// <summary>
        /// Draw lines in side two images
        /// </summary>
        /// <param name="line"></param>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        public void DrawLineImages(Line line, PictureBox image1, PictureBox image2)
        {
            using (g = image1.CreateGraphics())
            {
                switch (line.Point1)
                {
                    case Type.Up:
                        g.DrawLine(pen,
                            new PointF(sizeWidth / 2, 0),
                            new PointF(sizeWidth / 2, sizeHeight / 2));
                        break;
                    case Type.Down:
                        g.DrawLine(pen,
                            new PointF(sizeWidth / 2, sizeHeight / 2),
                            new PointF(sizeWidth / 2, sizeHeight));
                        break;
                    case Type.Right:
                        g.DrawLine(pen,
                            new PointF(sizeWidth / 2, sizeHeight / 2),
                            new PointF(sizeWidth, sizeHeight / 2));
                        break;
                    case Type.Left:
                        g.DrawLine(pen,
                            new PointF(0, sizeHeight / 2),
                            new PointF(sizeWidth / 2, sizeHeight / 2));
                        break;
                }
            }

            using (g = image2.CreateGraphics())
            {
                switch (line.Point2)
                {
                    case Type.Up:
                        g.DrawLine(pen,
                            new PointF(sizeWidth / 2, 0),
                            new PointF(sizeWidth / 2, sizeHeight / 2));
                        break;
                    case Type.Down:
                        g.DrawLine(pen,
                            new PointF(sizeWidth / 2, sizeHeight / 2),
                            new PointF(sizeWidth / 2, sizeHeight));
                        break;
                    case Type.Right:
                        g.DrawLine(pen,
                            new PointF(sizeWidth / 2, sizeHeight / 2),
                            new PointF(sizeWidth, sizeHeight / 2));
                        break;
                    case Type.Left:
                        g.DrawLine(pen,
                            new PointF(0, sizeHeight / 2),
                            new PointF(sizeWidth / 2, sizeHeight / 2));
                        break;
                }
            }
        }
        #endregion

        #region LogicGame

        /// <summary>
        /// Summary logic all check cases
        /// </summary>
        /// <param name="firstImage"></param>
        /// <param name="secondImage"></param>
        /// <returns></returns>
        private Line checkTwoImages(PictureBox firstImage, PictureBox secondImage)
        {
            Line line = null;

            var pointFirstImage = new PointF(firstImage.Location.X + sizeWidth / 2, firstImage.Location.Y + sizeHeight / 2);
            var pointSecondImage = new PointF(secondImage.Location.X + sizeWidth / 2, secondImage.Location.Y + sizeHeight / 2);

            var p1 = new Point();
            var p2 = new Point();

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    if (matrix[i][j] == firstImage)
                    {
                        p1.X = j;
                        p1.Y = i;
                    }

                    if (matrix[i][j] == secondImage)
                    {
                        p2.X = j;
                        p2.Y = i;
                    }
                }
            }

            // Check line x
            if (p1.Y == p2.Y)
            {
                if (checkLineX(p1.X, p2.X, p1.Y))
                {
                    line = new Line();
                    line.Points = new List<PointF>()
                    {
                        pointFirstImage,
                        pointSecondImage
                    };

                    line.Point1 = Type.Right;
                    line.Point2 = Type.Left;
                    if (p1.X > p2.X)
                    {
                        line.Point1 = Type.Left;
                        line.Point2 = Type.Right;
                    }

                    return line;
                }
            }

            // Check line y
            if (p1.X == p2.X)
            {
                if (checkLineY(p1.Y, p2.Y, p1.X))
                {
                    line = new Line();
                    line.Points = new List<PointF>()
                    {
                        pointFirstImage,
                        pointSecondImage
                    };

                    line.Point1 = Type.Down;
                    line.Point2 = Type.Up;
                    if (p1.Y > p2.Y)
                    {
                        line.Point1 = Type.Up;
                        line.Point2 = Type.Down;
                    }

                    return line;
                }
            }

            // Check x axis or y axis is found in rectangle check
            int axis = -1;

            // Check rectangle x
            if ((axis = checkRectX(p1, p2)) != -1)
            {
                if (p1.X > p2.X)
                {
                    SwapPoint(ref pointFirstImage, ref pointSecondImage);
                }

                int tmp = (p1.X < p2.X) ? axis - p1.X : axis - p2.X;

                // axis is X
                line = new Line();
                line.Points = new List<PointF>()
                {
                    pointFirstImage,
                    new PointF(pointFirstImage.X + tmp*sizeWidth + tmp*margin, pointFirstImage.Y),
                    new PointF(pointFirstImage.X + tmp*sizeWidth + tmp*margin, pointSecondImage.Y),
                    pointSecondImage
                };

                line.Point1 = Type.Right;
                line.Point2 = Type.Left;
                if (p1.X > p2.X)
                {
                    line.Point1 = Type.Left;
                    line.Point2 = Type.Right;
                }

                return line;
            }

            // Check rectangle y
            if ((axis = checkRectY(p1, p2)) != -1)
            {
                if (p1.Y > p2.Y)
                {
                    SwapPoint(ref pointFirstImage, ref pointSecondImage);
                }

                int tmp = (p1.Y < p2.Y) ? axis - p1.Y : axis - p2.Y;


                // axis is Y
                line = new Line();
                line.Points = new List<PointF>()
                {
                    pointFirstImage,
                    new PointF(pointFirstImage.X, pointFirstImage.Y + tmp*sizeHeight + tmp*margin),
                    new PointF(pointSecondImage.X, pointFirstImage.Y + tmp*sizeHeight + tmp*margin),
                    pointSecondImage
                };

                line.Point1 = Type.Down;
                line.Point2 = Type.Up;
                if (p1.Y > p2.Y)
                {
                    line.Point1 = Type.Up;
                    line.Point2 = Type.Down;
                }

                return line;
            }

            // Check extend left
            if ((axis = checkExtendLineX(p1, p2, -1)) != -1)
            {
                // axis is X
                line = new Line();

                if (p1.Y > p2.Y)
                {
                    SwapPoint(ref pointFirstImage, ref pointSecondImage);
                }

                int tmp1 = (p1.Y < p2.Y) ? p1.X - axis : p2.X - axis;
                int tmp2 = (p1.Y < p2.Y) ? p2.X - axis : p1.X - axis;
                
                line.Points = new List<PointF>()
                {
                    pointFirstImage,
                    new PointF(pointFirstImage.X - tmp1*sizeWidth - tmp1*margin, pointFirstImage.Y),
                    new PointF(pointSecondImage.X - tmp2*sizeWidth - tmp2*margin, pointSecondImage.Y),
                    pointSecondImage
                };

                line.Point1 = line.Point2 = Type.Left;

                if (p1.Y < p2.Y)
                {
                    if (tmp1 == 0)
                    {
                        line.Point1 = Type.Down;
                    }
                    if (tmp2 == 0)
                    {
                        line.Point2 = Type.Up;
                    }
                }
                else
                {
                    if (tmp1 == 0)
                    {
                        line.Point2 = Type.Down;
                    }
                    if (tmp2 == 0)
                    {
                        line.Point1 = Type.Up;
                    }
                }

                return line;
            }

            // Check extend right
            if ((axis = checkExtendLineX(p1, p2, 1)) != -1)
            {
                // axis is X
                line = new Line();

                if (p1.Y > p2.Y)
                {
                    SwapPoint(ref pointFirstImage, ref pointSecondImage);
                }

                int tmp1 = (p1.Y < p2.Y) ? axis - p1.X : axis - p2.X;
                int tmp2 = (p1.Y < p2.Y) ? axis - p2.X : axis - p1.X;
                
                line.Points = new List<PointF>()
                {
                    pointFirstImage,
                    new PointF(pointFirstImage.X + tmp1*sizeWidth + tmp1*margin, pointFirstImage.Y),
                    new PointF(pointSecondImage.X + tmp2*sizeWidth + tmp2*margin, pointSecondImage.Y),
                    pointSecondImage
                };

                line.Point1 = line.Point2 = Type.Right;

                if (p1.Y < p2.Y)
                {
                    if (tmp1 == 0)
                    {
                        line.Point1 = Type.Down;
                    }
                    if (tmp2 == 0)
                    {
                        line.Point2 = Type.Up;
                    }
                }
                else
                {
                    if (tmp1 == 0)
                    {
                        line.Point2 = Type.Down;
                    }
                    if (tmp2 == 0)
                    {
                        line.Point1 = Type.Up;
                    }
                }

                return line;
            }

            // Check extend up
            if ((axis = checkExtendLineY(p1, p2, -1)) != -1)
            {
                if (p1.X > p2.X)
                {
                    SwapPoint(ref pointFirstImage, ref pointSecondImage);
                }

                int tmp1 = (p1.X < p2.X) ? p1.Y - axis : p2.Y - axis;
                int tmp2 = (p1.X < p2.X) ? p2.Y - axis : p1.Y - axis;

                // axis is Y
                line = new Line();
                line.Points = new List<PointF>()
                {
                    pointFirstImage,
                    new PointF(pointFirstImage.X, pointFirstImage.Y - tmp1*sizeHeight - tmp1*margin),
                    new PointF(pointSecondImage.X, pointSecondImage.Y - tmp2*sizeHeight - tmp2*margin),
                    pointSecondImage
                };

                line.Point1 = line.Point2 = Type.Up;

                if (p1.X < p2.X)
                {
                    if (tmp1 == 0)
                    {
                        line.Point1 = Type.Right;
                    }
                    if (tmp2 == 0)
                    {
                        line.Point2 = Type.Left;
                    }
                }
                else
                {
                    if (tmp1 == 0)
                    {
                        line.Point2 = Type.Right;
                    }
                    if (tmp2 == 0)
                    {
                        line.Point1 = Type.Left;
                    }
                }

                return line;
            }

            // Check extend down
            if ((axis = checkExtendLineY(p1, p2, 1)) != -1)
            {
                // axis is Y
                line = new Line();

                if (p1.X > p2.X)
                {
                    SwapPoint(ref pointFirstImage, ref pointSecondImage);
                }

                int tmp1 = (p1.X < p2.X) ? axis - p1.Y : axis - p2.Y;
                int tmp2 = (p1.X < p2.X) ? axis - p2.Y : axis - p1.Y;
               
                line.Points = new List<PointF>()
                {
                    pointFirstImage,
                    new PointF(pointFirstImage.X, pointFirstImage.Y + tmp1*sizeHeight + tmp1*margin),
                    new PointF(pointSecondImage.X, pointSecondImage.Y + tmp2*sizeHeight + tmp2*margin),
                    pointSecondImage
                };

                line.Point1 = line.Point2 = Type.Down;

                if (p1.X < p2.X)
                {
                    if (tmp1 == 0)
                    {
                        line.Point1 = Type.Right;
                    }
                    if (tmp2 == 0)
                    {
                        line.Point2 = Type.Left;
                    }
                }
                else
                {
                    if (tmp1 == 0)
                    {
                        line.Point2 = Type.Right;
                    }
                    if (tmp2 == 0)
                    {
                        line.Point1 = Type.Left;
                    }
                }

                return line;
            }

            return line;
        }

        private void SwapPoint(ref PointF point1, ref PointF point2)
        {
            PointF tmp = point1;
            point1 = point2;
            point2 = tmp;
        }

        private bool checkLineX(int x1, int x2, int y)
        {
            // Column max, min
            int min = Math.Min(x1, x2);
            int max = Math.Max(x1, x2);

            for (int x = min; x <= max; x++)
            {
                if (matrix[y][x].Visible && matrix[y][x] != firstClick && matrix[y][x] != secondClick)
                {
                    return false;
                }
            }

            return true;
        }

        private bool checkLineY(int y1, int y2, int x)
        {
            // Row max, min
            int min = Math.Min(y1, y2);
            int max = Math.Max(y1, y2);

            for (int y = min; y <= max; y++)
            {
                if (matrix[y][x].Visible && matrix[y][x] != firstClick && matrix[y][x] != secondClick)
                {
                    return false;
                }
            }

            return true;
        }

        private int checkRectX(Point p1, Point p2)
        {
            // Find point have minX, maxX
            Point pMinX = p1, pMaxX = p2;
            if (p1.X > p2.X)
            {
                pMinX = p2;
                pMaxX = p1;
            }

            for (int x = pMinX.X + 1; x < pMaxX.X; x++)
            {
                if (checkLineX(pMinX.X, x, pMinX.Y) &&
                    checkLineY(pMinX.Y, pMaxX.Y, x) &&
                    checkLineX(x, pMaxX.X, pMaxX.Y))
                {
                    return x;
                }
            }

            return -1;
        }

        private int checkRectY(Point p1, Point p2)
        {
            // Find point have minY, maxY
            Point pMinY = p1, pMaxY = p2;
            if (p1.Y > p2.Y)
            {
                pMinY = p2;
                pMaxY = p1;
            }

            for (int y = pMinY.Y + 1; y < pMaxY.Y; y++)
            {
                if (checkLineY(pMinY.Y, y, pMinY.X) &&
                    checkLineX(pMinY.X, pMaxY.X, y) &&
                    checkLineY(y, pMaxY.Y, pMaxY.X))
                {
                    return y;
                }
            }

            return -1;
        }

        /// <param name="type">-1 is check left, +1 is check right (follow x axis)</param>
        private int checkExtendLineX(Point p1, Point p2, int type)
        {
            // Find point have minX, maxX
            Point pMinX = p1, pMaxX = p2;
            if (p1.X > p2.X)
            {
                pMinX = p2;
                pMaxX = p1;
            }

            // Find line and x begin
            int x = pMaxX.X;
            int y = pMinX.Y;
            if (type == -1)
            {
                x = pMinX.X;
                y = pMaxX.Y;
            }

            // Check extend
            if (checkLineX(pMinX.X, pMaxX.X, y))
            {
                while ((!matrix[pMinX.Y][x].Visible || matrix[pMinX.Y][x] == firstClick || matrix[pMinX.Y][x] == secondClick) &&
                       (!matrix[pMaxX.Y][x].Visible || matrix[pMaxX.Y][x] == firstClick || matrix[pMaxX.Y][x] == secondClick))
                {
                    if (checkLineY(pMinX.Y, pMaxX.Y, x))
                    {
                        return x;
                    }
                    x += type;
                }
            }

            return -1;
        }

        /// <param name="type">-1 is check up, +1 is check down (follow y axis)</param>
        private int checkExtendLineY(Point p1, Point p2, int type)
        {
            // Find point have minY, maxY
            Point pMinY = p1, pMaxY = p2;
            if (p1.Y > p2.Y)
            {
                pMinY = p2;
                pMaxY = p1;
            }

            // Find line and y begin
            int y = pMaxY.Y;
            int x = pMinY.X;
            if (type == -1)
            {
                y = pMinY.Y;
                x = pMaxY.X;
            }

            // Check extend
            if (checkLineY(pMinY.Y, pMaxY.Y, x))
            {
                while ((!matrix[y][pMinY.X].Visible || matrix[y][pMinY.X] == firstClick || matrix[y][pMinY.X] == secondClick) &&
                       (!matrix[y][pMaxY.X].Visible || matrix[y][pMaxY.X] == firstClick || matrix[y][pMaxY.X] == secondClick))
                {
                    if (checkLineX(pMinY.X, pMaxY.X, y))
                    {
                        return y;
                    }
                    y += type;
                }
            }

            return -1;
        }

        #endregion
    }
}
