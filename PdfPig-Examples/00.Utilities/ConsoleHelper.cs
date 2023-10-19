using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microshaoft;

public static class ConsoleHelper
{
    public static void WriteHightLight
                        (
                            this TextWriter @this
                            , Action action
                            , ConsoleColor foregroundColor = ConsoleColor.Red
                            , ConsoleColor backgroundColor = ConsoleColor.Black
                            , string begin = "<<<<<<<<<<<<<<<<<<<<<<<<<<<<"
                            , string end = ">>>>>>>>>>>>>>>>>>>>>>>>>>>>"

                        )
    {
        Console.WriteLine(begin);
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        action();
        Console.ResetColor();
        Console.WriteLine(end);
    }
    public static void WriteHightLightLine
                    (
                        this TextWriter @this
                        , string text
                        , ConsoleColor foregroundColor = ConsoleColor.Red
                        , ConsoleColor backgroundColor = ConsoleColor.Black
                        , string begin = "<<<<<<<<<<<<<<<<<<<<<<<<<<<<"
                        , string end = ">>>>>>>>>>>>>>>>>>>>>>>>>>>>"

                    )
    {
        Console.WriteLine(begin);
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine(end);
    }
}
