using DominoesSimulator.BusinessLogic;
using DominoesSimulator.Display.Components;
using DominoesSimulator.Interfaces;
using static System.Console;
using static System.StringExtensions;

namespace DominoesSimulator.Display.Generators;

internal class GamePage : IPage
{
    // Fields.
    private string Scoreboard { get; set; }
    private string DominoSetString { get; set; }
    private string Results { get; set; }
    public IGame CurrentGame { get; set; }


    // IPage contracts.
    public string[] MenuOptions { get; set; }
    public string Prompt { get; set; }
    public int ActiveMenuIndex { get; set; }
    public void RefreshScreen()
    {
        Clear();
        Scoreboard = GetScoreBoardString();
        DominoSetString = GetSetString();
        Results = GetResults();
        Write($"{Scoreboard}{DominoSetString}{Results}{Prompt}");
    }

    // Private constructor.
    public GamePage(IGame game)
    {
        CurrentGame = game;
        GetGameState();
    }

    // Public method.
    public void Run()
    {
        while (true)
        {
            // State 0: Set number of dominoes.
            while (CurrentGame.StateValue == 0)
            {
                ActiveMenuIndex = MenuSelection();
                ChangeNumberOfDominoes(ActiveMenuIndex);
            }

            // State 1: Analyse or regenerate domino set.
            while (CurrentGame.StateValue == 1)
            {
                ActiveMenuIndex = MenuSelection();
                AnalyseOrRegenerate(ActiveMenuIndex);
            }

            // State 2: Review results.
            while (CurrentGame.StateValue == 2)
            {
                ActiveMenuIndex = MenuSelection();
                ReviewResults(ActiveMenuIndex);
            }

        }
        GameOver();
        Console.ReadKey(true);
    }

    // Outcomes of menu selections.
    // StateValue = 0.
    private void ChangeNumberOfDominoes(int selectedOption)
    {
        // More dominoes.
        if (selectedOption == 0
            && CurrentGame.NumberOfDominoes < 10)

        {
            CurrentGame.NumberOfDominoes++;
        }

        // Fewer dominoes.
        if (selectedOption == 1
            && CurrentGame.NumberOfDominoes > 1)
        {
            CurrentGame.NumberOfDominoes--;
        }

        // Generate dominoes.
        if (selectedOption == 2)
        {
            CurrentGame.DominoSet = DominoGenerator.GenerateDominoSet(CurrentGame.NumberOfDominoes);
            CurrentGame.StateValue++;
            GetGameState();
        }

        // Return to Main Menu.
        if (selectedOption == 3)
        {
            CurrentGame.StateValue = 4;
        }
    }

    // StateValue = 1.
    private void AnalyseOrRegenerate(int selectedOption)
    {
        switch (selectedOption)
        {
            // Analyse dominoes.
            case 0:
                CurrentGame.Solution = DominoSetValidator.SetValidator(CurrentGame.DominoSet);

                CurrentGame.TotalDominoSets++;

                if (CurrentGame.Solution is not null)
                {
                    CurrentGame.SolvedSets++;
                }
                else
                {
                    CurrentGame.UnsolvedSets++;
                }

                CurrentGame.StateValue++;
                GetGameState();
                return;

            // Regenerate dominoes.
            case 1:
                CurrentGame.DominoSet = DominoGenerator.GenerateDominoSet(CurrentGame.NumberOfDominoes);
                return;

            // Back.
            case 2:
                CurrentGame.DominoSet = null;
                CurrentGame.NumberOfDominoes = 1;
                CurrentGame.StateValue--;
                GetGameState();
                return;

            // Return to Main Menu.
            case 3:
                CurrentGame.StateValue = 4;
                return;

            default:
                break;
        }
    }

    // StateValue = 2.
    private void ReviewResults(int selectedOption)
    {
        switch (selectedOption)
        {
            // Quick Replay.
            case 0:
                CurrentGame.Solution = null;
                CurrentGame.DominoSet = DominoGenerator.GenerateDominoSet(CurrentGame.NumberOfDominoes);
                CurrentGame.Solution = DominoSetValidator.SetValidator(CurrentGame.DominoSet);

                CurrentGame.TotalDominoSets++;

                if (CurrentGame.Solution is not null)
                {
                    CurrentGame.SolvedSets++;
                }
                else
                {
                    CurrentGame.UnsolvedSets++;
                }
                GetGameState();
                return;

            // New Set.
            case 1:
                CurrentGame.Solution = null;
                CurrentGame.DominoSet = null;
                CurrentGame.NumberOfDominoes = 1;
                CurrentGame.StateValue = 0;
                GetGameState();
                return;

            // End Game.
            case 2:
                CurrentGame.StateValue++;
                return;

            default:
                break;
        }
    }

