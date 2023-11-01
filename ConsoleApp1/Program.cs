// See https://aka.ms/new-console-template for more information
using Tabula;
using Tabula.Detectors;
using Tabula.Extractors;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;

Console.WriteLine("Hello, World!");
var filePath = @"D:\MyGitHub\PyMuPDF-Utilities\00.MCD\pdfs\智能取餐柜营运操作手册.pdf";
filePath = @"D:\MyGitHub\PyMuPDF-Utilities\pdfs\Simple.Rowspan.for.Pdf.Print.pdf";

using (var pdfDocument = PdfDocument.Open(filePath, new ParsingOptions() { ClipPaths = true }))
{
    var objectExtractor = new ObjectExtractor(pdfDocument);
    foreach (var page in pdfDocument.GetPages())
    {
        var words = page.GetWords();
        var blocks = RecursiveXYCut.Instance.GetBlocks(words);
        var i = 0;
        foreach (var block in blocks)
        {
            i++;
            Console.WriteLine("==============================");
            Console.WriteLine($"block/page: {i}/{page.Number}");
            Console.WriteLine(block.Text);
            Console.WriteLine(block.BoundingBox);
            Console.WriteLine("==============================");
        }
        
        var pageArea = objectExtractor.Extract(page.Number);
        IExtractionAlgorithm extractionAlgorithm = new BasicExtractionAlgorithm();

        // detect canditate table zones
        var detectionAlgorithm = new SimpleNurminenDetectionAlgorithm();
        var tableRegions = detectionAlgorithm.Detect(pageArea);
        i = 0;
        foreach (var tableRegion in tableRegions)
        {
            var tables = extractionAlgorithm.Extract(pageArea.GetArea(tableRegion.BoundingBox));
            foreach (var table in tables)
            {
                i++;
                //Console.WriteLine("==============================");
                //Console.WriteLine($"Stream mode Basic table/page: {i}/{page.Number}");
                ////Console.WriteLine(table[0, 0].GetText());
                //Console.WriteLine(table.BoundingBox);
                //Console.WriteLine("==============================");
            }
        }
        //Console.ReadLine();
        {
            i = 0;
            extractionAlgorithm = new SpreadsheetExtractionAlgorithm();
            var tables = extractionAlgorithm.Extract(pageArea);
            foreach (var table in tables)
            {
                i++;
                //Console.WriteLine("==============================");
                //Console.WriteLine($"Lattice mode Spreadsheet table/page: {i}/{page.Number}");
                //Console.WriteLine(table[0,0].GetText());
                //Console.WriteLine(table.BoundingBox);
                //Console.WriteLine("==============================");

                

                foreach (var row in table.Rows)
                {
                    foreach (var cell in row)
                    {
                        Console.Write($"{cell.GetText(false)} | ");
                        //Console.Write("\t");
                    }
                    Console.WriteLine();
                }
            }
            //Console.ReadLine();
        }
        var pdfImages = page.GetImages();
        i = 0;
        foreach (var pdfImage in pdfImages)
        {
            i ++;
            if (!pdfImage.TryGetBytes(out var bytes))
            {
                bytes = pdfImage.RawBytes;
            }
            Console.WriteLine($"Images:{bytes.Count} {i}/{page.Number}");
        }
    }
}