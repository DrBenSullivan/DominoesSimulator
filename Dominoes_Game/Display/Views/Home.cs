using DominoesSimulator.Display.Generators;

using static System.ProcessExtensions;

namespace DominoesSimulator.Display.Views;

public enum ScreenName
{
    Home,
    Play,
    StartGame,
    About,
    Exit,
    ConfirmExit,
    Exercism,
    GitHub,
    LinkedIn
}

internal class Home
{
    private string Prompt { get; }
    private string[] MenuOptions { get; }
    private ScreenName[] Routes { get; }

    private Home(string prompt, string[] menuOptions, ScreenName[] routes)
    {
        Prompt = prompt;
        MenuOptions = menuOptions;
        Routes = routes;
    }

    public static void Run()
    {
        ScreenName? currentScreen = ScreenName.Home;

        while (currentScreen is not null &&
                currentScreen != ScreenName.StartGame &&
                currentScreen != ScreenName.ConfirmExit)
        {
            Home? page = GetPage(currentScreen);
            if (page is null) break;
            currentScreen = HomePage.GetMainMenuSelection(page.Prompt, page.MenuOptions, page.Routes);
        }
    }

    private static Home? GetPage(ScreenName? screenName)
    {
        switch (screenName)
        {
            case ScreenName.Home:
                return Index;

            case ScreenName.Play:
                return Play;

            case ScreenName.About:
                return About;

            case ScreenName.Exit:
                return Exit;

            case ScreenName.ConfirmExit:
                System.Environment.Exit(0);
                return null;

            case ScreenName.StartGame:
                // Exit loop pass control back to Program.cs.
                return null;

            case ScreenName.Exercism:
                OpenUrl("https://exercism.org/tracks/csharp/exercises/dominoes");
                return About;

            case ScreenName.GitHub:
                OpenUrl("https://github.com/DrBenSullivan");
                return About;

            case ScreenName.LinkedIn:
                OpenUrl("https://www.linkedin.com/in/drbensullivan/");
                return About;

            default:
                throw new ArgumentException("Invalid ScreenName provided");
        }
    }

    private static readonly Home Index = new(
        "Welcome to Dominoes, please select an option:",
        ["Play", "About", "Exit Game"],
        [ScreenName.Play, ScreenName.About, ScreenName.Exit]
    );

    private static readonly Home Play = new(
        "Are you ready to start a new game?",
        ["Yes, Let's Play!", "No, Return To Main Menu"],
        [ScreenName.StartGame, ScreenName.Home]
    );

    private static readonly Home About = new(
        @"Thanks for checking out DOMINOES, inspired by Erik Schierboom's
practice project,available on Exercism:
https://exercism.org/tracks/csharp/exercises/dominoes 

If you have any feedback or would like to connect, please feel
free to visit my profiles:

- GitHub:   https://github.com/DrBenSullivan/
- LinkedIn: https://linkedin.com/in/DrBenSullivan/

Thanks again,
Ben
",
        ["Return to Main Menu", "Open Exercism", "Open GitHub", "Open LinkedIn"],
        [ScreenName.Home, ScreenName.Exercism, ScreenName.GitHub, ScreenName.LinkedIn]
    );

    private static readonly Home Exit = new(
        "Are you sure you want to exit the game?",
        ["No, Return to Main Menu", "Yes, Exit Dominoes"],
        [ScreenName.Home, ScreenName.ConfirmExit,]
    );
}
