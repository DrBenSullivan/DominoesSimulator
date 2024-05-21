using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dominoes_Game.Display.Components;

namespace Dominoes_Game.Display.View;

public class MainMenu
{
    private Action DisplayHeader { get; }
    private string Prompt { get; }
    private string[] MenuOptions { get; }
    private ScreenName[] Routes { get; }

    internal MainMenu(ScreenName selectedScreen)
    {
        (Action headerMethod, string prompt, string[] menuOptions, ScreenName[] routes) = GetScreenData(selectedScreen);
        Prompt = prompt;
        MenuOptions = menuOptions;
        DisplayHeader = headerMethod;
        Routes = routes;
    }

    public static void Display()
    {
        ScreenName currentScreen = ScreenName.MainMenu;
        while (true)
        {
            currentScreen = DynamicScreenDisplay(currentScreen);
            switch (currentScreen)
            {
                case ScreenName.ConfirmExit:
                    System.Environment.Exit(0);
                    break;

                case ScreenName.StartGame:
                    return;

                default:
                    continue;
            }
        }
    }

    private static ScreenName DynamicScreenDisplay(ScreenName selectedScreen)
    {
        var screen = new MainMenu(selectedScreen);
        screen.DisplayHeader.Invoke();

        int selectedMenuOption = Menu.GetMenuChoice(
            screen.DisplayHeader,
            screen.Prompt,
            screen.MenuOptions
        ) ;

        return screen.Routes[selectedMenuOption];
    }

    private static (Action, string, string[], ScreenName[]) GetScreenData(ScreenName selectedScreen)
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = Path.Combine("Display", "Screens", "ScreenContent.json");
        string jsonFilePath = Path.Combine(basePath, relativePath);

        if (!File.Exists(jsonFilePath))
            throw new FileNotFoundException("ScreenContent.json not found.", jsonFilePath);

        string jsonString = File.ReadAllText(jsonFilePath);
        var screens = JsonConvert.DeserializeObject<JObject>(jsonString)
                      ?? throw new NullReferenceException("Error parsing `screens` from ScreenContent.json");
        var screen = screens[selectedScreen.ToString()] as JObject 
            ?? throw new NullReferenceException("Error parsing `screen` from ScreenContent.json");

        string header = screen["Header"]?.ToString()
                        ?? throw new NullReferenceException("Error parsing `header` from ScreenContent.json");

        Action headerMethod = header switch
        {
            "Home" => Headers.Home,
            "Subheader" => Headers.Subheader,
            _ => throw new ArgumentException("Unable to apply headerMethod in GetScreenData()"),
        };

        var promptArray = screen[nameof(Prompt)].ToObject<string[]>();
        string prompt = string.Join("\n", promptArray);

        string[] menuOptions = screen[nameof(MenuOptions)].ToObject<string[]>();
        ScreenName[] routes = screen[nameof(Routes)].ToObject<ScreenName[]>();

        return (headerMethod, prompt, menuOptions, routes);
    }
}
































//try
//        {
//            jsonScreens = File.ReadAllText("ScreenContent.json")
//                          ?? throw new Exception("");

//            parsedJsonScreens = JsonNode.Parse(jsonScreens).AsArray()
//                                ?? throw new Exception("Invalid JSON format in ScreenContent.json.");

//            jsonScreen = parsedJsonScreens
//                .OfType<JsonObject>()
//                .FirstOrDefault(screen => screen["Name"]?.GetValue<string>() == selectedScreen.ToString())
//                         ?? throw new Exception($"Screen configuration for {selectedScreen} not found.");
//        }
//        catch (Exception ex)
//        {
//            throw new Exception("An error occurred while processing ScreenContent.json", ex);
//        }

//        return jsonScreen.AsObject();
//    }
//}


////            ?? 

////        if (jsonScreen["Prompt"]?.AsArray() is not JsonArray promptArray)
////        {
////            throw new InvalidOperationException("Invalid or missing 'Prompt' in JSON configuration.");
////        }

