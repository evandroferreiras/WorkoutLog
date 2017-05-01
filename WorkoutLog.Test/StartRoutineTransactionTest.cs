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
            var id = new Workout.WorkoutIdentity(workoutId, routineId, dayId, routineExerciseId);
            CreateWorkOutAndRoutines(id, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var tId = new Training.TrainingIdentity(id, dayAndHour);
            var srt = new StartRoutineTransaction(tId);
            srt.Execute();

            var tr = TrainingDayDatabase.GetTrainingRoutine(tId);

            tr.BeginDate.Should().Be(dayAndHour);
            tr.TrainingDays.Should().HaveCount(1);
            var td = tr.TrainingDays.First(x => x.DayId.Equals(dayId));
            td.Should().NotBeNull();
        }
    }
}
