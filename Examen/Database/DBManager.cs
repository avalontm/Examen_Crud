using PluginSQL;
using Examen.Database.Tables;

namespace Examen
{
    public static class DBManager
    {
        public static bool Status
        {
            get { return MYSQL.CheckStatus(); }
        }

        public static void Connect(string host, int port, string user, string password, string database)
        {
            LOG.WriteLine("Iniciando MYSQL...");
            LOG.WriteLine($"MYSQL => [HOST] {host}");
            LOG.WriteLine($"MYSQL => [PORT] {port}");
            LOG.WriteLine($"MYSQL => [USER] {user}");
            LOG.WriteLine($"MYSQL => [PASS] {password}");
            LOG.WriteLine($"MYSQL => [NAME] {database}");

            MYSQL.Init(host, port, user, password, database);

            Create();
        }

        public static bool Create()
        {
            LOG.WriteLine($"[DB]: Generando Base de Datos...", ConsoleColor.White);
            return MYSQL.CreateDataBase();
        }

        public static void Generate()
        {
            LOG.WriteLine($"[DB]: Generando Tablas...", ConsoleColor.White);

            /* CREAMOS LAS TABLAS  */
            MYSQL.CreateTable<User>();
            MYSQL.CreateTable<Ratings>();
            MYSQL.CreateTable<Alumno>();
            MYSQL.CreateTable<Profesor>();
            MYSQL.CreateTable<AlumnoGrado>();
            MYSQL.CreateTable<Grado>();

            /* RELLENAR DATOS */
            if (MYSQL.Table<User>().Count == 0)
            {
                User item = new User();
                item.created_at = DateTime.Now;
                item.updated_at = DateTime.Now;
                item.email = "demo@demo.com";
                item.password = "demo";
                item.name = "usuario demo";
                item.Insert();
            }

        }

        public static void Init(WebApplication app)
        {
            bool generate = bool.Parse(app.Configuration["Generate"]);

            string mysql_host = app.Configuration["MYSQL:Host"];
            int mysql_port = int.Parse(app.Configuration?["MYSQL:Port"] ?? "3306");
            string mysql_user = app.Configuration["MYSQL:User"];
            string mysql_password = app.Configuration["MYSQL:Password"];
            string mysql_name = app.Configuration["MYSQL:Name"];

            //Iniciamos la base de datos
            Connect(mysql_host, mysql_port, mysql_user, mysql_password, mysql_name);

            LOG.WriteLine($"=============================================");

            if (Status)
            {
                LOG.WriteLine("Iniciando MYSQL... [OK]", ConsoleColor.Green);

                if (generate)
                {
                    Generate();
                }
            }
            else
            {

                LOG.WriteLine("Iniciando MYSQL... [BAD]", ConsoleColor.Red);
            }

            LOG.WriteLine($"=============================================");

        }
    }
}
