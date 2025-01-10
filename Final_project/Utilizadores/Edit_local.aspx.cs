using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final_project.Utilizadores
{
    public partial class Edit_local : System.Web.UI.Page
    {

        private string connectionString = @"data source=.\sqlexpress; initial catalog = Locais; integrated security=true;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //1 - ler dados do local
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = " SELECT L.Nome, L.Morada, L.Localidade, L.Descricao, " +
                     "CAST(L.Concelho AS NVARCHAR), CAST(C.Distrito AS NVARCHAR) " +
                     "FROM Local L JOIN Concelho C ON L.Concelho = C.Id WHERE L.Id = @local";
                    command.Parameters.AddWithValue("@local", Session["local"]);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    //variáveis a utilizar na seleção do concelho e respetivo distrito
                    string idDistrito = "", idConcelho = "";
                    while (reader.Read())
                    {
                        text_name.Text = reader.GetString(0);
                        //textNome.Text = reader.GetString(0);
                        text_address.Text = reader.GetValue(1)?.ToString() ?? string.Empty;
                        text_town.Text = reader.GetString(2);
                        text_description.Text = reader.GetString(3);
                        idConcelho = reader.GetString(4).ToString();
                        idDistrito = reader.GetString(5).ToString();
                    }
                    reader.Close();
                    connection.Close();


                    //2 - carregar distritos
                    load_distritos();

                    //selecionar o distrito
                    if (list_district.Items.FindByValue(idDistrito) != null)
                    {
                        list_district.SelectedValue = idDistrito;
                        listDistrito_SelectedIndexChanged(null, null); // atualiza os concelhos
                    }

                    //3 - selecionar o concelho
                    if (list_council.Items.FindByValue(idConcelho) != null)
                    {
                        list_council.SelectedValue = idConcelho;
                    }

                    //4 - carregar 
                    GetFotosLocal(Session["local"].ToString());
                }
            }
        }


        private void load_distritos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nome FROM Distrito ", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                list_district.DataSource = reader;
                list_district.DataTextField = "Nome";
                list_district.DataValueField = "Id";
                list_district.DataBind();

                list_district.Items.Insert(0, "Selecione um Distrito");
            }
        }


        private void load_concelhos(int distritoId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nome FROM Concelho " +
                "WHERE Distrito = @DistritoId", connection);
                cmd.Parameters.AddWithValue("@DistritoId", distritoId);
                SqlDataReader reader = cmd.ExecuteReader();
                list_council.DataSource = reader;
                list_council.DataTextField = "Nome";
                list_council.DataValueField = "Id";
                list_council.DataBind();

                list_council.Items.Insert(0, "Selecione um Concelho");
            }
        }

        protected void listDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_district.SelectedValue != "Selecione um Distrito")
            {
                load_concelhos(int.Parse(list_district.SelectedValue));
            }
            else
            {
                list_council.Items.Clear();
                list_council.Items.Insert(0, "Escolha um Distrito primeiro");
            }
        }

        protected void GetFotosLocal(string local)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT Id, Ficheiro, Legenda FROM Foto WHERE Local = @local";

                command.Parameters.AddWithValue("@local", local);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                list_photos.DataSource = table;
                list_photos.DataBind();
            }
        }

        protected void button_save_local(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "LocalEditar";
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@id", Session["local"]);


                command.Parameters.AddWithValue("@nome", text_name.Text);
                command.Parameters.AddWithValue("@descricao", text_description.Text);
                if (text_address.Text == "")
                    command.Parameters.AddWithValue("@morada", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@morada", text_address.Text);
                command.Parameters.AddWithValue("@localidade", text_town.Text);
                command.Parameters.AddWithValue("@concelho", list_council.SelectedValue);
                command.Parameters.AddWithValue("@latitude", DBNull.Value);
                command.Parameters.AddWithValue("@longitude", DBNull.Value);


                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        protected void button_save_photo(object sender, EventArgs e)
        {
            if (photo_upload.HasFile)
            {
                if (ViewState["idLocal"] == null)
                {

                    // No photo selected - cancel sql command and show error message
                    Response.Write("<script>alert('Não tem nehum local selecionado. Caso queira adicionar fotos a locais que tenha adicionado anteriormente " +
                        "por favor faça-o desde a sua área pessonal.');</script>");
                    return;
                }

                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand();
                        command.Connection = connection;
                        command.CommandText = "LocalFotoCriar";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@local", ViewState["idLocal"]);
                        command.Parameters.AddWithValue("@legenda", text_legend.Text);

                        //Types of files allowed
                        string[] ext = { ".jpg", ".jpeg", ".png", ".gif", ".tiff" };
                        bool ok = false;

                        // Get the file extension
                        string extensao = System.IO.Path.GetExtension(photo_upload.FileName).ToString();

                        // Check if the extension is valid
                        foreach (string item in ext)
                            if (extensao == item)
                                ok = true;


                        if (ok)
                        {
                            // Generates a GUID to stop reapeating names 
                            Guid g = Guid.NewGuid();
                            string fileName = $"{g}-{photo_upload.FileName}";
                            photo_upload.SaveAs(Server.MapPath("../imagens/") + fileName);

                            command.Parameters.AddWithValue("@ficheiro", "imagens/" + fileName);

                            connection.Open();
                            command.ExecuteNonQuery();

                            // Updates the DataList with the photos from the Local
                            GetFotosLocal((Session["local"].ToString()));
                        }
                        else
                        {
                            // Invalid file type- cancel sql command and show error message
                            Response.Write("<script>alert('Selecione um ficheiro do tipo \".jpg\", \".jpeg\", "
                           + "\".png\", \".gif\" ou \".tiff.');</script>");
                            return;
                        }
                    }
                }
            }

            else
            {
                // No photo selected - cancel sql command and show error message
                Response.Write("<script>alert('Selecione um ficheiro.');</script>");
                return;
            }
        }
    }
}