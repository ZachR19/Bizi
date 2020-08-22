using Stylet;
using System;
using System.ComponentModel;

namespace Bizi.UI
{
    /// <summary>
    /// Disposable wrapper for any viewmodel
    /// </summary>
    public class BaseViewModel : Screen, IDisposable, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BaseViewModel"/>
        /// </summary>
        public BaseViewModel()
        {
        }

        public bool IsBusy { get; set; }

        protected bool Disposed { get; set; }

        /// <summary>
        /// Releases unmanaged objects from memory
        /// </summary>
        public void Dispose()
        {
            if (Disposed)
                return;

            //Free unmanaged objects here

            Disposed = true;

            GC.SuppressFinalize(this);
        }
    }
}
