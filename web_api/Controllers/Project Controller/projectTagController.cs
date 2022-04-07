using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace web_api
{
    [ApiController]
    [Route("api/[controller]")]
    public class projectTagController : ControllerBase
    {
        public AppDatabase Db { get; }
        public projectTagController(AppDatabase db)
        {
            Db = db;
        }

        // GET 
        [EnableCors("MyPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new projectTagQuery(Db);
            var result = await query.LatestPostAsync();
            return new OkObjectResult(result);
        }

        // GET (ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new projectTagQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }
    }
}