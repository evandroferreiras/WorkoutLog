namespace WorkoutLog.Test
{
    internal interface ISet
    {
        int Reps { get; }
        int SetId { get; }

        void UpdateReps(int value);
    }
}