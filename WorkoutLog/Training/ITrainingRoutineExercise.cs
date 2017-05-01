using System;

namespace WorkoutLog.Training
{
    public interface ITrainingRoutineExercise
    {
        int ExerciseId { get;  }
        int NumberOfPendingRepetitions { get; }
        (int repNbr, double weight)[] RepsDone { get; }
        void DoRep(double weight);
    }
}