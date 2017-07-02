using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using WorkoutLog.Database;
using WorkoutLog.Test.Base;

namespace WorkoutLog.Test
{
    [TestClass]
    public class AddRoutineTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleCreateARoutine()
        {
            const int exerciseId = 10;

            var routine = new RoutineBuilder(1445, "Default")
                          .AddDayAndNormalRoutineExercise(DayOfWeek.Monday,  exerciseId, 10, 50)
                          .Build();

            var routineReturned = WorkoutDatabase.GetRoutine(1445);

            routineReturned.ShouldBeEquivalentTo(routine);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The routine name is required")]
        public void ShouldntBePossibleCreateAWorkoutWithABlankRoutineName()
        {
            const int exerciseId = 10;

            var routine = new RoutineBuilder( 2, "")
                          .AddDayAndNormalRoutineExercise(DayOfWeek.Monday, exerciseId, 10, 25)
                          .Build();
        }

    }
}

