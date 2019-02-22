using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mlShared.Models
{
    public class Permission : StandardModel
    {
        /// <summary>
        /// Can edit permissions of others, and do everything below.
        /// </summary>
        public bool Admin { get; set; }

        /// <summary>
        /// Can change the world and has no viewing restrictions, and can do everything below.
        /// </summary>
        public bool Builder { get; set; }

        /// <summary>
        /// Can view the world and can interact with certain elements of it.
        /// </summary>
        public bool Player { get; set; }

        /// <summary>
        /// Can view the world as if a player, but cannot interact with anything.
        /// </summary>
        public bool Spectator { get; set; }

        public string UserId { get; set; }
        public virtual MlUser User { get; set; }

        public Guid WorldId { get; set; }
        public virtual World World { get; set; }
    }
}
