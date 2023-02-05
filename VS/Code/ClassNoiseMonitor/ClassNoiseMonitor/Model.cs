using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassNoiseMonitor
{
    internal class Model
    {
        #region Private Members
        MMDevice? _microphone;
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
            GetMicrophoneDevice();
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    UpdateMicrophoneVolume();
                    Thread.Sleep(10);
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

        private void UpdateMicrophoneVolume()
        {
            var currentVolume = (_microphone?.AudioMeterInformation.MasterPeakValue * 100) ?? int.MaxValue;
            UpdatedVolumeEvent?.Invoke(this, new VolumeUpdateEvent((int)currentVolume));
        }
        #endregion
    }
}
