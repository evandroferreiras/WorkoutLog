using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Workout;

namespace WorkoutLog.Extensions
{
    public static class TrainingDayExtensions
    {
        public static ITrainingDay ToTrainingDay(this IDay d) 
        {
            return new TrainingDay(d.DayId, d.RoutineExercises.ToTrainingRoutineExercise());
        }

        public static ITrainingDay[] ToTrainingDay(this IDay[] d)
        {
            var tds = new ITrainingDay[d.Length];
            var i = 0;
            foreach (var item in d)            
                tds[i++] = item.ToTrainingDay();

            return tds;
            
        }
    }
}
