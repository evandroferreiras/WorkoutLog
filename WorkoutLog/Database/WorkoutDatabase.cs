using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutLog.Workout;

namespace WorkoutLog.Database
{
    public class WorkoutDatabase
    {

        static IList<Workout.Workout> workouts = new List<Workout.Workout>();

        public static void SaveWorkout(int workoutId, IRoutine[] days)
        {
            var workout = new Workout.Workout(workoutId, days);
            workouts.Add(workout);
        }
            
        public static IRoutine GetRoutine(int workoutId, int routineId)
        {
            var workout = GetWorkout(workoutId);
            var routine = workout.Routines.First(x => x.RoutineId == routineId);
            
            return routine;
        }

        public static IDay[] GetDaysByRotine(int workoutId, int routineId)
        {
            var routine = GetRoutine(workoutId, routineId);
            return routine.Days;
        }

        //public static IRoutine[] GetRoutinesByDay(int workoutId, int dayId)
        //{
        //    var workout = GetWorkout(workoutId);
        //    var day = workout.Routines.First(x => x.DayId == dayId);
        //    return day.RoutineExercises;
        //}

        public static IRoutineExercise[] GetRoutineExercises(int workoutId, int routineId, int dayId)
        {
            var routine = GetRoutine(workoutId,routineId);
            var day = routine.Days.FirstOrDefault(x => x.DayId == dayId);
            return day.RoutineExercises;
        }

        public static Workout.Workout GetWorkout(int workoutId)
        {
            var workout = workouts.First(x => x.WorkoutId == workoutId);
            return workout;
        }


    }
}