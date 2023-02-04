using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassNoiseMonitor
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private Members
        #endregion

        #region Public Members
        public int CurrentVolume { get; set; }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            CurrentVolume = 10;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Members
        #endregion

        #region INotify Support
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
