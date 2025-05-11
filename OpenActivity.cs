using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Pdf;
using Android.OS;
using Android.Widget;

namespace Nopdf;

[Activity(Label = "@string/open_activity_name")]
public sealed class OpenActivity : Activity
{
    private const int PickPdfRequest = 1;

    private ImageView _imageView = null!;
    
    private ParcelFileDescriptor? _fileDescriptor;
    private PdfRenderer? _pdfRenderer;
    private PdfRenderer.Page? _currentPage;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        SetContentView(Resource.Layout.activity_open);

        InitializeViews();
        AskForFile();
    }

    private void InitializeViews()
    {
        _imageView = FindViewById<ImageView>(Resource.Id.container) ?? throw new Exception();
    }

    private void AskForFile()
    {
        var intent = new Intent(Intent.ActionGetContent);
        intent.SetType("application/pdf");
        StartActivityForResult(intent, PickPdfRequest);
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
    {
        if (requestCode == PickPdfRequest && resultCode == Result.Ok && data is not null && data.Data is not null)
        {
            OpenPdf(data.Data);
        }
        else
        {
            GoBack();
        }
    }

    private void GoBack()
    {
        var intent = new Intent(this, typeof(MainActivity));
        StartActivity(intent);
    }
    
    private void OpenPdf(Android.Net.Uri uri)
    {
        try
        {
            CloseRenderer();

            _fileDescriptor = ApplicationContext?.ContentResolver?.OpenFileDescriptor(uri, "r") ?? throw new Exception();
            _pdfRenderer = new PdfRenderer(_fileDescriptor);

            ShowPage(0);
        }
        catch (Exception ex)
        {
            Toast.MakeText(ApplicationContext, "Error opening PDF: " + ex.Message, ToastLength.Long)?.Show();
        }
    }

    private void ShowPage(int index)
    {
        if (_pdfRenderer!.PageCount <= index) return;

        _currentPage?.Close();

        _currentPage = _pdfRenderer.OpenPage(index);

        var bitmap = Bitmap.CreateBitmap(
            _currentPage.Width,
            _currentPage.Height,
            Bitmap.Config.Argb8888!);

        _currentPage.Render(bitmap, null, null, PdfRenderMode.ForDisplay);
        
        _imageView.SetImageBitmap(bitmap);
    }

    private void CloseRenderer()
    {
        if (_currentPage != null)
        {
            _currentPage.Close();
            _currentPage = null;
        }
        
        if (_pdfRenderer != null)
        {
            _pdfRenderer.Close();
            _pdfRenderer = null;
        }
        
        if (_fileDescriptor != null)
        {
            try { _fileDescriptor.Close(); }
            catch
            {
                // ignored
            }

            _fileDescriptor = null;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        CloseRenderer();
    }
}