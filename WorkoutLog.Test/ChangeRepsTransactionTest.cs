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
            var workoutId = 2;
            var routineId = 1;
            var dayId = 1;
            var setId = 100;
            var exerciseId = 10;
            CreateWorkOutAndRoutines(workoutId, routineId, dayId, setId, exerciseId, 10, 50);

            var changeRepsTransaction = new ChangeRepsTransaction(workoutId, dayId, routineId, 100, 20);
            changeRepsTransaction.Execute();

            var routine = ReturnFirstRoutine(workoutId, routineId);
            var days = routine.Days;
            days.Should().HaveCount(1);
            var res = days.FirstOrDefault(x => x.DayId == dayId);
            res.Should().NotBeNull();
            var re = res.RoutineExercises.FirstOrDefault(x => x.ExerciseId == exerciseId);
            re.Reps.Should().Be(20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The number of series should'nt be negative")]
        public void ShouldntBePossibleChangeTheNumberOfRepsToNegative()
        {
            var workoutId = 2;
            var routineId = 1;
            var dayId = 1;
            var setId = 100;
            var exerciseId = 10;
            CreateWorkOutAndRoutines(workoutId, routineId, dayId, setId, exerciseId, 10, 50);

            var changeRepsTransaction = new ChangeRepsTransaction(workoutId, dayId, routineId, 100, -20);
            changeRepsTransaction.Execute();

            var routine = ReturnFirstRoutine(workoutId, routineId);
            var days = routine.Days;
            days.Should().HaveCount(1);
            var res = days.FirstOrDefault(x => x.DayId == dayId);
            res.Should().NotBeNull();
            var re = res.RoutineExercises.FirstOrDefault(x => x.ExerciseId == exerciseId);
            re.Reps.Should().Be(20);
        }
    }
}
