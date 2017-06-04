using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;

namespace WorkoutLog.Training
{
    public class TrainingDay : ITrainingDay
    {
        private readonly TrainingIdentity _tId;
        private readonly int dayId;
        private ITrainingRoutineExercise[] tre;

        public TrainingDay(TrainingIdentity tId, int dayId, ITrainingRoutineExercise[] tre)
        {
            this.dayId = dayId;

            this.tre = tre;
            this._tId = tId;
        }

        public int DayId
        {
            get
            {
                return dayId;
            }
        }

        public DateTime BeginDate
        {
            get {
                return _tId.DayAndHour;
            }
        }

        public ITrainingRoutineExercise[] TrainingRoutineExercises
        {
            get
            {
                return tre;
            }
        }

        public ITrainingRoutineExercise GetNextExercise()
        {
            return TrainingDayDatabase.GetNextExercise(tre);
        }
    }
}
