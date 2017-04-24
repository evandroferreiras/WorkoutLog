using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class TrainingDayDatabase
    {
        static IList<ITrainingDay> trainingDays = new List<ITrainingDay>();
        internal static void SaveTrainingDay(ITrainingDay trainingDay)
        {
            trainingDays.Add(trainingDay);
        }

        internal static ITrainingDay GetTrainingDay( int dayId, DateTime beginDate)
        {
            return trainingDays.First(x => x.DayId == dayId &&
                                           x.BeginDate == beginDate);

        }

        internal static ITrainingRoutineExercise GetTrainingRoutineExercise(int dayId, int routineId, DateTime dayAndHour, int exerciseId)
        {
            var trainingRoutine = GetTrainingRoutine( dayId, routineId, dayAndHour);
            return trainingRoutine.TrainingRoutineExercises.FirstOrDefault(x => x.ExerciseId == exerciseId);            
        }

        internal static void UpdateTrainingRoutine(int dayId, DateTime beginDate, int routineId, ITrainingRoutineExercise tre)
        {
            var trainingRoutine = GetTrainingRoutine(dayId, routineId, beginDate);
            var list = trainingRoutine.TrainingRoutineExercises.ToList();
            list.Remove(tre);
            list.Add(tre);
            trainingRoutine.UpdateTrainingRoutineExercises(list.ToArray());

        }

        internal static ITrainingRoutine GetTrainingRoutine(int dayId, int routineId, DateTime dayAndHour)
        {
            var trainingDay = GetTrainingDay( dayId, dayAndHour);
            return trainingDay.TrainingRoutines.FirstOrDefault(x => x.RoutineId == routineId);
        }
    }
}
