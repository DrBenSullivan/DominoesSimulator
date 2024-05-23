using System.Diagnostics;

namespace System;

public static class StringExtensions
{
    public static string Centre(string source, int width = 61)
    {
        string s = source.Trim();
        int totalPadding = width - s.Length;
        int leftPad = totalPadding / 2;

        if (totalPadding % 2 != 0)
        {
            leftPad++;
        }

        return $"|{s.PadLeft(s.Length + leftPad).PadRight(width)}|";
    }
}

public static class ProcessExtensions
{
    public static void OpenUrl(string url)
    {
        try
        {
            ProcessStartInfo psi = new()
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening URL: {ex.Message}");
        }
    }
}
