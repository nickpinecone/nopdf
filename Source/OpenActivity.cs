using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.Fragment.App;

namespace Oakay;


[Activity(Label = "@string/open_activity_name")]
public sealed class OpenActivity : AppCompatActivity
{
    private const int PickPdfRequest = 1;

    private PdfViewFragment _pdfViewFragment = null!;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        SetContentView(Resource.Layout.activity_open);

        InitializeViews();
        AskForFile();
    }

    private void InitializeViews()
    {
        _pdfViewFragment = new PdfViewFragment();
        var transaction = SupportFragmentManager.BeginTransaction();
        transaction.Replace(Resource.Id.container, _pdfViewFragment);
        transaction.Commit();
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
            _pdfViewFragment.OpenPdf(data.Data);
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
}