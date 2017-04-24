using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutLog.Test
{
    internal class WorkoutDatabase
    {

        static IList<Workout> workouts = new List<Workout>();

        internal static void SaveWorkout(int workoutId, IDay[] days)
        {
            var workout = new Workout(workoutId, days);
            workouts.Add(workout);
        }
            
        internal static IRoutine GetRoutine(int workoutId, int dayId, int routineId)
        {
            var workout = GetWorkout(workoutId);
            var day = workout.Days.First(x => x.DayId == dayId);
            var routine = day.Routines.First(x => x.RoutineId == routineId);
            
            return routine;
        }

        internal static IRoutine[] GetRoutinesByDay(int workoutId, int dayId)
        {
            var workout = GetWorkout(workoutId);
            var day = workout.Days.First(x => x.DayId == dayId);
            return day.Routines;
        }

        internal static IRoutineExercise[] GetSets(int workoutId, int dayId, int routineId)
        {
            var routine = GetRoutine(workoutId,dayId,routineId);
            return routine.RoutineExercises;
        }

        internal static Workout GetWorkout(int workoutId)
        {
            var workout = workouts.First(x => x.WorkoutId == workoutId);
            return workout;
        }


    }
}