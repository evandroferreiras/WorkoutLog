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
            var wId = new WorkoutIdentity(4587687, DayOfWeek.Monday, 54);

            var routine = new RoutineBuilder(wId.RoutineId, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(wId.DayOfWeek, wId.RoutineExerciseId, 10, 10, 50)
                                            .Build();

            
            

            var rdt = new RemoveDayTransaction(wId.RoutineId, wId.DayOfWeek);
            rdt.Execute();

            var returned = ReturnFirstRoutine(wId.RoutineId);
            returned.Days.Should().HaveCount(0);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The day doesnt exist.")]
        public void ShouldntBePossibleToRemoveAInexistentDay()
        {
            var wId = new WorkoutIdentity( 44325, DayOfWeek.Monday, 54432);
            var differentDay = DayOfWeek.Tuesday;

            var routine = new RoutineBuilder(wId.RoutineId, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(differentDay, wId.RoutineExerciseId, 10, 10, 50)
                                            .Build();

            
            

            var rdt = new RemoveDayTransaction(wId.RoutineId, wId.DayOfWeek);
            rdt.Execute();

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The routine doesnt exist.")]
        public void ShouldntBePossibleToRemoveADayFromAInexistentRoutine()
        {

            var anotherRoutine = 38382;

            var routine = new RoutineBuilder( 9999999, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(DayOfWeek.Monday, 544543, 10, 10, 50)
                                            .Build();

            
            

            var anotherWId = new WorkoutIdentity(anotherRoutine, DayOfWeek.Monday, 544543);
            var rdt = new RemoveDayTransaction(anotherWId.RoutineId, anotherWId.DayOfWeek);
            rdt.Execute();

        }
    }
}
