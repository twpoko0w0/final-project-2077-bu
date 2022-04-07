using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class projectCategory
    {
        public int Id { get; set; }
        public string Project_category_name { get; set; }

    internal AppDatabase Db { get; set; }

        public projectCategory()
        {

        }

        internal projectCategory(AppDatabase db)
        {
            Db = db;
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
                ParameterName = "@project_category_name",
                DbType = DbType.String,
                Value = Project_category_name,
            });
        }
    }
}