using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using WorkoutLog.Database;
using WorkoutLog.Test.Base;
using WorkoutLog.Transactions;

namespace WorkoutLog.Test
{ 
    [TestClass]
    public class AddWorkoutTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleCreateAWorkout()
        {
            var workoutId = 1;
            var routineId = 1;
            var dayId = 1;
            var routineExerciseId = 102;
            var exerciseId = 10;
            var identity = new Workout.WorkoutIdentity(workoutId, routineId, dayId, routineExerciseId);
            var routines = CreateWorkOutAndRoutines(identity, exerciseId, 10, 50);
            var workoutReturned = WorkoutDatabase.GetWorkout(identity);

            workoutReturned.WorkoutId.Should().Be(workoutId);
            workoutReturned.Routines.Should().BeEquivalentTo(routines);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The number of series should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeNumberOfSeries()
        {
            var workoutId = 1;
            var routineId = 1;
            var dayId = 1;
            var routineExerciseId = 102;
            var exerciseId = 10;
            var identity = new Workout.WorkoutIdentity(workoutId, routineId, dayId, routineExerciseId);
            CreateWorkOutAndRoutines(identity, exerciseId, -10, 50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The weight should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeWeight()
        {
            var exerciseId = 10;
            var identity = new Workout.WorkoutIdentity(1, 1, 1, 102);
            CreateWorkOutAndRoutines(identity, exerciseId, 10, -50);
            var workoutReturned = WorkoutDatabase.GetWorkout(identity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The routine name is required")]
        public void ShouldntBePossibleCreateAWorkoutWithABlankRoutineName()
        {
            const int exerciseId = 10;
            var wid = new Workout.WorkoutIdentity(1, 1, 1, 102);
            var routine = new RoutineBuilder(wid.WorkoutId, wid.RoutineId, "")
                          .AddNormalRoutineExercise(wid.DayId, wid.RoutineExerciseId, exerciseId, 10, 25)
                          .Build();
        }

    }
}

