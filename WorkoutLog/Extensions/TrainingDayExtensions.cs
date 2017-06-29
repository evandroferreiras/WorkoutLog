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
        public static ITrainingDay ToTrainingDay(this IDay d, DateTime dayAndHour) 
        {
            return new TrainingDay(dayAndHour, d.DayOfWeek, d.RoutineExercises.ToTrainingRoutineExercise());
        }

        public static ITrainingDay[] ToTrainingDay(this IDay[] d, DateTime dayAndHour)
        {
            var tds = new ITrainingDay[d.Length];
            var i = 0;
            foreach (var item in d)            
                tds[i++] = item.ToTrainingDay(dayAndHour);

            return tds;
            
        }
    }
}
