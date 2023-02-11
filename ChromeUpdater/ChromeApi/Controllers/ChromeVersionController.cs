using Microsoft.AspNetCore.Mvc;
using ChromeUpdater.Model;
using Newtonsoft.Json;

namespace ChromeApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ChromeVersionController:ControllerBase{

        [HttpGet("version")]
        public string GetChromeVersion() {
            var data = new ChromeUpdate();
             
            data.Beta = new AppUpdateWithArch();
            var dataString = "";
            var res = JsonConvert.DeserializeObject<ChromeUpdate>(dataString);
            return dataString;
        }
    }
}
