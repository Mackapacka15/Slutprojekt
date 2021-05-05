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
            Color plateGreen = new Color(0, 207, 21, 255);
            Color background = new Color(236, 250, 235, 255);
            List<plate> plates = new List<plate>();
            Player player = new Player();
            plate p1 = new plate() { rect = new Rectangle(0, 850, 800, 50) };
            plates.Add(p1);
            List<string> scores = new List<string>();
            scores = LoadScores(scores);
            plates = StartGame(plates);

            while (!Raylib.WindowShouldClose())
            {

                Raylib.BeginDrawing();
                Raylib.ClearBackground(background);
                if (state == "game")
                {
                    Raylib.DrawText("score: " + score, 10, 10, 20, Color.BLUE);
                    for (int i = 0; i < plates.Count; i++)
                    {
                        plates[i].Movement();
                        plates[i].Draw(plateGreen);
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
                    DisplayScores(scores, score);
                }
                Raylib.EndDrawing();
                plates = ClearList(plates, speed);
            }
            SaveScores(scores, score);
        }

        static List<plate> StartGame(List<plate> plates)
        {
            for (int i = 0; i < 10; i++)
            {
                plate p = new plate();
                p.rect.y = i * 80;
                plates.Add(p);
            }
            return plates;
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
                try
                {
                    Directory.CreateDirectory(@"Saves");
                    File.Create(@"Saves/High-Scores");
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            else
            {
                string[] contents = File.ReadAllLines(@"Saves/High-Scores");
                scores = new List<string>(contents);
            }
            if (scores.Count <= 4)
            {
                System.Console.WriteLine("För många");
                scores.RemoveRange(2, scores.Count - 3);
            }

            return scores;
        }
        static void SaveScores(List<string> scores, int score)
        {
            string newscore = score + "";
            scores.Add(newscore);
            scores.Sort();
            scores.Reverse();
            int length = scores.Count;
            if (length > 3)
            {
                scores.RemoveAt(length - 1);
            }
            try
            {
                File.WriteAllLines(@"Saves/High-Scores", scores);
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Could not save highscores, Sorry");
                throw;
            }
        }
        static void DisplayScores(List<string> scores, int score)
        {
            Raylib.DrawText("Game Over", 230, 400, 50, Color.BLUE);
            Raylib.DrawText("Latest Run: " + score, 230, 480, 20, Color.BLUE);
            Raylib.DrawText("Best Scores:", 230, 450, 20, Color.BLUE);

            for (int i = 0; i < scores.Count; i++)
            {
                if (i == 0)
                {

                    Raylib.DrawText((i + 1 + "   :") + scores[i], 230, 20 * i + 500, 20, Color.BLUE);
                }
                else
                {
                    Raylib.DrawText((i + 1 + "  :") + scores[i], 230, 20 * i + 500, 20, Color.BLUE);
                }

            }
        }
    }

}
