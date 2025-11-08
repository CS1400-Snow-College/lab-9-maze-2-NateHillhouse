/*
Nathan Hillhouse
10/24/2025
Lab 9 - Maze 2
*/

//Note: This week has been difficult, so this assignment was more difficult than it should have been

Console.Clear();
Console.WriteLine("Welcome to the Maze! You will use your arrow keys to navigate the maze.");
Console.WriteLine();

Random rand = new Random();

string[] maprows = File.ReadAllLines("./maze.txt");
(int x, int y) buffer = (2, 2);
(int x, int y) cursorPosition = (buffer.x, buffer.y);
foreach (string item in maprows) Console.WriteLine(item);

int rows = maprows.Length;
int columns = maprows[0].Length;
Console.SetCursorPosition(cursorPosition.x, cursorPosition.y);
List<(int x, int y)> chords = new List<(int, int)>(); ;
GetEnemyChords(maprows, chords);
int collected = 0;
do
{
    System.ConsoleKey key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.Escape)
    {
        EndCondition("You exited the game.");
        break;
    }
    else cursorPosition = CheckKeys(cursorPosition, key, buffer, maprows);
    //else continue;
    if (maprows[cursorPosition.y - buffer.y][cursorPosition.x] == '$')
    {
        EndCondition("You Win!");
        break;
    }
    else if (maprows[cursorPosition.y - buffer.y][cursorPosition.x] == '%')
    {
        EndCondition("You Died!");
        break;
    }
    Console.SetCursorPosition(0, 10);

    GetCoins();
    if (collected >= 10) //greater than just in case; doesn't hurt to include
    {
        for (int item = 0; item < maprows.Length; item++)
        {
            for (int i = 0; item < maprows[0].Length; i ++)
            {
                if (maprows[item][i] == '|')
                {
                    maprows[item].Remove(i).Insert(i, " ");
                }

            }
        }
    }


    chords = EnemyMovement(chords, maprows);   
    if (chords.Any(e => e.x == cursorPosition.x && e.y == cursorPosition.y - buffer.y))
    {
        EndCondition("You Died!");
        break;
    }
    //Console.WriteLine(maprows[cursorPosition.y]);
    Console.SetCursorPosition(cursorPosition.x, cursorPosition.y);

}
while (true);


void EndCondition(string message)
{
    Console.SetCursorPosition(0, rows + 1 + buffer.y);
    Console.WriteLine(message);
}

(int, int) CheckKeys((int x, int y) cursorPosition, ConsoleKey key, (int x, int y) buffer, string[] maprows)
{
    (int x, int y) map = (cursorPosition.x - buffer.x, cursorPosition.y - buffer.y);

    char locationUp = ' ';
    char locationDown = ' ';
    char locationRight = ' ';
    char locationLeft = ' ';

    if (map.y > 0)
        locationUp = maprows[cursorPosition.y - buffer.y - 1][cursorPosition.x];
    if (map.y < maprows.Length - 1)
        locationDown = maprows[cursorPosition.y - buffer.y + 1][cursorPosition.x];
    if (cursorPosition.x < maprows[map.y].Length - 1)
        locationRight = maprows[cursorPosition.y - buffer.y][cursorPosition.x + 1];
    if (cursorPosition.x > 0)
        locationLeft = maprows[cursorPosition.y - buffer.y][cursorPosition.x - 1];


    if (key == ConsoleKey.UpArrow && locationUp != '*' && locationUp != '|') cursorPosition.y--;
    else if (key == ConsoleKey.DownArrow && locationDown != '*' && locationDown != '|') cursorPosition.y++;
    else if (key == ConsoleKey.RightArrow && locationRight != '*' && locationRight != '|') cursorPosition.x++;
    else if (key == ConsoleKey.LeftArrow && locationLeft != '*' && locationLeft != '|') cursorPosition.x--;


    if (cursorPosition.x < buffer.x) cursorPosition.x = buffer.x;
    else if (cursorPosition.x >= maprows[0].Length) cursorPosition.x = maprows[0].Length;
    if (cursorPosition.y < buffer.y) cursorPosition.y = buffer.y;
    else if (cursorPosition.y >= maprows.Length) cursorPosition.y = maprows.Length;// + buffer.y;

    Console.SetCursorPosition(0, 0);
    Console.WriteLine($"U:{locationUp}, D:{locationDown}, L:{locationLeft}, R:{locationRight}          ");
    Console.SetCursorPosition(cursorPosition.x, cursorPosition.y);


    return cursorPosition;
}

// call signature: chords = EnemyMovement(chords, maprows);
List<(int x, int y)> EnemyMovement(List<(int x, int y)> enemies, string[] maprows)
{
    var newLocations = new List<(int x, int y)>(enemies.Count);

    // local snapshot of current enemy positions for collision checks
    var occupied = new HashSet<(int x, int y)>(enemies);

    for (int i = 0; i < enemies.Count; i++)
    {
        int oldX = enemies[i].x;
        int oldY = enemies[i].y;

        // erase old enemy from screen (use buffer when drawing)
        Console.SetCursorPosition(oldX + buffer.x, oldY + buffer.y);
        Console.Write(" ");

        // try up to N times to find a valid random move (including staying put)
        (int x, int y) chosen = (oldX, oldY);
        while (true)
        {
            int dx = rand.Next(-1, 2); // -1,0,1
            int dy = rand.Next(-1, 2);
            int newX = oldX + dx;
            int newY = oldY + dy;

            // bounds
            if (newY < buffer.y || newY >= maprows.Length || newX < 0 || newX >= maprows[newY].Length)
                continue;

            // don't walk into walls / obstacles
            char target = maprows[newY][newX + buffer.x];
            if (target == '*' || target == '|' || target == '^')
                continue;
                
            if (newLocations.Contains((newX, newY))) continue;


            if (occupied.Contains((newX, newY)) && !(newX == oldX && newY == oldY)) continue;

            // OK this location is valid
            chosen = (newX, newY);
            break;
        }

        // draw enemy in chosen spot
        Console.SetCursorPosition(chosen.x + buffer.x, chosen.y + buffer.y);
        Console.Write("%");

        newLocations.Add(chosen);
    }

    return newLocations;
}


    
void GetCoins()
{
    int mapY = cursorPosition.y - buffer.y;
    int mapX = cursorPosition.x;
    if (maprows[mapY][mapX] == '^')
    {
        collected++;
        maprows[mapY] = maprows[mapY].Remove(mapX, 1).Insert(mapX, " ");
        Console.SetCursorPosition(mapX, mapY + buffer.y);
        Console.Write(" "); // visually erase '^'
        Console.SetCursorPosition(0, rows + 2 + buffer.y);
        Console.Write($"Collected: {collected}  ");
    }
}

List<(int x, int y)> GetEnemyChords(string[] map, List<(int x, int y)> chords)
{
    for (int y = 0; y < rows; y++)
    {
        for (int x = 0; x < columns; x++)
        {
            if (maprows[y][x] == '%') chords.Add((x, y));
        }
    }
    return chords;
} 
