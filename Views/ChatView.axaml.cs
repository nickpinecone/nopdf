using System.Collections.Specialized;
using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Robochat.ViewModels;

namespace Robochat.Views;

public partial class ChatView : ReactiveUserControl<ChatViewModel>
{
    public ChatView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            ScrollArea.ScrollToEnd();

            if (ViewModel is not null)
            {
                ViewModel.Messages.CollectionChanged += OnNewMessage;
            }
        });
    }

    private async Task SendMessage(string? content)
    {
        if (ViewModel is not null && !string.IsNullOrEmpty(content))
        {
            MessageContent.Text = "";
            await ViewModel.SendMessage(content);
        }
    }

    private void OnNewMessage(object? sender, NotifyCollectionChangedEventArgs args)
    {
        ScrollArea.ScrollToEnd();
    }

    public void BackButton_Click(object sender, RoutedEventArgs args)
    {
        ViewModel?.RouteToAllChats();
    }

    public async void SendMessage_Click(object sender, RoutedEventArgs args)
    {
        await SendMessage(MessageContent.Text);
    }

    public async void Input_KeyDown(object sender, KeyEventArgs args)
    {
        if (args.Key == Key.Enter)
        {
            await SendMessage(MessageContent.Text);
        }
    }
}