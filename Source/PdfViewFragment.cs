using System;
using Android.Graphics;
using Android.Graphics.Pdf;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;

namespace Oakay;

public class PdfViewFragment : Fragment
{
    private ImageView? _imageView;
    private ParcelFileDescriptor? _fileDescriptor;
    private PdfRenderer? _pdfRenderer;
    private PdfRenderer.Page? _currentPage;

    public override View? OnCreateView(LayoutInflater? inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        var view = inflater?.Inflate(Resource.Layout.fragment_pdf, container, false); 
        _imageView = view?.FindViewById<ImageView>(Resource.Id.image_view);
        return view;
    }
    
    public void OpenPdf(Android.Net.Uri uri)
    {
        try
        {
            CloseRenderer();

            _fileDescriptor = Context?.ContentResolver?.OpenFileDescriptor(uri, "r") ?? throw new Exception();
            _pdfRenderer = new PdfRenderer(_fileDescriptor);

            ShowPage(0);
        }
        catch (Exception ex)
        {
            Toast.MakeText(Context, "Error opening PDF: " + ex.Message, ToastLength.Long)?.Show();
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
        
        _imageView?.SetImageBitmap(bitmap);
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

    public override void OnDestroy()
    {
        base.OnDestroy();
        CloseRenderer();
    }
}