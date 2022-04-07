using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class userSoftwareQuery
    {
        public AppDatabase Db { get; }

        public userSoftwareQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<userSoftware> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `user_software` WHERE `id` = @id";
            //cmd.CommandText = @"SELECT `id`, `project_tag_name` FROM `project_tag` WHERE `id` = @id"; เผื่อไว้ใช้
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<userSoftware>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `user_software` ORDER BY `id` DESC;";
            //cmd.CommandText = @"SELECT `id`, `project_tag_name` FROM `project_tag` ORDER BY `id` DESC;"; เผื่อไว้ใช้
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<userSoftware>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<userSoftware>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new userSoftware(Db)
                    {
                        Id = reader.GetInt32(0),
                        User_software = reader.GetString(1),
                        Software_image_link = reader.GetString(2),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}