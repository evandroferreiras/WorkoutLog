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
            var wId = new Workout.WorkoutIdentity(12, 61, 107);
            CreateAndReturnRoutine(wId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var tId = new TrainingIdentity(wId, dayAndHour);
            var srt = new StartRoutineTransaction(tId);
            srt.Execute();

            var doSetTransaction = new DoSetTransaction(tId, exerciseId,50);
            doSetTransaction.Execute();

            VerifyPendingReps(tId, exerciseId, 9,50);
        }

        [TestMethod]
        public void ShouldBePossibleToDoExerciseChangingWeight()
        {
            var exerciseId = 101;
            var wId = new Workout.WorkoutIdentity(124, 614, 1074);
            CreateAndReturnRoutine(wId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var tId = new TrainingIdentity(wId, dayAndHour);
            var srt = new StartRoutineTransaction(tId);
            srt.Execute();

            var doSetTransaction = new DoSetTransaction(tId, exerciseId, 55);
            doSetTransaction.Execute();

            VerifyPendingReps(tId, exerciseId, 9, 55);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "There is no pending exercises for this training. Go to the next.")]
        public void ShouldntBePossibleDoMoreExercisesThanAvailable()
        {
            var exerciseId = 1019;
            var wId = new Workout.WorkoutIdentity(129, 619, 1097);
            CreateAndReturnRoutine(wId, exerciseId, 2, 50);

            var dayAndHour = DateTime.Now;
            var tId = new TrainingIdentity(wId, dayAndHour);
            var startTrainingDayTransaction = new StartRoutineTransaction(tId);
            startTrainingDayTransaction.Execute();

            var doSetTransaction = new DoSetTransaction(tId, exerciseId, 50);
            doSetTransaction.Execute();

            var doSetTransaction2 = new DoSetTransaction(tId, exerciseId, 50);
            doSetTransaction2.Execute();

            var doSetTransaction3 = new DoSetTransaction(tId, exerciseId, 50);
            doSetTransaction3.Execute();
        }

        private static void VerifyPendingReps(TrainingIdentity tId, int exerciseId, int expectedReps, double lastWeight)
        {
            var tr = TrainingDayDatabase.GetTrainingRoutine(tId);
            tr.BeginDate.Should().Be(tId.DayAndHour);
            tr.TrainingDays.Should().HaveCount(1);
            var td = tr.TrainingDays.First(x => x.DayId.Equals(tId.WId.DayId));
            var tre = td.TrainingRoutineExercises.First(x => x.ExerciseId == exerciseId);
            tre.NumberOfPendingRepetitions.Should().Be(expectedReps);

            var lastRep = tre.RepsDone.Last();
            lastRep.Item2.Should().Be(lastWeight);
        }
    }
}
