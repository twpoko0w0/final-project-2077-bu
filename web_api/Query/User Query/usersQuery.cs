using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class usersQuery
    {
        public AppDatabase Db { get; }

        public usersQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<users> FindOneAsync(string id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT
                                u.id,
                                u.email,
                                u.user_activated,
                                u.first_name,
                                u.last_name,
                                u.user_about,
                                u.user_website,
                                u.user_skill,
                                p.province_name,
                                u.user_province_id,
                                u.user_image_link,
                                group_concat(distinct ut.user_tag_name),
                                group_concat(distinct us.user_software),
                                group_concat(distinct us.software_image_link),
                                group_concat(distinct ut.id),
                                group_concat(distinct us.id),
                                group_concat(distinct utr.id),
                                u.user_blog,               
                                u.user_portfolio 
                                FROM user u
                                INNER JOIN user_tag_relation utr on u.id = utr.user_id
                                INNER JOIN user_tag ut on utr.user_tag_id = ut.id
                                INNER JOIN user_software_relation usr on usr.user_id = u.id
                                INNER JOIN user_software us on us.id = usr.user_software_id
                                INNER JOIN province p on p.id = u.user_province_id
                                WHERE u.id = @id
                                GROUP BY u.id;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.String,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<users>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT
                                u.id,
                                u.email,
                                u.user_activated,
                                u.first_name,
                                u.last_name,
                                u.user_about,
                                u.user_website,
                                u.user_skill,
                                p.province_name,
                                u.user_province_id,
                                u.user_image_link,
                                group_concat(distinct ut.user_tag_name),
                                group_concat(distinct us.user_software),
                                group_concat(distinct us.software_image_link),
                                group_concat(distinct ut.id),
                                group_concat(distinct us.id),
                                group_concat(distinct utr.id),
                                u.user_blog,               
                                u.user_portfolio              
                                FROM user u
                                INNER JOIN user_tag_relation utr on u.id = utr.user_id
                                INNER JOIN user_tag ut on utr.user_tag_id = ut.id
                                INNER JOIN user_software_relation usr on usr.user_id = u.id
                                INNER JOIN user_software us on us.id = usr.user_software_id
                                INNER JOIN province p on p.id = u.user_province_id
                                GROUP BY u.id;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<users>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<users>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new users(Db)
                    {
                        Id = reader.GetString(0),
                        Email = reader.GetString(1),
                        User_activated = reader.GetInt16(2),
                        First_name = reader.GetString(3),
                        Last_name = reader.GetString(4),
                        User_about = reader.GetString(5),
                        User_website = reader.GetString(6),
                        User_skill = reader.GetString(7),
                        Province_name = reader.GetString(8),
                        User_province_id = reader.GetInt16(9),
                        User_image_link = reader.GetString(10),
                        User_tag_name = reader.GetString(11),
                        User_software = reader.GetString(12),
                        Software_image_link = reader.GetString(13),
                        User_tag_id = reader.GetString(14),
                        User_software_id = reader.GetString(15),
                        User_tag_rel_id = reader.GetString(16),
                        User_blog = reader.GetString(17),
                        User_portfolio = reader.GetString(18),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}