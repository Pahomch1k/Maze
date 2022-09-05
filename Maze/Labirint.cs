﻿using System;
using System.Windows.Forms;
using System.Drawing;

namespace Maze
{
    class Labirint
    {
        public int height; // высота лабиринта (количество строк)
        public int width; // ширина лабиринта (количество столбцов в каждой строке)

        public MazeObject[,] maze;
        public PictureBox[,] images;

        public static Random r = new Random();
        public Form parent;

        public Labirint(Form parent, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.parent = parent;

            maze = new MazeObject[height, width];
            images = new PictureBox[height, width];

            Generate();
        }


        private void Generate()
        {
            int smileX = 0;
            int smileY = 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    MazeObject.MazeObjectType current = MazeObject.MazeObjectType.HALL;

                    // в 1 случае из 5 - ставим стену
                    if (r.Next(5) == 0) current = MazeObject.MazeObjectType.WALL;

                    // в 1 случае из 250 - кладём денежку
                    if (r.Next(100) == 0) current = MazeObject.MazeObjectType.MEDAL;

                    // в 1 случае из 250 - размещаем врага
                    if (r.Next(100) == 0) current = MazeObject.MazeObjectType.ENEMY;

                    // стены по периметру обязательны
                    if (y == 0 || x == 0 || y == height - 1 | x == width - 1) current = MazeObject.MazeObjectType.WALL;

                    // наш персонажик
                    //if (x == smileX && y == smileY) current = MazeObject.MazeObjectType.CHAR;

                    // есть выход, и соседняя ячейка справа всегда свободна
                    if (x == smileX + 1 && y == smileY || x == width - 1 && y == height - 3) current = MazeObject.MazeObjectType.HALL;

                    maze[y, x] = new MazeObject(current);
                    images[y, x] = new PictureBox();
                    images[y, x].Location = new Point(x * maze[y, x].width, y * maze[y, x].height);
                    images[y, x].Parent = parent;
                    images[y, x].Width = maze[y, x].width;
                    images[y, x].Height = maze[y, x].height;
                    images[y, x].BackgroundImage = maze[y, x].texture;
                    images[y, x].Visible = false;
                }
            }
        }

        public void Show()
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    images[y, x].Visible = true;
        }

        public bool CheckWall(int X, int Y)
        {  
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) 
                    if (images[y, x].BackgroundImage == maze[y, x].images[1] && images[y, x].Location == new Point(Y,X)) return true;   
            return false;
        }

        public bool CheckSrats(int X, int Y)
        { 
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    if (images[y, x].BackgroundImage == maze[y, x].images[2] && images[y, x].Location == new Point(Y, X))
                    {
                        images[y, x].BackgroundImage = maze[y, x].images[0];
                        return true;
                    }
            return false;
        }

        public bool CheckMonsters(int X, int Y)
        { 
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    if (images[y, x].BackgroundImage == maze[y, x].images[3] && images[y, x].Location == new Point(Y, X))
                    {
                        images[y, x].BackgroundImage = maze[y, x].images[0];
                        return true;
                    }
            return false;
        }
    }
}
