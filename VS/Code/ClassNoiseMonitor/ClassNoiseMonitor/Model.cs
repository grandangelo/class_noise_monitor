using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClassNoiseMonitor
{
    internal class Model : IDisposable
    {
        #region Private Members
        private static readonly int _updatePeriod_ms = 250;
        private int _maxVolume;
        private MMDevice? _microphone;
        #endregion

        #region Public Members
        #endregion

        #region Events
        public EventHandler<VolumeUpdateEvent>? UpdatedVolumeEvent;
        #endregion

        #region Constructor
        public Model()
        {

        }
        #endregion

        #region Public Methods
        public void StartMonitoring(CancellationToken ct)
        {
            var sw = new Stopwatch();
            sw.Start();
            GetMicrophoneDevice();
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    var currentVolume = (_microphone?.AudioMeterInformation.MasterPeakValue * 100) ?? int.MaxValue;
                    _maxVolume = Math.Max(_maxVolume, (int)currentVolume);
                    if (sw.ElapsedMilliseconds > _updatePeriod_ms)
                    {
                        UpdatedVolumeEvent?.Invoke(this, new VolumeUpdateEvent(_maxVolume));
                        _maxVolume = 0;
                        sw.Restart();
                    }
                }
                catch
                {

                }
            }
        }
        #endregion

        #region Private Methods
        private void GetMicrophoneDevice()
        {
            var deviceEnumerator = new MMDeviceEnumerator();
            var devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            _microphone = devices.First(d => d.DataFlow == DataFlow.Capture);
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
                    _microphone?.Dispose();
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
