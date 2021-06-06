using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace tetris_2 {
    class Map {
        // map is constructed using arrays for each row of the map
        public int[] lay1;
        public int[] lay2;
        public int[] lay3;
        public int[] lay4;
        public int[] lay5;
        public int[] lay6;
        public int[] lay7;
        public int[] lay8;
        public int[] lay9;
        public int[] lay10;
        public int[] lay11;
        public List<int[]> fullmap; // the whole map is represented using a list holding all of the layers
        public int Width;

        public Map(int width) {
            Width = width;
            lay1 = new int[width];
            lay2 = new int[width];
            lay3 = new int[width];
            lay4 = new int[width];
            lay5 = new int[width];
            lay6 = new int[width];
            lay7 = new int[width];
            lay8 = new int[width];
            lay9 = new int[width];
            lay10 = new int[width];
            lay11 = new int[width];
            fullmap = new List<int[]>() { lay1, lay2, lay3, lay4, lay5, lay6, lay7, lay8, lay9, lay10, lay11 };
        }

        public void DrawMap() {
            while (StaticState() == false) { // looping to allow for refreshing
                Console.WriteLine("FOR THE LOVE OF GOD, PLEASE DO NOT CLICK ON THE CONSOLE. PLEASE IM WARNING YOU !!!");
                for (int y = 0; y < fullmap.Count; ++y) {
                    Console.Write("|");
                    for (int x = 0; x < Width; ++x) {
                        Console.Write($"{ fullmap[y][x]}");
                    }
                    Console.Write("|\n");
                }
                Thread.Sleep(700);
                Fall();
                Console.Clear();
            }
        }

        public void AddShape(List<int> shape) {
            int midOfLine = Width / 2;
            int count = 0;

            for (int i = 0; i < shape.Count; ++i) {
                string thing = shape[i].ToString();

                if (thing.Length == 1) {
                    fullmap[i][midOfLine] = shape[i];
                }
                if  (thing.Length > 1) {
                    foreach (char part in thing) {
                        string thingToAdd = part.ToString();
                        fullmap[i][midOfLine + count] = int.Parse(thingToAdd);
                        count += 1;
                    }
                    count = 0;
                }
            }
        }

        public bool StaticState() { // checks if the map is currently static, if not used, becomes stuck in drawmap while loop
            int staticCount = 0;
            for (int y = 0; y < fullmap.Count; y = y + 1 ) {
                for (int x = 0; x < Width; ++x) {
                    if (fullmap[y][x] == 0 && ((x + 1) < Width && (x - 1) > 0)) {
                        staticCount += 1;
                    }
                    if (y != fullmap.Count - 1 && fullmap[y][x] != 0 && ((x + 1) < Width && (x - 1) > 0)) {
                        if (fullmap[y + 1][x - 1] == fullmap[y][x] && fullmap[y + 1][x] == fullmap[y][x] && fullmap[y + 1][x + 1] == fullmap[y][x] || fullmap[y + 1] == null) {
                            staticCount += 1;
                        }
                    }
                }
            }
            if (staticCount == fullmap.Count * Width) {
                return true;
            }
            return false;
        }

        public void Fall() {
            var lastOne = fullmap[fullmap.Count - 1];
            Random r = new Random();
            int LorR = r.Next(0, 10);

            for (int y = fullmap.Count - 1; y >= 0; y = y -1) {
                for (int x = 0; x < Width - 1; x = x + 1) {
                    if (y != fullmap.Count - 1 && fullmap[y][x] != 0 && fullmap[y + 1][x] != fullmap[y][x]) {
                        fullmap[y + 1][x] = fullmap[y][x];
                        fullmap[y][x] = 0;
                    }

                    if (y != fullmap.Count - 1 && fullmap[y][x] != 0) {
                        if (fullmap[y + 1][x - 1] == 0 && x - 1 != -1 && fullmap[y + 1][x + 1] == fullmap[y][x]) {  // checking if the lower left diagonal is free and the lower right is not. move to lower left not lower right
                            fullmap[y + 1][x - 1] = fullmap[y][x];
                            fullmap[y][x] = 0;
                        }
                        if (fullmap[y + 1][x + 1] == 0 && x + 1 != Width && fullmap[y + 1][x - 1] == fullmap[y][x]) { // checking if lower right diagonal is free and lower left is not;. move to lower right not left
                            fullmap[y + 1][x + 1] = fullmap[y][x];
                            fullmap[y][x] = 0;
                        }
                        if ((fullmap[y + 1][x + 1] == 0 && x + 1 != Width) && (fullmap[y + 1][x - 1] == 0 && x - 1 != -1)) { // if either is free, random number gen dictates whether it moves left or right
                            if (LorR >= 6) {
                                fullmap[y + 1][x - 1] = fullmap[y][x];
                                fullmap[y][x] = 0;
                            }
                            if (LorR <= 5) {
                                fullmap[y + 1][x + 1] = fullmap[y][x];
                                fullmap[y][x] = 0;
                            }
                        }
                    }
                }
            }
        }
    }
}