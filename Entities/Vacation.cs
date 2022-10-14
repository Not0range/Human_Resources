using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Entities
{
    internal class Vacation : Move
    {
        public static List<Vacation> Vacations = new List<Vacation>();

        public int Duration { get; set; }

        public Vacation(int id, Employee employee, DateTime date,int duration) : base(id, employee, date)
        {
            Duration = duration;
        }
    }
}
