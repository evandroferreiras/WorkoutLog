using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Test.Base;
using WorkoutLog.Transactions;
using WorkoutLog.Workout;

namespace WorkoutLog.Test
{
    [TestClass]
    public class AddDayTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToAdd()
        {
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dw = DayOfWeek.Monday;


            var routine = new RoutineBuilder(routineId, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(dw,  10, 10, 50)
                                            .Build();

            var adt = new AddDayTransaction(routineId, DayOfWeek.Monday);
            adt.Execute();

            var returned = ReturnFirstRoutine(routineId);
            returned.Days.Should().HaveCount(2);
            var day = returned.Days.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Monday);
            day.DayOfWeek.Should().Be(DayOfWeek.Monday);


        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The routine doesnt exist.")]
        public void ShouldntBePossibleToAddToAnInexistentRoutine()
        {

            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dw = DayOfWeek.Monday;


            var routine = new RoutineBuilder(routineId, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(dw,  10, 10, 50)
                                            .Build();

            var adt = new AddDayTransaction(7000, DayOfWeek.Monday);
            adt.Execute();
        }
    }
}
