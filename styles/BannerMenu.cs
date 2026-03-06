using System.Runtime.InteropServices.Marshalling;
using Spectre.Console;

namespace styles
{
    public static class BannerMenu
    {
        public static void ExibirBanner()
        {
            AnsiConsole.Write(
                new FigletText("Valoures Turismo&Câmbio")
                    //.LeftAligned()
                    .Color(Color.Green));
        }
    }
}