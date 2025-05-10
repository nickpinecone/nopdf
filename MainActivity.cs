using System.Text;
using Android.App;
using Android.OS;
using Android.Widget;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace Nopdf;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        var textView = new TextView(this);

        using var stream = Assets!.Open("sample.pdf");
        var pdfDoc = new PdfDocument(new PdfReader(stream));

        var text = new StringBuilder();
        var strategy = new SimpleTextExtractionStrategy();

        for (var i = 0; i < pdfDoc.GetNumberOfPages(); i++)
        {
            var page = pdfDoc.GetPage(i + 1);
            var current = PdfTextExtractor.GetTextFromPage(page, strategy);
            text.Append(current);
        }

        textView.Text = text.ToString();

        SetContentView(textView);
    }
}