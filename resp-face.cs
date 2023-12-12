using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision_API
{
    internal class resp_face
    {
        public string faceid;
        public rect rectangle;
    }
    public class rect
    {
        public int top;
        public int left;
        public int width;
        public int height;
    }
}
