using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class userSoftwareRelQuery
    {
        public AppDatabase Db { get; }

        public userSoftwareRelQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<userSoftwareRel> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM user_software_relation WHERE id = @id; ";

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<userSoftwareRel>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM user_software_relation ORDER BY id DESC; ";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<userSoftwareRel>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<userSoftwareRel>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new userSoftwareRel(Db)
                    {
                        Id = reader.GetInt32(0),
                        User_id = reader.GetString(1),
                        User_software_id = reader.GetInt16(2),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}