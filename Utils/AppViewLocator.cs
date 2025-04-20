using System;
using ReactiveUI;
using Robochat.ViewModels;
using Robochat.Views;

namespace Robochat.Utils;

public class AppViewLocator : IViewLocator
{
    public IViewFor? ResolveView<T>(T? viewModel, string? contract) => viewModel switch
    {
        ChatViewModel context => new ChatView { DataContext = context },
        AllChatsViewModel context => new AllChatsView { DataContext = context },
        _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
    };
}