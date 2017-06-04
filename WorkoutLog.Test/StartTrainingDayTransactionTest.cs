using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;
using WorkoutLog.Test.Base;
using WorkoutLog.Transactions;

namespace WorkoutLog.Test
{

    //TODO: Create a FinishTrainingDayTransaction
    [TestClass]
    public class StartTrainingDayTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToStartATrainingRoutine()
        {
            var exerciseId = 10;
            var wId = new Workout.WorkoutIdentity( 1, 1, 102);
            CreateAndReturnRoutine(wId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var tId = new Training.TrainingIdentity(wId, dayAndHour);
            var srt = new StartTrainingDayTransaction(tId);
            srt.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId);

            td.BeginDate.Should().Be(dayAndHour);
        }
    }
}
