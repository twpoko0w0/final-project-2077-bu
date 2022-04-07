﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace web_api
{
    [ApiController]
    [Route("api/[controller]")]
    public class projectTagRelController : ControllerBase
    {
        public AppDatabase Db { get; }
        public projectTagRelController(AppDatabase db)
        {
            Db = db;
        }

        // GET 
        [EnableCors("MyPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new projectTagRelQuery(Db);
            var result = await query.LatestPostAsync();
            return new OkObjectResult(result);
        }

        // GET (ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new projectTagRelQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] projectTagRel body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] projectTagRel body)
        {
            await Db.Connection.OpenAsync();
            var query = new projectTagRelQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Project_id = body.Project_id;
            result.Project_tag_id = body.Project_tag_id;
            result.Project_tag_role = body.Project_tag_role;
            result.Project_position_quantity_id = body.Project_position_quantity_id;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE (ID)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new projectTagRelQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }
    }
}