////        Prompt = string.Join("\n", promptArray.Select(p => p.GetValue<string>()));

////        // Ensure 'MenuOptions' is an array and not null
////        if (jsonScreen["MenuOptions"]?.AsArray() is not JsonArray menuOptionsArray)
////        {
////            throw new InvalidOperationException("Invalid or missing 'MenuOptions' in JSON configuration.");
////        }

////        MenuOptions = menuOptionsArray.Select(m => m.GetValue<string>()).ToArray();

////        // Ensure 'Header' is a valid string
////        string header = jsonScreen["Header"]?.GetValue<string>()
////                        ?? throw new InvalidOperationException("Invalid or missing 'Header' in JSON configuration.");

////        DisplayHeader = header switch
////        {
////            "Home" => Headers.Home,
////            "Subheader" => Headers.Subheader,
////            _ => throw new ArgumentOutOfRangeException(nameof(header), header, "Bad Header name provided in ScreenContent.json")
////        };
////        string jsonScreens = File.ReadAllText("\\ScreenContent.json") ?? throw new DirectoryNotFoundException("ScreenContent.json not found.");
////        JsonArray jsonScreensArray = JsonNode.Parse(jsonScreens).AsArray();
////        JsonObject jsonScreen = jsonScreensArray
////            .FirstOrDefault(screen => screen["Name"].GetValue<string>() == selectedScreen.ToString())
////            .AsObject();

////        Prompt = string.Join("\n", jsonScreen["Prompt"].AsArray().Select(p => p.GetValue<string>()));
////        MenuOptions = jsonScreen["MenuOptions"].AsArray().Select(m => m.GetValue<string>()).ToArray();
////        string header = jsonScreen["Header"].GetValue<string>();

////        DisplayHeader = header switch
////        {
////            "Home" => Headers.Home,
////            "Subheader" => Headers.Subheader,
////            _ => throw new ArgumentOutOfRangeException(nameof(header), header, "Bad Header name provided in ScreenContent.json")
////        }; 
////    }
////}

//////public class Screen(ScreenName selectedScreen)
//////{
//////    switch (selectedScreen)
//////    {
//////        case ScreenName.Home:
//////            Prompt = "Welcome to Dominoes, please select an option:";
//////            MenuOptions = ["Play", "About", "Exit Game"];
//////            DisplayHeader = Headers.Home;

//////        case ScreenName.About:
//////            Prompt = @" \n\n

//////\n
////// \n
//////-LinkedIn: https://linkedin.com/DrBenSullivan \n\n

//////Thanks again,\n
//////Ben\n\n

//////Press ENTER to return to Main Menu";
//////            MenuOptions = 
//////    }
//////    }
//////    internal class HomeScreen { }
//////        DisplayHeader = currentScreen switch
//////        {
//////            ScreenName.Home => Headers.Home,
//////            ScreenName.Play => Headers.Subheader,
//////            ScreenName.About => Headers.Subheader,
//////            ScreenName. Exit => Headers.Subheader
//////        };
//////        Prompt = prompt;
//////        MenuOptions = menuOptions;
//////        DisplayHeader = header switch
//////        {
//////            HeaderOption.MainHeader => Headers.Home,
//////            HeaderOption.Subheader => Headers.Subheader,
//////            _ => throw new ArgumentOutOfRangeException(nameof(header), header, null)
//////        };
//////    }

//////    public void Display()
//////    {
//////        Clear();
//////        DisplayHeader();

//////        int selectedMenuOption = Menu.GetSelectedMenuOption(
//////            DisplayHeader,
//////            Prompt,
//////            MenuOptions
//////        );

//////        switch (selectedMenuOption)
//////        {
//////            case 0:
//////                // play game();
//////                WriteLine("Play Game selected.");
//////                break;

//////            case 1:
//////                DisplayAboutScreen();
//////                break;

//////            case 2:
//////                Headers.Subheader();
//////                break;
//////        }
//////    }