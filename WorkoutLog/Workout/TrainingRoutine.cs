using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Workout
{
    public class TrainingRoutine : ITrainingRoutine
    {
        private DateTime beginDate;
        private readonly int routineId;
        private ITrainingDay[] trainingDays;

        public TrainingRoutine(int routineId, DateTime beginDate, ITrainingDay[] td)
        {
            this.routineId = routineId;
            this.beginDate = beginDate;
            trainingDays = td;
        }

        public DateTime BeginDate
        {
            get
            {
                return beginDate;
            }
        }

        public int RoutineId
        {
            get
            {
                return routineId;
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
