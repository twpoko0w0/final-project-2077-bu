using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class projectSeriousness
    {
        public int Id { get; set; }
        public string Project_seriousness_name { get; set; }

    internal AppDatabase Db { get; set; }

        public projectSeriousness()
        {

        }

        internal projectSeriousness(AppDatabase db)
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
                ParameterName = "@project_seriousness_name",
                DbType = DbType.String,
                Value = Project_seriousness_name,
            });
        }
    }
}