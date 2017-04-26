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
    public class DoSetTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToDoExerciseWithoutChangeWeightAndReps()
        {
            var workoutId = 10;
            var routineId = 12;
            var dayId = 61;
            var routineExerciseId = 107;
            var exerciseId = 101;
            CreateWorkOutAndRoutines(workoutId, routineId, dayId, routineExerciseId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var srt = new StartRoutineTransaction(workoutId, routineId, dayAndHour);
            srt.Execute();

            var doSetTransaction = new DoSetTransaction(routineId, dayAndHour, dayId, exerciseId);
            doSetTransaction.Execute();

            var tr = TrainingDayDatabase.GetTrainingRoutine(routineId, dayAndHour);
            tr.BeginDate.Should().Be(dayAndHour);
            tr.TrainingDays.Should().HaveCount(1);
            var td = tr.TrainingDays.First(x => x.DayId.Equals(dayId));
            var tre = td.TrainingRoutineExercises.First(x => x.ExerciseId == exerciseId);
            tre.NumberOfPendingRepetitions.Should().Be(9);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "There is no pending exercises for this training. Go to the next.")]
        public void ShouldntBePossibleDoMoreSetsThanAvailable()
        {
            var workoutId = 190;
            var routineId = 129;
            var dayId = 619;
            var routineExerciseId = 1097;
            var exerciseId = 1019;
            CreateWorkOutAndRoutines(workoutId, routineId, dayId, routineExerciseId, exerciseId, 2, 50);

            var dayAndHour = DateTime.Now;
            var startTrainingDayTransaction = new StartRoutineTransaction(workoutId, routineId, dayAndHour);
            startTrainingDayTransaction.Execute();

            var doSetTransaction = new DoSetTransaction(routineId, dayAndHour, dayId, exerciseId);
            doSetTransaction.Execute();

            var doSetTransaction2 = new DoSetTransaction(routineId, dayAndHour, dayId, exerciseId);
            doSetTransaction2.Execute();

            var doSetTransaction3 = new DoSetTransaction(routineId, dayAndHour, dayId, exerciseId);
            doSetTransaction3.Execute();


        }
    }
}
