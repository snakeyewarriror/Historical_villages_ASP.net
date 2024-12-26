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
        protected void Page_Load(object sender, EventArgs e)
        {
            ObterLocais();
        }

        void ObterLocais()
        {
            //verificar se o Id do utilizador se encontra em Session
            if (Session["id_utilizador"] != null)
            {
                using (SqlConnection connection = new SqlConnection(@"data source=.\Sqlexpress; initial catalog = Locais; integrated security = true;"))
                {
                    using (SqlCommand command = new SqlCommand()) 
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT Id, Nome, Foto FROM Local WHERE Utilizador = @id";
                        //associar Id do utilizador ao comando, de forma a
                        //obter apenas os locais criados pelo utilizador
                        command.Parameters.AddWithValue("@id", Session["id_utilizador"].ToString());
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        DataTable table = new DataTable();
                        table.Load(reader);
                        reader.Close();
                        gridLocais.DataSource = table;
                        gridLocais.DataBind();

                    }
                }

            }
        }
    }
}