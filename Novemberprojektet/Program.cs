using System;
using Raylib_cs;
using System.Numerics;

//Skrev fel när jag skapade Sulutionen därför heter den Novemberprojektet istället för Slutprojekt
namespace Novemberprojektet
{
    class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(700, 700, "Asteroids");
            Raylib.SetTargetFPS(60);
            int height = Raylib.GetScreenHeight();
            int width = Raylib.GetScreenWidth();





            while (!Raylib.WindowShouldClose())
            {
                Vector2 Mousepos = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                Raylib.DrawRectangle(1, 1, 10, 10, Color.WHITE);

                Raylib.EndDrawing();


            }
        }
    }
}
