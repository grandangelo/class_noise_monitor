using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassNoiseMonitor
{
    class MainWindowVMMock
    {
        #region Private Members
        #endregion

        #region Public Members
        public int MediumNoiseThreshold { get; set; } = 10;
        public int HighNoiseThreshold { get; set; } = 40;
        public int RawVolume { get; set; } = 0;
        #endregion

        #region Events
        #endregion

        #region Constructor
        public MainWindowVMMock()
        {
            // Do nothing
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
