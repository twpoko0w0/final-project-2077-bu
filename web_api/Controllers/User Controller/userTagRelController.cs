using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace web_api
{
    [ApiController]
    [Route("api/[controller]")]
    public class userTagRelController : ControllerBase
    {
        public AppDatabase Db { get; }
        public userTagRelController(AppDatabase db)
        {
            Db = db;
        }

        // GET
        [EnableCors("MyPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new userTagRelQuery(Db);
            var result = await query.LatestPostAsync();
            return new OkObjectResult(result);
        }

        // GET (ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new userTagRelQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] userTagRel body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] userTagRel body)
        {
            await Db.Connection.OpenAsync();
            var query = new userTagRelQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.User_id = body.User_id;
            result.User_tag_id = body.User_tag_id;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new userTagRelQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }
    }
}