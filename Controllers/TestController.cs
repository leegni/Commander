using Commander.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public JsonResult DESTest()
        {
            var desTable = new Dictionary<string, string>();
         
            desTable.Add("GK Enc", DES.Trienc("Server=GUENHEELEE-R10\\LEETESTSERVER;Initial Catalog=CommanderDB; User Id=CommanderAPI;Password=Test2022!;"));
            desTable.Add("GK Dec", DES.Trides("borNUHTwxM5d/KNhQtdvaOUSW9UNOPIq17cmAS/30fNU6IE1fRZTmRWITCNbRnHIR5MrWZBWjW5MvoNJtauvP+hihYGLesZxcf5IOlEtVTe0PqiP2Z85IUF5xT67ujzQP2Ddo64lra5ze7sZqwlfhYuHCCEknjfC"));
        
            return new JsonResult(desTable);
        }
    }
}