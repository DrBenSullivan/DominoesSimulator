using DominoesSimulator.Interfaces;
using static System.Console;

namespace DominoesSimulator.Display.Components;

internal class Menu : IMenu
{
    public IPage Page { get; }
    public string[] MenuOptions { get; set; }
    public string Prompt { get; set; }
    public int ActiveMenuIndex { get; set; }
    private void Refresh() => Page.RefreshScreen();

    public Menu(IPage page)
    {
        Page = page;
        MenuOptions = Page.MenuOptions;
        Prompt = Page.Prompt;
        ActiveMenuIndex = Page.ActiveMenuIndex;
    }

    // Logic to refresh console as selection changes and return the selected index.
    public int GetMenuSelection(IPage page)
    {
        Menu menu = new(page);
        menu.ActiveMenuIndex = page.ActiveMenuIndex;
        ConsoleKey keyPressed;
        do
        {
            Refresh();
            DisplayMenu();

            ConsoleKeyInfo keyInfo = ReadKey(true);
            keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.UpArrow)
            {
                if (ActiveMenuIndex > 0)
                {
                    ActiveMenuIndex--;
                }
                else if (ActiveMenuIndex == 0)
                {
                    ActiveMenuIndex = MenuOptions.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                if (ActiveMenuIndex < MenuOptions.Length - 1)
                {
                    ActiveMenuIndex++;
                }
                else if (ActiveMenuIndex == MenuOptions.Length - 1)
                {
                    ActiveMenuIndex = 0;
                }
            }

        } while (keyPressed != ConsoleKey.Enter);
        return ActiveMenuIndex;
    }

    // Logic to print Menu & emphasise currently selected index.
    public void DisplayMenu()
    {
        for (int i = 0; i < MenuOptions.Length; i++)
        {
            string prefix = "  ";
            string suffix = "  ";
            if (i == ActiveMenuIndex)
            {
                prefix = " >";
                suffix = "< ";
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
            }
            string option = MenuOptions[i];
            WriteLine($"{prefix}{option}{suffix}");
            ResetColor();
        }
    }
}
