using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartFive
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        //定义全局变量
        private bool flag = true, begin_flag = false, again_exit = true;
        private int[][] position = new int[15][];
        private int X = 0, Y = 0;
        
        /// <summary>
        /// 初始化应用
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width - 10;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight - this.Height - 80;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            PaintBoard();
            Initialize();
        }

        private void Menu_Ground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try { this.DragMove(); }
            catch { }
        }

        /// <summary>
        /// 初始化棋局
        /// </summary>
        private void Initialize()
        {
            for (int i = 0; i < 15; i++)
            {
                position[i] = new int[15];
                for (int j = 0; j < 15; j++)
                {
                    position[i][j] = 0;
                }
            }
        }

        /// <summary>
        /// 绘制棋盘
        /// </summary>
        private void PaintBoard()
        {
            Play_Ground.Background = new SolidColorBrush(Color.FromRgb(255, 147, 15));
            for (int i = 0; i < 15; i++)
            {
                Line xLine = new Line();
                Line yLine = new Line();
                xLine.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                yLine.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                xLine.X1 = 15;
                xLine.X2 = 435;
                yLine.Y1 = 15;
                yLine.Y2 = 435;
                xLine.Y1 = i * 30 + 15;
                xLine.Y2 = i * 30 + 15;
                yLine.X1 = i * 30 + 15;
                yLine.X2 = i * 30 + 15;
                Play_Ground.Children.Add(xLine);
                Play_Ground.Children.Add(yLine);
            }

            Ellipse sky_point = new Ellipse();
            sky_point.Height = 6;
            sky_point.Width = 6;
            sky_point.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Canvas.SetLeft(sky_point, 222);
            Canvas.SetTop(sky_point, 222);
            Play_Ground.Children.Add(sky_point);
            Ellipse star_point1 = new Ellipse();
            star_point1.Height = 6;
            star_point1.Width = 6;
            star_point1.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Canvas.SetLeft(star_point1, 102);
            Canvas.SetTop(star_point1, 102);
            Play_Ground.Children.Add(star_point1);
            Ellipse star_point2 = new Ellipse();
            star_point2.Height = 6;
            star_point2.Width = 6;
            star_point2.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Canvas.SetLeft(star_point2, 342);
            Canvas.SetTop(star_point2, 342);
            Play_Ground.Children.Add(star_point2);
            Ellipse star_point3 = new Ellipse();
            star_point3.Height = 6;
            star_point3.Width = 6;
            star_point3.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Canvas.SetLeft(star_point3, 342);
            Canvas.SetTop(star_point3, 102);
            Play_Ground.Children.Add(star_point3); 
            Ellipse star_point4 = new Ellipse();
            star_point4.Height = 6;
            star_point4.Width = 6;
            star_point4.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Canvas.SetLeft(star_point4, 102);
            Canvas.SetTop(star_point4, 342);
            Play_Ground.Children.Add(star_point4);
        }

        /// <summary>
        /// 重新绘制棋盘=新开一局
        /// </summary>
        private void RePaintBoard()
        {
            Play_Ground.Children.Clear();
            PaintBoard();
            Initialize();
        }

        /// <summary>
        /// 点击棋盘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Play_Ground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (begin_flag == false)
            {
                MessageBox.Show("还没开始呢");
                return;
            }
            //else if (again_exit == false)
            //{ }
            else if (begin_flag == true || again_exit == true)
            {
                Ellipse chess = new Ellipse();
                Point point = e.GetPosition(Play_Ground);
                if (point.X > 445 || point.Y > 445 || point.X < 5 || point.Y < 5)
                {
                    MessageBox.Show("表乱点");
                    return;
                }
                chess.Width = 20;
                chess.Height = 20;
                X = (int)(point.X - 15) / 30;
                double n = (point.X - 15) / 30 - Math.Truncate((point.X - 15) / 30);
                if (n > 0.5)
                {
                    X += 1;
                    Y = (int)(point.Y - 15) / 30;
                    n = (point.Y - 15) / 30 - Math.Truncate((point.Y - 15) / 30);
                    if (n > 0.5)
                       Y += 1;
                }
                else
                {
                    Y = (int)(point.Y - 15) / 30;
                    n = (point.Y - 15) / 30 - Math.Truncate((point.Y - 15) / 30);
                    if (n > 0.5)
                        Y += 1;
                }
                if (position[X][Y] != 0)
                {
                    MessageBox.Show("表乱点!");
                }
                else
                {
                    if (flag)
                    {
                        chess.Fill = Brushes.Black;
                        position[X][Y] = 1;
                        flag = false;
                        lbl_Notice.Content = "提示：请放白棋!";
                    }
                    else
                    {
                        chess.Fill = Brushes.White;
                        position[X][Y] = 2;
                        flag = true;
                        lbl_Notice.Content = "提示：请放黑棋!";
                    }
                    Canvas.SetLeft(chess, X * 30 + 5);
                    Canvas.SetTop(chess, Y * 30 + 5);
                    Play_Ground.Children.Add(chess);

                    if (JudgeWinner() > 0)
                    {
                        MessageBox.Show("黑方胜~白方自觉点");
                        begin_flag = false;
                    }
                    if (JudgeWinner() < 0)
                    {
                        MessageBox.Show("白方胜~黑方自觉点");
                        begin_flag = false;
                    }
                }
            }
        }

        private void mu_reStar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("是否重开一局?", "游戏提示", MessageBoxButton.YesNo);
            if (res.ToString() == MessageBoxResult.Yes.ToString())
            {
                RePaintBoard();
                mu_PiceStar_Click(sender, e);
            }
        }

        private void mu_back_Click(object sender, RoutedEventArgs e)
        {
            //需验证是否已开局
            if (Play_Ground.Children.Count <= 35)
            {
                return;
            }
            MessageBoxResult res = MessageBox.Show("是否悔棋?", "游戏提示", MessageBoxButton.YesNo);
            if (res.ToString() == MessageBoxResult.Yes.ToString())
            {
                Back_Step();
            }
        }

        /// <summary>
        /// 悔棋一步
        /// </summary>
        private void Back_Step()
        {
            Point pre = new Point();

            pre.X = Canvas.GetLeft(Play_Ground.Children[Play_Ground.Children.Count - 1]);
            pre.Y = Canvas.GetTop(Play_Ground.Children[Play_Ground.Children.Count - 1]);

            X = (int)(pre.X - 15) / 30;
            double n = (pre.X - 15) / 30 - Math.Truncate((pre.X - 15) / 30);
            if (n > 0.5)
            {
                X += 1;
                Y = (int)(pre.Y - 15) / 30;
                n = (pre.Y - 15) / 30 - Math.Truncate((pre.Y - 15) / 30);
                if (n > 0.5)
                    Y += 1;
            }
            else
            {
                Y = (int)(pre.Y - 15) / 30;
                n = (pre.Y - 15) / 30 - Math.Truncate((pre.Y - 15) / 30);
                if (n > 0.5)
                    Y += 1;
            }
            if (position[X][Y] == 1)
            {
                flag = true;
            }
            if (position[X][Y] == 2)
            {
                flag = false;
            }
            position[X][Y] = 0;
            Play_Ground.Children.Remove(Play_Ground.Children[Play_Ground.Children.Count - 1]);
        }

        /// <summary>
        /// 判断获胜者=大于0:黑方胜;小于0:白方胜
        /// </summary>
        /// <returns></returns>
        private int JudgeWinner()
        {
            int winner = 0;
            int i;
            for (i = 1; i <= 4 && Y + i >= 0 && Y + i < 15 && position[X][Y] == position[X][Y + i]; i++)
            {
                if (i == 4)
                {
                    if (flag == false)
                    {
                        winner = 1;
                    }
                    else
                    {
                        winner = -1;
                    }
                }
            }
            for (i = 1; i <= 4 && Y - i >= 0 && Y - i < 15 && position[X][Y] == position[X][Y - i]; i++)
            {
                if (i == 4)
                {
                    if (flag == false)
                    {
                        winner = 1;
                    }
                    else
                    {
                        winner = -1;
                    }
                }
            }
            for (i = 1; i <= 4 && X + i >= 0 && X + i < 15 && position[X][Y] == position[X + i][Y]; i++)
            {
                if (i == 4)
                {
                    if (flag == false)
                    {
                        winner = 1;
                    }
                    else
                    {
                        winner = -1;
                    }
                }
            }
            for (i = 1; i <= 4 && X - i >= 0 && X - i < 15 && position[X][Y] == position[X - i][Y]; i++)
            {
                if (i == 4)
                {
                    if (flag == false)
                    {
                        winner = 1;
                    }
                    else
                    {
                        winner = -1;
                    }
                }
            }
            for (i = 1; i <= 4 && X + i >= 0 && X + i < 15 && Y + i >= 0 && Y + i < 15 && position[X][Y] == position[X + i][Y + i]; i++)
            {
                if (i == 4)
                {
                    if (flag == false)
                    {
                        winner = 1;
                    }
                    else
                    {
                        winner = -1;
                    }
                }
            }
            for (i = 1; i <= 4 && X - i >= 0 && X - i < 15 && Y - i >= 0 && Y - i < 15 && position[X][Y] == position[X - i][Y - i]; i++)
            {
                if (i == 4)
                {
                    if (flag == false)
                    {
                        winner = 1;
                    }
                    else
                    {
                        winner = -1;
                    }
                }
            }
            for (i = 1; i <= 4 && X - i >= 0 && X - i < 15 && Y + i >= 0 && Y + i < 15 && position[X][Y] == position[X - i][Y + i]; i++)
            {
                if (i == 4)
                {
                    if (flag == false)
                    {
                        winner = 1;
                    }
                    else
                    {
                        winner = -1;
                    }
                }
            }
            for (i = 1; i <= 4 && X + i >= 0 && X + i < 15 && Y - i >= 0 && Y - i < 15 && position[X][Y] == position[X + i][Y - i]; i++)
            {
                if (i == 4)
                {
                    if (flag == false)
                    {
                        winner = 1;
                    }
                    else
                    {
                        winner = -1;
                    }
                }
            }
            return winner;
        }
        
        private void mu_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void mu_PiceStar_Click(object sender, RoutedEventArgs e)
        {
            begin_flag = true;
            if (flag == false)
                lbl_Notice.Content = "提示：请放白棋!";
            if (flag == true)
                lbl_Notice.Content = "提示：请放黑棋!";
        }
    }
}
