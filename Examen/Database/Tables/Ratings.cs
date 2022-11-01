using PluginSQL;

namespace Examen.Database.Tables
{
    public class Ratings : TableBase
    {
        [PrimaryKey]
        public int id { set; get; }
        public DateTime created_at { set; get; }
        public DateTime updated_at { set; get; }
        public string iduser { set; get; }
        public string name { set; get; }
        public string rating { set; get; }


        public static Ratings Find(int id)
        {
            return MYSQL.Query<Ratings>($"SELECT * FROM ratings WHERE id='{id}' LLIMIT 1").FirstOrDefault();
        }

        public static List<Ratings> Get(int iduser)
        {
            return MYSQL.Query<Ratings>($"SELECT * FROM ratings WHERE iduser='{iduser}' ORDER BY updated_at ASC");
        }
    }
}
