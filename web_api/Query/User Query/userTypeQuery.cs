using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class userTypeQuery
    {
        public AppDatabase Db { get; }

        public userTypeQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<userType> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `user_type` WHERE `id` = @id";
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

        public async Task<List<userType>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `user_type` ORDER BY `id` DESC;";
            //cmd.CommandText = @"SELECT `id`, `project_tag_name` FROM `project_tag` ORDER BY `id` DESC;"; เผื่อไว้ใช้
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<userType>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<userType>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new userType(Db)
                    {
                        Id = reader.GetInt32(0),
                        User_type = reader.GetString(1),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}