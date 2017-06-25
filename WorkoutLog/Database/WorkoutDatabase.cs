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

        [Obsolete]
        public static IRoutine GetRoutine(WorkoutIdentity wid)
        {
            return GetRoutine(wid.RoutineId);
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



        internal static IDay GetDay(WorkoutIdentity id)
        {
            var routine = GetRoutine(id.RoutineId);
            return routine.Days.FirstOrThrow(x => x.DayOfWeek == id.DayOfWeek);
        }

        public static IDay[] GetDaysByRoutine(WorkoutIdentity id)
        {
            var routine = GetRoutine(id.RoutineId);
            return routine.Days;
        }

        public static IRoutineExercise[] GetRoutineExercises(WorkoutIdentity id) {
            var routine = GetRoutine(id.RoutineId);
            var day = routine.Days.FirstOrDefault(x => x.DayOfWeek == id.DayOfWeek);
            return day.RoutineExercises;
            
        }

        internal static void AddDay(int routineId, Day day)
        {
            var routine = GetRoutine(routineId);
            var daysList = routine.Days.ToList();
            daysList.Add(day);
            routine.Days = daysList.ToArray();
        }

        internal static void RemoveDay(WorkoutIdentity wId)
        {
            var routine = GetRoutine(wId.RoutineId);
            var dayList = routine.Days.ToList();

            var dayToRemove = dayList.FirstOrThrow(x => x.DayOfWeek == wId.DayOfWeek);
            
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

        internal static void RemoveRoutineExercise(WorkoutIdentity wId, int routineExerciseIdx)
        {
            var day = GetDay(wId);
            var reList = day.RoutineExercises.ToList();
            var reToRemove = reList.ElementAtOrThrow(routineExerciseIdx);
            reList.Remove(reToRemove);
            day.RoutineExercises = reList.ToArray();
        }

        public static void SaveRoutine(WorkoutIdentity wId, string name)
        {
            var workout = new Routine(wId, name);
            routines.Add(workout);
        }
    }
}