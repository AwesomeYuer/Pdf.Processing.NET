namespace UglyToad.Examples;

using System;
using System.Text;
using Microshaoft;
using PdfPig;
using PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;

public static class OpenDocumentAndExtractWords
{
    public static void Run(string filePath)
    {
        filePath = @"D:\MyGitHub\PyMuPDF-Utilities\00.MCD\pdfs\智能取餐柜营运操作手册.pdf";
        var sb = new StringBuilder();

        using (var document = PdfDocument.Open(filePath))
        {
            foreach (var page in document.GetPages())
            {
                Console.WriteLine($"<<<<<<<<<<<<<<Page {page.Number}");
                var words = page.GetWords();
                var blocks = RecursiveXYCut.Instance.GetBlocks(words);
                var i = 0;
                foreach (var block in blocks)
                {
                    i++;
                    Console.WriteLine($"block: {i}");
                    Console.WriteLine(block.Text);
                    Console.WriteLine("==============================");
                }
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>");
            }
        }
        Console.Out.WriteHightLightLine(sb.ToString());
        //Console.WriteLine(sb.ToString());
    }


    public static void Run2(string filePath)
    {
        var sb = new StringBuilder();

        using (var document = PdfDocument.Open(filePath))
        {
            Word previous = null;
            foreach (var page in document.GetPages())
            {
                foreach (var word in page.GetWords())
                {
                    if (previous != null)
                    {
                        var hasInsertedWhitespace = false;
                        var bothNonEmpty = previous.Letters.Count > 0 && word.Letters.Count > 0;
                        if (bothNonEmpty)
                        {
                            var prevLetter1 = previous.Letters[0];
                            var currentLetter1 = word.Letters[0];

                            var baselineGap = Math.Abs(prevLetter1.StartBaseLine.Y - currentLetter1.StartBaseLine.Y);

                            if (baselineGap > 3)
                            {
                                hasInsertedWhitespace = true;
                                sb.AppendLine();
                            }
                        }

                        if (!hasInsertedWhitespace)
                        {
                            sb.Append(" ");
                        }
                    }

                    sb.Append(word.Text);

                    //if (word.Text == "\n")
                    {
                        Console.Write(word.Text);

                    }


                    previous = word;
                }
            }
        }
        Console.Out.WriteHightLightLine(sb.ToString());
        //Console.WriteLine(sb.ToString());
    }
}
