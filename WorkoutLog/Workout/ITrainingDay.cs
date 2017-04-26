using System;

namespace WorkoutLog.Workout
{
    public interface ITrainingDay
    {

        int DayId { get;  }

        ITrainingRoutineExercise[] TrainingRoutineExercises { get; }
    }
}