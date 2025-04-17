using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Robochat.Data;
using Robochat.Models;
using Robochat.Services;

namespace Robochat.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IActivatableViewModel
{
    public ViewModelActivator Activator { get; }

    public ObservableCollection<ChatDto> Chats { get; private set; } = new();

    private readonly IChatRepository _chatRepository;
    private readonly Mapper _mapper;

    public MainWindowViewModel(IChatRepository chatRepository, Mapper mapper)
    {
        _chatRepository = chatRepository;
        _mapper = mapper;

        Activator = new ViewModelActivator();

        this.WhenActivated(async (CompositeDisposable disposables) =>
        {
            var chats = await _chatRepository.GetChats();

            Chats.AddRange(_mapper.Map(chats));

            Disposable
                .Create(() => { })
                .DisposeWith(disposables);
        });
    }
}