    // Methods for drawing page.
    private string GetScoreBoardString()
    {
        string totalSets = CurrentGame.TotalDominoSets.ToString().PadRight(2);
        string chainableSets = CurrentGame.SolvedSets.ToString().PadRight(2);
        string unchainableSets = CurrentGame.UnsolvedSets.ToString().PadRight(2);
        return @$"+-------------------------------------------------------------+
{Centre("[D|O] [M|I] [N|O] [E|S]")}
+-------------------+----------------------+------------------+
|  Sets Checked: {totalSets} |   Chainable Sets: {chainableSets} |  Unchainable: {unchainableSets} |
+-------------------+----------------------+------------------+
";
    }

    private string GetSetString()
    {
        string DominoBorder = "";
        string DominoValues1 = "";
        string DominoValues2 = "";

        if (CurrentGame.DominoSet is not null)
        {
            foreach (var domino in CurrentGame.DominoSet)
            {
                DominoBorder += "+-+   ";
                DominoValues1 += $"|{domino.Item1}|   ";
                DominoValues2 += $"|{domino.Item2}|   ";
            }
        }

        else
        {
            for (int i = 1; i <= CurrentGame.NumberOfDominoes; i++)
            {
                DominoBorder += "+-+   ";
            }

            Centre(DominoBorder);

            DominoValues1 = DominoBorder.Replace('+', '|').Replace('-', 'X');
            DominoValues2 = DominoValues1;
        }

        return $@"|                                                             |
{Centre("DOMINO SET")}
|                                                             |
{Centre(DominoBorder)}
{Centre(DominoValues1)}
{Centre(DominoBorder)}
{Centre(DominoValues2)}
{Centre(DominoBorder)}
|                                                             |
+-------------------------------------------------------------+
";
    }

    private string GetResults()
    {
        string Title = "AWAITING VALID SOLUTION";
        string SolutionString = "";

        if (CurrentGame.Solution is null && CurrentGame.StateValue >= 2)
        {
            Title = "FAIL!";
            SolutionString = "DOMINO SET IS UNSOLVABLE!";
        }

        if (CurrentGame.Solution is not null)
        {
            Title = "SUCCESS!";
            foreach (var domino in CurrentGame.Solution)
            {
                SolutionString += $"[{domino.Item1}|{domino.Item2}] ";
            }
        }

        string OutputString = Centre(SolutionString);


        return $@"|                                                             |
{Centre(Title)}
|                                                             |
{OutputString}
|                                                             |
+-------------------------------------------------------------+

";
    }

    private void GameOver()
    {
        string totalSets = CurrentGame.TotalDominoSets.ToString().PadRight(2);
        string chainableSets = CurrentGame.SolvedSets.ToString().PadRight(2);
        string unchainableSets = CurrentGame.UnsolvedSets.ToString().PadRight(2);
        Console.Write(@$"###############################################################
###############################################################
##################### G A M E ##  O V E R #####################
###############################################################
###############################################################
|  Sets Checked: {totalSets} |   Chainable Sets: {chainableSets} |  Unchainable: {unchainableSets} |
+-------------------+----------------------+------------------+

Press any key to return to the Main Menu...");
    }


    private void GetGameState()
    {
        ActiveMenuIndex = 0;
        switch (CurrentGame.StateValue)
        {
            case 0:
                Prompt = "DOMINOES generates random sets of dominoes & determines if the \nset can be arranged into an unbroken loop. \nTo start, choose how many dominoes in the set [1-10].\n\n";
                MenuOptions = ["More", "Less", "Generate!", "Return To Main Menu"];
                return;
            case 1:
                Prompt = "Here is your Domino set. \n\nSelect ANALYSE to continue or REGENERATE to get a new set.\n\n";
                MenuOptions = ["Analyse", "Regenerate", "Back", "Return To Main Menu"];
                return;
            case 2:
                Prompt = ((CurrentGame.Solution is null) ? "The Domino set was unsolvable!" : "The Domino set was solvable!") + "\n\n";
                MenuOptions = ["Quick Replay", "New Set", "End Game"];
                return;
            default:
                throw new ArgumentException("Invalid gamestate provided");
        }
    }

    // Utility methods.
    private int MenuSelection()
    {
        Menu CurrentMenu = new(this);
        return CurrentMenu.GetMenuSelection(this);
    }
}
