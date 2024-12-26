using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final_project
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginUtilizador_LoggedIn(object sender, EventArgs e)
        {
            //obter dados do utilizador - Membership
            MembershipUser user = Membership.GetUser(loginUser.UserName);

            //Login de utilizador - obter ID (chave primária - Utilizador) e colocar em Session

            string id = "";

            using (SqlConnection connection = new SqlConnection(
            @"data source=.\Sqlexpress; initial catalog = Locais; integrated security = true;"))
            {

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Id FROM Utilizador WHERE ID = @user_id";
                    command.Parameters.AddWithValue("@user_id", user.ProviderUserKey.ToString());
                    connection.Open();
                    SqlDataReader reader;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        id = reader[0].ToString();
                    }
                    connection.Close();
                }
            }

            Session["id_utilizador"] = id;
            Response.Redirect("~/Main.aspx");
        }
    }
}