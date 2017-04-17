using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    public class BaseTest
    {

        internal static List<IDay> CreateWorkOutAndReturnDays(int workoutId, int trainingId, int dayId, int setId, int exerciseId, int reps, double weight)
        {
            var trainings = new List<ITraining>();
            var sets = new List<Set> { new NormalSet(setId, reps, new Exercise(exerciseId, weight)) };
            trainings.Add(new Training(trainingId, sets));

            var days = new List<IDay>();
            days.Add(new Day(dayId, trainings));

            var addWorkoutTransaction = new AddWorkoutTransaction(workoutId, days.ToArray());
            addWorkoutTransaction.Execute();

            return days;
        }

        internal static ITraining ReturnFirstTraining(int workoutId, int trainingId, int dayId)
        {
            var workoutUpdated = WorkoutDatabase.GetWorkout(workoutId);

            var dr = workoutUpdated.Days.First(x => x.DayId == dayId);
            dr.Should().NotBeNull();

            var tr = dr.Trainings.First(x => x.TrainingId == trainingId);
            tr.Should().NotBeNull();
            return tr;
        }

    }
}
