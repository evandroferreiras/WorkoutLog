using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;
using WorkoutLog.Test.Base;
using WorkoutLog.Training;
using WorkoutLog.Transactions;

namespace WorkoutLog.Test
{
    [TestClass]
    public class DoSetTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToDoExerciseWithoutChangeWeightAndReps()
        {
            var exerciseId = 101;
            var wId = new WorkoutIdentity(12, DayOfWeek.Monday, 107);
            CreateAndReturnRoutine(wId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var tId = new TrainingIdentity(wId.RoutineId, wId.DayOfWeek, dayAndHour);
            var srt = new StartTrainingDayTransaction(tId.RoutineId, tId.DayOfWeek, tId.DayAndHour);
            srt.Execute();

            var doSetTransaction = new DoSetTransaction(tId.DayOfWeek,tId.DayAndHour, exerciseId,50);
            doSetTransaction.Execute();

            VerifyPendingReps(tId, exerciseId, 9,50);
        }

        [TestMethod]
        public void ShouldBePossibleToDoExerciseChangingWeight()
        {
            var exerciseId = 101;
            var wId = new WorkoutIdentity(124, DayOfWeek.Monday, 1074);
            CreateAndReturnRoutine(wId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var tId = new TrainingIdentity(wId.RoutineId, wId.DayOfWeek, dayAndHour);
            var srt = new StartTrainingDayTransaction(tId.RoutineId, tId.DayOfWeek, tId.DayAndHour);
            srt.Execute();

            var doSetTransaction = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, exerciseId, 55);
            doSetTransaction.Execute();

            VerifyPendingReps(tId, exerciseId, 9, 55);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "There is no pending exercises for this training. Go to the next.")]
        public void ShouldntBePossibleDoMoreExercisesThanAvailable()
        {
            var exerciseId = 1019;
            var wId = new WorkoutIdentity(129, DayOfWeek.Monday, 1097);
            CreateAndReturnRoutine(wId, exerciseId, 2, 50);

            var dayAndHour = DateTime.Now;
            var tId = new TrainingIdentity(wId.RoutineId, wId.DayOfWeek, dayAndHour);
            var startTrainingDayTransaction = new StartTrainingDayTransaction(tId.RoutineId, tId.DayOfWeek, tId.DayAndHour);
            startTrainingDayTransaction.Execute();

            var doSetTransaction = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, exerciseId, 50);
            doSetTransaction.Execute();

            var doSetTransaction2 = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, exerciseId, 50);
            doSetTransaction2.Execute();

            var doSetTransaction3 = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, exerciseId, 50);
            doSetTransaction3.Execute();
        }

        [TestMethod]
        public void ExerciseShouldBeFinishedAfterAllSets()
        {
            var exerciseId = 2019;
            var wId = new WorkoutIdentity(2829, DayOfWeek.Monday, 2097);
            CreateAndReturnRoutine(wId, exerciseId, 2, 50);

            var dayAndHour = DateTime.Now;
            var tId = new TrainingIdentity(wId.RoutineId, wId.DayOfWeek, dayAndHour);
            var startTrainingDayTransaction = new StartTrainingDayTransaction(tId.RoutineId, tId.DayOfWeek, tId.DayAndHour);
            startTrainingDayTransaction.Execute();

            var doSetTransaction = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, exerciseId, 50);
            doSetTransaction.Execute();

            var doSetTransaction2 = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, exerciseId, 50);
            doSetTransaction2.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId.DayOfWeek, tId.DayAndHour);
            var tre = td.TrainingRoutineExercises.First(x => x.ExerciseId == exerciseId);
            tre.ExerciseFinished.Should().BeTrue();
        }

        private static void VerifyPendingReps(TrainingIdentity tId, int exerciseId, int expectedReps, double lastWeight)
        {
            var td = TrainingDayDatabase.GetTrainingDay(tId.DayOfWeek, tId.DayAndHour);
            td.BeginDate.Should().Be(tId.DayAndHour);            
            var tre = td.TrainingRoutineExercises.First(x => x.ExerciseId == exerciseId);
            tre.NumberOfPendingRepetitions.Should().Be(expectedReps);

            var lastRep = tre.RepsDone.Last();
            lastRep.Item2.Should().Be(lastWeight);
        }
    }
}
