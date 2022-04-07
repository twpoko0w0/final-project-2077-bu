using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class users
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public int User_activated { get; set; } //DataType = Tinyint (Boolean 0, 1)
        public string First_name { get; set; }
        public string Last_name { get; set; } 
        public string User_about { get; set; }
        public string User_website { get; set; }
        public string User_skill { get; set; }
        public string Province_name { get; set; }
        public int User_province_id { get; set; }
        public string User_tag_name { get; set; }
        public string User_software { get; set; }
        public string User_image_link { get; set; }
        public string Software_image_link { get; set; }
        public string User_tag_id { get; set; }
        public string User_software_id { get; set; }
        public string User_tag_rel_id { get; set; }
        public string User_blog { get; set; }
        public string User_portfolio { get; set; }

        internal AppDatabase Db { get; set; }

        public users()
        {

        }

        internal users(AppDatabase db)
        {
            Db = db;
        }

        // Insert Data
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `user`    (`id`, 
                                                       `email`,
                                                       `user_activated`,
                                                       `first_name`,
                                                       `last_name`,
                                                       `user_about`,
                                                       `user_website`,
                                                       `user_skill`,
                                                       `user_province_id`,
                                                       `user_image_link`,
                                                       `user_blog`,
                                                       `user_portfolio`) 

                                               VALUES (@id,
                                                       @email,
                                                       @user_activated,
                                                       @first_name,
                                                       @last_name,
                                                       @user_about,
                                                       @user_website,
                                                       @user_skill,
                                                       @user_province_id,
                                                       @user_image_link,
                                                       @user_blog,
                                                       @user_portfolio);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = cmd.LastInsertedId.ToString();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `user` SET    `id`= @id,  
                                                     `email`= @email,
                                                     `user_activated`= @user_activated,
                                                     `first_name`= @first_name,
                                                     `last_name`= @last_name,
                                                     `user_about`= @user_about,
                                                     `user_website`= @user_website,
                                                     `user_skill`= @user_skill,
                                                     `user_province_id`= @user_province_id,
                                                     `user_image_link`= @user_image_link,
                                                     `user_blog`= @user_blog,
                                                     `user_portfolio`= @user_portfolio WHERE `Id` =@id; ";
            BindParams(cmd);
            //BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.String,
                Value = Id,
            });

            cmd.Parameters.Add(new MySqlParameter 
            { 
                ParameterName = "@email",
                DbType = DbType.String,
                Value = Email,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_activated",
                DbType = DbType.Int16,
                Value = User_activated,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@first_name",
                DbType = DbType.String,
                Value = First_name,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@last_name",
                DbType = DbType.String,
                Value = Last_name,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_about",
                DbType = DbType.String,
                Value = User_about,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_website",
                DbType = DbType.String,
                Value = User_website,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_skill",
                DbType = DbType.String,
                Value = User_skill,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@province_name",
                DbType = DbType.String,
                Value = Province_name,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_province_id",
                DbType = DbType.Int16,
                Value = User_province_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_tag_name",
                DbType = DbType.String,
                Value = User_tag_name,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_software",
                DbType = DbType.String,
                Value = User_software,
            });
            
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_image_link",
                DbType = DbType.String,
                Value = User_image_link,
            });
            
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@software_image_link",
                DbType = DbType.String,
                Value = Software_image_link,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_tag_id",
                DbType = DbType.String,
                Value = User_tag_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_software_id",
                DbType = DbType.String,
                Value = User_software_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_tag_rel_id",
                DbType = DbType.String,
                Value = User_tag_rel_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_blog",
                DbType = DbType.String,
                Value = User_blog,

            });cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_portfolio",
                DbType = DbType.String,
                Value = User_portfolio,
            });
        }
    }
}
