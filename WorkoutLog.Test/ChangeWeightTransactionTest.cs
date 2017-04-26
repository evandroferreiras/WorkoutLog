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
    public class ChangeWeightTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleChangeTheWeight()
        {
            var workoutId = 2;
            var routineId = 1;
            var dayId = 1;
            var setId = 100;
            var exerciseId = 10;
            CreateWorkOutAndRoutines(workoutId, routineId, dayId, setId, exerciseId, 10, 50);

            var changeWeightTransaction = new ChangeWeightTransaction(workoutId, dayId, routineId, setId, 60);
            changeWeightTransaction.Execute();

            var routine = ReturnFirstRoutine(workoutId, routineId);
            var days = routine.Days;
            days.Should().HaveCount(1);
            var day = days.First(x => x.DayId == dayId);
            day.RoutineExercises.First().Should().BeOfType<NormalRoutineExercise>();
            var normalRoutineExercise = (NormalRoutineExercise)day.RoutineExercises.First(x => x.ExerciseId.Equals(exerciseId));
            normalRoutineExercise.Weight.Should().Be(60);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The weight should'nt be negative")]
        public void ShouldBePossibleChangeTheWeightToNegative()
        {
            var workoutId = 2;
            var routineId = 1;
            var dayId = 1;
            var setId = 100;
            var exerciseId = 10;
            CreateWorkOutAndRoutines(workoutId, routineId, dayId, setId, exerciseId, 10, 50);

            var changeWeightTransaction = new ChangeWeightTransaction(workoutId, dayId, routineId, setId, -60);
            changeWeightTransaction.Execute();
        }
    }
}
