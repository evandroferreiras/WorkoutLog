using System;

namespace WorkoutLog.Training
{
    public interface ITrainingRoutineExercise
    {
        int ExerciseId { get;  }
        
        int NumberOfPendingRepetitions { get; }
        bool ExerciseFinished { get; }
        (int repNbr, double weight)[] RepsDone { get; }
        void DoRep(double weight);

        string ExerciseName { get; set; }

    }
}