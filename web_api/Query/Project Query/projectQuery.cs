using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class projectQuery
    {
        public AppDatabase Db { get; }

        public projectQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<project> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT
                                p.id,
                                p.project_name,
                                p.project_activated,
                                p.project_status_id,
                                pst.status_name,
                                p.project_category_id,
                                pc.project_category_name,
                                p.project_seriousness_id,
                                ps.project_seriousness_name,
                                p.project_create_date,
                                p.project_detail,
                                p.project_brief_detail,
                                p.project_contact,
                                p.project_image_link,
                                p.project_duration_id,
                                pd.duration,
                                group_concat(pt.id),
                                group_concat(pt.project_tag_name),
                                group_concat(ppq.quantity),
                                group_concat(ptr.project_tag_role),
                                group_concat(ptr.id)
                                FROM project p
                                INNER JOIN project_seriousness ps on p.project_seriousness_id = ps.id
                                INNER JOIN project_category pc on p.project_category_id = pc.id
                                INNER JOIN project_status pst on p.project_status_id = pst.id
                                INNER JOIN project_tag_relation ptr on p.id = ptr.project_id
                                INNER JOIN project_tag pt on ptr.project_tag_id = pt.id
                                INNER JOIN project_duration pd on p.project_duration_id = pd.id
                                INNER JOIN project_position_quantity ppq on ppq.id = ptr.project_position_quantity_id
                                WHERE p.id = @id
                                GROUP BY p.id
                                ORDER BY id DESC;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<project>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT 
                                p.id,
                                p.project_name,
                                p.project_activated,
                                p.project_status_id,
                                pst.status_name,
                                p.project_category_id,
                                pc.project_category_name,
                                p.project_seriousness_id,
                                ps.project_seriousness_name,
                                p.project_create_date,
                                p.project_detail,
                                p.project_brief_detail,
                                p.project_contact,
                                p.project_image_link,
                                p.project_duration_id,
                                pd.duration,
                                group_concat(pt.id),
                                group_concat(pt.project_tag_name),
                                group_concat(ppq.quantity),
                                group_concat(ptr.project_tag_role),
                                group_concat(ptr.id)
                                FROM project p
                                INNER JOIN project_seriousness ps on p.project_seriousness_id = ps.id
                                INNER JOIN project_category pc on p.project_category_id = pc.id
                                INNER JOIN project_status pst on p.project_status_id = pst.id
                                INNER JOIN project_tag_relation ptr on p.id = ptr.project_id
                                INNER JOIN project_tag pt on ptr.project_tag_id = pt.id
                                INNER JOIN project_duration pd on p.project_duration_id = pd.id
                                INNER JOIN project_position_quantity ppq on ppq.id = ptr.project_position_quantity_id
                                GROUP BY p.id
                                ORDER BY id DESC;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<project>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<project>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new project(Db)
                    {
                        Id = reader.GetInt32(0),
                        Project_name = reader.GetString(1),
                        Project_activated = reader.GetInt16(2),
                        Project_status_id = reader.GetInt16(3),
                        Status_name = reader.GetString(4),
                        Project_category_id = reader.GetInt16(5),
                        Project_category_name = reader.GetString(6),
                        Project_seriousness_id = reader.GetInt16(7),
                        Project_seriousness_name = reader.GetString(8),
                        Project_create_date = reader.GetDateTime(9),
                        Project_detail = reader.GetString(10),
                        Project_brief_detail = reader.GetString(11),
                        Project_contact = reader.GetString(12),
                        Project_image_link = reader.GetString(13),
                        Project_duration_id = reader.GetInt16(14),
                        Duration = reader.GetInt16(15),
                        Project_tag_id = reader.GetString(16),
                        Project_tag_name = reader.GetString(17),
                        Quantity = reader.GetString(18),
                        Project_tag_role = reader.GetString(19),
                        Project_tag_relation_id = reader.GetString(20),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}