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
--------------------------------------------------------------
");
    }

    public static void Subheader()
    {
        WriteLine(@"
                    [D|O] [M|I] [N|O] [E|S]

--------------------------------------------------------------
");
    }
}
