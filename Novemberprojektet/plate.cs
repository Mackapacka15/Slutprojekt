using Raylib_cs;
using System;

namespace Novemberprojektet
{
    public class plate
    {

        public Rectangle rect = new Rectangle(0, 0, 100, 20);
        private Random generator = new Random();


        public plate()
        {
            rect.x = generator.Next(50, 750);
            rect.y = generator.Next(0, 10);
        }

        public void Movement()
        {
            rect.y += 2;
        }

        public void Draw()
        {
            Raylib.DrawRectangleRec(rect, Color.BROWN);
        }


    }
}
