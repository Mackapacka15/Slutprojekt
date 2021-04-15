using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

//Skrev fel när jag skapade Sulutionen därför heter den Novemberprojektet istället för Slutprojekt
namespace Novemberprojektet
{
    class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(800, 900, "Doodle Jump");
            Raylib.SetTargetFPS(60);
            int height = Raylib.GetScreenHeight();
            int width = Raylib.GetScreenWidth();
            int xplayerpos = 20;
            int yplayerpos = height - 100;
            List<Rectangle> plates = new List<Rectangle>();
            Rectangle ground = new Rectangle(0, height - 50, width, 50);
            Rectangle plate1 = new Rectangle(0, height - 150, 100, 50);
            plates.Add(ground);
            plates.Add(plate1);
            while (!Raylib.WindowShouldClose())
            {
                Rectangle player = new Rectangle(xplayerpos, yplayerpos, 20, 20);



                if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && xplayerpos >= 0)
                {
                    xplayerpos -= 10;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && xplayerpos <= width - 20)
                {
                    xplayerpos += 10;
                }

                yplayerpos = Movement(yplayerpos, plates, player);

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.GREEN);
                for (int i = 0; i < plates.Count; i++)
                {
                    Raylib.DrawRectangleRec(plates[i], Color.BROWN);
                }
                Raylib.DrawRectangleRec(player, Color.RED);
                Raylib.EndDrawing();


            }
        }
        static int Movement(int ypos, List<Rectangle> plates, Rectangle player)
        {

            if (CheckCollision(plates, player))
            {
                ypos = Jump(ypos);
            }
            else
            {
                ypos++;
            }

            return ypos;

        }
        static int Jump(int ypos)
        {
            ypos -= 200;

            return ypos;
        }
        static bool CheckCollision(List<Rectangle> plates, Rectangle player)
        {
            bool collision = false;
            for (int i = 0; i < plates.Count; i++)
            {
                if (!collision)
                {
                    collision = Raylib.CheckCollisionRecs(plates[i], player);
                }
            }
            return collision;
        }
    }

}
