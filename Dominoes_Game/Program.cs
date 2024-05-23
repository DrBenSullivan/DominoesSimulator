using DominoesSimulator.Display.Views;

using static System.Console;


namespace DominoesSimulator;

class DominoSimulator
{
    public static void App() { }

    static void Main()
    {
        SetWindowSize(63, 33);
        CursorVisible = false;
        Title = "DOMINOES - The CurrentGame";

        while (true)
        {
            Home.Run();
            Game.Run();
        }
    }
}