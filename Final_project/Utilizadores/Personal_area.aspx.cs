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
    public partial class Personal_area : System.Web.UI.Page
    {
        private string connectionString = @"data source=.\sqlexpress; initial catalog = Locais; integrated security=true;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_locais();
            }
        }

        private void Load_locais()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                // No photo selected - cancel sql command and show error message
                string id = Session["id_utilizador"].ToString();

                SqlCommand command = new SqlCommand("GetLocaisByUtilizador", connection);

                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@utilizador", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                //mostrar dados no controlo DataList
                listLocais.DataSource = table;
                listLocais.DataBind();
            }
        }

        protected void Button_create_local(object sender, EventArgs e)
        {
            Response.Redirect("Add_local.aspx", false);
        }


        protected void Link_details_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
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
                        //text_legend.Text = reader[0].ToString();
                    }
                    reader.Close();
                }
            }
        }
    }
}