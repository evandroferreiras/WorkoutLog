using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;
using WorkoutLog.Transactions;
using WorkoutLog.Workout;

namespace WorkoutLog.Test.Base
{
//TODO: Create a TestDataBuilder object
    public class BaseTest
    {
        internal static List<IRoutine> CreateWorkOutAndRoutines(int workoutId, int routineId, int dayId, int routineExerciseId, int exerciseId, int reps, double weight)
        {

            var sets = new RoutineExercise[] { new NormalRoutineExercise(routineExerciseId, exerciseId, reps, weight) };

            var routines = CreateWorkOutAndRoutines(workoutId, routineId, dayId, sets);

            return routines;
        }

        internal static List<IRoutine> CreateWorkOutAndRoutines(int workoutId, int routineId, int dayId, IRoutineExercise[] sets)
        {
            var routines = new List<IRoutine>();
            var days = new List<IDay> { new Day(dayId, sets) };
                     
            routines.Add(new Routine(routineId, days.ToArray()));
            var addWorkoutTransaction = new AddWorkoutTransaction(workoutId, routines.ToArray());
            addWorkoutTransaction.Execute();

            return routines;
        }

        internal static IRoutine ReturnFirstRoutine(int workoutId, int routineId)
        {
            var workoutUpdated = WorkoutDatabase.GetWorkout(workoutId);

            var r = workoutUpdated.Routines.First(x => x.RoutineId == routineId);
            r.Should().NotBeNull();

            return r;
        }

    }
}
