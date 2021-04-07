using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Country 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<City> cities { get; set; }
    }
}
