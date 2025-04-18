using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using DynamicData;
using ReactiveUI;
using Robochat.Data;
using Robochat.Models;
using Robochat.Services;

namespace Robochat.ViewModels;

public class AllChatsViewModel : ReactiveObject, IActivatableViewModel, IRoutableViewModel
{
    public ViewModelActivator Activator { get; }
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "all-chats";

    public ObservableCollection<ChatDto> Chats { get; private set; } = new();

    private readonly IChatRepository _chatRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly Mapper _mapper;

    public AllChatsViewModel(IScreen screen, IChatRepository chatRepository, Mapper mapper, IMessageRepository messageRepository)
    {
        Activator = new ViewModelActivator();

        HostScreen = screen;
        _chatRepository = chatRepository;
        _messageRepository = messageRepository;
        _mapper = mapper;

        this.WhenActivated(async (CompositeDisposable disposables) =>
        {
            var chats = await _chatRepository.GetChats();

            Chats.Clear();
            Chats.AddRange(_mapper.Map(chats));

            Disposable
                .Create(() => { })
                .DisposeWith(disposables);
        });
    }

    public void RouteToChat()
    {

        HostScreen.Router.Navigate.Execute(new ChatViewModel(HostScreen, _messageRepository, _mapper, 1));
    }
}