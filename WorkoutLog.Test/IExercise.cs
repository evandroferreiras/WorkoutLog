namespace WorkoutLog.Test 
{
    public interface IExercise
    {
        int ExerciseId { get; }
        double Weight { get;}
        void UpdateWeight(double weight);
    }
}
