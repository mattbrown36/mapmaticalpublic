using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mlShared.Models
{
    public class World : StandardModel
    {
        public string Name { get; set; } = "";

        public string OwnerId { get; set; }
        public virtual MlUser Owner { get; set; }

        public virtual List<Permission> Permissions { get; set; }
    }
}
