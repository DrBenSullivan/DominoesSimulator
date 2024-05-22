using static System.Console;
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

        // MainMenu.Display();
        // TESTING ONLY****************************************************************
        WriteLine("How many sets shall we test? Enter integer only:");
        int inputIterations = Int32.Parse(ReadLine());
        WriteLine("\nHow many dominoes per set? Enter integer only:");
        int inputNumberOfDominoes = Int32.Parse(ReadLine());
        int totalCount = 0;
        int solvable = 0;
        int notSolvable = 0;
        for (int i = 0; i<inputIterations; i++)
        {
            Write(@$"
+---------------++--------------++----------------+
|Total Sets: {totalCount,-2} || Solvable: {solvable,-2} || Unsolvable: {notSolvable,-3}|
+---------------++--------------++----------------+
The random domino set is:
");
            var DominoList = DominoGenerator.GenerateDominoSet(inputNumberOfDominoes);
            
            foreach (var domino in DominoList)
            {
                Write($"[{domino.Item1}|{domino.Item2}] ");
            }
            totalCount++;
            (bool isSolveable, List<(int, int)> Solution) = SetChecker.DominoChecker(DominoList);
            if (isSolveable && Solution is not null)
            {
                WriteLine("\n\nDomino set is solvable! Solution:");
                foreach (var domino in Solution)
                {
                    Write($"[{domino.Item1}|{domino.Item2}] ");
                }
                solvable++;
                Console.ReadKey(true);
                continue;
            }
            WriteLine("\nDomino set is unsolvable\n\n\n");
            notSolvable++;
            Console.ReadKey(true);
        }
        Write(@$"
===================== RESULT ======================

+---------------++--------------++----------------+
|Total Sets: {totalCount,-2} || Solvable: {solvable,-2} || Unsolvable: {notSolvable,-3}|
+---------------++--------------++----------------+

=================== END OF TEST ===================          
");
    }
}