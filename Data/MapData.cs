using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdelieEngine.Data
{
    public class MapData : Data
    {
        public float Width;
        public float Height;
        public float BlockSize;
        public List<int[]> Rooms;
        public int[] BlockData;
    }
}
