using System;
using System.Media;
using System.Xml;
using System.Threading;
using System.Windows.Forms;
using System.IO; 
using System.Drawing;


namespace Maze
{
    
    public partial class Form1 : Form
    {
        Labirint l;
        SoundPlayer soundAudio;
        
        int count = 0;
        int xp = 100;

        public Form1()
        { 
            InitializeComponent();
            Options();
            StartGame();
            var path = System.IO.Path.GetFullPath(@"img\player.png");



            pictureBox1.BackgroundImage = Image.FromFile("player.png");
            pictureBox1.Location = new Point(16, 32);
            
        }
        public void Options()
        {
            Text = "Maze";

            BackColor = Color.FromArgb(255, 92, 118, 137);

            int sizeX = 40;
            int sizeY = 20;

            Width = sizeX * 16 + 16;
            Height = sizeY * 16 + 40;
            StartPosition = FormStartPosition.CenterScreen;
            
        } 

        public void StartGame()
        {
            l = new Labirint(this, 40, 20); 
            l.Show();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int x = pictureBox1.Top;
            int y = pictureBox1.Left;
              
            switch (e.KeyChar)
            { 
                case 'w': 
                    if (l.CheckWall(x - 16, y) == false) pictureBox1.Top -= 16;
                    Points(x - 16, y);
                    Monsters(x - 16, y);
                    break; 
                case 'd': 
                    if (l.CheckWall(x, y + 16) == false) pictureBox1.Left += 16;
                    Points(x, y + 16);
                    Monsters(x, y + 16);
                    break;
                case 's': 
                    if (l.CheckWall(x + 16, y) == false) pictureBox1.Top += 16;
                    Points(x + 16, y);
                    Monsters(x + 16, y);
                    break;
                case 'a': 
                    if (l.CheckWall(x, y - 16) == false) pictureBox1.Left -= 16;
                    Points(x, y - 16);
                    Monsters(x, y - 16);
                    break;
                default: break;
            } 

            if (y == 608 && x == 272) MessageBox.Show("Win"); 
        }

        private void Points(int x, int y)
        {
            if (l.CheckSrats(x, y) == true)
            {
                count++;
                label1.Text = "Очков: " + count;
                soundAudio = new SoundPlayer("zvukEdit.wav");
                soundAudio.Play();

                if (count == 5) MessageBox.Show("Win"); 
            } 
        }

        private void Monsters(int x, int y)
        {
            if (l.CheckMonsters(x, y) == true)
            {
                xp -= 50;
                label2.Text = "XP: " + xp + "%";
                soundAudio = new SoundPlayer("zvukExit.wav");
                soundAudio.Play();

                if (xp == 0)
                {
                    MessageBox.Show("Game Over");
                    soundAudio = new SoundPlayer("ToPdf.wav");
                    soundAudio.Play(); 
                }
            }
        }
    }
}
