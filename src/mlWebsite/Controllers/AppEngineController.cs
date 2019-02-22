using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mlShared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace mlWebsite.Controllers
{
    public class AppEngineController : Controller
    {
        [Route("/.well-known/acme-challenge/{id}")]
        string AcmeChallenge(string id)
        {
            string challengeResponse = @"FSfJKi0WTyxDOJgsrbJVA7W4OdDW0m6FKzydFqgoOTk.AcsZUmF_k95fO--Jl_qpnn5DZ3OreADBhCvaowMZ-10";
            return challengeResponse;
        }

        [Route("/_ah/start")]
        public IActionResult Start() {
            return new StatusCodeResult(200);
        }

        [Route("/_ah/stop")]
        public IActionResult Stop() {
            return new StatusCodeResult(200);
        }

        [Route("/_ah/ex")]
        public IActionResult Ex() {
            //Allows us to check if exception filtering/logging is working.
            throw new Exception("Test Exception was thrown.");
        }

        [Route("/_ah/db")]
        public string Db(string connectionString = null) {
            var message = "success";
            if (connectionString != null) {
                connectionString = connectionString.Replace("_is_","=");
            }

            try
            {
                if (connectionString == null) {
                    connectionString = ApplicationDbContext.DefaultConnectionString;
                }
                ApplicationDbContext.Migrate(connectionString);
            }
            catch (Exception ex) {
                message = ex.ToString();
            }

            if (connectionString.Contains("pwd=")) {
                connectionString = connectionString.Substring(0, connectionString.IndexOf("pwd="));
            }

            message += Environment.NewLine + Environment.NewLine + "connectionString: " + connectionString;

            return message;
        }

        [Route("/_ah/health")]
        public IActionResult Health() {
            return new StatusCodeResult(200);
            //Return a 503 if we're not healthy.
            //return new StatusCodeResult(503);
        }
    }
}
