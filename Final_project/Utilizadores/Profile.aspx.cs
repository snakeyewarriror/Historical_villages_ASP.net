using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace Final_project.Utilizadores
{
    public partial class Profile : System.Web.UI.Page
    {

        string connection_string = @"data source=.\Sqlexpress; initial catalog = Locais; integrated security = true;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInfo();
            }
        }


        protected void ChangeProfileButton_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                using(SqlCommand command = new SqlCommand())
                {

                    
                    command.Connection = connection;
                    connection.Open();

                    SqlTransaction transaction = connection.BeginTransaction();
                    command.Transaction = transaction;

                    // Alter table Utilizador
                    command.CommandText = "Update Utilizador set Nome = @user_name, Email = @user_email WHERE Id = @user_id";

                    command.Parameters.AddWithValue("@user_name", TextBoxUsername.Text);
                    command.Parameters.AddWithValue("@user_email", TextBoxEmail.Text);
                    command.Parameters.AddWithValue("@user_id", Session["id_utilizador"]);
                    command.ExecuteNonQuery();

                    // Alter table Users
                    command.CommandText = "Update Users set UserName = @user_name WHERE UserId = @user_id";

                    command.Parameters.Clear(); // Clear previous parameters
                    command.Parameters.AddWithValue("@user_name", TextBoxUsername.Text);
                    command.Parameters.AddWithValue("@user_id", Session["id_utilizador"]);
                    command.ExecuteNonQuery();

                    // Alter table Menberships
                    command.CommandText = "Update Memberships set Email = @user_email WHERE UserId = @user_id";

                    command.Parameters.Clear(); // Clear previous parameters
                    command.Parameters.AddWithValue("@user_email", TextBoxEmail.Text);
                    command.Parameters.AddWithValue("@user_id", Session["id_utilizador"]);
                    command.ExecuteNonQuery();


                    transaction.Commit();
                    connection.Close();
                }
            }
        }

        protected void LoadInfo()
        {

            string user_id = Session["id_utilizador"].ToString();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                // 1 - Read local data
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "SELECT U.Nome, U.Email from Utilizador U WHERE Id = @user_id";
                    command.Parameters.AddWithValue("@user_id", user_id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        TextBoxUsername.Text = reader.GetString(0);
                        TextBoxEmail.Text = reader.GetString(1);
                    }
                    reader.Close();
                    connection.Close();
                }
            }
        }

    }
}