using PluginSQL;

namespace Examen.Database.Tables
{
    [TableName("users")]
    public class User : TableBase
    {
        [PrimaryKey]
        public int id { set; get; }
        public DateTime created_at { set; get; }
        public DateTime updated_at { set; get; }
        public string name { set; get; }
        public string email { set; get; }
        public string password { set; get; }

        public static User Get(int id)
        {
            return MYSQL.Query<User>($"SELECT * FROM users WHERE id='{id}' LIMIT 1").FirstOrDefault();
        }

        public static User Get(string email, string password)
        {
            return MYSQL.Query<User>($"SELECT * FROM users WHERE email='{email}' AND password='{password}' LIMIT 1").FirstOrDefault();
        }
    }
}
