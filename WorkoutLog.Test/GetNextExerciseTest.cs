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
    public class GetNextExerciseTest : BaseTest
    {
        private int firstExercise = 101;
        private int secondExercise = 102;
        private int thirdExercise = 103;
        Workout.WorkoutIdentity wId = new Workout.WorkoutIdentity(12, DayOfWeek.Monday, 107);
        TrainingIdentity tId;

        public GetNextExerciseTest()
        {

        }

        [TestInitialize]
        public void Initialize()
        {
            WorkoutDatabase.Clear();
            TrainingDayDatabase.Clear();

            tId = new TrainingIdentity(wId, DateTime.Now);

            var routine = new RoutineBuilder(wId.RoutineId, "Default")
              .AddDayAndNormalRoutineExercise(DayOfWeek.Friday, wId.RoutineExerciseId, firstExercise, 1, 10)
              .AddDayAndNormalRoutineExercise(DayOfWeek.Monday, wId.RoutineExerciseId, secondExercise, 1, 10)
              .AddDayAndNormalRoutineExercise(DayOfWeek.Thursday, wId.RoutineExerciseId, thirdExercise, 1, 10)
              .Build();
            
            var srt = new StartTrainingDayTransaction(tId);
            srt.Execute();


        }

        [TestMethod]
        public void ShouldReturnTheFirstExercise()
        {
            var _wId = new Workout.WorkoutIdentity(12, DayOfWeek.Friday, 107);
            var _tId = new TrainingIdentity(_wId, tId.DayAndHour);
            var srt = new StartTrainingDayTransaction(_tId);
            srt.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(_tId);



            var tre = td.GetNextExercise();
            tre.ExerciseId.Should().Be(firstExercise);
        }

        [TestMethod]
        public void ShouldReturnTheSecondExercise()
        {
            var doSetTransaction = new DoSetTransaction(tId, firstExercise, 10);
            doSetTransaction.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId);
            var tre = td.GetNextExercise();
            tre.ExerciseId.Should().Be(secondExercise);
        }

        [TestMethod]
        public void ShouldReturnNextExerciseEvenWithoutOrder()
        {
            var doSetTransaction = new DoSetTransaction(tId, firstExercise, 10);
            doSetTransaction.Execute();

            doSetTransaction = new DoSetTransaction(tId, thirdExercise, 10);
            doSetTransaction.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId);
            var tre = td.GetNextExercise();
            tre.ExerciseId.Should().Be(secondExercise);
        }

        [TestMethod]
        public void ShouldntReturnAnyExercise()
        {
            var doSetTransaction = new DoSetTransaction(tId, firstExercise, 10);
            doSetTransaction.Execute();

            doSetTransaction = new DoSetTransaction(tId, secondExercise, 10);
            doSetTransaction.Execute();

            doSetTransaction = new DoSetTransaction(tId, thirdExercise, 10);
            doSetTransaction.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId);
            var tre = td.GetNextExercise();
            tre.Should().BeNull();
        }


    }
}

