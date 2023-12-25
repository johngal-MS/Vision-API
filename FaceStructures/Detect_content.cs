using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision_API
{
    internal class Detect_content
    {
        public string PersonGroupId;
        public string[] faceids;
        public int maxNumOfCandidatesReturned = 1;
        public double confidenceThreshold = 0.7;
    }
}
