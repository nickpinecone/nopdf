using System.Reactive.Disposables;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Robochat.Data;
using Robochat.Services;

namespace Robochat.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IActivatableViewModel, IScreen
{
    public ViewModelActivator Activator { get; }
    public RoutingState Router { get; }

    public MainWindowViewModel(ViewModelActivator activator, RoutingState router)
    {
        Activator = activator;
        Router = router;

        this.WhenActivated((CompositeDisposable disposables) =>
        {
            var allChats = ServiceManager.ServiceProvider.GetRequiredService<AllChatsViewModel>();

            Router.Navigate.Execute(allChats);

            Disposable
                .Create(() => { })
                .DisposeWith(disposables);
        });
    }
}