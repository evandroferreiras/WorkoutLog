using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WorkoutLog.Database;
using WorkoutLog.Test.Base;
using WorkoutLog.Transactions;
using WorkoutLog.Workout;

namespace WorkoutLog.Test
{
    [TestClass]
    public class RemoveRoutineExerciseTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToRemoveARoutineExercise()
        {
            var wId = new WorkoutIdentity(12992, 123398, 3228);

            var routine = new RoutineBuilder(wId.RoutineId, "Default")
                              .AddNormalRoutineExercise(wId.DayId, wId.RoutineExerciseId, 10, 10, 70)
                              .Build();

            var art = new AddRoutineTransaction(wId, routine.Name, routine.Days);
            art.Execute();

            var rret = new RemoveRoutineExerciseTransaction(wId);
            rret.Execute();

            var returned = ReturnFirstRoutine(wId);
            var day = returned.Days.First(x => x.DayId == wId.DayId);
            day.RoutineExercises.Should().HaveCount(0);


        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The routine exercise doesnt exist.")]
        public void ShouldntBePossibleToRemoveAnInexistentRoutineExercise()
        {
            var wId = new WorkoutIdentity(12992, 123398, 3228);

            var routine = new RoutineBuilder( wId.RoutineId, "Default")
                              .AddNormalRoutineExercise(wId.DayId, wId.RoutineExerciseId, 10, 10, 70)
                              .Build();

            var art = new AddRoutineTransaction(wId, routine.Name, routine.Days);
            art.Execute();

            var newWId = new WorkoutIdentity(12992, 123398, 9000);
            var rret = new RemoveRoutineExerciseTransaction(newWId);
            rret.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The day doesnt exist.")]
        public void ShouldntBePossibleToRemoveFromAnInexistentDay()
        {
            var wId = new WorkoutIdentity(12992, 123398, 3228);

            var routine = new RoutineBuilder(wId.RoutineId, "Default")
                              .AddNormalRoutineExercise(wId.DayId, wId.RoutineExerciseId, 10, 10, 70)
                              .Build();

            var art = new AddRoutineTransaction(wId, routine.Name, routine.Days);
            art.Execute();

            var newWId = new WorkoutIdentity( 12992, 9000, 9000);
            var rret = new RemoveRoutineExerciseTransaction(newWId);
            rret.Execute();
        }
    }
}
