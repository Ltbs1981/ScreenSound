using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
namespace ScreenSound.Banco
{
    internal class ScreenSoundContext: DbContext
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSound;Integrated Security=True;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False\r\n";

        public SqlConnection ObterConexao()
        {
            return new SqlConnection(connectionString);
        }
        public IEnumerable<Artista> Listar()
                        {
            var lista = new List<Artista>();
            using var connection = ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM Artistas";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                string nomeArtista = Convert.ToString(dataReader["Nome"]);
                string bioArtista = Convert.ToString(dataReader["Bio"]);
                int idArtista = Convert.ToInt32(dataReader["Id"]);
                Artista artista = new(nomeArtista, bioArtista) { Id = idArtista };

                lista.Add(artista);
            }

            return lista;
        }
    }
}
