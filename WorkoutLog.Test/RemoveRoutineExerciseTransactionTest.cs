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
            var wId = new WorkoutIdentity(12992, DayOfWeek.Monday, 3228);

            var routine = new RoutineBuilder(wId.RoutineId, "Default")
                              .AddDayAndNormalRoutineExercise(wId.DayOfWeek, wId.RoutineExerciseId, 10, 10, 70)
                              .Build();

            
            

            var rret = new RemoveRoutineExerciseTransaction(wId,0);
            rret.Execute();

            var returned = ReturnFirstRoutine(wId);
            var day = returned.Days.First(x => x.DayOfWeek == wId.DayOfWeek);
            day.RoutineExercises.Should().HaveCount(0);


        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The routine exercise doesnt exist.")]
        public void ShouldntBePossibleToRemoveAnInexistentRoutineExercise()
        {
            var wId = new WorkoutIdentity(12992, DayOfWeek.Monday, 3228);

            var routine = new RoutineBuilder( wId.RoutineId, "Default")
                              .AddDayAndNormalRoutineExercise(wId.DayOfWeek, wId.RoutineExerciseId, 10, 10, 70)
                              .Build();

            
            

            var newWId = new WorkoutIdentity(12992, DayOfWeek.Monday, 9000);
            var rret = new RemoveRoutineExerciseTransaction(newWId,1);
            rret.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The day doesnt exist.")]
        public void ShouldntBePossibleToRemoveFromAnInexistentDay()
        {
            var wId = new WorkoutIdentity(12992, DayOfWeek.Monday, 3228);

            var routine = new RoutineBuilder(wId.RoutineId, "Default")
                              .AddDayAndNormalRoutineExercise(wId.DayOfWeek, wId.RoutineExerciseId, 10, 10, 70)
                              .Build();

            
            

            var newWId = new WorkoutIdentity( 12992, DayOfWeek.Friday, 9000);
            var rret = new RemoveRoutineExerciseTransaction(newWId,0);
            rret.Execute();
        }
    }
}
