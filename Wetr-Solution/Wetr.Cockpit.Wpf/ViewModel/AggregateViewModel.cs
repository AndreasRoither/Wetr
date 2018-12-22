using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    public class AggregateViewModel
    {
        public SeriesCollection SeriesCollection { get; set; }
        public ObservableCollection<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public AggregateViewModel()
        {
            SeriesCollection = new SeriesCollection();
            Labels = new ObservableCollection<string>();
            YFormatter = value => value.ToString("");
        }
        
        public void AddToCollection(string name, double smoothness, double[] values)
        {
            SeriesCollection.Add(new LineSeries
            {
                Title = name,
                LineSmoothness = smoothness,
                Values = new ChartValues<double>(values)
            });
        }
    }
}
