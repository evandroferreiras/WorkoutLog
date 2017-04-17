using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    [TestClass]
    public class ChangeRepsTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleChangeTheNumberOfReps()
        {
            var workoutId = 2;
            var trainingId = 1;
            var dayId = 1;
            var setId = 100;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId, trainingId, dayId, setId, exerciseId, 10, 50);

            var changeRepsTransaction = new ChangeRepsTransaction(workoutId, dayId, trainingId, 100, 20);
            changeRepsTransaction.Execute();

            var training = ReturnFirstTraining(workoutId, trainingId, dayId);
            var setReturned = training.Sets;
            setReturned.Should().HaveCount(1);
            setReturned.FirstOrDefault().Reps.Should().Be(20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The number of series should'nt be negative")]
        public void ShouldntBePossibleChangeTheNumberOfRepsToNegative()
        {
            var workoutId = 2;
            var trainingId = 1;
            var dayId = 1;
            var setId = 100;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId, trainingId, dayId, setId, exerciseId, 10, 50);

            var changeRepsTransaction = new ChangeRepsTransaction(workoutId, dayId, trainingId, 100, -20);
            changeRepsTransaction.Execute();

            var training = ReturnFirstTraining(workoutId, trainingId, dayId);
            var setReturned = training.Sets;
            setReturned.Should().HaveCount(1);
            setReturned.FirstOrDefault().Reps.Should().Be(20);
        }
    }
}
