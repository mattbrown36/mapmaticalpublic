using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace mlShared.Models
{
    public class MlUser : IdentityUser {

        /// <summary>
        /// Is a developer or admin and can see special tools.
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// Ignore account paid until. User's account always enabled.
        /// </summary>
        public bool FreeAccount { get; set; }

        /// <summary>
        /// If this is less than the current date, then the user cannot start up their multiverse.
        /// </summary>
        public DateTime AccountPaidUntil { get; set; }

        /// <summary>
        /// Maximum amount of storage for multiverse file, pictures, and other resources.
        /// </summary>
        public int AllowedStorageMBs { get; set; }

        public virtual List<World> Worlds { get; set; }
        public virtual List<Permission> Permissions { get; set; }
    }
}



