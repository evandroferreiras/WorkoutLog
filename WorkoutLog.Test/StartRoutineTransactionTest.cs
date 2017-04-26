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
    [TestClass]
    public class StartRoutineTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToStartATrainingRoutine()
        {
            var workoutId = 1;
            var routineId = 1;
            var dayId = 1;
            var routineExerciseId = 102;
            var exerciseId = 10;
            CreateWorkOutAndRoutines(workoutId, routineId, dayId, routineExerciseId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var srt = new StartRoutineTransaction(workoutId, dayId, dayAndHour);
            srt.Execute();

            var tr = TrainingDayDatabase.GetTrainingRoutine(routineId, dayAndHour);

            tr.BeginDate.Should().Be(dayAndHour);
            tr.TrainingDays.Should().HaveCount(1);
            var td = tr.TrainingDays.First(x => x.DayId.Equals(dayId));
            td.Should().NotBeNull();
        }
    }
}
