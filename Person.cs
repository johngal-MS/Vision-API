using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision_API
{
    internal class Person
    {
        public string name { get; set; }
        public string[] persistedFaceIds { get; set; }
        public string personId { get; set; }
        public string userData { get; set; }
    }
}
