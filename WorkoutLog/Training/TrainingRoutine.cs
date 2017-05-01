using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Training;

namespace WorkoutLog.Training
{
    public class TrainingRoutine : ITrainingRoutine
    {
        private readonly TrainingIdentity tId;
        private ITrainingDay[] trainingDays;

        public TrainingRoutine(TrainingIdentity tId, ITrainingDay[] td)
        {

            trainingDays = td;
            this.tId = tId;
        }

        public DateTime BeginDate
        {
            get
            {
                return tId.DayAndHour;
            }
        }

        public int RoutineId
        {
            get
            {
                return tId.WId.RoutineId;
            }
        }

        public ITrainingDay[] TrainingDays
        {
            get
            {
                return trainingDays;
            }
        }

        public void UpdateTrainingDays(ITrainingDay[] td)
        {
            this.trainingDays = td;
        }
    }
}
