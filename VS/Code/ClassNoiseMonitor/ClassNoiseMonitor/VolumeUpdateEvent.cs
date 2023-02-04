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
        public int UpdateVolume { get; private set; }
        #endregion

        #region Constructor
        public VolumeUpdateEvent(int updatedVolume)
        {
            UpdateVolume = updatedVolume;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
