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
            Raylib.SetExitKey(KeyboardKey.KEY_KP_SUBTRACT);
            Variabler v = new Variabler();
            RestartGame(v);

            while (!Raylib.WindowShouldClose())
            {

                Raylib.BeginDrawing();
                Raylib.ClearBackground(v.background);
                if (v.state == "Menu" || v.state == "Scores")
                {
                    v.Menu();
                    if (v.reset)
                    {
                        RestartGame(v);
                        v.reset = false;
                    }
                }
                if (v.state == "game")
                {
                    v.Logic();
                    v.Draw();
                }
                if (v.state == "gameover")
                {

                    v.GameOver();
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_R))
                    {
                        RestartGame(v);
                    }
                }
                Raylib.EndDrawing();
            }
            SaveScores(v);
        }

        static List<plate> StartGame(List<plate> plates)
        {
            plates.Clear();
            for (int i = 0; i < 10; i++)
            {
                plate p = new plate();
                p.rect.y = i * 80;
                plates.Add(p);
            }
            plate p1 = new plate() { rect = new Rectangle(0, 850, 800, 50) };
            plates.Add(p1);
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
                try
                {
                    string[] contents = File.ReadAllLines(@"Saves/High-Scores");
                    scores = new List<string>(contents);
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine("Failed To load highscores");
                    throw;
                }
            }
            foreach (var item in scores)
            {
                System.Console.WriteLine(item);
            }
            return scores;
        }

        static void SaveScores(Variabler v)
        {
            string newscore = v.score + "";
            v.scores.Add(newscore);
            v.scores.Sort();
            v.scores.Reverse();
            int length = v.scores.Count;
            if (length > 5)
            {
                v.scores.RemoveAt(length - 1);
            }
            try
            {
                File.WriteAllLines(@"Saves/High-Scores", v.scores);
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Could not save highscores, Sorry");
                throw;
            }
        }
        static Variabler RestartGame(Variabler v)
        {
            SaveScores(v);
            v.score = 0;
            v.timer = 0;
            v.difficulty = 0;
            v.speed = 2;
            v.state = "Menu";
            v.plateGreen = new Color(0, 207, 21, 255);
            v.background = new Color(236, 250, 235, 255);
            v.scores = LoadScores(v.scores);
            v.plates = StartGame(v.plates);
            v.player.rect.y = 360;
            v.player.rect.x = 460;

            return v;
        }
    }

}
