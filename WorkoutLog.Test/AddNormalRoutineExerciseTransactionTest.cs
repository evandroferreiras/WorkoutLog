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
using WorkoutLog.Workout;

namespace WorkoutLog.Test
{

    //TODO: Modify to transactions do the correct action and delete WorkoutIdentity
    //TODO: Create am Id renegerator to RoutineId (could be a guid)
    [TestClass]
    public class AddNormalRoutineExerciseTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToAdd()
        {
            var wId = new WorkoutIdentity( 47, DayOfWeek.Monday, 54);

            var routine = new RoutineBuilder( wId.RoutineId, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(wId.DayOfWeek, wId.RoutineExerciseId, 10, 10, 50)
                                            .Build();


            var aret = new AddNormalRoutineExerciseTransaction(wId.RoutineId, wId.DayOfWeek, exerciseId: 50, reps: 7, weight: 89.6);
            aret.Execute();

            var returned = ReturnFirstRoutine(wId.RoutineId);
            var day = returned.Days.FirstOrDefault(x => x.DayOfWeek == wId.DayOfWeek);
            day.Should().NotBeNull();
            day.RoutineExercises.Should().HaveCount(2);
            day.RoutineExercises.Last().Weight.Should().Be(89.6);


        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The day doesnt exist.")]
        public void ShouldntBePossibleToAddToAnInexistentDay()
        {
            var wId = new WorkoutIdentity( 45, DayOfWeek.Monday, 54);
            
            var routine = new RoutineBuilder( wId.RoutineId, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(DayOfWeek.Friday, wId.RoutineExerciseId, 10, 10, 50)
                                            .Build();

            const DayOfWeek anotherDay = DayOfWeek.Saturday;
            var anotherWId = new WorkoutIdentity( wId.RoutineId, anotherDay, wId.RoutineExerciseId);
            var aret = new AddNormalRoutineExerciseTransaction(anotherWId.RoutineId, anotherWId.DayOfWeek, exerciseId: 50, reps: 7, weight: 89.6);
            aret.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The number of series should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeNumberOfSeries()
        {
            const int exerciseId = 10;
            var identity = new WorkoutIdentity(3);
            CreateAndReturnRoutine(identity, exerciseId, -10, 50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The weight should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeWeight()
        {
            const int exerciseId = 10;
            var identity = new WorkoutIdentity(1);
            CreateAndReturnRoutine(identity, exerciseId, 10, -50);
            var routineReturned = WorkoutDatabase.GetRoutine(identity.RoutineId);
        }
    }
}
