using Microsoft.AspNetCore.SignalR;
using PluginSQL;

namespace Examen.Database.Tables
{
    [TableName("grados")]
    public class Grado :TableBase
    {
        [PrimaryKey]
        public int id { set; get; }
        public string nombre { set; get; }
        public int profesor_id { set; get; }

        public static Grado Find(int id)
        {
            return MYSQL.Query<Grado>($"SELECT * FROM grados WHERE id='{id}' LIMIT 1").FirstOrDefault();
        }

        public static List<Grado> Get()
        {
            return MYSQL.Query<Grado>($"SELECT * FROM grados");
        }

       
    }
}
