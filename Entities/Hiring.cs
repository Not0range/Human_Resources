using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Entities
{
    internal class Hiring : Move, IState
    {
        public static List<Hiring> Hirings = new List<Hiring>();

        public Subdivision Subdivision { get; set; }

        public string Position { get; set; }

        public Hiring(int id, Employee employee, DateTime date, Subdivision subdivision, string position) : base(id, employee, date)
        {
            Subdivision = subdivision;
            Position = position;
        }
    }
}
