using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
//TODO: Create a TestDataBuilder object
    public class BaseTest
    {
        internal static List<IDay> CreateWorkOutAndReturnDays(int workoutId, int routineId, int dayId, int routineExerciseId, int exerciseId, int reps, double weight)
        {

            var sets = new RoutineExercise[] { new NormalRoutineExercise(routineExerciseId, exerciseId, reps, weight) };

            var days = CreateWorkOutAndReturnDays(workoutId, routineId, dayId, sets);

            return days;
        }

        internal static List<IDay> CreateWorkOutAndReturnDays(int workoutId, int routineId, int dayId, IRoutineExercise[] sets)
        {
            var routines = new List<IRoutine>();            
            routines.Add(new Routine(routineId, sets.ToArray()));
            var days = new List<IDay>();
            days.Add(new Day(dayId, routines.ToArray()));

            var addWorkoutTransaction = new AddWorkoutTransaction(workoutId, days.ToArray());
            addWorkoutTransaction.Execute();

            return days;
        }

        internal static IRoutine ReturnFirstTraining(int workoutId, int routineId, int dayId)
        {
            var workoutUpdated = WorkoutDatabase.GetWorkout(workoutId);

            var dr = workoutUpdated.Days.First(x => x.DayId == dayId);
            dr.Should().NotBeNull();

            var tr = dr.Routines.First(x => x.RoutineId == routineId);
            tr.Should().NotBeNull();
            return tr;
        }

    }
}
