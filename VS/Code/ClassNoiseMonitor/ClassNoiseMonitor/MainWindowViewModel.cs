using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClassNoiseMonitor
{
    internal class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Private Members
        private const int _mediumNoiseThreshold = 10;
        private const int _highNoiseThreshold = 40;
        private static readonly string _lowNoiseMessage = "Ottimo!";
        private static readonly string _mediumNoiseMessage = "Ragazzi attenzione...";
        private static readonly string _highNoiseMessage = "SILENZIO!";
        private readonly Model _model;
        private readonly CancellationTokenSource _cts;
        private readonly Task _monitoringTask;
        private int _currentVolume;
        private string _labelContent;
        #endregion

        #region Public Members
        public int CurrentVolume { get => _currentVolume; set { if (value == _currentVolume) return; _currentVolume = value; OnPropertyChanged(); } }
        public string LabelContent { get => _labelContent; set { if (value == _labelContent) return; _labelContent = value; OnPropertyChanged(); } }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            _cts = new CancellationTokenSource();
            _model = new Model();
            _model.UpdatedVolumeEvent += OnUpdatedVolumeEvent;
            _monitoringTask = Task.Run(() => _model.StartMonitoring(_cts.Token));
            _labelContent = string.Empty;
            CurrentVolume = 0;
            LabelContent = string.Empty;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Command Support
        #endregion

        #region Private Methods
        private void OnUpdatedVolumeEvent(object? sender, VolumeUpdateEvent e)
        {
            CurrentVolume = e.UpdateVolume;
            LabelContent = GetLabelContent();
        }

        private string GetLabelContent()
        {
            return CurrentVolume switch
            {
                > _highNoiseThreshold => _highNoiseMessage,
                > _mediumNoiseThreshold => _mediumNoiseMessage,
                _ => _lowNoiseMessage,
            };
        }
        #endregion

        #region INotify Support
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region IDisposable Support
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _cts.Cancel();
                    _monitoringTask.Wait();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
