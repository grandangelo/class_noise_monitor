using NAudio.CoreAudioApi;
using NAudio.Wave;
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
        private readonly WaveInEvent _waveIn;
        #endregion

        #region Public Members
        #endregion

        #region Events
        public EventHandler<VolumeUpdateEvent>? UpdatedVolumeEvent;
        #endregion

        #region Constructor
        public Model()
        {
            _waveIn = new WaveInEvent();
        }
        #endregion

        #region Public Methods
        public void StartMonitoring()
        {
            _waveIn.DataAvailable += OnDataAvailable;
            _waveIn.StartRecording();
        }
        #endregion

        #region Private Methods
        private void OnDataAvailable(object? sender, WaveInEventArgs e)
        {
            float max = 0;
            var buffer = new WaveBuffer(e.Buffer);
            for (int index = 0; index < e.BytesRecorded / 4; index++)
            {
                var sample = buffer.FloatBuffer[index];

                if (sample < 0) sample = -sample;
                if (sample > max) max = sample;
            }

            UpdatedVolumeEvent?.Invoke(this, new VolumeUpdateEvent((int)(max * 100), max));
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
                    _waveIn?.StopRecording();
                    _waveIn?.Dispose();
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
