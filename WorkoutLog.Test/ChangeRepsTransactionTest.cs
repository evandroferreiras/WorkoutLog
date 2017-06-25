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
    public class ChangeRepsTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleChangeTheNumberOfReps()
        {
            var exerciseId = 10;
            var id = new Workout.WorkoutIdentity( 1, DayOfWeek.Monday, 100);
            CreateAndReturnRoutine(id, exerciseId, 10, 50);

            var changeRepsTransaction = new ChangeRepsTransaction(0, id, 20);
            changeRepsTransaction.Execute();

            var routine = ReturnFirstRoutine(id);
            var days = routine.Days;
            days.Should().HaveCount(1);
            var res = days.FirstOrDefault(x => x.DayOfWeek == id.DayOfWeek);
            res.Should().NotBeNull();
            var re = res.RoutineExercises.FirstOrDefault(x => x.ExerciseId == exerciseId);
            re.Reps.Should().Be(20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The number of series should'nt be negative")]
        public void ShouldntBePossibleChangeTheNumberOfRepsToNegative()
        {

            var exerciseId = 10;
            var wid = new Workout.WorkoutIdentity( 1, DayOfWeek.Monday, 100);
            CreateAndReturnRoutine(wid, exerciseId, 10, 50);

            var changeRepsTransaction = new ChangeRepsTransaction(0, wid, -20);
            changeRepsTransaction.Execute();

            var routine = ReturnFirstRoutine(wid);
            var days = routine.Days;
            days.Should().HaveCount(1);
            var res = days.FirstOrDefault(x => x.DayOfWeek == wid.DayOfWeek);
            res.Should().NotBeNull();
            var re = res.RoutineExercises.FirstOrDefault(x => x.ExerciseId == exerciseId);
            re.Reps.Should().Be(20);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),"The routine doesnt exist")]
        public void ShouldntBePossibleChangeTheRepsOfAnInexistentWorkout()
        {
            var wid = new Workout.WorkoutIdentity(91, DayOfWeek.Thursday, 100);
            var changeRepsTransaction = new ChangeRepsTransaction(0, wid, -10);
            changeRepsTransaction.Execute();
        }
    }
}
