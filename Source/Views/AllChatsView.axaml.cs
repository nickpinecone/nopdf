using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Material.Styles.Controls;
using ReactiveUI;
using Robochat.Models;
using Robochat.ViewModels;

namespace Robochat.Views;

public partial class AllChatsView : ReactiveUserControl<AllChatsViewModel>
{
    public AllChatsView()
    {
        InitializeComponent();

        this.WhenActivated(disposables => { });
    }

    public void Chat_Click(object sender, RoutedEventArgs args)
    {
        if (sender is Control control && control.DataContext is ChatDto chat)
        {
            ViewModel?.RouteToChat(chat.Id);
        }
    }
}