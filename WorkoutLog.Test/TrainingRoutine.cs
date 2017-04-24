using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class TrainingRoutine : ITrainingRoutine
    {
        private readonly int routineId;
        private ITrainingRoutineExercise[] trainingRoutineExercises;

        public TrainingRoutine(int routineId, IRoutineExercise[] routineExercise)
        {
            this.routineId = routineId;
            trainingRoutineExercises = new ITrainingRoutineExercise[routineExercise.Length];
            var i = 0;
            foreach (var item in routineExercise)
            {
                trainingRoutineExercises[i++] = new TrainingRoutineExercise(item.ExerciseId, item.Reps);               
            }
        }

        public int RoutineId
        {
            get
            {
                return routineId;
            }
        }

        public ITrainingRoutineExercise[] TrainingRoutineExercises
        {
            get
            {
                return trainingRoutineExercises;
            }
        }

        public void UpdateTrainingRoutineExercises(ITrainingRoutineExercise[] trainingRoutineExercises)
        {
            this.trainingRoutineExercises = trainingRoutineExercises;
        }
    }
}
