using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Robochat.ViewModels;

namespace Robochat.Views;

public partial class AllChatsView : ReactiveUserControl<AllChatsViewModel>
{
    public AllChatsView()
    {
        AvaloniaXamlLoader.Load(this);

        this.WhenActivated(disposables => { });
    }

    public void Card_PointerPressed(object sender, RoutedEventArgs args)
    {
        ViewModel?.RouteToChat();
    }
}