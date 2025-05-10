using System;
using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Service.Autofill;
using Android.Util;
using Android.Views;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Colorspace;

namespace Nopdf;

public class AndroidTextExtractor : ITextExtractionStrategy
{
    public List<TextChunk> Chunks { get; } = new List<TextChunk>();

    public void EventOccurred(IEventData data, iText.Kernel.Pdf.Canvas.Parser.EventType type)
    {
        if (data is TextRenderInfo text)
        {
            var baseline = text.GetBaseline();
            Vector start = baseline.GetStartPoint();
            
            Chunks.Add(new TextChunk
            {
                Text = text.GetText(),
                X = start.Get(Vector.I1),
                Y = start.Get(Vector.I2),
                FontSize = text.GetFontSize(),
                Color = ToAndroidColor(text.GetFillColor())
            });
        }
    }

    private Color ToAndroidColor(iText.Kernel.Colors.Color pdfColor)
    {
        return Color.Black;
    }

    public ICollection<iText.Kernel.Pdf.Canvas.Parser.EventType> GetSupportedEvents() => null;
    public string GetResultantText() => string.Empty;
}

public class TextChunk
{
    public string Text { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float FontSize { get; set; }
    public Color Color { get; set; }
}

public class MyView : View
{
    protected MyView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }

    public MyView(Context? context, IAttributeSet? attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
    {
    }

    public MyView(Context? context, IAttributeSet? attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
    {
    }

    public MyView(Context? context, IAttributeSet? attrs) : base(context, attrs)
    {
    }

    public MyView(Context? context) : base(context)
    {
    }

    protected override void OnDraw(Canvas canvas)
    {
        base.OnDraw(canvas);

        using var pdfStream = Context.Assets.Open("sample.pdf");
        var pdfDoc = new PdfDocument(new PdfReader(pdfStream));

        var scaleX = canvas.Width / pdfDoc.GetFirstPage().GetPageSize().GetWidth();
        var scaleY = canvas.Height / pdfDoc.GetFirstPage().GetPageSize().GetHeight();
        
        for (var i = 0; i < pdfDoc.GetNumberOfPages(); i++)
        {
            var strategy = new AndroidTextExtractor();
            var page = pdfDoc.GetPage(i + 1);
            
            PdfTextExtractor.GetTextFromPage(page, strategy);
        
            float pageHeight = page.GetPageSize().GetHeight();
            Paint paint = new Paint { AntiAlias = true };
            paint.SetStyle(Paint.Style.Fill);
        
            foreach (var chunk in strategy.Chunks)
            {
                paint.Color = chunk.Color;
                paint.TextSize = 12f * scaleX;
        
                float androidY = pageHeight - chunk.Y;
        
                canvas.DrawText(chunk.Text, chunk.X * scaleX, androidY * scaleY, paint);
            }
        }
    }
}
