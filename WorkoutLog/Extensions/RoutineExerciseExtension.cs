using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Workout;

namespace WorkoutLog.Extensions
{
    public static class RoutineExerciseExtension
    {
        public static ITrainingRoutineExercise ToTrainingRoutineExercise(this IRoutineExercise re ) 
        {
            return new TrainingRoutineExercise(re.ExerciseId, re.Reps);
        }

        public static ITrainingRoutineExercise[] ToTrainingRoutineExercise(this IRoutineExercise[] re)
        {
            var tre = new ITrainingRoutineExercise[re.Length];
            var i = 0;
            foreach (var item in re)
                tre[i++] = item.ToTrainingRoutineExercise();

            return tre;
        }
    }
}
