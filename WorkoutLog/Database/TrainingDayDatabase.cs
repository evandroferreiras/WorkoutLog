using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Workout;

namespace WorkoutLog.Database
{
    public class TrainingDayDatabase
    {
        static IList<ITrainingRoutine> trs = new List<ITrainingRoutine>();
        public static void SaveTrainingRoutine(ITrainingRoutine tr)
        {
            trs.Add(tr);
        }

        public static ITrainingRoutine GetTrainingRoutine( int routineId, DateTime beginDate)
        {
            return trs.First(x => x.RoutineId == routineId &&
                                           x.BeginDate == beginDate);

        }

        public static ITrainingRoutineExercise GetTrainingRoutineExercise(int routineId, DateTime dayAndHour, int dayId, int exerciseId)
        {
            var tr = GetTrainingRoutine(routineId, dayAndHour);
            var td = tr.TrainingDays.FirstOrDefault(x => x.DayId == dayId);
            return td.TrainingRoutineExercises.FirstOrDefault(x => x.ExerciseId == exerciseId);
        }

        public static void UpdateTrainingRoutineExercise(int dayId, DateTime beginDate, int routineId, ITrainingRoutineExercise tre)
        {


        }

    }
}
