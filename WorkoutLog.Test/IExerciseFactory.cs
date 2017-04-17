namespace WorkoutLog.Test
{
    internal interface IExerciseFactory
    {
        IExercise Make(ISet set);
    }
}