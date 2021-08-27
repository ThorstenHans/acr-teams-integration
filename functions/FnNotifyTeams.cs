using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;

namespace Thns.Functions
{
    public class FnNotifyTeams
    {
        public FnNotifyTeams(IOptions<TeamsConfig> options)
        {
            Config = options.Value; 
        }

        protected TeamsConfig Config { get; }

        
        [FunctionName("FnNotifyTeams")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "images")] HttpRequest req,
            ILogger log)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic d = JsonConvert.DeserializeObject(body);
            string action = d?.action;

            if (action != "push" && action != "chart_push")
            {
                log.LogWarning($"Action {action} is not supported.");
                return new BadRequestResult();
            }
            var metadata = ImageMetadata.FromPayload(d);
            if(metadata == null)
            {
                log.LogWarning($"Received invalid request. Got {body}");
                return new BadRequestResult();
            }
            var message = new {
                Title = $"New Container Image published in ACR",
                Text = $"`{metadata.Repository}:{metadata.Tag}` has been published at `{metadata.Registry}`. You can pull it now using: {Environment.NewLine}`docker pull {metadata.Registry}/{metadata.Repository}:{metadata.Tag}{Environment.NewLine}`"
            };
            var h = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

            var r = await h.PostAsync(Config.TeamsWebhookUrl, content);
            if (r.IsSuccessStatusCode){
                return new OkResult();
            }
            log.LogError($"Teams response -> {r.StatusCode}: {r.ReasonPhrase}");
            return new StatusCodeResult(500);            
        }
    }
}
