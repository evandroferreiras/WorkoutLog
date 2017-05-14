using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutLog.Workout;

namespace WorkoutLog.Database
{

    public static class DayExtensions
    {
        public static IDay FirstOrThrow(this IEnumerable<IDay> days, Func<IDay,bool> predicate)
        {
            var day = days.FirstOrDefault(predicate);
            if (day == null)
                throw new Exception("The day doesnt exist.");

            return day;
        }
    }

    public static class RoutineExercisesExtensions
    {
        public static IRoutineExercise FirstOrThrow(this IEnumerable<IRoutineExercise> res, Func<IRoutineExercise,bool> predicate)
        {
            var re = res.FirstOrDefault(predicate);
            if (re == null)
                throw new Exception("The routine exercise doesnt exist.");

            return re;
        }
    }

    public class WorkoutDatabase
    {

        static IList<Workout.Routine> routines = new List<Workout.Routine>();
                      
        public static IRoutine GetRoutine(WorkoutIdentity wid)
        {

            var routine = (from r in routines
                          where r.RoutineId == wid.RoutineId
                          select (r ?? null )).FirstOrDefault();
            if (routine == null)
            {
                throw new Exception("The routine doesnt exist.");
            }
            return routine;
        }



        internal static IDay GetDay(WorkoutIdentity id)
        {
            var routine = GetRoutine(id);
            return routine.Days.FirstOrThrow(x => x.DayId == id.DayId);
        }

        public static IDay[] GetDaysByRoutine(WorkoutIdentity id)
        {
            var routine = GetRoutine(id);
            return routine.Days;
        }

        public static IRoutineExercise[] GetRoutineExercises(WorkoutIdentity id) {
            var routine = GetRoutine(new WorkoutIdentity(id.RoutineId));
            var day = routine.Days.FirstOrDefault(x => x.DayId == id.DayId);
            return day.RoutineExercises;
            
        }

        internal static void AddDay(WorkoutIdentity wId, Day day)
        {
            var routine = GetRoutine(wId);
            var daysList = routine.Days.ToList();
            daysList.Add(day);
            routine.Days = daysList.ToArray();
        }

        internal static void RemoveDay(WorkoutIdentity wId)
        {
            var routine = GetRoutine(wId);
            var dayList = routine.Days.ToList();

            var dayToRemove = dayList.FirstOrThrow(x => x.DayId == wId.DayId);
            
            dayList.Remove(dayToRemove);
            routine.Days = dayList.ToArray();
        }

        internal static void AddRoutineExercise(WorkoutIdentity wId, IRoutineExercise re)
        {
            var day = GetDay(wId);
            var reList = day.RoutineExercises.ToList();
            reList.Add(re);
            day.RoutineExercises = reList.ToArray();            
        }

        internal static void RemoveRoutineExercise(WorkoutIdentity wId)
        {
            var day = GetDay(wId);
            var reList = day.RoutineExercises.ToList();
            var reToRemove = reList.FirstOrThrow(x => x.RoutineExerciseId == wId.RoutineExerciseId);
            reList.Remove(reToRemove);
            day.RoutineExercises = reList.ToArray();
        }

        public static void SaveRoutine(WorkoutIdentity wId, string name, IDay[] days)
        {
            var workout = new Routine(wId, name, days);
            routines.Add(workout);
        }
    }
}