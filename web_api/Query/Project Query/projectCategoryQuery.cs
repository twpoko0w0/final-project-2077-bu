using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class projectCategoryQuery
    {
        public AppDatabase Db { get; }

        public projectCategoryQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<projectCategory> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `project_category` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<projectCategory>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `project_category` ORDER BY `id` DESC;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<projectCategory>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<projectCategory>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new projectCategory(Db)
                    {
                        Id = reader.GetInt32(0),
                        Project_category_name = reader.GetString(1),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}