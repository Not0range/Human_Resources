using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Entities
{
    internal class Subdivision
    {
        public static List<Subdivision> Subdivisions = new List<Subdivision>();
        public int Id { get; set; }
        public string Name { get; set; }

        public Subdivision(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool Check()
        {
            return Hiring.Hirings.Any(t => t.Subdivision == this) &&
                SubdivisionChange.SubdivisionChanges.Any(t => t.Subdivision == this);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
