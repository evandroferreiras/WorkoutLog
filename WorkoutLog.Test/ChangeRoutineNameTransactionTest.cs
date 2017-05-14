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
            var wid = new Workout.WorkoutIdentity(13, 14, 1042);
            var r = new RoutineBuilder(wid.RoutineId, "Default")
                          .AddNormalRoutineExercise(wid.DayId, wid.RoutineExerciseId, exerciseId, 10, 25)
                          .Build();

            var art = new AddRoutineTransaction(wid, r.Name, r.Days);
            art.Execute();

            var crnt = new ChangeRoutineNameTransaction(wid, "Default1");
            crnt.Execute();

            var returned = ReturnFirstRoutine(wid);
            returned.Name.Should().Be("Default1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The routine name is required")]
        public void NameShouldBeRequiredField()
        {
            const int exerciseId = 10;
            var wid = new Workout.WorkoutIdentity( 13, 14, 1042);
            var r = new RoutineBuilder(wid.RoutineId, "Default")
                          .AddNormalRoutineExercise(wid.DayId, wid.RoutineExerciseId, exerciseId, 10, 25)
                          .Build();

            var art = new AddRoutineTransaction(wid, r.Name, r.Days);
            art.Execute();

            var crnt = new ChangeRoutineNameTransaction(wid, "");
            crnt.Execute();

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The routine doesnt exist.")]
        public void ShouldntBePossibleChangeANameForAInexistentRoutine()
        {
            var wid = new Workout.WorkoutIdentity(1366, 1466, 10426);
            var crnt = new ChangeRoutineNameTransaction(wid, "teste");
            crnt.Execute();
        }
    }
}
