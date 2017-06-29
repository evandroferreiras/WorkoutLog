﻿using FluentAssertions;
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
    public class StartTrainingDayTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToStartATrainingRoutine()
        {
            var exerciseId = 10;
            var wId = new WorkoutIdentity( 1, DayOfWeek.Monday, 102);
            CreateAndReturnRoutine(wId, exerciseId, 10, 50);

            var dayAndHour = DateTime.Now;
            var tId = new TrainingIdentity(wId.RoutineId, wId.DayOfWeek, dayAndHour);
            var srt = new StartTrainingDayTransaction(tId.RoutineId, tId.DayOfWeek, tId.DayAndHour);
            srt.Execute();

            var td = TrainingDayDatabase.GetTrainingDay(tId.DayOfWeek, tId.DayAndHour);

            td.BeginDate.Should().Be(dayAndHour);
        }
    }
}
