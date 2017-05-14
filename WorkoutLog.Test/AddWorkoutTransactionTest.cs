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
            var wId = new Workout.WorkoutIdentity(1445, 1432, 102);
            var routine = CreateAndReturnRoutine(wId, exerciseId, 10, 50);
            var routineReturned = WorkoutDatabase.GetRoutine(wId);

            routineReturned.ShouldBeEquivalentTo(routine);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The number of series should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeNumberOfSeries()
        {
            const int exerciseId = 10;
            var identity = new Workout.WorkoutIdentity(3, 4, 103);
            CreateAndReturnRoutine(identity, exerciseId, -10, 50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The weight should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeWeight()
        {
            const int exerciseId = 10;
            var identity = new Workout.WorkoutIdentity(1, 1, 102);
            CreateAndReturnRoutine(identity, exerciseId, 10, -50);
            var workoutReturned = WorkoutDatabase.GetRoutine(identity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The routine name is required")]
        public void ShouldntBePossibleCreateAWorkoutWithABlankRoutineName()
        {
            const int exerciseId = 10;
            var wid = new Workout.WorkoutIdentity( 1, 1, 102);
            var routine = new RoutineBuilder( wid.RoutineId, "")
                          .AddNormalRoutineExercise(wid.DayId, wid.RoutineExerciseId, exerciseId, 10, 25)
                          .Build();
        }

    }
}

