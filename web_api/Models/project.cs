using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class project
    {
        public int Id { get; set; }
        public string Project_name { get; set; }
        public int Project_activated { get; set; } //DataType = Tinyint (Boolean 0, 1)
        public int Project_status_id { get; set; } 
        public string Status_name { get; set; }
        public int Project_category_id { get; set; } 
        public string Project_category_name { get; set; }
        public int Project_seriousness_id { get; set; }
        public string Project_seriousness_name { get; set; }
        public DateTime Project_create_date { get; set; }
        public string Project_detail { get; set; }
        public string Project_brief_detail { get; set; }
        public string Project_contact { get; set; }
        public string Project_image_link { get; set; }
        public string Project_tag_name { get; set; }

    internal AppDatabase Db { get; set; }

        public project()
        {

        }

        internal project(AppDatabase db)
        {
            Db = db;
        }

        // Insert Data
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `project` (`project_name`, 
                                                       `project_activated`,
                                                       `project_status_id`,
                                                       `project_category_id`,
                                                       `project_seriousness_id`,
                                                       `project_create_date`,
                                                       `project_detail`,
                                                       `project_brief_detail`,
                                                       `project_contact`,
                                                       `project_image_link`
                                                       `project_tag_name`) 

                                               VALUES (@project_name,
                                                       @project_name,
                                                       @project_activated,
                                                       @project_status_id,
                                                       @project_category_id,
                                                       @project_seriousness_id,
                                                       @project_create_date,
                                                       @project_detail,
                                                       @project_brief_detail,
                                                       @project_contact,
                                                       @project_image_link
                                                       @project_tag_name);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `project` SET `project_name`= @project_name,  
                                                     `project_activated`= @project_activated,  
                                                     `project_status_id`= @project_status_id,
                                                     `project_category_id`= @project_category_id,
                                                     `project_seriousness_id`= @project_seriousness_id,
                                                     `project_detail`= @`project_detail`,
                                                     `project_brief_detail`= @project_brief_detail,
                                                     `project_contact`= @project_contact,
                                                     `project_image_link`= @project_image_link,
                                                     `project_tag_name`= @project_tag_name  WHERE `Id`=@id ; ";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName="@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter 
            { 
                ParameterName = "@project_name",
                DbType = DbType.String,
                Value = Project_name,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_activated",
                DbType = DbType.Int16,
                Value = Project_activated,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_status_id",
                DbType = DbType.Int16,
                Value = Project_status_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@status_name",
                DbType = DbType.String,
                Value = Status_name,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_category_id",
                DbType = DbType.Int16,
                Value = Project_category_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_category_name",
                DbType = DbType.String,
                Value = Project_category_name,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_seriousness_id",
                DbType = DbType.Int16,
                Value = Project_seriousness_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_seriousness_name",
                DbType = DbType.String,
                Value = Project_seriousness_name,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_create_date",
                DbType = DbType.DateTime,
                Value = Project_create_date,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_detail",
                DbType = DbType.String,
                Value = Project_detail,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_brief_detail",
                DbType = DbType.String,
                Value = Project_brief_detail,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_contact",
                DbType = DbType.String,
                Value = Project_contact,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_image_link",
                DbType = DbType.String,
                Value = Project_image_link,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_tag_name",
                DbType = DbType.String,
                Value = Project_tag_name,
            });
        }
    }
}
