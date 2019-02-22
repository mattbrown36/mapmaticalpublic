using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mlShared.Data;
using mlShared.Models;
using mlWebsite.Models.ApiViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mlWebsite.Controllers
{
    [Route("api/v1/[controller]")]
    public class WorldsController : Controller
    {

        private readonly UserManager<MlUser> _userManager;

        public WorldsController(UserManager<MlUser> userManager) {
            this._userManager = userManager;
        }

        public async Task<List<WorldSummaryViewModel>> Get() {
            if (this.User == null || !this.User.Identity.IsAuthenticated) {
                //Shouldn't happen, but if it does we'll return an empty list.
                return new List<WorldSummaryViewModel>();
            }

            var userId = (await this._userManager.GetUserAsync(this.User)).Id;

            var theirWorlds = ApplicationDbContext.WithDb((db)=> {
                return db.Worlds.Where(x => x.OwnerId == userId).OrderBy(x => x.Name).ToList();
            });

            var worldsWithPermissions = ApplicationDbContext.WithDb((db) => {
                return db.Permissions.Where(x => x.UserId == userId).Select(x => x.World).ToList();
            }).OrderBy(x => x.OwnerId).ThenBy(x => x.Name).ToList();

            var ret = new List<WorldSummaryViewModel>();

            Action<World> addWorld = (world) =>
            {
                if(!ret.Any(x => x.id == world.Id))
                {
                    ret.Add(new WorldSummaryViewModel()
                    {
                        id = world.Id,
                        name = world.Name,
                    });
                }
            };

            foreach (var world in theirWorlds)
                addWorld(world);

            foreach (var world in worldsWithPermissions)
                addWorld(world);

            return ret;
        }
    }
}
