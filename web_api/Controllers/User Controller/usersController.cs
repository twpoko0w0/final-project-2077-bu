using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace web_api
{
    [ApiController]
    [Route("api/[controller]")]
    public class usersController : ControllerBase
    {
        public AppDatabase Db { get; }
        public usersController(AppDatabase db)
        {
            Db = db;
        }

        // GET 
        [EnableCors("MyPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new usersQuery(Db);
            var result = await query.LatestPostAsync();
            return new OkObjectResult(result);
        }

        // GET (ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            await Db.Connection.OpenAsync();
            var query = new usersQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] users body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(string id, [FromBody] users body)
        {
            await Db.Connection.OpenAsync();
            var query = new usersQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Id = body.Id;
            result.Email = body.Email;
            result.User_activated = body.User_activated;
            result.First_name = body.First_name;
            result.Last_name = body.Last_name;
            result.User_about = body.User_about;
            result.User_website = body.User_website;
            result.User_skill = body.User_skill;
            result.User_province_id = body.User_province_id;
            result.User_image_link = body.User_image_link;
            result.User_blog = body.User_blog;
            result.User_portfolio = body.User_portfolio;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }
    }
}