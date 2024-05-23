namespace DominoesSimulator.Interfaces;

public interface IGame
{
    int StateValue { get; set; }
    int TotalDominoSets { get; set; }
    int SolvedSets { get; set; }
    int UnsolvedSets { get; set; }
    int NumberOfDominoes { get; set; }
    List<(int, int)>? DominoSet { get; set; }
    List<(int, int)>? Solution { get; set; }
}
