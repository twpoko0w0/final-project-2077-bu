using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class projectSoftwareRelQuery
    {
        public AppDatabase Db { get; }

        public projectSoftwareRelQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<projectSoftwareRel> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM project_software_relation WHERE id = @id; ";

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<projectSoftwareRel>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM project_software_relation ORDER BY id DESC; ";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<projectSoftwareRel>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<projectSoftwareRel>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new projectSoftwareRel(Db)
                    {
                        Id = reader.GetInt32(0),
                        Project_id = reader.GetInt32(1),
                        Project_software_id = reader.GetInt16(2),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}