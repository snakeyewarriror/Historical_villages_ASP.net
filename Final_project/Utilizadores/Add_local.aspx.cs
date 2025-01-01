using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final_project.Utilizadores
{
    public partial class Add_local : System.Web.UI.Page
    {

        private string connectionString = @"data source=.\sqlexpress; initial catalog = Locais; integrated security=true;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                list_council.Items.Insert(0, "Escolha um Distrito primeiro");
                load_distritos();
                ViewState["idLocal"] = 0;
            }
        }



        private void load_distritos()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nome FROM Distrito ", conn);
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nome FROM Concelho " +
                "WHERE Distrito = @DistritoId", conn);
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
                list_council.Items.Insert(0, "Selecione um Concelho");
            }
        }


        protected void button_save_local(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "LocalCriar";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@nome", text_name.Text);
                command.Parameters.AddWithValue("@descricao", text_description.Text);
                if (text_address.Text == "")
                    command.Parameters.AddWithValue("@morada", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@morada", text_address.Text);
                command.Parameters.AddWithValue("@localidade", text_town.Text);
                command.Parameters.AddWithValue("@concelho", list_council.SelectedValue);
                command.Parameters.AddWithValue("@utilizador", Session["id_utilizador"]);
                command.Parameters.AddWithValue("@latitude", DBNull.Value);
                command.Parameters.AddWithValue("@longitude", DBNull.Value);

                //obter latitude e longitude
                /*
                Datum localizacao = new Datum();
                string key = "chave de acesso à API";
                string local = $"{textLocalidade.Text},Portugal";
                WebRequest request = WebRequest.Create

                ($"https://api.positionstack.com/v1/forward?access_key={key}&query={local}");
                WebResponse response = request.GetResponse();
                if (response != null)
                {
                 Stream stream = response.GetResponseStream();
                 StreamReader reader = new StreamReader(stream);
                 string json = reader.ReadToEnd();
                 Root result = JsonConvert.DeserializeObject<Root>(json);
                 // obter latitude e longitude
                 if (result.data != null && result.data.Count > 0)
                 {
                 localizacao = result.data[0];
                 command.Parameters.AddWithValue("@latitude", localizacao.latitude);
                 command.Parameters.AddWithValue("@longitude", localizacao.longitude);
                 }
                 else
                 {
                 command.Parameters.AddWithValue("@latitude", DBNull.Value);
                 command.Parameters.AddWithValue("@longitude", DBNull.Value);
                 }
                }
                */

                connection.Open();

                //guardar em ViewState o ID atribuído ao local
                ViewState["idLocal"] = command.ExecuteScalar();

                //ativar botão Guardar foto
                save_photo_button.Enabled = true;
            }
        }

         protected void GetFotosLocal()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT Id, Ficheiro, Legenda FROM Foto WHERE Local = @local";

                //ID do local, definido aquando da criação do local
                command.Parameters.AddWithValue("@local", ViewState["idLocal"]);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                list_photos.DataSource = table;
                list_photos.DataBind();
            }
        }


        protected void button_save_photo(object sender, EventArgs e)
        {
            //ficheiro - nome do controlo FileUpload
            if (photo_upload.HasFile)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "LocalFotoCriar";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@local", ViewState["idLocal"]);
                    command.Parameters.AddWithValue("@legenda", text_legend.Text);

                    //tipos de ficheiros admitidos
                    string[] ext = { ".jpg", ".jpeg", ".png", ".gif", ".tiff" };
                    bool ok = false;

                    //obter extensão do ficheiro
                    string extensao = System.IO.Path.GetExtension(photo_upload.FileName).ToString();

                    //verificar se a extensão se encontra no Array de ficheiros admitidos
                    foreach (string item in ext)
                        if (extensao == item)
                            ok = true;

                    //se o tipo de ficheiro está correto
                    if (ok)
                    {
                        //gerar Guid para evitar nomes repetidos
                        Guid g = Guid.NewGuid();
                        string fileName = $"{g}-{photo_upload.FileName}";
                        photo_upload.SaveAs(Server.MapPath("../imagens/") + fileName);

                        //definir parâmetro
                        command.Parameters.AddWithValue("@ficheiro", "imagens/" + fileName);

                        //tipo de ficheiro correto - colocar informação na base de dados
                        connection.Open();
                        command.ExecuteNonQuery();

                        //atualizar DataList referente às fotos
                        GetFotosLocal();
                    }
                    else
                    {
                        //ficheiro inválido - cancelar execução do procedimento
                        Response.Write("<script>alert('Selecione um ficheiro do tipo \".jpg\", \".jpeg\", "
                       + "\".png\", \".gif\" ou \".tiff.');</script>");
                        return;
                    }
                }
            }

            else
            {
                //não foi selecionado um ficheiro - cancelar execução do procedimento
                Response.Write("<script>alert('Selecione um ficheiro do tipo \".jpg\", \".jpeg\", "
               + "\".png\", \".gif\" ou \".tiff.');</script>");
                return;
            }
        }


        protected void link_details_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                using(SqlConnection connection = new SqlConnection(connectionString))
                {

                    //colocar em ViewState o Id da foto selecionada
                    ViewState["idFoto"] = e.CommandArgument.ToString();

                    //selecionar legenda da foto selecionada
                    SqlCommand commandFoto = new SqlCommand();
                    commandFoto.Connection = connection;
                    commandFoto.CommandText = "SELECT Legenda FROM Foto WHERE Id = @id";
                    commandFoto.Parameters.AddWithValue("@id", ViewState["idFoto"]);

                    connection.Open();
                    SqlDataReader reader = commandFoto.ExecuteReader();

                    while (reader.Read())
                    {
                        text_legend.Text = reader[0].ToString();
                    }
                    reader.Close();
                }
            }
        }

        protected void button_edit_legend(object sender, EventArgs e)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand commandFoto = new SqlCommand();
                commandFoto.Connection = connection;
                commandFoto.CommandText = "UPDATE Foto SET Legenda = @legenda WHERE Id = @id";
                commandFoto.CommandType = CommandType.Text;

                //ID da foto definido quando a foto é selecionada
                commandFoto.Parameters.AddWithValue("@id", ViewState["idFoto"]);
                commandFoto.Parameters.AddWithValue("@legenda", text_legend.Text);
                connection.Open();
                commandFoto.ExecuteNonQuery();

                text_legend.Text = string.Empty;

                //atualizar DataList fotos do local
                GetFotosLocal();
            }
        }

        protected void button_eliminate_photo(object sender, EventArgs e)
        {

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                //eliminar ficheiro
                SqlCommand commandFoto = new SqlCommand();
                commandFoto.Connection = connection;
                commandFoto.CommandText = "SELECT Ficheiro FROM Foto WHERE Id = @id";

                //ID da foto definido quando a foto é selecionada
                commandFoto.Parameters.AddWithValue("@id", ViewState["idFoto"]);
                connection.Open();

                //obter nome do ficheiro a eliminar
                SqlDataReader reader = commandFoto.ExecuteReader();
                while (reader.Read())
                {
                    string ficheiro = Server.MapPath("../" + reader[0].ToString());
                    //eliminar ficheiro
                    if (File.Exists(ficheiro))
                        File.Delete(ficheiro);
                }
                reader.Close();

                //eliminar dados na tabela
                commandFoto.Parameters.Clear();
                commandFoto.CommandText = "DELETE Foto WHERE Id = @id";
                commandFoto.CommandType = CommandType.Text;
                commandFoto.Parameters.AddWithValue("@id", ViewState["idFoto"]);
                commandFoto.ExecuteNonQuery();

                text_legend.Text = string.Empty;

                //atualizar DataList fotos do local
                GetFotosLocal();
            }
        }

        protected void button_cancel(object sender, EventArgs e)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {

                //obter nome dos ficheiros associados ao local
                SqlCommand commandFotos = new SqlCommand();
                commandFotos.Connection = connection;
                commandFotos.CommandText = "SELECT Ficheiro FROM Foto WHERE Local = @local";
                commandFotos.Parameters.AddWithValue("@local", ViewState["idLocal"]);

                connection.Open();
                SqlDataReader reader = commandFotos.ExecuteReader();
                while (reader.Read())
                {
                    string ficheiro = Server.MapPath("../" + reader[0].ToString());
                    //eliminar ficheiros
                    if (File.Exists(ficheiro))
                        File.Delete(ficheiro);
                }
                reader.Close();

                //eliminar dados das tabelas
                SqlCommand commandLocal = new SqlCommand();
                commandLocal.Connection = connection;
                commandLocal.CommandText = "LocalEliminar";
                commandLocal.CommandType = CommandType.StoredProcedure;
                Response.Write("Current idLocal: " + ViewState["idLocal"]?.ToString());
                commandLocal.Parameters.AddWithValue("@local", ViewState["idLocal"]);
                commandLocal.ExecuteNonQuery();
                ViewState["idLocal"] = null;
            }
        }

    }
}