using System.Reflection;

namespace DominoesSimulator.Interfaces;

public interface IMenu
{
    string Prompt { get; }
    string[] MenuOptions { get; }
    int ActiveMenuIndex { get; set; }
    void DisplayMenu();
    int GetMenuSelection(IPage Page);
}
