/*
Nathan Hillhouse
10/24/2025
Lab 9 - Maze 2
*/


Console.Clear();
Console.WriteLine("Welcome to the Maze! You will use your arrow keys to navigate the maze.");
Console.WriteLine();

string[] maprows = File.ReadAllLines("./maze.txt");
(int x, int y) buffer = (2, 2);
(int x, int y) cursorPosition = (buffer.x, buffer.y);
foreach (string item in maprows) Console.WriteLine(item);

int rows = maprows.Length;
int columns = maprows[0].Length;
Console.SetCursorPosition(cursorPosition.x, cursorPosition.y);
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
    if (collected >= 10) //greater than just in case; doesn't hurt to include
    {
        
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

static (int, int) CheckKeys((int x, int y) cursorPosition, ConsoleKey key, (int x, int y) buffer, string[] maprows)
{
    (int x, int y) map = (cursorPosition.x - buffer.x, cursorPosition.y - buffer.y);

    char locationUp = ' ';
    char locationDown = ' ';
    char locationRight = ' ';
    char locationLeft = ' ';

    if (map.y > 0) 
        locationUp = maprows[cursorPosition.y - buffer.y-1][cursorPosition.x];
    if (map.y < maprows.Length - 1) 
        locationDown = maprows[cursorPosition.y - buffer.y+1][cursorPosition.x];
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