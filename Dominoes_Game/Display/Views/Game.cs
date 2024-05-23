using DominoesSimulator.Interfaces;
using DominoesSimulator.Display.Generators;

namespace DominoesSimulator.Display.Views;

public class Game : IGame
{
    public int StateValue { get; set; } = 0;
    public int TotalDominoSets { get; set; } = 0;
    public int SolvedSets { get; set; } = 0;
    public int UnsolvedSets { get; set; } = 0;
    public int NumberOfDominoes { get; set; } = 1;
    public List<(int, int)>? DominoSet { get; set; }
    public List<(int, int)>? Solution { get; set; }

    public static void Run()
    {
        var currentGame = new Game();
        GamePage currentGamePage = new(currentGame);
        currentGamePage.Run();
    }
    
}
