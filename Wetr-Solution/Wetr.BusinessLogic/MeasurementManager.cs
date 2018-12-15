using System.Collections.Generic;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class MeasurementManager
    {
        public MeasurementManager()
        {
        }

        #region functions

        public IEnumerable<Measurement> GetAllMeasurements()
        {
            return null;
        }

        public IEnumerable<Measurement> GetAllMeasurementsForStation(int stationId)
        {
            return null;
        }

        public IEnumerable<MeasurementType> GetAllMesurementTypes()
        {
            return null;
        }

        public IEnumerable<Unit> GetAllUnitTypes()
        {
            return null;
        }

        #endregion functions
    }
}