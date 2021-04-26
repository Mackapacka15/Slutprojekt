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
            int yspeed = -1;
            int nwerec=60;
            int score=0;
            string state="game";
            List<Rectangle> plates = new List<Rectangle>();
            Rectangle ground = new Rectangle(0, height - 50, width, 50);
            Rectangle plate1 = new Rectangle(50, height - 300, 100, 20);
            Rectangle plate2 = new Rectangle(200, height - 600, 100, 20);
            Rectangle player = new Rectangle(xplayerpos, yplayerpos, 40, 40);
            plates.Add(ground);
            plates.Add(plate1);
            plates.Add(plate2);

            while (!Raylib.WindowShouldClose())
            {   
                plates = MovementPlates(plates); 

                if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && player.x >= 20)
                {
                    player.x -= 20;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && player.x <= width - 50)
                {
                    player.x += 20;
                }
                
                player.y+=yspeed;
                (int yspeed, Rectangle newplayer) result= Movement(yspeed, plates, player); 
                yspeed = result.yspeed;
                player = result.newplayer;
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.GREEN);
                Raylib.DrawText("score: " + score,0,0,20,Color.BLUE);
                if (state=="game")
                {
                     for (int i = 0; i < plates.Count; i++)
                    {  
                        Raylib.DrawRectangleRec(plates[i], Color.BROWN);
                    }

                    Raylib.DrawRectangleRec(player, Color.RED);
                    

                    if (nwerec == 80)
                    {
                        plates.Add(NewRectangle());
                        nwerec=0;
                        score++;
                    }
                    else
                    {
                        nwerec++;
                        
                        
                    }
                    if (player.y>height+20)
                    {
                        state="gameover";   
                    }
                }
               if (state=="gameover")
               {
                   Raylib.DrawText("Game Over",width/2-170,height/2-50,50,Color.BLUE);
               }
                Raylib.EndDrawing();
            }
        }
        static (int, Rectangle) Movement(int speed, List<Rectangle> plates, Rectangle player)
        {
            (bool collided,Rectangle newplayer) result=CheckCollision( plates, player);
            if (result.collided && speed > 0)
            {
                speed = -30; 
            }
            else
            {
                speed++;
            }

            return (speed, player);
        }

        static (bool,Rectangle) CheckCollision(List<Rectangle> plates, Rectangle player)
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
                        //Igenom platotrna
                    }
                }
            }
            return (false, player) ;
        }
        static List<Rectangle> MovementPlates(List<Rectangle> plates) {

        List<int> remove =new List<int>();
            for (int i = 0; i < plates.Count; i++)
            {
                Rectangle tmp = plates[i];
                tmp.y+=2;
                plates[i] = tmp; 
            }
            return plates;
        }
        static Rectangle NewRectangle(){

            Random generator = new Random();
            int x= generator.Next(50, 650);
            int y= generator.Next(0,10);
            Rectangle r= new Rectangle (x, y, 100, 20);
            //Rectangle rec= new Rectangle (generator.Next(50, 750), generator.Next(0,10), 100,20);
            return r;
        }
    }

}
