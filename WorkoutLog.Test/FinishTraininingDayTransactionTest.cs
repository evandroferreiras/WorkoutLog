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
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            CreateAndReturnRoutine(routineId, dayOfWeek, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var tId = new TrainingIdentity(routineId, dayOfWeek, dayAndHour);
            var srt = new StartTrainingDayTransaction(tId.RoutineId, tId.DayOfWeek, tId.DayAndHour);
            srt.Execute();

            var endDate = DateTime.Now;
            var ftt = new FinishTraininingDayTransaction(tId.DayOfWeek, tId.DayAndHour, endDate);
            ftt.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId.DayOfWeek, tId.DayAndHour);

            td.EndDate.Should().Be(endDate);
        }
    }
}
