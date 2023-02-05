using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassNoiseMonitor
{
    internal class CfgManager
    {
        #region Private Members
        private static readonly string _cfgFileName = "ClassNoiseMonitorCfg.xml";
        #endregion

        #region Public Members
        public int LowNoiseMaxValue { get; set; }
        public int MediumNoiseMaxValue { get; set; }
        public string LowNoiseMessage { get; set; } = string.Empty;
        public string MediumNoiseMessage { get; set; } = string.Empty;
        public string HighNoiseMessage { get; set; } = string.Empty;
        #endregion

        #region Events
        #endregion

        #region Constructor
        public CfgManager()
        {

        }
        #endregion

        #region Public Methods
        public void ReadCfg()
        {
            XDocument doc = XDocument.Load(_cfgFileName);
            var levels = doc?.Element("root")?.Element("levels");
            LowNoiseMaxValue = Convert.ToInt32(levels?.Element("low")?.Attribute("max_value")?.Value ?? int.MaxValue.ToString());
            MediumNoiseMaxValue = Convert.ToInt32(levels?.Element("medium")?.Attribute("max_value")?.Value ?? int.MaxValue.ToString());
            LowNoiseMessage = levels?.Element("low")?.Attribute("message")?.Value ?? "Invalid Message";
            MediumNoiseMessage = levels?.Element("medium")?.Attribute("message")?.Value ?? "Invalid Message";
            HighNoiseMessage = levels?.Element("high")?.Attribute("message")?.Value ?? "Invalid Message";
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
