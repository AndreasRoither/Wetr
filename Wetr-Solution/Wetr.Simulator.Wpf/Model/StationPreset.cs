using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Cockpit.Wpf.Model
{
    public class StationPreset
    {
        public Station Station { get; } 

        private ObservableCollection<Preset> presets;

        public StationPreset(Station station)
        {
            this.Station = station;
        }

        public ObservableCollection<Preset> Presets
        {
            get { return presets; }
            set { presets = value; }
        }

        public void AddPreset(Preset preset)
        {
            presets.Add(preset);
        }

    }
}
