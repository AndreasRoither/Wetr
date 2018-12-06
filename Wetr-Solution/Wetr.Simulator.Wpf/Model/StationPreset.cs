using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Cockpit.Wpf.Model
{
    public class StationPreset
    {
        private ObservableCollection<Preset> presets;

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
