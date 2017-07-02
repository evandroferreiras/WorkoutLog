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
    public class ChangeRoutineNameTransactionTest : BaseTest
    {
        [TestMethod]
        public void NameShouldBeChanged()
        {
            const int exerciseId = 10;

            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            var r = new RoutineBuilder(routineId, "Default")
                          .AddDayAndNormalRoutineExercise(dayOfWeek, exerciseId, 10, 25)
                          .Build();


            var crnt = new ChangeRoutineNameTransaction(routineId, "Default1");
            crnt.Execute();

            var returned = ReturnFirstRoutine(routineId);
            returned.Name.Should().Be("Default1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The routine name is required")]
        public void NameShouldBeRequiredField()
        {
            const int exerciseId = 10;
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var r = new RoutineBuilder(routineId, "Default")
                          .AddDayAndNormalRoutineExercise(DayOfWeek.Monday, exerciseId, 10, 25)
                          .Build();

            
            

            var crnt = new ChangeRoutineNameTransaction(routineId, "");
            crnt.Execute();

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The routine doesnt exist.")]
        public void ShouldntBePossibleChangeANameForAInexistentRoutine()
        {
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;
            var crnt = new ChangeRoutineNameTransaction(routineId, "teste");
            crnt.Execute();
        }
    }
}
