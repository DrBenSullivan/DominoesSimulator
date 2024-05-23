using DominoesSimulator.Display.Views;

using static System.Console;
using static System.StringExtensions;

namespace DominoesSimulator.Display.Generators;

internal class HomePage(string prompt, string[] options)
{
    private readonly string[] MenuOptions = options;
    private int ActiveMenuIndex = 0;

    public static ScreenName GetMainMenuSelection(string prompt, string[] options, ScreenName[] screensArray)
    {
        var menu = new HomePage(prompt, options);
        ConsoleKey keyPressed;
        do
        {
            Clear();
            DisplayHeader();
            menu.DisplayOptions();

            ConsoleKeyInfo keyInfo = ReadKey(true);
            keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.UpArrow)
            {
                if (menu.ActiveMenuIndex > 0)
                {
                    menu.ActiveMenuIndex--;
                }
                else if (menu.ActiveMenuIndex == 0)
                {
                    menu.ActiveMenuIndex = menu.MenuOptions.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                if (menu.ActiveMenuIndex < menu.MenuOptions.Length - 1)
                {
                    menu.ActiveMenuIndex++;
                }
                else if (menu.ActiveMenuIndex == menu.MenuOptions.Length - 1)
                {
                    menu.ActiveMenuIndex = 0;
                }
            }

        } while (keyPressed != ConsoleKey.Enter);

        return screensArray[menu.ActiveMenuIndex];
    }

    private void DisplayOptions()
    {
        WriteLine(prompt + "\n");
        for (int i = 0; i < MenuOptions.Length; i++)
        {
            string prefix;
            if (i == ActiveMenuIndex)
            {
                prefix = " >";
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
            }
            else
            {
                prefix = "  ";
            }
            string option = MenuOptions[i];
            WriteLine($"{prefix}{option}");
            ResetColor();
        }
    }

    private static void DisplayHeader()
    {
        WriteLine(@$"+-------------------------------------------------------------+
{Centre("WELCOME TO...")}
{Centre("")}
{Centre("+---+---+ +---+---+ +---+---+ +---+---+")}
{Centre("| D | O | | M | I | | N | O | | E | S |")}
{Centre("+---+---+ +---+---+ +---+---+ +---+---+")} 
{Centre("")}
{Centre("SIMULATOR!")}
+-------------------------------------------------------------+

");
    }
}
