using Dalamud.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReeNamererer
{
    [Serializable]
    public class Configuration : IPluginConfiguration, IDisposable
    {
        public int Version { get; set; } = 1;

        public List<Entry> Entries { get; set; } = new List<Entry>();

        public void Dispose()
        {
            
        }

        public void Save()
        {
            Service.PluginInterface.SavePluginConfig(this);
            Service.NamePlateGui.RequestRedraw();
        }
    }
}
