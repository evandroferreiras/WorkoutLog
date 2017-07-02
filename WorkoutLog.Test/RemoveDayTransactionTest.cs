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

            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;


            var routine = new RoutineBuilder(routineId, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(dayOfWeek, 10, 10, 50)
                                            .Build();
            
            var rdt = new RemoveDayTransaction(routineId, dayOfWeek);
            rdt.Execute();

            var returned = ReturnFirstRoutine(routineId);
            returned.Days.Should().HaveCount(0);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The day doesnt exist.")]
        public void ShouldntBePossibleToRemoveAInexistentDay()
        {
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            var differentDay = DayOfWeek.Tuesday;

            var routine = new RoutineBuilder(routineId, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(differentDay, 10, 10, 50)
                                            .Build();

            
            

            var rdt = new RemoveDayTransaction(routineId, dayOfWeek);
            rdt.Execute();

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The routine doesnt exist.")]
        public void ShouldntBePossibleToRemoveADayFromAInexistentRoutine()
        {
            var anotherroutineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            var routine = new RoutineBuilder( 9999999, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(DayOfWeek.Monday, 10, 10, 50)
                                            .Build();
          
            var rdt = new RemoveDayTransaction(anotherroutineId, dayOfWeek);
            rdt.Execute();

        }
    }
}
