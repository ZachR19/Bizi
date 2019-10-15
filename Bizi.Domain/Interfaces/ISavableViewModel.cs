using System.Windows.Input;

namespace Bizi.Domain
{
    public interface ISavableViewModel
    {
        ICommand SaveCommand { get; }
    }
}
