using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Training;
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

        public static ITrainingRoutine GetTrainingRoutine(TrainingIdentity tId)
        {
            return trs.First(x => x.RoutineId == tId.WId.RoutineId &&
                                           x.BeginDate == tId.DayAndHour);

        }

        public static ITrainingRoutineExercise GetTrainingRoutineExercise(TrainingIdentity tId, int exerciseId)
        {
            var tr = GetTrainingRoutine(tId);
            var td = tr.TrainingDays.FirstOrDefault(x => x.DayId == tId.WId.DayId);
            return td.TrainingRoutineExercises.FirstOrDefault(x => x.ExerciseId == exerciseId);
        }

        public static void UpdateTrainingRoutineExercise(TrainingIdentity tId, ITrainingRoutineExercise tre)
        {


        }

        internal static (int repNbr, double weight)[] DoRep((int repNbr, double weight)[] repsDone, double weight)
        {
            var list = repsDone.ToList();
            list.Add((repsDone.Count()+1, weight));

            return list.ToArray();

        }
    }
}
