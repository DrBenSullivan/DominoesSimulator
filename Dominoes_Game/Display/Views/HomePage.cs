using Dominoes_Game.Display.Components;

namespace Dominoes_Game.Display.Views;

public enum ScreenName
{
    Home,
    Play,
    StartGame,
    About,
    Exit,
    ConfirmExit
}

internal class HomePage
{
    public string Prompt { get; set; }
    public string[] MenuOptions { get; set; }
    public ScreenName[] Routes { get; set; }

    private HomePage(string prompt, string[] menuOptions, ScreenName[] routes)
    {
        Prompt = prompt;
        MenuOptions = menuOptions;
        Routes = routes;
    }

    public static void Display()
    {
        ScreenName? currentScreen = ScreenName.Home;

        while (currentScreen is not null && 
               currentScreen != ScreenName.StartGame &&
               currentScreen != ScreenName.ConfirmExit)
        {
            HomePage? page = GetPage(currentScreen);
            if (page is null) break;
            Action headerMethod = Headers.Home;
            currentScreen = Menu.GetMenuChoice(headerMethod, page.Prompt, page.MenuOptions, page.Routes);
        }
    }

    private static HomePage? GetPage(ScreenName? screenName)
    {
        switch (screenName)
        {
            case ScreenName.Home:
                return new HomePage
                (
                    "Welcome to Dominoes, please select an option:",
                    ["Play", "About", "Exit Game"],
                    [ScreenName.Play, ScreenName.About, ScreenName.Exit]
                );

            case ScreenName.Play:
                return new HomePage
                (
                    "Are you ready to start a new game?",
                    ["Yes, Let's Play!", "No, Return To Main Menu"],
                    [ScreenName.StartGame, ScreenName.Home]
                );

            case ScreenName.About:
                return new HomePage
                (
                    @"
Thanks for checking out my Dominoes Game!

It was inspired by Erik Schierboom's practice project,
available on Exercism:
https://exercism.org/tracks/csharp/exercises/dominoes 

If you have any feedback or would like to connect, please
feel free to visit my profiles:

- GitHub:   https://github.com/DrBenSulliv
- LinkedIn: https://linkedin.com/DrBenSullivan 

Thanks again,
Ben
",
                    ["Return to Main Menu"],
                    [ScreenName.Home]
                );

            case ScreenName.Exit:
                return new HomePage
                (
                    "Are you sure you want to exit the game?",
                    ["Yes, Exit Dominoes", "No, Return to Main Menu"],
                    [ScreenName.ConfirmExit, ScreenName.Home]
                );

            case ScreenName.ConfirmExit:
                System.Environment.Exit(0);
                return null;

            case ScreenName.StartGame:
                // Exit loop pass control back to Program.cs.
                return null;

            default:
                throw new ArgumentException("Invalid screen name");
        }
    }
}