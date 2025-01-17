using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final_project
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Checks if a user is logged in already
            if(System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string id = "";

                // Gets the user id by searching with its name
                using (SqlConnection connection = new SqlConnection(
                @"data source=.\Sqlexpress; initial catalog = Locais; integrated security = true;"))
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT Id FROM Utilizador WHERE Nome = @user_name";
                        command.Parameters.AddWithValue("@user_name", HttpContext.Current.User.Identity.Name.ToString());
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

            }

        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Main_page.aspx");
        }

    }
}