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

            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            var exerciseId = 10;

            CreateAndReturnRoutine(routineId, dayOfWeek, exerciseId, 10, 50);

            var changeWeightTransaction = new ChangeWeightTransaction(routineId, dayOfWeek,0, 60);
            changeWeightTransaction.Execute();

            var routine = ReturnFirstRoutine(routineId);
            var days = routine.Days;
            days.Should().HaveCount(1);
            var day = days.First(x => x.DayOfWeek == dayOfWeek);
            day.RoutineExercises.First().Should().BeOfType<NormalRoutineExercise>();
            var normalRoutineExercise = (NormalRoutineExercise)day.RoutineExercises.First(x => x.ExerciseId.Equals(exerciseId));
            normalRoutineExercise.Weight.Should().Be(60);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The weight should'nt be negative")]
        public void ShouldBePossibleChangeTheWeightToNegative()
        {
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            var exerciseId = 10;
            
            CreateAndReturnRoutine(routineId, dayOfWeek,  exerciseId, 10, 50);

            var changeWeightTransaction = new ChangeWeightTransaction(routineId, dayOfWeek, 0, -60);
            changeWeightTransaction.Execute();
        }
    }
}
