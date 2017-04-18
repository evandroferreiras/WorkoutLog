namespace WorkoutLog.Test
{
    public class NullExercise : IExercise
    {
        public int ExerciseId => 0;

        public double Weight => 0;

        public void UpdateWeight(double weight)
        {                        
        }
    }
}
