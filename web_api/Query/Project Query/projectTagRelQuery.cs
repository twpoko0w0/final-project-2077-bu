using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class projectTagRelQuery
    {
        public AppDatabase Db { get; }

        public projectTagRelQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<projectTagRel> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM project_tag_relation WHERE id = @id; ";

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<projectTagRel>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM project_tag_relation ORDER BY id DESC; ";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<projectTagRel>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<projectTagRel>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new projectTagRel(Db)
                    {
                        Id = reader.GetInt32(0),
                        Project_id = reader.GetInt16(1),
                        Project_tag_id = reader.GetInt16(2),
                        Project_tag_role = reader.GetString(3),
                        Project_position_quantity_id = reader.GetInt16(4),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}