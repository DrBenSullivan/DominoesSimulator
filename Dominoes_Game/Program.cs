using static System.Console;
using Dominoes_Game.Display.View;
using Dominoes_Game.BusinessLogic;

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

        //MainMenu.Display();

        var DominoList = new List<(int, int)>
        {
            (1, 1),
            (2, 2),
            (2, 1),
            (2, 2),
            (1, 2)
        };
        (bool isSolveable, List<(int, int)> Solution) = SetChecker.DominoChecker(DominoList);
        if (isSolveable)
        {
            string solutionString = "";
            foreach (var s in Solution)
            {
                solutionString = String.Concat(solutionString, $" [{s.Item1}|{s.Item2}]");
            }
            Console.Write($"Dominoes are solveable. Solution:\n{solutionString}");
        }
        else Console.Write("Dominoes are not solveable");
        Console.ReadKey(true);
    }
}