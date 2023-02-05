using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassNoiseMonitor
{
    internal class VolumeUpdateEvent : EventArgs
    {
        #region Private Members
        #endregion

        #region Public Members
        public int UpdatedVolume { get; private set; }
        public float RawVolume { get; private set; }
        #endregion

        #region Constructor
        public VolumeUpdateEvent(int updatedVolume, float rawVolume)
        {
            UpdatedVolume = updatedVolume;
            RawVolume = rawVolume;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
