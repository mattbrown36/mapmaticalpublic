using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mlShared.WebModels
{
    public class ServerManager
    {
        public string IP { get; set; }
        public DateTime LastHeardFromUtc { get; set; }
        public long TotalMemoryMb { get; set; }
        public List<MultiverseContainer> Multiverses { get; set; } = new List<MultiverseContainer>();

        public long UsedMemory {
            get {
                return Multiverses.Sum(x => x.UsedMemoryMb);
            }
        }

        public long ReservedMemory {
            get {
                return Multiverses.Sum(x => x.ReservedMemoryMb);
            }
        }

        public long RemainingMemory {
            get {
                return TotalMemoryMb - ReservedMemory;
            }
        }
    }
}





