using System.Runtime.InteropServices.Marshalling;
using Spectre.Console;

namespace styles
{
    public static class BannerMenu
    {
        public static void Banner_Menu()
        {
            AnsiConsole.Write(
                new FigletText("Valoures Turismo&Câmbio")
                    //.LeftAligned()
                    .Color(Color.Green));
        }
    }
}