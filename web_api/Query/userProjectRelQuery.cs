using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class userProjectRelQuery
    {
        public AppDatabase Db { get; }

        public userProjectRelQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<userProjectRel> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM user_project_relation WHERE id = @id; ";

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<userProjectRel>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM user_project_relation ORDER BY id DESC; ";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<userProjectRel>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<userProjectRel>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new userProjectRel(Db)
                    {
                        Id = reader.GetInt32(0),
                        User_id = reader.GetString(1),
                        Project_id = reader.GetInt32(2),
                        Project_role_id = reader.GetInt16(3),
                        Project_tag_rel_id = reader.GetInt16(4),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}