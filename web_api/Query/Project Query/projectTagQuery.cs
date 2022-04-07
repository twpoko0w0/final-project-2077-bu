using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class projectTagQuery
    {
        public AppDatabase Db { get; }

        public projectTagQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<projectTag> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `project_tag` WHERE `id` = @id";
            //cmd.CommandText = @"SELECT `id`, `project_tag_name` FROM `project_tag` WHERE `id` = @id"; เผื่อไว้ใช้
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<projectTag>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `project_tag` ORDER BY `id` DESC;";
            //cmd.CommandText = @"SELECT `id`, `project_tag_name` FROM `project_tag` ORDER BY `id` DESC;"; เผื่อไว้ใช้
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<projectTag>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<projectTag>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new projectTag(Db)
                    {
                        Id = reader.GetInt32(0),
                        Project_tag_name = reader.GetString(1),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}