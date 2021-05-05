using System.Security.AccessControl;
using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;
//Behövs för att spara high score
using System.IO;

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
            string state = "game";
            List<plate> plates = new List<plate>();
            Player player = new Player();
            plate p1 = new plate() { rect = new Rectangle(0, 850, 800, 50) };
            plates.Add(p1);
            List<string> scores = new List<string>();
            scores = LoadScores(scores);

            for (int i = 0; i < 10; i++)
            {
                plate p = new plate();
                p.rect.y = i * 80;
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
                    if (timer == 60)
                    {
                        timer = 0;
                        difficulty++;
                        score++;
                    }
                    else
                    {
                        timer++;
                    }

                    if (difficulty >= 50)
                    {
                        difficulty = 0;
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
                    Savescores(scores, score);
                }
                Raylib.EndDrawing();
                plates = ClearList(plates, speed);

            }
        }
        static plate NewRectangle(int speed)
        {
            plate r = new plate();
            r.speed = speed;
            return r;
        }
        static List<plate> ClearList(List<plate> plates, int speed)
        {
            List<plate> remove = new List<plate>();
            foreach (plate item in plates)
            {
                if (item.rect.y >= 1000)
                {
                    remove.Add(item);
                }
            }
            for (int i = 0; i < remove.Count; i++)
            {
                plates.Add(NewRectangle(speed));
            }
            foreach (plate item in remove)
            {
                plates.Remove(item);
                System.Console.WriteLine(item);
            }

            return plates;
        }
        static List<string> LoadScores(List<string> scores)
        {

            if (!File.Exists(@"Saves/High-Scores"))
            {
                Directory.CreateDirectory(@"Saves");
                File.Create(@"Saves/High-Scores");
            }
            else
            {
                string[] contents2 = File.ReadAllLines(@"Saves/High-Scores");
                scores = new List<string>(contents2);
            }

            return scores;
        }
        static void Savescores(List<string> scores, int score)
        {

            string newscore = score + "";
            scores.Add(newscore);
            scores.Sort();
            scores.Reverse();

            Raylib.DrawText(("Latest Run: ") + newscore, 230, 480, 20, Color.BLUE);
            Raylib.DrawText("Best Scores:", 230, 450, 20, Color.BLUE);
            int length = scores.Count;
            if (length > 3)
            {
                scores.RemoveAt(length - 1);
            }
            for (int i = 0; i < scores.Count; i++)
            {
                if (i <= 3)
                {
                    Raylib.DrawText((i + 1 + " :") + scores[i], 230, 20 * i + 500, 20, Color.BLUE);
                }
            }
            File.WriteAllLines(@"Saves/High-Scores", scores);
        }
    }

}
