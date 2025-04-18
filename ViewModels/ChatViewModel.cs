using ReactiveUI;
using Robochat.Data;
using Robochat.Services;

namespace Robochat.ViewModels;

public class ChatViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "chat";

    private readonly IChatRepository _chatRepository;
    private readonly Mapper _mapper;

    public ChatViewModel(IScreen screen, IChatRepository chatRepository, Mapper mapper)
    {
        HostScreen = screen;
        _chatRepository = chatRepository;
        _mapper = mapper;
    }

    public void RouteToAllChats()
    {
        HostScreen.Router.NavigateBack.Execute();
    }
}