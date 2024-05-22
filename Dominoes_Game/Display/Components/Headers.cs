using static System.Console;

namespace Dominoes_Game.Display.Components;
public abstract class Headers
{
    public static void Home()
    {
        WriteLine(@"
WELCOME TO...

            +---+---+ +---+---+ +---+---+ +---+---+
            | D | O | | M | I | | N | O | | E | S |
            +---+---+ +---+---+ +---+---+ +---+---+           

                                                   ...THE GAME!
---------------------------------------------------------------
");
    }

    public static void Scoreboard(int TotalSets, int ChainableSets, int UnchainableSets)
    {
        string totalSets = TotalSets.ToString().PadRight(2);
        string chainableSets = ChainableSets.ToString().PadRight(2);
        string unchainableSets = UnchainableSets.ToString().PadRight(2);
        WriteLine(@$"+-------------------------------------------------------------+
|                   [D|O] [M|I] [N|O] [E|S]                   |
+-------------------+----------------------+------------------+
|  Sets Checked: {totalSets} |   Chainable Sets: {chainableSets} |  Unchainable: {unchainableSets} |
+-------------------+----------------------+------------------+");
    }
}
