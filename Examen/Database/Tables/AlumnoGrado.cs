using PluginSQL;

namespace Examen.Database.Tables
{
    [TableName("alumnos_grados")]
    public class AlumnoGrado :TableBase
    {
        [PrimaryKey]
        public int id { set; get; }
        public int alumno_id { set; get; }
        public int grado_id { set; get; }
        public int seleccion { set; get; }

        public static AlumnoGrado Find(int id)
        {
            return MYSQL.Query<AlumnoGrado>($"SELECT * FROM alumnos_grados WHERE id='{id}' LIMIT 1").FirstOrDefault();
        }

        public static List<AlumnoGrado> Get()
        {
            return MYSQL.Query<AlumnoGrado>($"SELECT * FROM alumnos_grados ORDER BY id");
        }
    }
}
