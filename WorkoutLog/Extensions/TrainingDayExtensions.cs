using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Training;
using WorkoutLog.Workout;

namespace WorkoutLog.Extensions
{
    public static class TrainingDayExtensions
    {
        public static ITrainingDay ToTrainingDay(this IDay d, TrainingIdentity tId) 
        {
            return new TrainingDay(tId,d.DayId, d.RoutineExercises.ToTrainingRoutineExercise());
        }

        public static ITrainingDay[] ToTrainingDay(this IDay[] d, TrainingIdentity tId)
        {
            var tds = new ITrainingDay[d.Length];
            var i = 0;
            foreach (var item in d)            
                tds[i++] = item.ToTrainingDay(tId);

            return tds;
            
        }
    }
}
