using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Test.Base;
using WorkoutLog.Transactions;

namespace WorkoutLog.Test
{
    [TestClass]
    public class RemoveDayTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToRemoveADay()
        {
            var wId = new Workout.WorkoutIdentity(4587687,4532423,54);

            var routine = new RoutineBuilder(wId.RoutineId, "DefaultRoutine")
                                            .AddNormalRoutineExercise(wId.DayId, wId.RoutineExerciseId, 10, 10, 50)
                                            .Build();

            var art = new AddRoutineTransaction(wId, routine.Name, routine.Days);
            art.Execute();

            var rdt = new RemoveDayTransaction(wId);
            rdt.Execute();

            var returned = ReturnFirstRoutine(wId);
            returned.Days.Should().HaveCount(0);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The day doesnt exist.")]
        public void ShouldntBePossibleToRemoveAInexistentDay()
        {
            var wId = new Workout.WorkoutIdentity( 44325, 45343, 54432);
            var differentDay = 12102;

            var routine = new RoutineBuilder(wId.RoutineId, "DefaultRoutine")
                                            .AddNormalRoutineExercise(differentDay, wId.RoutineExerciseId, 10, 10, 50)
                                            .Build();

            var art = new AddRoutineTransaction(wId, routine.Name, routine.Days);
            art.Execute();

            var rdt = new RemoveDayTransaction(wId);
            rdt.Execute();

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The routine doesnt exist.")]
        public void ShouldntBePossibleToRemoveADayFromAInexistentRoutine()
        {
            var wId = new Workout.WorkoutIdentity( 45324, 45565, 544543);
            var anotherRoutine = 38382;

            var routine = new RoutineBuilder( anotherRoutine, "DefaultRoutine")
                                            .AddNormalRoutineExercise(wId.DayId, wId.RoutineExerciseId, 10, 10, 50)
                                            .Build();

            var art = new AddRoutineTransaction(wId, routine.Name, routine.Days);
            art.Execute();

            var anotherWId = new Workout.WorkoutIdentity(anotherRoutine, wId.DayId, wId.RoutineExerciseId);
            var rdt = new RemoveDayTransaction(anotherWId);
            rdt.Execute();

        }
    }
}
