using System.Windows.Input;

namespace Bizi.Domain
{
    public interface ILoadableViewModel
    {
        bool IsLoaded { get; }

        ICommand LoadCommand { get; }
        ICommand RefreshCommand { get; }
    }
}
