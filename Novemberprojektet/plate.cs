using Raylib_cs;
using System;

namespace Novemberprojektet
{
    public class plate
    {
        
        public int x;
        public int y;
        public int width=100;
        public int height=20;
        private Random generator = new Random();
        

        public plate() 
        {
            x = generator.Next (50,750);
            y = generator.Next (0,10);
        }
        

        
    }
}
