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
    
    [TestClass]
    public class AddNormalRoutineExerciseTransactionTest : BaseTest
    {
        [TestMethod]
        public void ShouldBePossibleToAdd()
        {
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            var routine = new RoutineBuilder(routineId, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(dayOfWeek,  10, 10, 50)
                                            .Build();


            var aret = new AddNormalRoutineExerciseTransaction(routineId, dayOfWeek, exerciseId: 50, reps: 7, weight: 89.6);
            aret.Execute();

            var returned = ReturnFirstRoutine(routineId);
            var day = returned.Days.FirstOrDefault(x => x.DayOfWeek == dayOfWeek);
            day.Should().NotBeNull();
            day.RoutineExercises.Should().HaveCount(2);
            day.RoutineExercises.Last().Weight.Should().Be(89.6);


        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The day doesnt exist.")]
        public void ShouldntBePossibleToAddToAnInexistentDay()
        {
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;

            
            var routine = new RoutineBuilder(routineId, "DefaultRoutine")
                                            .AddDayAndNormalRoutineExercise(dayOfWeek, 10, 10, 50)
                                            .Build();

            const DayOfWeek anotherDay = DayOfWeek.Saturday;            
            var aret = new AddNormalRoutineExerciseTransaction(routineId, anotherDay, exerciseId: 50, reps: 7, weight: 89.6);
            aret.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The number of series should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeNumberOfSeries()
        {
            const int exerciseId = 10;
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();
            var dayOfWeek = DayOfWeek.Monday;
            CreateAndReturnRoutine(routineId,dayOfWeek, exerciseId, -10, 50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The weight should'nt be negative")]
        public void ShouldntBePossibleCreateAWorkoutWithNegativeWeight()
        {
            const int exerciseId = 10;
            var routineId = Database.WorkoutDatabase.GetNextRoutineId();

            CreateAndReturnRoutine(routineId, DayOfWeek.Monday, exerciseId, 10, -50);
            var routineReturned = WorkoutDatabase.GetRoutine(routineId);
        }
    }
}
