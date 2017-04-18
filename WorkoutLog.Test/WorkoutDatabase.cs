using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutLog.Test
{
    internal class WorkoutDatabase
    {

        static IList<Workout> workouts = new List<Workout>();
        static IList<ITrainingDay> trainingDays = new List<ITrainingDay>();
        internal static void SaveWorkout(int workoutId, IDay[] days)
        {
            var workout = new Workout(workoutId, days);
            workouts.Add(workout);
        }
            
        internal static ITraining GetTraining(int workoutId, int dayId, int trainingId)
        {
            var workout = GetWorkout(workoutId);
            var day = workout.Days.First(x => x.DayId == dayId);
            var training = day.Trainings.First(x => x.TrainingId == trainingId);
            
            return training;
        }

        internal static ISet[] GetSets(int workoutId, int dayId, int trainingId)
        {
            var training = GetTraining(workoutId,dayId,trainingId);
            return training.Sets;
        }

        internal static Workout GetWorkout(int workoutId)
        {
            var workout = workouts.First(x => x.WorkoutId == workoutId);
            return workout;
        }

        internal static void SaveTrainingDay(ITrainingDay trainingDay)
        {
            trainingDays.Add(trainingDay);
        }

        internal static ITrainingDay GetTrainingDay(int workoutId, int dayId, int trainingId, DateTime beginDate)
        {
            return trainingDays.First(x => x.WorkoutId == workoutId &&
                                   x.DayId == dayId &&
                                   x.TrainingId == trainingId &&
                                   x.BeginDate == beginDate);

        }
    }
}