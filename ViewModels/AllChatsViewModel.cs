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
    private readonly Mapper _mapper;

    public AllChatsViewModel(IScreen screen, IChatRepository chatRepository, Mapper mapper)
    {
        HostScreen = screen;
        _chatRepository = chatRepository;
        _mapper = mapper;

        Activator = new ViewModelActivator();

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
        HostScreen.Router.Navigate.Execute(new ChatViewModel(HostScreen, _chatRepository, _mapper));
    }
}