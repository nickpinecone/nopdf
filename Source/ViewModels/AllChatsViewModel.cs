using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Robochat.Models;
using Robochat.Services;

namespace Robochat.ViewModels;

public class AllChatsViewModel : ReactiveObject, IActivatableViewModel, IRoutableViewModel
{
    public ViewModelActivator Activator { get; }
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "all-chats";

    public ObservableCollection<ChatDto> Chats { get; private set; } = new();

    private readonly ChatService _chatService;
    private readonly Mapper _mapper;

    public AllChatsViewModel(ChatService chatService, Mapper mapper, ViewModelActivator activator)
    {
        _chatService = chatService;
        _mapper = mapper;

        Activator = activator;
        HostScreen = ServiceManager.ServiceProvider.GetRequiredService<MainWindowViewModel>();

        this.WhenActivated(async (CompositeDisposable disposables) =>
        {
            var chats = await _chatService.GetChats();

            Chats.Clear();
            Chats.AddRange(_mapper.Map(chats));

            Disposable
                .Create(() => { })
                .DisposeWith(disposables);
        });
    }

    public void RouteToChat(int chatId)
    {
        var chatView = ActivatorUtilities.CreateInstance<ChatViewModel>(ServiceManager.ServiceProvider, chatId);

        HostScreen.Router.Navigate.Execute(chatView);
    }
}