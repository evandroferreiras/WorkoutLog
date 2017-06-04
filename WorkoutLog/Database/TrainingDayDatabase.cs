﻿using System;
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
        public static void Clear()
        {
            tds.Clear();
        }

        static IList<ITrainingDay> tds = new List<ITrainingDay>();
        public static void SaveTrainingDay(ITrainingDay td)
        {
            tds.Add(td);
        }

        public static ITrainingRoutineExercise GetTrainingRoutineExercise(TrainingIdentity tId, int exerciseId)
        {
            var td = GetTrainingDay(tId);
            return td.TrainingRoutineExercises.FirstOrDefault(x => x.ExerciseId == exerciseId);
        }

        public static ITrainingDay GetTrainingDay(TrainingIdentity tId)
        {
            return tds.FirstOrDefault(x => x.DayId == tId.WId.DayId);
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

        internal static ITrainingRoutineExercise GetNextExercise(ITrainingRoutineExercise[] tre)
        {
            var list = tre.ToList();
            return list.FirstOrDefault(x => !x.ExerciseFinished);
        }
    }
}
