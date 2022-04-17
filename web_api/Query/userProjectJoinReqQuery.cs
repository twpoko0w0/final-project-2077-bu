using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class userProjectJoinReqQuery
    {
        public AppDatabase Db { get; }

        public userProjectJoinReqQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<userProjectJoinReq> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM user_project_join_request WHERE id = @id; ";

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<userProjectJoinReq>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM user_project_join_request ORDER BY id DESC; ";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<userProjectJoinReq>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<userProjectJoinReq>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new userProjectJoinReq(Db)
                    {
                        Id = reader.GetInt32(0),
                        User_id = reader.GetString(1),
                        Project_id = reader.GetInt32(2),
                        Date_time = reader.GetDateTime(3),
                        Project_tag_rel_id = reader.GetInt32(4),
                        Interview = reader.GetString(5),
                        Facebook = reader.GetString(6),
                        Email = reader.GetString(7),
                        Line = reader.GetString(8),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}