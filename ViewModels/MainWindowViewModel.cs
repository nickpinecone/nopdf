using System.Reactive.Disposables;
using ReactiveUI;
using Robochat.Data;
using Robochat.Services;

namespace Robochat.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IActivatableViewModel, IScreen
{
    public ViewModelActivator Activator { get; }
    public RoutingState Router { get; } = new RoutingState();

    private readonly IChatRepository _chatRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly Mapper _mapper;

    public MainWindowViewModel(IChatRepository chatRepository, Mapper mapper, IMessageRepository messageRepository)
    {
        _chatRepository = chatRepository;
        _messageRepository = messageRepository;
        _mapper = mapper;

        Activator = new ViewModelActivator();

        this.WhenActivated((CompositeDisposable disposables) =>
        {
            Router.Navigate.Execute(new AllChatsViewModel(this, _chatRepository, _mapper, _messageRepository));

            Disposable
                .Create(() => { })
                .DisposeWith(disposables);
        });
    }
}