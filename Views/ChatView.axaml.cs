using System;
using System.Collections.Specialized;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Robochat.ViewModels;

namespace Robochat.Views;

public class StatusToBrushConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() switch
        {
            Config.UserName => Brushes.LightBlue,
            _ => Brushes.White
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public partial class ChatView : ReactiveUserControl<ChatViewModel>
{
    public ChatView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            if (ViewModel is not null)
            {
                ViewModel.Messages.CollectionChanged += OnNewMessage;
            }
        });
    }

    private void OnNewMessage(object? sender, NotifyCollectionChangedEventArgs args)
    {
        ScrollArea.ScrollToEnd();
    }

    public void BackButton_Click(object sender, RoutedEventArgs args)
    {
        ViewModel?.RouteToAllChats();
    }

    public void SendMessage_Click(object sender, RoutedEventArgs args)
    {
        ViewModel?.SendMessage(MessageContent.Text!);
    }
}