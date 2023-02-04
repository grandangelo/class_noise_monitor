using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassNoiseMonitor
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private Members
        private readonly Model _model;
        private int _currentVolume;
        #endregion

        #region Public Members
        public int CurrentVolume { get => _currentVolume; set { if (value == _currentVolume) return; _currentVolume = value; OnPropertyChanged(); } }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            _model = new Model();
            _model.UpdatedVolumeEvent += OnUpdatedVolumeEvent;
            CurrentVolume = 10;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void OnUpdatedVolumeEvent(object? sender, VolumeUpdateEvent e)
        {
            CurrentVolume = e.UpdateVolume;
        }
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
