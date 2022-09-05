using System;
using System.Drawing;

namespace Maze
{
    class MazeObject
    {
        public enum MazeObjectType { HALL, WALL, MEDAL, ENEMY};

        public Bitmap[] images = {new Bitmap("hall.png"),
            new Bitmap("wall.png"),
            new Bitmap("medal.png"),
            new Bitmap("enemy.png")};

        public MazeObjectType type;
        public int width;
        public int height;
        public Image texture;

        public MazeObject(MazeObjectType type)
        {
            this.type = type;
            width = 16;
            height = 16;
            texture = images[(int)type];
        }

    }
}
