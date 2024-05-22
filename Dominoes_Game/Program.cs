using Dominoes_Game.Display.Views;

using static System.Console;


namespace Dominoes_Game;

class Game
{
    public static void Dominoes_Game()
    { }

    static void Main()
    {
        SetWindowSize(63, 30);
        CursorVisible = false;
        Title = "DOMINOES - The Game";

        HomePage.Display();
        ReadKey(true);
    }
}