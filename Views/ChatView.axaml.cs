using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Robochat.ViewModels;

namespace Robochat.Views;

public partial class ChatView : ReactiveUserControl<ChatViewModel>
{
    public ChatView()
    {
        AvaloniaXamlLoader.Load(this);

        this.WhenActivated(disposables => { });
    }

    public void Button_Click(object sender, RoutedEventArgs args)
    {
        ViewModel?.RouteToAllChats();
    }
}