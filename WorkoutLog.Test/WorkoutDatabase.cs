using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutLog.Test
{
    internal class WorkoutDatabase
    {
        //TODO: Adicionar método de GetSet
        static IList<Workout> workouts = new List<Workout>();

        internal static void SaveWorkout(int workoutId, IDay[] days)
        {
            var workout = new Workout(workoutId, days);
            workouts.Add(workout);
        }
            
        internal static Workout GetWorkout(int workoutId) =>
            workouts.First(x => x.WorkoutId == workoutId);
            
    }
}