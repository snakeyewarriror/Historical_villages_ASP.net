using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

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
                    // 1 - Read local data
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = " SELECT L.Nome, L.Morada, L.Localidade, L.Descricao, " +
                     "CAST(L.Concelho AS NVARCHAR), CAST(C.Distrito AS NVARCHAR) " +
                     "FROM Local L JOIN Concelho C ON L.Concelho = C.Id WHERE L.Id = @local";
                    command.Parameters.AddWithValue("@local", Session["id_local"]);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Variables to select distrito and concelho
                    string idDistrito = "", idConcelho = "";
                    while (reader.Read())
                    {
                        text_name.Text = reader.GetString(0);
                        text_address.Text = reader.GetValue(1)?.ToString() ?? string.Empty;
                        text_town.Text = reader.GetString(2);
                        text_description.Text = reader.GetString(3);
                        idConcelho = reader.GetString(4).ToString();
                        idDistrito = reader.GetString(5).ToString();
                    }
                    reader.Close();
                    connection.Close();


                    // 2 - Load distritos
                    load_distritos();

                    // Select district
                    if (list_district.Items.FindByValue(idDistrito) != null)
                    {
                        list_district.SelectedValue = idDistrito;
                        listDistrito_SelectedIndexChanged(null, null); // Update concelhos
                    }

                    // 3 - Select concelhos
                    if (list_council.Items.FindByValue(idConcelho) != null)
                    {
                        list_council.SelectedValue = idConcelho;
                    }

                    //4 - Load 
                    GetFotosLocal(Session["id_local"].ToString());
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


                command.Parameters.AddWithValue("@id", Session["id_local"]);


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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "LocalFotoCriar";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@local", Session["id_local"]);
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
                        GetFotosLocal((Session["id_local"].ToString()));
                    }
                    else
                    {
                        // Invalid file type- cancel sql command and show error message
                        Response.Write("<script>alert('Selecione um ficheiro do tipo \".jpg\", \".jpeg\", "
                        + "\".png\", \".gif\" ou \".tiff.');</script>");
                        return;
                    }
                    text_legend.Text = string.Empty;
                }
            }

            else
            {
                // No photo selected - cancel sql command and show error message
                Response.Write("<script>alert('Selecione um ficheiro.');</script>");
                return;
            }
        }


        protected void link_details_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    // Puts the photo ID on the viewstate
                    ViewState["idFoto"] = e.CommandArgument.ToString();

                    // Selects the Legenda fro mthe selected photo
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

            if (ViewState["idFoto"] == null)
            {

                // No photo selected - cancel sql command and show error message
                Response.Write("<script>alert('Selecione uma foto.');</script>");
                return;
            }

            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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

                    // Updates the DataList with the photos from the Local
                    GetFotosLocal((Session["id_local"].ToString()));
                }
            }
        }

        protected void button_eliminate_photo(object sender, EventArgs e)
        {

            if (ViewState["idFoto"] == null)
            {

                // No photo selected - cancel sql command and show error message
                Response.Write("<script>alert('Selecione uma foto.');</script>");
                return;
            }

            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Eliminates the file
                    SqlCommand commandFoto = new SqlCommand();
                    commandFoto.Connection = connection;
                    commandFoto.CommandText = "SELECT Ficheiro FROM Foto WHERE Id = @id";

                    // ID of the photo definited when the photo is selected
                    commandFoto.Parameters.AddWithValue("@id", ViewState["idFoto"]);
                    connection.Open();

                    // Get the name of the file
                    SqlDataReader reader = commandFoto.ExecuteReader();
                    while (reader.Read())
                    {
                        string ficheiro = Server.MapPath("../" + reader[0].ToString());
                        //eliminar ficheiro
                        if (File.Exists(ficheiro))
                            File.Delete(ficheiro);
                    }
                    reader.Close();

                    // Eliminates the data on the databse table
                    commandFoto.Parameters.Clear();
                    commandFoto.CommandText = "DELETE Foto WHERE Id = @id";
                    commandFoto.CommandType = CommandType.Text;
                    commandFoto.Parameters.AddWithValue("@id", ViewState["idFoto"]);
                    commandFoto.ExecuteNonQuery();

                    text_legend.Text = string.Empty;

                    // Updates the DataList with the photos from the Local
                    GetFotosLocal(Session["id_local"].ToString());
                }
            }
        }

        protected void button_cancel_photo(object sender, EventArgs e)
        {
            text_legend.Text = string.Empty;
        }


        protected void clear_local_fields(object sender, EventArgs e)
        {
            text_name.Text = string.Empty;
            text_description.Text = string.Empty;
            text_address.Text = string.Empty;
            text_town.Text = string.Empty;


            list_council.Items.Clear();
            list_council.Items.Insert(0, "Escolha um Distrito primeiro");
            load_distritos();
            ViewState["idLocal"] = 0;

        }


        protected void eliminate_local(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                // Get the name of the file for the associated locais
                SqlCommand command= new SqlCommand();

                command.CommandText = "SELECT Ficheiro FROM Foto WHERE Local = @local";
                command.Parameters.AddWithValue("@local", Session["id_local"]);

                command.Connection = connection;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string ficheiro = Server.MapPath("../" + reader[0].ToString());

                    // Delete files
                    if (File.Exists(ficheiro))
                        File.Delete(ficheiro);
                }
                reader.Close();


                // Delete data from tables
                SqlCommand commandLocal = new SqlCommand();

                commandLocal.Connection = connection;
                commandLocal.CommandText = "LocalEliminar";
                commandLocal.CommandType = CommandType.StoredProcedure;
                commandLocal.Parameters.AddWithValue("@idlocal", Session["id_local"]);
                commandLocal.ExecuteNonQuery();

                Session["id_local"] = null;
                Response.Redirect("~/Utilizadores/Personal_area.aspx");
            }
        }

        protected void buttonCancelar_Click(object sender, EventArgs e)
        {
            Session["id_local"] = null;
            Response.Redirect("~/Utilizadores/Personal_area.aspx");
        }
    }
}