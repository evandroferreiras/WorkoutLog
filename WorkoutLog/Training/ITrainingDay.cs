using System;

namespace WorkoutLog.Training
{
    public interface ITrainingDay
    {

        DayOfWeek DayOfWeek { get;  }

        ITrainingRoutineExercise[] TrainingRoutineExercises { get; }
        DateTime BeginDate { get; }
        DateTime EndDate { get; set; }

        ITrainingRoutineExercise GetNextExercise();
    }
}