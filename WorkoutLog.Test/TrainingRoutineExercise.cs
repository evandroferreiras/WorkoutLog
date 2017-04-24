using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    public class TrainingRoutineExercise : ITrainingRoutineExercise
    {
        private readonly int reps;
        private readonly int exerciseId;
        private int numberOfPendingRepetitions;

        public TrainingRoutineExercise(int exerciseId, int reps)
        {
            this.exerciseId = exerciseId;
            this.reps = reps;
            this.numberOfPendingRepetitions = reps;
        }

        public int ExerciseId
        {
            get
            {
                return exerciseId;
            }
        }

        public int NumberOfPendingRepetitions
        {
            get
            {
                return numberOfPendingRepetitions;
            }
        }

        public void DoRep()
        {

            if (numberOfPendingRepetitions == 0)
            {
                throw new ArgumentException("There is no pending exercises for this training. Go to the next.");
            }
            numberOfPendingRepetitions--;
        }
    }
}
