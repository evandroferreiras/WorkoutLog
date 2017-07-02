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
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            var routine = new RoutineBuilder(routineId, "Default")
                              .AddDayAndNormalRoutineExercise(dayOfWeek, 10, 10, 70)
                              .Build();

            
            

            var rret = new RemoveRoutineExerciseTransaction(routineId, dayOfWeek, 0);
            rret.Execute();

            var returned = ReturnFirstRoutine(routineId);
            var day = returned.Days.First(x => x.DayOfWeek == dayOfWeek);
            day.RoutineExercises.Should().HaveCount(0);


        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The routine exercise doesnt exist.")]
        public void ShouldntBePossibleToRemoveAnInexistentRoutineExercise()
        {
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            var routine = new RoutineBuilder( routineId, "Default")
                              .AddDayAndNormalRoutineExercise(dayOfWeek, 10, 10, 70)
                              .Build();

            var newroutineId = Database.WorkoutDatabase.GetNextRoutineId();
            var rret = new RemoveRoutineExerciseTransaction(newroutineId, dayOfWeek, 1);
            rret.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The day doesnt exist.")]
        public void ShouldntBePossibleToRemoveFromAnInexistentDay()
        {
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            var routine = new RoutineBuilder(routineId, "Default")
                              .AddDayAndNormalRoutineExercise(dayOfWeek, 10, 10, 70)
                              .Build();

            
            

            var newroutineId = Database.WorkoutDatabase.GetNextRoutineId();
            var rret = new RemoveRoutineExerciseTransaction(newroutineId, dayOfWeek, 0);
            rret.Execute();
        }
    }
}
