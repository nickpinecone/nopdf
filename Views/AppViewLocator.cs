using System;
using ReactiveUI;
using Robochat.ViewModels;

namespace Robochat.Views;

public class AppViewLocator : ReactiveUI.IViewLocator
{
    public IViewFor? ResolveView<T>(T? viewModel, string? contract) => viewModel switch
    {
        ChatViewModel context => new ChatView { DataContext = context },
        AllChatsViewModel context => new AllChatsView { DataContext = context },
        _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
    };
}