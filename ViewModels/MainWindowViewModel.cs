using System.Reactive.Disposables;
using ReactiveUI;
using Robochat.Data;
using Robochat.Services;

namespace Robochat.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IActivatableViewModel, IScreen
{
    public ViewModelActivator Activator { get; }
    public RoutingState Router { get; } = new RoutingState();

    public MainWindowViewModel()
    {
        Activator = new ViewModelActivator();

        this.WhenActivated((CompositeDisposable disposables) =>
        {
            Router.Navigate.Execute(new AllChatsViewModel(this));

            Disposable
                .Create(() => { })
                .DisposeWith(disposables);
        });
    }
}