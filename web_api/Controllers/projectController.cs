using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace web_api
{
    [ApiController]
    [Route("api/[controller]")]
    public class projectController : ControllerBase
    {
        public AppDatabase Db { get; }
        public projectController(AppDatabase db)
        {
            Db = db;
        }

        // GET api/project
        [EnableCors("MyPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new projectQuery(Db);
            var result = await query.LatestPostAsync();
            return new OkObjectResult(result);
        }

        // GET api/project/(Project ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new projectQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] project body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] project body)
        {
            await Db.Connection.OpenAsync();
            var query = new projectQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Project_name = body.Project_name;
            result.Project_activated = body.Project_activated;
            result.Project_status_id = body.Project_status_id;
            result.Project_category_id = body.Project_category_id;
            result.Project_category_name = body.Project_category_name;
            result.Project_seriousness_id = body.Project_seriousness_id;
            result.Project_seriousness_name = body.Project_seriousness_name;
            result.Status_name = body.Status_name;
            result.Project_detail = body.Project_detail;
            result.Project_brief_detail = body.Project_brief_detail;
            result.Project_contact = body.Project_contact;
            result.Project_image_link = body.Project_image_link;
            result.Project_tag_name = body.Project_tag_name;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }
    }
}