/* 
Nathan Hillhouse
10/24/2025
Lab 9 - Maze 2
*/

Random rand = new Random();

string[] maprows = File.ReadAllLines("./maze.txt");
char playercharacter = '█';
char[,] grid = new char[maprows.Length, maprows[0].Length];
char walls = '*';
char finish = '$';
char coins = '^';
char enemies = '%';
char gate = '|';
int collectedCoins = 0;

//Transfer data into grid
for (int i = 0; i < maprows.Length; i++)
    for (int j = 0; j < maprows[0].Length; j++)
        grid[i, j] = maprows[i][j];

(int x, int y) playerlocation = (0, 0);

grid[playerlocation.y, playerlocation.x] = playercharacter;
string message = "You Win!";

Console.CursorVisible = false;
DrawGrid();

do
{
    ConsoleKey key = Console.ReadKey(true).Key;

    Console.SetCursorPosition(playerlocation.x, playerlocation.y+2);
    
    if (key == ConsoleKey.Escape)
    {
        message = "You exited the game.";
        break;
    }
    else if (key == ConsoleKey.RightArrow)
    {
        if (Collision(grid, new(playerlocation.x + 1, playerlocation.y)))
        {
            Console.Write(" ");

            playerlocation.x = playermovement(1, playerlocation.x);

            if (grid[playerlocation.y, playerlocation.x] == finish) break;
            else if (grid[playerlocation.y, playerlocation.x] == coins) collectedCoins += 1;
            else if (grid[playerlocation.y, playerlocation.x] == enemies) 
            {
                message = "You Lose";
                break;
            }

            grid[playerlocation.y, playerlocation.x] = playercharacter;

            Console.SetCursorPosition(playerlocation.x, playerlocation.y+2);
            Console.Write(playercharacter);
        }
        
    }
    else if (key == ConsoleKey.LeftArrow)
    {
        if (Collision(grid, new(playerlocation.x - 1, playerlocation.y)))
        {
            Console.Write(" ");

            playerlocation.x = playermovement(-1, playerlocation.x);

            if (grid[playerlocation.y, playerlocation.x] == finish) break;
            else if (grid[playerlocation.y, playerlocation.x] == coins) collectedCoins += 1;
            else if (grid[playerlocation.y, playerlocation.x] == enemies) 
            {
                message = "You Lose";
                break;
            }

            grid[playerlocation.y, playerlocation.x] = playercharacter;

            Console.SetCursorPosition(playerlocation.x, playerlocation.y+2);
            Console.Write(playercharacter);
        }
        
    }
    else if (key == ConsoleKey.UpArrow)
    {
        if (Collision(grid, new(playerlocation.x, playerlocation.y - 1)))
        {
            Console.Write(" ");

            playerlocation.y = playermovement(-1, playerlocation.y);

            if (grid[playerlocation.y, playerlocation.x] == finish) break;
            else if (grid[playerlocation.y, playerlocation.x] == coins) collectedCoins += 1;
            else if (grid[playerlocation.y, playerlocation.x] == enemies) 
            {
                message = "You Lose";
                break;
            }

            grid[playerlocation.y, playerlocation.x] = playercharacter;

            Console.SetCursorPosition(playerlocation.x, playerlocation.y+2);
            Console.Write(playercharacter);
        }
        
    }
    else if (key == ConsoleKey.DownArrow)
    {
        if (Collision(grid, new(playerlocation.x, playerlocation.y + 1)))
        {
            Console.Write(" ");
            playerlocation.y = playermovement(1, playerlocation.y);

            if (grid[playerlocation.y, playerlocation.x] == finish) break;
            else if (grid[playerlocation.y, playerlocation.x] == coins) collectedCoins += 1;
            else if (grid[playerlocation.y, playerlocation.x] == enemies) 
            {
                message = "You Lose";
                break;
            }
            

            grid[playerlocation.y, playerlocation.x] = playercharacter;

            
            Console.SetCursorPosition(playerlocation.x, playerlocation.y+2);
            Console.Write(playercharacter);
        }
    }
    if (collectedCoins == 10)
    {
        for (int i = 0; i < maprows.Length; i++)
        {
            for (int j = 0; j < maprows[0].Length; j++)
            {
                if (grid[i,j] == gate) 
                {
                    grid[i,j] = ' ';
                    Console.SetCursorPosition(j, i+2);
                    Console.Write(" ");
                    Console.SetCursorPosition(playerlocation.x, playerlocation.y+2);
                }
            }
        }
    }
    enemymovement();

} while (true);

Console.SetCursorPosition(0, grid.GetLength(0) + 3);
Console.WriteLine(message);

bool Collision(char[,] grid, (int x, int y) location)
{
    if (location.x < 0 || location.y < 0 || location.x >= grid.GetLength(1) || location.y >= grid.GetLength(0) ||
        grid[location.y, location.x] == walls || grid[location.y, location.x] == gate)
        return false;

    return true;
}

int playermovement(int movement, int location)
{
    grid[playerlocation.y, playerlocation.x] = ' ';
    location += movement;
    return location;
}

void enemymovement()
{
    for (int i = 0; i < maprows.Length; i++)
        {
            for (int j = 0; j < maprows[0].Length; j++)
            {
                if (grid[i,j] == enemies) 
                {
                    int num = rand.Next(-1,2);
                    int position = rand.Next(1, 3);
                    while (true) 
                    {
                        if (position == 1 && (grid[i+num, j] == walls || grid[i+num, j] == gate || grid[i+num, j] == coins || i+num > grid.GetLength(1) || i+num < 1)) 
                        {    
                            num = rand.Next(-1,2);
                            continue;
                        }
                        else if (position == 2 && (grid[i, j+num] == walls || grid[i, j+num] == gate || grid[i, j+num] == coins || j+num > grid.GetLength(1) || j+num < 1)) 
                        {
                            num = rand.Next(-1,2);
                            continue;
                        }
                        else break;

                    }
                    grid[i,j] = ' ';
                    Console.SetCursorPosition(j,i+2);
                    Console.Write(' ');
                    if (position == 1)
                    {
                        grid[i+num, j] = '%';
                        Console.SetCursorPosition(j, i+num+2);
                        Console.Write(grid[i+num,j]);
                    }
                    else if (position == 2)
                    {
                        grid[i, j+num] = '%';
                        Console.SetCursorPosition(j+num, i+2);
                        Console.Write(grid[i,j+num]);
                    }
                    Console.SetCursorPosition(playerlocation.x, playerlocation.y+2);
                }
            }
        }
}

void DrawGrid()
{
    Console.Clear();
    Console.WriteLine("Welcome to the Maze! You will use your arrow keys to navigate the maze.");
    Console.WriteLine();

    for (int i = 0; i < maprows.Length; i++)
    {
        for (int j = 0; j < maprows[0].Length; j++)
        {
            Console.Write(grid[i, j]);
        }
        Console.WriteLine();
    }
}
