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
    public class ChangeWeightTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleChangeTheWeight()
        {
            var workoutId = 2;
            var trainingId = 1;
            var dayId = 1;
            var setId = 100;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId, trainingId, dayId, setId, exerciseId, 10, 50);

            var changeWeightTransaction = new ChangeWeightTransaction(workoutId, dayId, trainingId, setId, exerciseId, 60);
            changeWeightTransaction.Execute();

            var training = ReturnFirstTraining(workoutId, trainingId, dayId);
            var setReturned = training.Sets;
            setReturned.Should().HaveCount(1);
            setReturned.First().Should().BeOfType<NormalSet>();
            var normalSet = (NormalSet)setReturned.First();
            normalSet.Exercise.Weight.Should().Be(60);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The weight should'nt be negative")]
        public void ShouldBePossibleChangeTheWeightToNegative()
        {
            var workoutId = 2;
            var trainingId = 1;
            var dayId = 1;
            var setId = 100;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId, trainingId, dayId, setId, exerciseId, 10, 50);

            var changeWeightTransaction = new ChangeWeightTransaction(workoutId, dayId, trainingId, setId, exerciseId, -60);
            changeWeightTransaction.Execute();
        }
    }
}
