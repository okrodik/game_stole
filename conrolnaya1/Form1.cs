using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace conrolnaya1
{
    public partial class Form1 : Form
    {
        PictureBox [,] pictureBoxes;
        Random rnd = new Random();

        int randX = 0;

        int x1 = 0, y1 = 0;
        int x2 = 0, y2 = 0;

        int PervuiHod1 = 0, PervuiHod2 = 0;

        public Form1()
        {
            InitializeComponent();
            createPole();
        }

        public void createPole()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1200, 1000);

            pictureBoxes = new PictureBox[10, 10];

            //pictureBoxes[4, 1, 1] = "3";
            //pictureBoxes[10, 0, 1] = "6";
            //pictureBoxes[8, 2, 1] = "15";
            //pictureBoxes[8, 9, 1] = "19";
            //pictureBoxes[3, 6, 1] = "21";
            //pictureBoxes[6, 7, 1] = "49"; 
            //pictureBoxes[2, 7, 1] = "15";
            //pictureBoxes[9, 4, 1] = "21";
            //pictureBoxes[7, 9, 1] = "88";
            //pictureBoxes[8, 6, 1] = "49";
            //pictureBoxes[5, 7, 1] = "33";
            //pictureBoxes[2,8, 1] = "80";

            //ТУТ можно добавить пуски на змейкх и поднятие на летницах

            int x = 1;

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    PictureBox pictureBox = new PictureBox();

                    int nomer;
                    if (row % 2 == 0)
                    {
                        nomer = (9 - row) * 10 + (9 - col) + 1;
                    }
                    else
                    {
                        nomer = (9 - row) * 10 + col + 1;
                    }

                    pictureBox.Name = $"pictureBox_{row}_{col}";

                    Label label = new Label();
                    label.Text = nomer.ToString();
                    label.Size = new Size(50, 50);
                    label.Location = new Point(100 + col * 70, 100 + row * 70);

                    Controls.Add(label);

                    pictureBox.Size = new Size(50, 50);

                    int posX = 100 + col * 70;
                    int posY = 100 + row * 70;

                    pictureBox.Location = new Point(posX, posY);

                    pictureBox.Visible = true;

                    Controls.Add(pictureBox);

                    pictureBoxes[row, col] = pictureBox;
                }
            }

            pictureBox2.Size = new Size(680, 680);
            pictureBox2.Location = new Point(100, 100);
            pictureBox2.SendToBack();

            button1.BackColor = Color.Blue;
            button2.BackColor = Color.Red;

            button1.Text = "Player1";
            button2.Text = "Player2";

            button2.Enabled = false;

            button1.Location = pictureBoxes[9, 0].Location; 
            button2.Location = pictureBoxes[9, 0].Location;

            button1.Size = new Size(40, 40);
            button2.Size = new Size(40, 40);

            viborKtoNashinaet();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;

            await animation();

            int x = randInt();

            Dvizenie1(x);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;

            await animation();

            int x = randInt();

            Dvizenie2(x);
        }    



        private async Task animation()
        {
            for (int j = 0; j < 10; j++)
            {
                int qwer = rnd.Next(1, 7);

                switch (qwer)
                {
                    case 1: pictureBox1.Image = Properties.Resources._1; break;
                    case 2: pictureBox1.Image = Properties.Resources._2; break;
                    case 3: pictureBox1.Image = Properties.Resources._3; break;
                    case 4: pictureBox1.Image = Properties.Resources._4; break;
                    case 5: pictureBox1.Image = Properties.Resources._5; break;
                    case 6: pictureBox1.Image = Properties.Resources._6; break;

                }
                await Task.Delay(200);
            }

        }

        private async void Dvizenie1(int step)
        {
            int Postion = y1 * 10 + x1 + 1;
            int Ostalos = 100 - Postion;

            if (step > Ostalos)
            {
                MessageBox.Show("Остаётесь на месте.");
                return;
            }

            for (int i = 0; i < step; i++)
            {
                if (y1 % 2 == 0)
                {
                    x1++;
                    if (x1 >= 10)
                    {
                        x1 = 9; 
                        y1++;    
                    }
                }
                else
                {
                    x1--;
                    if (x1 < 0)
                    {
                        x1 = 0;  
                        y1++;   
                    }
                }

                if (y1 >= 10)
                {
                    y1 = 9;
                    x1 = 9;
                    break;
                }

                int posX = 100 + x1 * 70;
                int posY = 100 + (9 - y1) * 70;
                button1.Location = new Point(posX, posY);

                await Task.Delay(300);


                if (x1 == 9 && y1 == 9)
                {
                    MessageBox.Show("Победил игрок 1");
                    ResetGame();
                }
            }

            if (x2 == x1 && y2 == y1)
            {
                MessageBox.Show("Игрок 1 попадает на клетку игрока 2! Один шаг назад.");
                await Task.Delay(500);

                if (y1 % 2 == 0)
                {
                    x1--;
                    if (x1 < 0)
                    {
                        x1 = 0;
                        y1--;
                    }
                }
                else
                {
                    x1++;
                    if (x1 >= 10)
                    {
                        x1 = 9;
                        y1--;
                    }
                }

                if (y1 < 0)
                {
                    y1 = 0;
                    x1 = 0;
                }

                int newPosX = 100 + x1 * 70;
                int newPosY = 100 + (9 - y1) * 70;

                button1.Location = new Point(newPosX, newPosY);
            }

            AddGameEvent("Player 1", step);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void ResetGame()
        {
            x2 = 0;
            x1 = 0;
            y1 = 0;
            y2 = 0;

            button1.Location = pictureBoxes[9, 0].Location;
            button2.Location = pictureBoxes[9, 0].Location;

            PervuiHod1 = 0;
            PervuiHod2 = 0;

            button1.Enabled = true;
            button2.Enabled = false;
        }

        private async void Dvizenie2(int step)
        {
            int Postion = y2 * 10 + x2 + 1;
            int Ostalos = 100 - Postion;

            if (step > Ostalos)
            {
                MessageBox.Show("Остаётесь на месте.");
                return;
            }

            for (int i = 0; i < step; i++)
            {
                if (y2 % 2 == 0)
                {
                    x2++;
                    if (x2 >= 10)
                    {
                        x2 = 9; 
                        y2++;   
                    }
                }
                else
                {
                    x2--;
                    if (x2 < 0)
                    {
                        x2 = 0;  
                        y2++; 
                    }
                }

                if (y2 >= 10)
                {
                    y2 = 9;
                    x2 = 9;
                    break;
                }

                int posX = 100 + x2 * 70;
                int posY = 100 + (9 - y2) * 70;
                button2.Location = new Point(posX, posY);

                await Task.Delay(300);

                if (x2 == 9 && y2 == 9)
                {
                    MessageBox.Show("Победил игрок 2");
                    ResetGame();
                }

            }


            dataGridView1.Rows.Add(step);

            if (x2 == x1 && y2 == y1)
            {
                MessageBox.Show("Игрок 2 попадает на клетку игрока 1! Один шаг назад.");
                await Task.Delay(500);

                if (y2 % 2 == 0)
                {
                    x2--;
                    if (x2 < 0)
                    {
                        x2 = 0; 
                        y2--;   
                    }
                }
                else
                {
                    x2++;
                    if (x2 >= 10)
                    {
                        x2 = 9;  
                        y2--;   
                    }
                }

                if (y2 < 0)
                {
                    y2 = 0;
                    x2 = 0;
                }

                int newPosX = 100 + x2 * 70;
                int newPosY = 100 + (9 - y2) * 70;

                button2.Location = new Point(newPosX, newPosY);
            }

            AddGameEvent("Player 2", step);
        }

        private void AddGameEvent(string kto, int x)
        {
            dataGridView1.Rows.Add( kto, x);
        }


        private async void viborKtoNashinaet() 
        {
            MessageBox.Show("Определяем кто первый ходит");
            MessageBox.Show("Бросает первый человек, нажми ок для продолжения");

            await animation();
            int xplayer = randInt();

            MessageBox.Show("Ваш результат: " + xplayer.ToString());

            MessageBox.Show("Бросает второй человек, нажми ок для продолжения");

            await animation();
            int yplayer = randInt();

            MessageBox.Show("Ваш результат: " + yplayer.ToString());

            if (xplayer > yplayer )
            {
                MessageBox.Show("Первый человек ходит первым. Результаты бросков. Первый игрок: " + xplayer.ToString() + ". Второй игрок:" + yplayer.ToString());
            }

            else if (xplayer < yplayer)
            {
                MessageBox.Show("Второй человек ходит первым. Результаты бросков. Первый игрок: " + xplayer.ToString() + ". Второй игрок:" + yplayer.ToString());
            }

            else
            {
                MessageBox.Show("Ничья! Первый человек ходит первым по умолчанию");
            }
        }

        private int randInt()
        {
            randX = rnd.Next(1, 7);

            switch (randX)
            {
                case 1: pictureBox1.Image = Properties.Resources._1; break;
                case 2: pictureBox1.Image = Properties.Resources._2; break;
                case 3: pictureBox1.Image = Properties.Resources._3; break;
                case 4: pictureBox1.Image = Properties.Resources._4; break;
                case 5: pictureBox1.Image = Properties.Resources._5; break;
                case 6: pictureBox1.Image = Properties.Resources._6; break;
            }

            return randX;
        }
    }
}
