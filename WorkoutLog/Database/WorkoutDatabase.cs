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
        public static IRoutineExercise ElementAtOrThrow(this IEnumerable<IRoutineExercise> res, int idx)
        {
            var re = res.ElementAtOrDefault(idx);
            if (re == null)
                throw new Exception("The routine exercise doesnt exist.");

            return re;
        }
    }

    public class WorkoutDatabase
    {

        static IList<Workout.Routine> routines = new List<Workout.Routine>();
               
        public static void Clear()
        {
            routines.Clear();
        }

        public static IRoutine GetRoutine(int routineId)
        {
            var routine = (from r in routines
                           where r.RoutineId == routineId
                           select (r ?? null)).FirstOrDefault();
            if (routine == null)
            {
                throw new Exception("The routine doesnt exist.");
            }
            return routine;
        }

        internal static IDay GetDay(int routineId, DayOfWeek dayOfWeek)
        {
            var routine = GetRoutine(routineId);
            return routine.Days.FirstOrThrow(x => x.DayOfWeek == dayOfWeek);
        }

        internal static void AddDay(int routineId, Day day)
        {
            var routine = GetRoutine(routineId);
            var daysList = routine.Days.ToList();
            daysList.Add(day);
            routine.Days = daysList.ToArray();
        }

        internal static void RemoveDay(int routineId, DayOfWeek dayOfWeek)
        {
            var routine = GetRoutine(routineId);
            var dayList = routine.Days.ToList();

            var dayToRemove = dayList.FirstOrThrow(x => x.DayOfWeek == dayOfWeek);
            
            dayList.Remove(dayToRemove);
            routine.Days = dayList.ToArray();
        }

        internal static void AddRoutineExercise(int routineId, DayOfWeek dayOfWeek, IRoutineExercise re)
        {
            var day = GetDay(routineId, dayOfWeek);
            var reList = day.RoutineExercises.ToList();
            reList.Add(re);
            day.RoutineExercises = reList.ToArray();            
        }

        internal static void RemoveRoutineExercise(int routineId, DayOfWeek dayOfWeek, int routineExerciseIdx)
        {
            var day = GetDay(routineId, dayOfWeek);
            var reList = day.RoutineExercises.ToList();
            var reToRemove = reList.ElementAtOrThrow(routineExerciseIdx);
            reList.Remove(reToRemove);
            day.RoutineExercises = reList.ToArray();
        }

        public static void SaveRoutine(int routineId, string name)
        {
            var workout = new Routine(routineId, name);
            routines.Add(workout);
        }
    }
}