using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;



namespace jb_function
{
        public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HireDate { get; set; }
        public string DischargeDate { get; set; }
        public decimal Position { get; set; }
        public decimal ManagerId { get; set; }
    }
    public static class users
    {
        [FunctionName("users")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "HumanResources",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection")]out dynamic document,
            ILogger log)
        {
            //document = new User();

            string requestBody = await req.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(requestBody);
            //await document.AddAsync(user);

            document = new { Description = req, id = Guid.NewGuid() };


            log.LogInformation($"C# Queue trigger function inserted one row");
            log.LogInformation($"Description={req}");
        }
    }
}
