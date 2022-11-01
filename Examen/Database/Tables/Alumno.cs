using PluginSQL;
using System.ComponentModel.DataAnnotations;

namespace Examen.Database.Tables
{
    [TableName("alumnos")]
    public class Alumno : TableBase
    {
        [PrimaryKey]
        public int id { get; set; }
        public string nombre { set; get; }
        public string apellidos { set; get; }
        public int genero { set; get; }
        public DateTime fecha_nacimiento { set; get; }

        public static Alumno Find(int id)
        {
            return MYSQL.Query<Alumno>($"SELECT * FROM alumnos WHERE id='{id}' LIMIT 1").FirstOrDefault();
        }

        public static List<Alumno> Get()
        {
            return MYSQL.Query<Alumno>($"SELECT * FROM alumnos ORDER BY nombre ASC");
        }

       
    }
}
