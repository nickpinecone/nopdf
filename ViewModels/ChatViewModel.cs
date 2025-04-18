using System.Collections.ObjectModel;
using System.Reactive.Disposables;
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

    private readonly IMessageRepository _messageRepository;
    private readonly Mapper _mapper;
    private readonly int _chatId;

    public ChatViewModel(IScreen screen, IMessageRepository messageRepository, Mapper mapper, int chatId)
    {
        Activator = new ViewModelActivator();

        HostScreen = screen;
        _messageRepository = messageRepository;
        _mapper = mapper;
        _chatId = chatId;

        this.WhenActivated(async (CompositeDisposable disposables) =>
        {
            var messages = await _messageRepository.GetMessages(_chatId);

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
}