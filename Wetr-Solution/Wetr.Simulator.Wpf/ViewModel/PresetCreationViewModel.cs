using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Wetr.BusinessLogic;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;
using Wetr.Simulator.Wpf.Interface;
using Wetr.Simulator.Wpf.Model;

namespace Wetr.Simulator.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the PresetCreationView can bind to
    /// </summary>
    /// <seealso cref="Wetr.Simulator.Wpf.Views.PresetCreationView"/>
    /// <seealso cref="Wetr.Simulator.Wpf.Interface.IWetrViewModelBase"/>
    public class PresetCreationViewModel : ViewModelBase, IWetrViewModelBase
    {
        #region variables

        public Collection<MeasurementType> MeasurementTypeList { get; set; }
        public MeasurementType SelectedMeasurementType { get; set; }
        public Preset SelectedPreset { get; set; }

        public Frequency SelectedFrequency { get; set; }
        public Distribution SelectedDistribution { get; set; }

        private SolidColorBrush presetNameColor;

        public SolidColorBrush PresetNameColor
        {
            get
            {
                return presetNameColor;
            }
            set
            {
                if (presetNameColor != value)
                    Set(ref presetNameColor, value);
            }
        }

        private string addWarningText;

        public string AddWarningText
        {
            get { return addWarningText; }
            set
            {
                Set(ref addWarningText, value);
            }
        }

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                    Set(ref startDate, value);
            }
        }

        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                    Set(ref endDate, value);
            }
        }

        private double minVal;

        public double MinVal
        {
            get { return minVal; }
            set
            {
                if (minVal != value)
                    Set(ref minVal, value);
            }
        }

        private string presetName;

        public string PresetName
        {
            get { return presetName; }
            set
            {
                if (presetName != value)
                    Set(ref presetName, value);
            }
        }

        private double maxVal;

        public double MaxVal
        {
            get { return maxVal; }
            set
            {
                if (maxVal != value)
                    Set(ref maxVal, value);
            }
        }

        private ObservableCollection<Preset> presetList;

        public ObservableCollection<Preset> PresetList
        {
            get { return presetList; }
            set
            {
                if (presetList != value)
                    Set(ref presetList, value);
            }
        }

        #endregion variables

        #region commands

        public RelayCommand AddPreset { get; private set; }
        public RelayCommand DeletePreset { get; private set; }

        private bool CanExecuteAddPreset()
        {
            /* TODO: Add constraint */
            return true;
        }

        /// <summary>
        /// AddPreset Command Execution
        /// <para>Adds preset to preset list</para>
        /// </summary>
        private void ExecuteAddPreset()
        {
            if (this.PresetName != null && this.PresetName != String.Empty)
            {
                this.PresetNameColor = Brushes.Black;
                this.AddWarningText = "";
                this.PresetList.Add(new Preset()
                {
                    Id = Preset.NextInt(),
                    Name = this.PresetName,
                    EndDate = this.EndDate,
                    StartDate = this.StartDate,
                    Frequency = this.SelectedFrequency,
                    Distribution = this.SelectedDistribution,
                    MeasurementType = this.SelectedMeasurementType,
                    MinValue = this.MinVal,
                    MaxValue = this.MaxVal
                });
                this.PresetName = String.Empty;
                this.DeletePreset.RaiseCanExecuteChanged();
            }
            else
            {
                this.PresetNameColor = Brushes.Red;
                this.AddWarningText = "Missing name";
            }
        }

        private bool CanExecuteDeletePreset()
        {
            return this.PresetList.Count > 0;
        }

        /// <summary>
        /// Removes preset from the preset list
        /// </summary>
        private void ExecuteDeletePreset()
        {
            if (this.SelectedPreset == null)
                return;
            this.PresetList.Remove(this.SelectedPreset);
            this.DeletePreset.RaiseCanExecuteChanged();
        }

        #endregion commands

        public PresetCreationViewModel()
        {
            presetNameColor = Brushes.Black;
            this.MeasurementTypeList = new Collection<MeasurementType>();

            /* Loading measurementtypes from db */
            IMeasurementTypeDao measurementDao = AdoFactory.Instance.GetMeasurementTypeDao("wetr");
            IEnumerable<MeasurementType> measurementTypes = measurementDao.FindAllAsync().Result;
            foreach (MeasurementType t in measurementTypes)
            {
                this.MeasurementTypeList.Add(t);
            }
            this.SelectedMeasurementType = this.MeasurementTypeList[0];

            /* Init Dates */
            this.StartDate = DateTime.Now;
            this.endDate = DateTime.Now.AddMonths(2);

            /* Init preset list */
            this.PresetList = new ObservableCollection<Preset>();

            /* Add preset command */
            AddPreset = new RelayCommand(
                ExecuteAddPreset,
                CanExecuteAddPreset
            );

            /* Remove preset command */
            DeletePreset = new RelayCommand(
                ExecuteDeletePreset,
                CanExecuteDeletePreset
            );
        }

        public void CleanUp()
        {
            base.Cleanup();
        }
    }
}