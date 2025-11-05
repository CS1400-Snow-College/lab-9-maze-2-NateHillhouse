/*
Nathan Hillhouse
10/24/2025
Lab 8 - Maze
*/


Console.Clear();
Console.WriteLine("Welcome to the Maze! You will use your arrow keys to navigate the maze.");
Console.WriteLine();

string[] maprows = File.ReadAllLines("./maze.txt");
foreach (string item in maprows) Console.WriteLine(item);
(int x, int y) buffer = (1, 2);
(int x, int y) cursorPosition = (buffer.x, buffer.y);

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

Console.SetCursorPosition(0, 10);
Console.WriteLine("You win!");


static (int, int) CheckKeys((int x, int y) cursorPosition, ConsoleKey key, (int x, int y) buffer, string[] maprows)
{
    Console.Write(maprows[cursorPosition.y-buffer.y][cursorPosition.x]);
    if (key == ConsoleKey.UpArrow && (maprows[cursorPosition.y-buffer.y-1][cursorPosition.x] != '*' && maprows[cursorPosition.y-buffer.y-1][cursorPosition.x] !='|')) cursorPosition.y--;
    else if (key == ConsoleKey.DownArrow && (maprows[cursorPosition.y-buffer.y-1][cursorPosition.x] != '*' && maprows[cursorPosition.y-buffer.y-1][cursorPosition.x] !='|')) cursorPosition.y++;
    else if (key == ConsoleKey.RightArrow&& (maprows[cursorPosition.y-buffer.y-1][cursorPosition.x] != '*' && maprows[cursorPosition.y-buffer.y-1][cursorPosition.x] !='|')) cursorPosition.x++;
    else if (key == ConsoleKey.LeftArrow&& (maprows[cursorPosition.y-buffer.y-1][cursorPosition.x] != '*' && maprows[cursorPosition.y-buffer.y-1][cursorPosition.x] !='|')) cursorPosition.x--;

    
    if (cursorPosition.x < buffer.x) cursorPosition.x = buffer.x;
    else if (cursorPosition.x > maprows[0].Length) cursorPosition.x = maprows[0].Length;
    if (cursorPosition.y < buffer.y) cursorPosition.y = buffer.y;
    else if (cursorPosition.y > maprows.Length) cursorPosition.y = maprows.Length;// + buffer.y;
    Console.SetCursorPosition(cursorPosition.x, cursorPosition.y);




    return cursorPosition;
}