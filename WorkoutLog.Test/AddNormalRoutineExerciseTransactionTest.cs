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
    public class AddNormalRoutineExerciseTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToAdd()
        {
            var wId = new Workout.WorkoutIdentity( 45, 45, 54);

            var routine = new RoutineBuilder( wId.RoutineId, "DefaultRoutine")
                                            .AddNormalRoutineExercise(wId.DayId, wId.RoutineExerciseId, 10, 10, 50)
                                            .Build();


            var art = new AddRoutineTransaction(wId, routine.Name, routine.Days);
            art.Execute();

            var aret = new AddNormalRoutineExerciseTransaction(wId, exerciseId: 50, reps: 7, weight: 89.6);
            aret.Execute();

            var returned = ReturnFirstRoutine(wId);
            var day = returned.Days.FirstOrDefault(x => x.DayId == wId.DayId);
            day.Should().NotBeNull();
            day.RoutineExercises.Should().HaveCount(2);
            day.RoutineExercises.Last().Weight.Should().Be(89.6);


        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The day doesnt exist.")]
        public void ShouldntBePossibleToAddToAnInexistentDay()
        {
            var wId = new Workout.WorkoutIdentity( 45, 45, 54);
            const int anotherDay = 30303;
            var routine = new RoutineBuilder( wId.RoutineId, "DefaultRoutine")
                                            .AddNormalRoutineExercise(anotherDay, wId.RoutineExerciseId, 10, 10, 50)
                                            .Build();


            var art = new AddRoutineTransaction(wId,routine.Name, routine.Days);
            art.Execute();

            var anotherWId = new WorkoutIdentity( wId.RoutineId, anotherDay, wId.RoutineExerciseId);
            var aret = new AddNormalRoutineExerciseTransaction(anotherWId, exerciseId: 50, reps: 7, weight: 89.6);
            aret.Execute();
        }
    }
}
