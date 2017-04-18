using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using System.Linq;

namespace WorkoutLog.Test
{
//TODO: Corrigir os namespaces 
    [TestClass]
    public class AddWorkoutTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleCreateAWorkout()
        {
            var workoutId = 1;
            var trainingId = 1;
            var dayId = 1;
            var setId = 102;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId,trainingId,dayId, setId, exerciseId, 10, 50);
            var workoutReturned = WorkoutDatabase.GetWorkout(workoutId);

            workoutReturned.WorkoutId.Should().Be(workoutId);
            workoutReturned.Days.Should().BeEquivalentTo(days);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The number of series should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeNumberOfSeries()
        {
            var workoutId = 1;
            var trainingId = 1;
            var dayId = 1;
            var setId = 102;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId, trainingId, dayId, setId, exerciseId, -10, 50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The weight should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeWeight()
        {
            var workoutId = 1;
            var trainingId = 1;
            var dayId = 1;
            var setId = 102;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId, trainingId, dayId, setId, exerciseId, 10, -50);
            var workoutReturned = WorkoutDatabase.GetWorkout(workoutId);
        }

        [TestMethod]
        public void ShouldBePossibleDoStartATrainingDay()
        {
            var workoutId = 1;
            var trainingId = 1;
            var dayId = 1;
            var setId = 102;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId, trainingId, dayId, setId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var startTrainingDayTransaction = new StartTrainingDayTransaction(workoutId, dayId, trainingId, dayAndHour);
            startTrainingDayTransaction.Execute();

            var trainingDayReturned = WorkoutDatabase.GetTrainingDay(workoutId, dayId, trainingId, dayAndHour);

            trainingDayReturned.BeginDate.Should().Be(dayAndHour);
            trainingDayReturned.TrainingSets.Should().HaveCount(1);
            var trainingSet = trainingDayReturned.TrainingSets.First(x => x.Set.SetId.Equals(setId));
            trainingSet.Should().NotBeNull();
            //TrainingDay
            //-----------------
            // + StartedDate
            // + TrainingSet[] trainingSets

            //TrainingSet
            //-----------------
            // + ISet set
            // + FinishedDateTime 

        }

    }
}

