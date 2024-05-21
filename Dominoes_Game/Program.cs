using static System.Console;
using Dominoes_Game.Display.View;

namespace Dominoes_Game;

public enum ScreenName
{
    MainMenu,
    Play,
    StartGame,
    About,
    Exit,
    ConfirmExit
}

class Game
{
    public static void Dominoes_Game()
    { }
    static void Main()
    {
        SetWindowSize(63, 30);
        CursorVisible = false;
        Title = "DOMINOES - The Game";

        MainMenu.Display();
    }
}