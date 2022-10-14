using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Entities
{
    internal class Dismissal : Move, IState
    {
        public const string DISMISSALED = "Уволен";

        public static List<Dismissal> Dismissals = new List<Dismissal>();

        public Dismissal(int id, Employee employee, DateTime date) : base(id, employee, date) { }

        public Subdivision Subdivision => null;

        public string Position => DISMISSALED;
    }
}
