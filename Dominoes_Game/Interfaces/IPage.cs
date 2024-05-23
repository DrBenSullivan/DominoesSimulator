namespace DominoesSimulator.Interfaces;

public interface IPage
{
    string[] MenuOptions { get; }
    string Prompt { get; }
    int ActiveMenuIndex { get; set; }
    void RefreshScreen(); 
}
