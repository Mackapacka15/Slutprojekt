using System.Collections.Generic;
using System;
using Raylib_cs;

namespace Novemberprojektet
{
    public class Player
    {
        public int speed = -1;
        public Rectangle rect = new Rectangle(460, 360, 40, 40);

        public Player()
        {

        }
        public void Movement(List<plate> plates)
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && rect.x >= 20)
            {
                rect.x -= 20;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && rect.x <= 750)
            {
                rect.x += 20;
            }
            for (int i = 0; i < plates.Count; i++)
            {
                bool collision = Raylib.CheckCollisionRecs(plates[i].rect, rect);
                if (!collision)
                {
                    speed = -30;
                }
                else
                {
                    speed++;
                }
            }
            rect.y += speed;
        }

        public void Draw()
        {
            Raylib.DrawRectangleRec(rect, Color.RED);
        }
    }

}
