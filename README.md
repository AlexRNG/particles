# Rudamentary Particle Simulation
I made this project to create a simple falling sand simulation in C sharp as a way to practice OOP and just for fun.
The simulations consists of a small square map in the console where each cell is a digit, if the digit is 0 the cell is empty, if the digit is a 1 the cell has a particle.
As the simulation is being displayed on the console the map has to be input to the screen from top to bottom in layers, 
```c#
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
```
these layers are arrays of predetermined length and are stored in the fullmap list so that each cell can easily be accessed. 

As the simulation develops the particles can either fall straight down if space allows, fall down a slope, or remain still when the ground has been reached. 
To do this each particle needs to check the three cells below it to decide where it must move in the next update, if I were to do this in the same order I am writing the 
map to the screen (top to bottom) I would run into the problem of particles overriding particles within the cell beneath them so I opted to update the map from the bottom
up.

## Particle falling function
```c#
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
```

The first if statement deals with checking if the cell below the particle is free (the simplest scenario), in which case the cell below is changed to 1 and the current cell is changed to 0.
the next if statement breaks out to checking the lower diagonal cells with a similar sequence taking place if either is free (eg if the lower left is open and the lower right isnt, it moves to the lower left etc...)
If both are open then a random number between 0 and 10 is used to determine which side the particle falls to.

## Adding Shapes to the map

I add shapes by creating a list in which each index is a layer of the shape (eg a 2x2 square would be the list [11, 11])
```c#
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

```

for example, an input could be 
```c#
List<int> rectangle = new List<int>() {11, 11, 11, 11, 11};
gameMap.AddShape(rectangle);
```
