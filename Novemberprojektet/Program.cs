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
            int score = 0;
            int timer = 0;
            int difficulty = 0;
            int speed = 2;
            int maxtimer = 60;
            string state = "game";
            List<plate> plates = new List<plate>();
            Player player = new Player();
            plate p1 = new plate() { rect = new Rectangle(0, 850, 800, 50) };
            plates.Add(p1);

            for (int i = 0; i < 10; i++)
            {
                plate p = new plate();
                p.rect.y = i * 70;
                plates.Add(p);
            }

            while (!Raylib.WindowShouldClose())
            {

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.GREEN);
                Raylib.DrawText("score: " + score, 0, 0, 20, Color.BLUE);
                if (state == "game")
                {
                    for (int i = 0; i < plates.Count; i++)
                    {
                        plates[i].Movement();
                        plates[i].Draw();
                    }
                    player.Draw();
                    if (player.rect.y > 920)
                    {
                        state = "gameover";
                    }
                    if (timer == maxtimer)
                    {
                        plates.Add(NewRectangle(speed));
                        timer = 0;
                        score++;
                        difficulty++;
                    }
                    else
                    {
                        timer++;
                    }
                    if (difficulty >= 20)
                    {
                        difficulty = 0;
                        maxtimer = 60 / speed;
                        speed++;
                        for (int i = 0; i < plates.Count; i++)
                        {
                            plates[i].speed = speed;
                        }
                    }
                    player.Movement(plates);

                }
                if (state == "gameover")
                {
                    Raylib.DrawText("Game Over", 230, 400, 50, Color.BLUE);
                }
                Raylib.EndDrawing();
                plates = ClearList(plates);
            }
        }
        static plate NewRectangle(int speed)
        {
            plate r = new plate();
            r.speed = speed;
            return r;
        }
        static List<plate> ClearList(List<plate> plates)
        {
            List<plate> remove = new List<plate>();

            foreach (plate item in plates)
            {
                if (item.rect.y >= 1000)
                {
                    remove.Add(item);
                }
            }
            foreach (plate item in remove)
            {
                plates.Remove(item);
                System.Console.WriteLine(item);
            }

            return plates;
        }
    }

}
