using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Workout
{
    public class TrainingDay : ITrainingDay
    {
        private readonly int dayId;
        private ITrainingRoutineExercise[] tre;

        public TrainingDay(int dayId, ITrainingRoutineExercise[] tre)
        {
            this.dayId = dayId;

            this.tre = tre;            
        }

        public int DayId
        {
            get
            {
                return dayId;
            }
        }

        public ITrainingRoutineExercise[] TrainingRoutineExercises
        {
            get
            {
                return tre;
            }
        }
    }
}
