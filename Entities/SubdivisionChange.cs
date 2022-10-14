using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Entities
{
    internal class SubdivisionChange : Move, IState
    {
        public static List<SubdivisionChange> SubdivisionChanges = new List<SubdivisionChange>();
        public Subdivision Subdivision { get; set; }

        public string Position => null;

        public SubdivisionChange(int id, Employee employee, DateTime date,Subdivision subdivision) : base(id, employee, date)
        {
            Subdivision = subdivision;
        }
    }
}
