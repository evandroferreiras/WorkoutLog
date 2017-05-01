using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutLog.Workout;

namespace WorkoutLog.Database
{
    public class WorkoutDatabase
    {

        static IList<Workout.Workout> workouts = new List<Workout.Workout>();

        public static void SaveWorkout(WorkoutIdentity workoutId, IRoutine[] days)
        {
            var workout = new Workout.Workout(workoutId, days);
            workouts.Add(workout);
        }
                      
        public static IRoutine GetRoutine(WorkoutIdentity wid)
        {

            var routine = (from w in workouts
                          from r in w.Routines
                          where w.WorkoutId == wid.WorkoutId
                          && r.RoutineId == wid.RoutineId
                          select (r ?? null )).FirstOrDefault();
            if (routine == null)
            {
                throw new Exception("The routine doesnt exist.");
            }
            return routine;
        }


        internal static IRoutineExercise[] UpdateRoutineExercise(IRoutineExercise[] routineExercises, IRoutineExercise re)
        {
            var res = routineExercises.ToList();

            var reu = res.FirstOrDefault(x => x.RoutineExerciseId == re.RoutineExerciseId);
            if (reu != null) 
                res.Remove(reu);
            
            res.Add(re);

            return res.ToArray();

        }

        internal static IRoutineExercise[] AddRoutineExercise(IRoutineExercise[] routineExercises, IRoutineExercise re)
        {
            var res = routineExercises.ToList();
            res.Add(re);

            return res.ToArray();
        }

        internal static IDay GetDay(WorkoutIdentity id)
        {
            var routine = GetRoutine(id);
            return routine.Days.FirstOrDefault(x => x.DayId == id.DayId);
        }

        public static IDay[] GetDaysByRotine(WorkoutIdentity id)
        {
            var routine = GetRoutine(id);
            return routine.Days;
        }

        public static IRoutineExercise[] GetRoutineExercises(WorkoutIdentity id) {
            return GetRoutineExercises(id.WorkoutId, id.RoutineId, id.DayId);
        }

        public static IRoutineExercise[] GetRoutineExercises(int workoutId, int routineId, int dayId)
        {
            var routine = GetRoutine(new WorkoutIdentity(workoutId,routineId));
            var day = routine.Days.FirstOrDefault(x => x.DayId == dayId);
            return day.RoutineExercises;
        }

        public static Workout.Workout GetWorkout(WorkoutIdentity id)
        {
            var workout = workouts.First(x => x.WorkoutId == id.WorkoutId);
            return workout;
        }


    }
}