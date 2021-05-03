using System.Collections.Generic;
using System;
using Raylib_cs;

namespace Novemberprojektet
{
    public class Player
    {
        public int speed;
        public Rectangle rect = new Rectangle(460, 360, 40, 40);
        public void Movement(List<plate> plates)
        {
            bool collision = false;
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
                if (!collision)
                {
                    collision = Raylib.CheckCollisionRecs(plates[i].rect, rect);
                }
            }
            if (collision && speed > 0)
            {
                speed = -20;
            }
            else
            {
                speed += 1;
            }
            rect.y += speed;

        }

        public void Draw()
        {
            Raylib.DrawRectangleRec(rect, Color.RED);
        }
    }

}
