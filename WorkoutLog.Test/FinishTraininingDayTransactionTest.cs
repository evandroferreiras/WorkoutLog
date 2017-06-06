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
    public class FinishTraininingDayTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldFinishTheTraining()
        {
            var exerciseId = 10221;
            var wId = new Workout.WorkoutIdentity(122, 612, 10272);
            CreateAndReturnRoutine(wId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var tId = new TrainingIdentity(wId, dayAndHour);
            var srt = new StartTrainingDayTransaction(tId);
            srt.Execute();

            var endDate = DateTime.Now;
            var ftt = new FinishTraininingDayTransaction(tId, endDate);
            ftt.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId);

            td.EndDate.Should().Be(endDate);
        }
    }
}
