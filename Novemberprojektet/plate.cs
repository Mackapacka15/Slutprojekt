using Raylib_cs;
using System;

namespace Novemberprojektet
{
    public class plate
    {

        public Rectangle rect = new Rectangle(0, 0, 100, 20);
        public Color color = new Color();
        public int speed = 2;



        public plate()
        {
            Random generator = new Random();
            rect.x = generator.Next(100, 700);
            rect.y = generator.Next(0, 10);
        }

        public void Movement()
        {
            rect.y += speed;
        }

        public void Draw(Color plateGreen)
        {
            Raylib.DrawRectangleRec(rect, plateGreen);
        }


    }
}
