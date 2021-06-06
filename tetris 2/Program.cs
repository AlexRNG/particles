using System;
using System.Collections.Generic;

namespace tetris_2 {
    class Program {
        static void Main(string[] args) {
            List<int> test = new List<int>() {111111, 1111111111, 1111111111, 1111111111, 1111111111 };
            Map gameMap = new Map(22);
            
            gameMap.AddShape(test);

            gameMap.DrawMap();

            
        }
    }
}
