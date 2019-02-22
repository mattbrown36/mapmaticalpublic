using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mlShared.WebModels
{
    public class MultiverseContainer
    {
        public string UserId { get; set; }
        public DateTime LastHeardFromUtc { get; set; }
        public long ReservedMemoryMb { get; set; }
        public long UsedMemoryMb { get; set; }
        public long WorldSizeMb { get; set; }
        public string IP { get; set; }
        public int ExternalPort { get; set; }
        
    }
}
