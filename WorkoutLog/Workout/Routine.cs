using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Workout
{
    public class Routine : IRoutine
    {
        private string name;
        private readonly int routineId;
        private IDay[] days;

        public Routine(int routineId, string name)
        {
            
            this.routineId = routineId;
            if (routineId <= 0)
                throw new ArgumentException("routineId should be greater than zero");
            UpdateName(name);

            days = new IDay[0];
        }

        public int RoutineId => routineId;
       
        public string Name => name;

        public IDay[] Days { get => days; set => days = value; }

        public void UpdateName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("The routine name is required");
            this.name = name;
        }
    }
}
