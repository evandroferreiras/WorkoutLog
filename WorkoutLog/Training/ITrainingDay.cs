using System;

namespace WorkoutLog.Training
{
    public interface ITrainingDay
    {

        int DayId { get;  }

        ITrainingRoutineExercise[] TrainingRoutineExercises { get; }
        DateTime BeginDate { get; }
        DateTime EndDate { get; set; }

        ITrainingRoutineExercise GetNextExercise();
    }
}