using PluginSQL;

namespace Examen.Database.Tables
{
    [TableName("profesores")]
    public class Profesor : TableBase
    {
        [PrimaryKey]
        public int id { set; get; } 
        public string nombre { set; get; }
        public string apellidos { set; get; }
        public int genero { set; get; }

        public static Profesor Find(int id)
        {
            return MYSQL.Query<Profesor>($"SELECT * FROM profesores WHERE id='{id}' LIMIT 1").FirstOrDefault();
        }

        public static List<Profesor> Get()
        {
            return MYSQL.Query<Profesor>($"SELECT * FROM profesores ORDER BY nombre ASC");
        }
    }
}
