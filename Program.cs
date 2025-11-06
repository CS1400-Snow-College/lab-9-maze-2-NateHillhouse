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

do
{
    System.ConsoleKey key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.Escape) break;
    else cursorPosition = CheckKeys(cursorPosition, key, buffer, maprows);
    //else continue;
    if (maprows[cursorPosition.y - buffer.y][cursorPosition.x] == '$') break;
    Console.SetCursorPosition(0, 10);
    //Console.WriteLine(maprows[cursorPosition.y]);
    Console.SetCursorPosition(cursorPosition.x, cursorPosition.y);

}
while (true);

Console.SetCursorPosition(0, rows + 1);
Console.WriteLine("You win!");


static (int, int) CheckKeys((int x, int y) cursorPosition, ConsoleKey key, (int x, int y) buffer, string[] maprows)
{
    (int x, int y) map = (cursorPosition.y - buffer.y, cursorPosition.x - buffer.x);
    char locationUp = ' ';
    char locationDown = ' ';
    char locationRight = ' ';
    char locationLeft = ' ';

    if (map.y > 0) 
        locationUp = maprows[cursorPosition.y - buffer.y-1][cursorPosition.x];
    if (map.y < maprows.Length - 1) 
        locationDown = maprows[cursorPosition.y - buffer.y+1][cursorPosition.x];
    if (map.x < maprows.Length - 1)
        locationRight = maprows[cursorPosition.y - buffer.y][cursorPosition.x + 1];
    if (map.x > 0)
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