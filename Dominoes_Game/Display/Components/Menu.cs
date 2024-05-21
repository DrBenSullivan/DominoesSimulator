using static System.Console;

namespace Dominoes_Game.Display.Components;

internal class Menu(Action displayHeaderMethod, string prompt, string[] options)
{
    private readonly Action DisplayHeaderMethod = displayHeaderMethod;
    private readonly string[] Options = options;
    private int ActiveIndex = 0;

    private void DisplayOptions()
    {
        WriteLine(prompt + "\n");
        for (int i = 0; i < Options.Length; i++)
        {
            string prefix;
            if (i == ActiveIndex)
            {
                prefix = " >";
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
            }
            else
            {
                prefix = "  ";
            }
            string option = Options[i];
            WriteLine($"{prefix}{option}");
            ResetColor();
        }
    }

    public static int GetMenuChoice(Action displayHeaderMethod, string prompt, string[] options)
    {
        var menu = new Menu(displayHeaderMethod, prompt, options);
        ConsoleKey keyPressed;
        do
        {
            Clear();

            menu.DisplayHeaderMethod.Invoke();
            menu.DisplayOptions();

            ConsoleKeyInfo keyInfo = ReadKey(true);
            keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.UpArrow)
            {
                if (menu.ActiveIndex > 0)
                {
                    menu.ActiveIndex--;
                }
                else if (menu.ActiveIndex == 0)
                {
                    menu.ActiveIndex = menu.Options.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                if (menu.ActiveIndex < menu.Options.Length - 1)
                {
                    menu.ActiveIndex++;
                }
                else if (menu.ActiveIndex == menu.Options.Length - 1)
                {
                    menu.ActiveIndex = 0;
                }
            }

        } while (keyPressed != ConsoleKey.Enter);

        return menu.ActiveIndex;
    }
}