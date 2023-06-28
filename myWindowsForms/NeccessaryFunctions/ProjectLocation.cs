using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWindowsForms.NeccessaryFunctions
{
    public class ProjectLocation
    {
        public string Provence { get; set; }
        public string City { get; set; }
        public string District { get; set; }

        public ProjectLocation(string provence, string city, string district)
        {
            this.Provence = provence.Trim();
            this.City = city.Trim();
            this.District = district.Trim();
        }
    }
}
