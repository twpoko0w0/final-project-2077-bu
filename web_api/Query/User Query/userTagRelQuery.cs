using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class userTagRelQuery
    {
        public AppDatabase Db { get; }

        public userTagRelQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<userTagRel> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM user_tag_relation WHERE id = @id; ";

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<userTagRel>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM user_tag_relation ORDER BY id DESC; ";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<userTagRel>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<userTagRel>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new userTagRel(Db)
                    {
                        Id = reader.GetInt32(0),
                        User_id = reader.GetString(1),
                        User_tag_id = reader.GetInt16(2),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}