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
        private readonly CfgManager _cfgManager;
        private readonly Model _model;
        private readonly List<int> _volumes;
        private int _currentVolume;
        private int _currentVolumeIndex;
        private float _rawVolume;
        private string _lowNoiseMessage = string.Empty;
        private string _mediumNoiseMessage = string.Empty;
        private string _highNoiseMessage = string.Empty;
        private string _labelContent = "No message";
        #endregion

        #region Public Members
        public int MediumNoiseThreshold { get; set; }
        public int HighNoiseThreshold { get; set; }
        public int CurrentVolume { get => _currentVolume; set { if (value == _currentVolume) return; _currentVolume = value; OnPropertyChanged(); } }
        public float RawVolume { get => _rawVolume; set { if (value == _rawVolume) return; _rawVolume = value; OnPropertyChanged(); } }
        public string LabelContent { get => _labelContent; set { if (value == _labelContent) return; _labelContent = value; OnPropertyChanged(); } }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            _cfgManager = new CfgManager();
            ReadCfg();
            _volumes = new List<int>(new int[_cfgManager.VolumeElements]);
            _model = new Model(_cfgManager);
            _model.UpdatedVolumeEvent += OnUpdatedVolumeEvent;
            _model.StartMonitoring();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Command Support
        #endregion

        #region Private Methods
        private void ReadCfg()
        {
            _cfgManager.ReadCfg();
            _lowNoiseMessage = _cfgManager.LowNoiseMessage;
            _mediumNoiseMessage = _cfgManager.MediumNoiseMessage;
            _highNoiseMessage = _cfgManager.HighNoiseMessage;
            MediumNoiseThreshold = _cfgManager.LowNoiseMaxValue;
            HighNoiseThreshold = _cfgManager.MediumNoiseMaxValue;
        }

        private void OnUpdatedVolumeEvent(object? sender, VolumeUpdateEvent e)
        {
            RawVolume = e.RawVolume;
            PopulateVolumes(e.UpdatedVolume);
            CurrentVolume = _volumes.Max();
            LabelContent = GetLabelContent();
        }

        private string GetLabelContent()
        {
            string label;

            if (CurrentVolume > HighNoiseThreshold)
            {
                label = _highNoiseMessage;
            }
            else if (CurrentVolume > MediumNoiseThreshold)
            {
                label = _mediumNoiseMessage;
            }
            else
            {
                label = _lowNoiseMessage;
            }

            return label;
        }

        private void PopulateVolumes(int volume)
        {
            _volumes[_currentVolumeIndex] = volume;
            _currentVolumeIndex++;
            if (_currentVolumeIndex >= _cfgManager.VolumeElements)
            {
                _currentVolumeIndex = 0;
            }
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
                    _model?.Dispose();
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
