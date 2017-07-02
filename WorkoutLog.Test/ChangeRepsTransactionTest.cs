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
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;
            var exerciseId = 10;
            
            CreateAndReturnRoutine(routineId, dayOfWeek, exerciseId, 10, 50);

            var changeRepsTransaction = new ChangeRepsTransaction(routineId, dayOfWeek, 0, 20);
            changeRepsTransaction.Execute();

            var routine = ReturnFirstRoutine(routineId);
            var days = routine.Days;
            days.Should().HaveCount(1);
            var res = days.FirstOrDefault(x => x.DayOfWeek == dayOfWeek);
            res.Should().NotBeNull();
            var re = res.RoutineExercises.FirstOrDefault(x => x.ExerciseId == exerciseId);
            re.Reps.Should().Be(20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The number of series should'nt be negative")]
        public void ShouldntBePossibleChangeTheNumberOfRepsToNegative()
        {

            var exerciseId = 10;
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;
            CreateAndReturnRoutine(routineId, dayOfWeek, exerciseId, 10, 50);

            var changeRepsTransaction = new ChangeRepsTransaction(routineId, dayOfWeek, 0, -20);
            changeRepsTransaction.Execute();

            var routine = ReturnFirstRoutine(routineId);
            var days = routine.Days;
            days.Should().HaveCount(1);
            var res = days.FirstOrDefault(x => x.DayOfWeek == dayOfWeek);
            res.Should().NotBeNull();
            var re = res.RoutineExercises.FirstOrDefault(x => x.ExerciseId == exerciseId);
            re.Reps.Should().Be(20);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),"The routine doesnt exist")]
        public void ShouldntBePossibleChangeTheRepsOfAnInexistentWorkout()
        {

            var routineId = Database.WorkoutDatabase.GetNextRoutineId();

            var changeRepsTransaction = new ChangeRepsTransaction(routineId, DayOfWeek.Thursday, 0, -10);
            changeRepsTransaction.Execute();
        }
    }
}
