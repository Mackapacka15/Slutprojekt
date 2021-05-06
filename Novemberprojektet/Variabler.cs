using System.Numerics;
using System;
using Raylib_cs;
using System.Collections.Generic;

namespace Novemberprojektet
{
    public class Variabler
    {
        public bool reset = false;
        public Vector2 mousepos = Raylib.GetMousePosition();
        private Rectangle button1 = new Rectangle(200, 250, 400, 170);
        private Rectangle button2 = new Rectangle(200, 550, 400, 170);
        private Rectangle button3 = new Rectangle(200, 150, 400, 170);
        public int score = 0;
        public int timer = 0;
        public int difficulty = 0;
        public int speed = 2;
        public string state = "Menu";
        public List<string> scores = new List<string>();
        public Player player = new Player();
        public List<plate> plates = new List<plate>();
        public Color plateGreen = new Color(0, 207, 21, 255);
        public Color background = new Color(236, 250, 235, 255);

        public void Logic()
        {
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
            plates = ClearList(plates, speed);
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
            }

            return plates;
        }
        static plate NewRectangle(int speed)
        {
            plate r = new plate();
            r.speed = speed;
            return r;
        }
        public void Draw()
        {

            Raylib.DrawText("score: " + score, 10, 10, 20, Color.BLUE);
            for (int i = 0; i < plates.Count; i++)
            {
                plates[i].Movement();
                plates[i].Draw(plateGreen);
            }
            player.Draw();

        }

        public void Menu()
        {
            if (state == "Menu")
            {
                mousepos = Raylib.GetMousePosition();
                Raylib.DrawText("Welcome To Doodle Jump", 100, 150, 50, Color.BLUE);
                Raylib.DrawRectangleRec(button1, Color.DARKBLUE);
                Raylib.DrawRectangleRec(button2, Color.DARKBLUE);
                Raylib.DrawText("Play", 350, 300, 50, background);
                Raylib.DrawText("Highscore", 275, 600, 50, background);
                if (Raylib.CheckCollisionPointRec(mousepos, button1) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    state = "game";
                }
                if (Raylib.CheckCollisionPointRec(mousepos, button2) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    state = "Scores";
                }
            }

            if (state == "Scores")
            {
                DisplayScores();
            }

        }
        public void DisplayScores()
        {
            mousepos = Raylib.GetMousePosition();
            Raylib.DrawRectangleRec(button3, Color.DARKBLUE);
            Raylib.DrawText("Back", 350, 200, 50, background);
            if (Raylib.CheckCollisionPointRec(mousepos, button3) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                state = "Menu";
            }
            Raylib.DrawText("Highscores:", 250, 450, 40, Color.BLUE);

            for (int i = 0; i < scores.Count; i++)
            {
                if (i == 0)
                {

                    Raylib.DrawText((i + 1 + "   :") + scores[i], 250, 20 * i + 500, 30, Color.BLUE);
                }
                else
                {
                    Raylib.DrawText((i + 1 + "  :") + scores[i], 250, 20 * i + 500, 30, Color.BLUE);
                }

            }
        }
        public void GameOver()
        {
            mousepos = Raylib.GetMousePosition();
            Raylib.DrawRectangleRec(button3, Color.DARKBLUE);
            Raylib.DrawText("Menu", 350, 200, 50, background);
            if (Raylib.CheckCollisionPointRec(mousepos, button3) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                state = "Menu";
                reset = true;
            }
            Raylib.DrawText("Game Over", 230, 400, 50, Color.BLUE);
            Raylib.DrawText("Last Run: " + score, 230, 480, 20, Color.BLUE);
        }

    }
}
