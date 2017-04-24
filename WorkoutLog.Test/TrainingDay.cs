using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class TrainingDay : ITrainingDay
    {
        private readonly DateTime beginDate;
        private readonly int dayId;
        private ITrainingRoutine[] trainingRoutines;

        public TrainingDay(int dayId, DateTime beginDate, IRoutine[] routines)
        {
            this.dayId = dayId;
            this.beginDate = beginDate;

            trainingRoutines = new ITrainingRoutine[routines.Length];
            var i = 0;
            foreach (var item in routines)
            {
                trainingRoutines[i++] = new TrainingRoutine(item.RoutineId, item.RoutineExercises);
            }
        }

        public DateTime BeginDate
        {
            get
            {
                return beginDate;
            }
        }

        public int DayId
        {
            get
            {
                return dayId;
            }
        }

        public ITrainingRoutine[] TrainingRoutines
        {
            get
            {
                return trainingRoutines;
            }
        }
    }
}
