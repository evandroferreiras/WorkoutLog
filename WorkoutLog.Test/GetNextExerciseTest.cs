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
        
        TrainingIdentity tId;
        private int routineId;

        public GetNextExerciseTest()
        {

        }

        [TestInitialize]
        public void Initialize()
        {
            WorkoutDatabase.Clear();
            TrainingDayDatabase.Clear();

            routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            tId = new TrainingIdentity(routineId, dayOfWeek, DateTime.Now);

            var routine = new RoutineBuilder(routineId, "Default")
              .AddDayAndNormalRoutineExercise(DayOfWeek.Friday, firstExercise, 1, 10)
              .AddDayAndNormalRoutineExercise(DayOfWeek.Monday, secondExercise, 1, 10)
              .AddDayAndNormalRoutineExercise(DayOfWeek.Thursday, thirdExercise, 1, 10)
              .Build();
            
            var srt = new StartTrainingDayTransaction(tId.RoutineId, tId.DayOfWeek, tId.DayAndHour);
            srt.Execute();


        }

        [TestMethod]
        public void ShouldReturnTheFirstExercise()
        {            
            var dayOfWeek = DayOfWeek.Friday;

            var tId = new TrainingIdentity(routineId, dayOfWeek, this.tId.DayAndHour);
            var srt = new StartTrainingDayTransaction(tId.RoutineId, tId.DayOfWeek, tId.DayAndHour);
            srt.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId.DayOfWeek, tId.DayAndHour);

            var tre = td.GetNextExercise();
            tre.ExerciseId.Should().Be(firstExercise);
        }

        [TestMethod]
        public void ShouldReturnTheSecondExercise()
        {
            var doSetTransaction = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, firstExercise, 10);
            doSetTransaction.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId.DayOfWeek, tId.DayAndHour);
            var tre = td.GetNextExercise();
            tre.ExerciseId.Should().Be(secondExercise);
        }

        [TestMethod]
        public void ShouldReturnNextExerciseEvenWithoutOrder()
        {
            var doSetTransaction = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, firstExercise, 10);
            doSetTransaction.Execute();

            doSetTransaction = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, thirdExercise, 10);
            doSetTransaction.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId.DayOfWeek, tId.DayAndHour);
            var tre = td.GetNextExercise();
            tre.ExerciseId.Should().Be(secondExercise);
        }

        [TestMethod]
        public void ShouldntReturnAnyExercise()
        {
            var doSetTransaction = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, firstExercise, 10);
            doSetTransaction.Execute();

            doSetTransaction = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, secondExercise, 10);
            doSetTransaction.Execute();

            doSetTransaction = new DoSetTransaction(tId.DayOfWeek, tId.DayAndHour, thirdExercise, 10);
            doSetTransaction.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId.DayOfWeek, tId.DayAndHour);
            var tre = td.GetNextExercise();
            tre.Should().BeNull();
        }


    }
}

