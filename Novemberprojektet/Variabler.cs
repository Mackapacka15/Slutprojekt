using System;
using Raylib_cs;
using System.Collections.Generic;

namespace Novemberprojektet
{
    public class Variabler
    {
        public int score = 0;
        public int timer = 0;
        public int difficulty = 0;
        public int speed = 2;
        public string state = "game";
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

        

    }
}
