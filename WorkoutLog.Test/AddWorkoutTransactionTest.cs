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
            var routineId = 1;
            var dayId = 1;
            var setId = 102;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId,routineId,dayId, setId, exerciseId, 10, 50);
            var workoutReturned = WorkoutDatabase.GetWorkout(workoutId);

            workoutReturned.WorkoutId.Should().Be(workoutId);
            workoutReturned.Days.Should().BeEquivalentTo(days);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The number of series should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeNumberOfSeries()
        {
            var workoutId = 1;
            var routineId = 1;
            var dayId = 1;
            var setId = 102;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId, routineId, dayId, setId, exerciseId, -10, 50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The weight should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeWeight()
        {
            var workoutId = 1;
            var routineId = 1;
            var dayId = 1;
            var setId = 102;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId, routineId, dayId, setId, exerciseId, 10, -50);
            var workoutReturned = WorkoutDatabase.GetWorkout(workoutId);
        }

        [TestMethod]
        public void ShouldBePossibleToStartATrainingDay()
        {
            var workoutId = 1;
            var routineId = 1;
            var dayId = 1;
            var routineExerciseId = 102;
            var exerciseId = 10;
            var days = CreateWorkOutAndReturnDays(workoutId, routineId, dayId, routineExerciseId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var startTrainingDayTransaction = new StartTrainingDayTransaction(workoutId, dayId,  dayAndHour);
            startTrainingDayTransaction.Execute();

            var trainingDayReturned = TrainingDayDatabase.GetTrainingDay(dayId, dayAndHour);

            trainingDayReturned.BeginDate.Should().Be(dayAndHour);
            trainingDayReturned.TrainingRoutines.Should().HaveCount(1);
            var trainingRoutine = trainingDayReturned.TrainingRoutines.First(x => x.RoutineId.Equals(routineId));
            trainingRoutine.Should().NotBeNull();
        }

        [TestMethod]
        public void ShouldBePossibleToDoExerciseWithoutChangeWeightAndReps()
        {
            var workoutId = 10;
            var routineId = 12;
            var dayId = 61;
            var routineExerciseId = 107;
            var exerciseId = 101;
            var days = CreateWorkOutAndReturnDays(workoutId, routineId, dayId, routineExerciseId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var startTrainingDayTransaction = new StartTrainingDayTransaction(workoutId, dayId, dayAndHour);
            startTrainingDayTransaction.Execute();

            var doSetTransaction = new DoSetTransaction(dayId, routineId, dayAndHour, exerciseId);
            doSetTransaction.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(dayId, dayAndHour);
            td.BeginDate.Should().Be(dayAndHour);
            td.TrainingRoutines.Should().HaveCount(1);
            var tr = td.TrainingRoutines.First(x => x.RoutineId.Equals(routineId));
            var tre = tr.TrainingRoutineExercises.First(x => x.ExerciseId == exerciseId);
            tre.NumberOfPendingRepetitions.Should().Be(9);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"There is no pending exercises for this training. Go to the next.")]
        public void ShouldntBePossibleDoMoreSetsThanAvailable()
        {
            var workoutId = 10;
            var routineId = 12;
            var dayId = 61;
            var routineExerciseId = 107;
            var exerciseId = 101;
            var days = CreateWorkOutAndReturnDays(workoutId, routineId, dayId, routineExerciseId, exerciseId, 2, 50);

            var dayAndHour = DateTime.Now;
            var startTrainingDayTransaction = new StartTrainingDayTransaction(workoutId, dayId,  dayAndHour);
            startTrainingDayTransaction.Execute();

            var doSetTransaction = new DoSetTransaction(dayId, routineId, dayAndHour, exerciseId);
            doSetTransaction.Execute();

            var doSetTransaction2 = new DoSetTransaction( dayId, routineId, dayAndHour, exerciseId);
            doSetTransaction2.Execute();

            var doSetTransaction3 = new DoSetTransaction(dayId, routineId, dayAndHour, exerciseId);
            doSetTransaction3.Execute();


        }
    }
}

