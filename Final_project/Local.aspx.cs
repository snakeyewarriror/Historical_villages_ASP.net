using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final_project
{
    public partial class Local : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection connection = new SqlConnection(
                @"data source=.\sqlexpress; initial catalog = Locais; " +
                "integrated security = true;"))
                {
                    // query para obter as fotos
                    string query = "SELECT Id, Legenda, Ficheiro FROM Foto WHERE Local = @id";

                    using (SqlCommand commandFotos = new SqlCommand(query, connection))
                    {
                        commandFotos.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                        SqlDataAdapter adapter = new SqlDataAdapter(commandFotos);
                        DataTable fotos = new DataTable();
                        adapter.Fill(fotos);

                        listLocais.DataSource = fotos;
                        listLocais.DataBind();
                    }

                    // query para obter os dados
                    query = "SELECT L.Nome, L.Descricao, L.Localidade, C.Nome 'Concelho', " +
                    " D.Nome 'Distrito' " +
                    "FROM Local L JOIN Concelho C ON L.Concelho = C.Id " +
                    "JOIN Distrito D ON C.Distrito = D.Id WHERE L.Id = @id";

                    using (SqlCommand commandDados = new SqlCommand(query, connection))
                    {
                        commandDados.Parameters.AddWithValue("@id",
                       Request.QueryString["id"]);
                        connection.Open();
                        SqlDataReader reader = commandDados.ExecuteReader();
                        while (reader.Read())
                        {
                            nameLocal.Text = !reader.IsDBNull(0) ? reader.GetString(0) : "Sem Nome disponivel";
                            description.Text = !reader.IsDBNull(1) ? reader.GetString(1) : "Sem Descrição disponivel";

                            string localidade = !reader.IsDBNull(2) ? reader.GetString(2) : "Sem Localidade";
                            string concelho = !reader.IsDBNull(3) ? reader.GetString(3) : "Sem Concelho";
                            string distrito = !reader.IsDBNull(4) ? reader.GetString(4) : "Sem Distrito";

                            localization.Text = $"{localidade} | {concelho} | {distrito}";
                        }
                        reader.Close();
                    }
                }
            }
        }
    }
}