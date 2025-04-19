using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using Robochat.Data;
using Robochat.Models;
using Robochat.Services;

namespace Robochat.ViewModels;

public class ChatViewModel : ReactiveObject, IActivatableViewModel, IRoutableViewModel
{
    public ViewModelActivator Activator { get; }
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "chat";

    public ObservableCollection<MessageDto> Messages { get; private set; } = new();

    private UserDto? _user;
    public UserDto? User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }

    private readonly MessageService _messageService;
    private readonly ChatService _chatService;
    private readonly Mapper _mapper;
    private readonly int _chatId;

    public ChatViewModel(IScreen screen, int chatId)
    {
        HostScreen = screen;

        Activator = new ViewModelActivator();
        _messageService = new MessageService();
        _chatService = new ChatService();
        _mapper = new Mapper();

        _chatId = chatId;

        this.WhenActivated(async (CompositeDisposable disposables) =>
        {
            var chat = await _chatService.GetChatById(_chatId);
            User = _mapper.Map(chat!).Users.First();

            var messages = await _messageService.GetMessages(_chatId);

            Messages.Clear();
            Messages.AddRange(_mapper.Map(messages));

            Disposable
                .Create(() => { })
                .DisposeWith(disposables);
        });
    }

    public void RouteToAllChats()
    {
        HostScreen.Router.NavigateBack.Execute();
    }

    public void SendMessage(string content)
    {
        Messages.Add(new MessageDto()
        {
            Content = content,
            CreatedAt = DateTime.UtcNow.ToLocalTime(),
            User = User!,
        });
    }
}