﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;

namespace WorkoutLog.Training
{
    public class TrainingRoutineExercise : ITrainingRoutineExercise
    {
        private readonly int reps;
        private readonly int exerciseId;
        private int numberOfPendingRepetitions;
        private string exerciseName;
        private (int repNbr, double weight)[] repsDone;

        public TrainingRoutineExercise(int exerciseId, int reps)
        {
            this.exerciseId = exerciseId;
            this.reps = reps;
            this.numberOfPendingRepetitions = reps;
            this.repsDone = new(int, double)[0];
            
        }

        public int ExerciseId
        {
            get
            {
                return exerciseId;
            }
        }

        public bool ExerciseFinished => (numberOfPendingRepetitions == 0);

        public int NumberOfPendingRepetitions
        {
            get
            {
                return numberOfPendingRepetitions;
            }
        }

        public (int repNbr, double weight)[] RepsDone
        {
            get
            {
                return repsDone;
            }
        }

        public string ExerciseName { get => exerciseName; set => exerciseName = value; }

        public void DoRep(double weight)
        {
            if (ExerciseFinished)
            {
                throw new ArgumentException("There is no pending exercises for this training. Go to the next.");
            }

            repsDone = TrainingDayDatabase.DoRep(repsDone, weight);
            numberOfPendingRepetitions--;
        }
    }
}
