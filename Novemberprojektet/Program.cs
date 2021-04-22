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
            int xplayerpos = width/2 -20;
            int yplayerpos = height - 100;
            int yspeed = 0;
            List<Rectangle> plates = new List<Rectangle>();
            Rectangle ground = new Rectangle(0, height - 50, width, 50);
            Rectangle plate1 = new Rectangle(0, height - 150, 100, 20);
            Rectangle player = new Rectangle(xplayerpos, yplayerpos, 40, 40);
            plates.Add(ground);
            plates.Add(plate1);
            while (!Raylib.WindowShouldClose())
            {
                


                if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && xplayerpos >= 20)
                {
                    player.x -= 20;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && xplayerpos <= width - 50)
                {
                    player.x += 20;
                }

                player.y += yspeed;
                (int yspeed, Rectangle newplayer) result= Movement(yspeed, plates, player); 
                yspeed = result.yspeed;
                player= result.newplayer;
                //plates = MovementPlates(plates); 

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
        static (int, Rectangle) Movement(int speed, List<Rectangle> plates, Rectangle player)
        {
            (bool collided,Rectangle newplayer) result=CheckCollision( plates, player);
            if (result.collided)
            {
                speed = -30; 
            }
            else
            {
                speed++;
            }

            return (speed, player);
        }

        static ( bool,Rectangle) CheckCollision(List<Rectangle> plates, Rectangle player)
        {
            bool collision = false;
            for (int i = 0; i < plates.Count; i++)
            {
                if (!collision)
                {
                    collision = Raylib.CheckCollisionRecs(plates[i], player);
                    if (collision) {
                        Rectangle overlap = Raylib.GetCollisionRec(plates[i],player);
                        player.y-=overlap.height;
                        return (true, player);
                    }
                }
            }
            return (false, player) ;
        }
        static List<Rectangle> MovementPlates(List<Rectangle> plates) {

            for (int i = 0; i < plates.Count; i++)
            {
                Rectangle tmp = plates[i];
                tmp.y+=1;
                plates[i] = tmp; 
            }
//Igenom platotrna
            return plates;
        }
    }

}
