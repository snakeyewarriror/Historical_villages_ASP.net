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
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Nome { get; set; }
        public string Concelho { get; set; }

        private string connection_string = @"data source=.\sqlexpress; initial catalog = Locais; integrated security = true;";

        private int currentPage;

        bool PodeComentar()
        {
            using(SqlConnection connection = new SqlConnection(connection_string))
            {
                // Check if the person already commented
                SqlCommand commandComentarios = new SqlCommand();
                commandComentarios.Connection = connection;

                commandComentarios.CommandText = "SELECT COUNT(*) FROM Comentario C JOIN Local L ON C.Local = L.Id " +
                "WHERE C.Utilizador = @utilizador AND L.Id = @local";
                commandComentarios.Parameters.AddWithValue("@utilizador", Session["id_utilizador"]);
                commandComentarios.Parameters.AddWithValue("@local", Request.QueryString["id"]);

                connection.Open();

                int i = (int)commandComentarios.ExecuteScalar();
                connection.Close();

                if (i > 0)
                {
                    System.Diagnostics.Debug.WriteLine("\n\n\n\n here");
                    return false;
                }


                // Check if the person is the author of the Local
                SqlCommand commandAutorLocal = new SqlCommand();
                commandAutorLocal.Connection = connection;

                commandAutorLocal.CommandText = "SELECT COUNT(*) FROM Local L JOIN Utilizador U " +
                "ON L.Utilizador = U.ID WHERE L.Id = @id AND L.Utilizador = @utilizador";
                commandAutorLocal.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                commandAutorLocal.Parameters.AddWithValue("@utilizador", Session["id_utilizador"]);

                connection.Open();

                int i2 = (int)commandAutorLocal.ExecuteScalar();

                if (i2 > 0)
                {
                    System.Diagnostics.Debug.WriteLine("\n\n\n\n here instead" + i2);
                    return false;
                }
                return true;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Checks if the user is authenticated and can comment
                divComment.Visible = HttpContext.Current.User.Identity.IsAuthenticated && PodeComentar();

                ViewState["contador"] = 0;

                using (SqlConnection connection = new SqlConnection(connection_string))
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

                    // Query to get data
                    query = "SELECT L.Nome, L.Descricao, L.Localidade, C.Nome 'Concelho', D.Nome 'Distrito', " +
                    "L.Latitude, L.Longitude " +
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

                            Nome = reader[0].ToString();
                            Latitude = reader[5].ToString();
                            Longitude = reader[6].ToString();

                            Concelho = concelho;

                            localization.Text = $"{localidade} | {concelho} | {distrito}";
                        }
                        reader.Close();
                        connection.Close();
                    }

                    ObterComentarios();
                }
            }
            currentPage = (int)ViewState["contador"];
        }

        protected void linkFirst_click(object sender, EventArgs e)
        {
            currentPage = 0;
            ViewState["contador"] = currentPage;
            ObterComentarios();
        }

        protected void linkPrevious_click(object sender, EventArgs e)
        {
            currentPage = (int)ViewState["contador"];
            currentPage -= 1;
            ViewState["contador"] = currentPage;
            BindListComentarios((DataTable)ViewState["dataSource"]);
        }

        protected void linkNext_click(object sender, EventArgs e)
        {
            currentPage = (int)ViewState["contador"];
            currentPage += 1;
            ViewState["contador"] = currentPage;
            BindListComentarios((DataTable)ViewState["dataSource"]);
        }

        protected void linkLast_click(object sender, EventArgs e)
        {
            currentPage = (int)ViewState["total"] - 1;
            ViewState["contador"] = currentPage;
            BindListComentarios((DataTable)ViewState["dataSource"]);
        }

        void BindListComentarios(DataTable table)
        {
            PagedDataSource paged = new PagedDataSource();
            paged.DataSource = table.DefaultView;
            paged.PageSize = 5;
            paged.AllowPaging = true;
            paged.CurrentPageIndex = currentPage;
            linkFirst.Enabled = !paged.IsFirstPage;
            linkPrevious.Enabled = !paged.IsFirstPage;
            linkNext.Enabled = !paged.IsLastPage;
            linkLast.Enabled = !paged.IsLastPage;
            ViewState["total"] = paged.PageCount;
            listComentarios.DataSource = paged;
            listComentarios.DataBind();
            ViewState["dataSource"] = table;
        }

        void ObterComentarios()
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                using (SqlCommand commandComentarios = new SqlCommand("LocalComentarios", connection))
                {
                    commandComentarios.CommandType = CommandType.StoredProcedure;
                    commandComentarios.Parameters.AddWithValue("@local", Request.QueryString["id"]);
                    SqlDataReader reader;
                    DataTable table = new DataTable();
                    connection.Open();
                    reader = commandComentarios.ExecuteReader();
                    table.Load(reader);
                    connection.Close();
                    BindListComentarios(table);
                }
            }
        }

        protected void button_comment(object sender, EventArgs e)
        {
            using(SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT Comentario(Comentario, Classificacao, Data, Local, Utilizador) "
                    + "VALUES (@comentario, @classificacao, @data, @local, @utilizador)";

                command.Parameters.AddWithValue("@comentario", textComentario.Text);
                command.Parameters.AddWithValue("@classificacao", listClassificacao.SelectedValue);
                command.Parameters.AddWithValue("@data", DateTime.Now);
                command.Parameters.AddWithValue("@local", Request.QueryString["id"]);
                command.Parameters.AddWithValue("@utilizador", Session["id_utilizador"]);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                // Update comments
                ObterComentarios();

                //Hide div that allows to comment
                divComment.Visible = HttpContext.Current.User.Identity.IsAuthenticated && PodeComentar();
            }
        }

    }
}