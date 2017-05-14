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
    //TODO: Modify to StartTrainingDayTransaction
    //TODO: Create a GetNextExerciseTransaction
    //TODO: Create a FinishTrainingDayTransaction
    [TestClass]
    public class StartRoutineTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToStartATrainingRoutine()
        {
            var exerciseId = 10;
            var wId = new Workout.WorkoutIdentity( 1, 1, 102);
            CreateAndReturnRoutine(wId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var tId = new Training.TrainingIdentity(wId, dayAndHour);
            var srt = new StartRoutineTransaction(tId);
            srt.Execute();

            var tr = TrainingDayDatabase.GetTrainingRoutine(tId);

            tr.BeginDate.Should().Be(dayAndHour);
            tr.TrainingDays.Should().HaveCount(1);
            var td = tr.TrainingDays.First(x => x.DayId.Equals(wId.DayId));
            td.Should().NotBeNull();
        }
    }
}
