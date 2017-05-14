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
            var wId = new Workout.WorkoutIdentity(45, 45, 54);

            var routine = new RoutineBuilder(wId.RoutineId, "DefaultRoutine")
                                            .AddNormalRoutineExercise(wId.DayId, wId.RoutineExerciseId, 10, 10, 50)
                                            .Build();

            var art = new AddRoutineTransaction(wId, routine.Name, routine.Days);
            art.Execute();

            IRoutineExercise re = new NormalRoutineExercise(wId, 30, 10, 89.7);

            var adt = new AddDayTransaction(wId,  new[] { re });
            adt.Execute();

            var returned = ReturnFirstRoutine(wId);
            returned.Days.Should().HaveCount(2);
            var day = returned.Days.FirstOrDefault(x => x.DayId == wId.DayId);
            day.DayId.Should().Be(wId.DayId);


        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The routine doesnt exist.")]
        public void ShouldntBePossibleToAddToAnInexistentRoutine()
        {
            var wId = new Workout.WorkoutIdentity( 45, 45, 54);
            var routine = new RoutineBuilder( wId.RoutineId, "DefaultRoutine")
                                            .AddNormalRoutineExercise(wId.DayId, wId.RoutineExerciseId, 10, 10, 50)
                                            .Build();
            var art = new AddRoutineTransaction(wId, routine.Name, routine.Days);
            art.Execute();

            IRoutineExercise re = new NormalRoutineExercise(wId, 30, 10, 89.7);

            var anotherWId = new WorkoutIdentity(7000, wId.DayId, wId.RoutineExerciseId);
            var adt = new AddDayTransaction(anotherWId, new[] { re });
            adt.Execute();
        }
    }
}
