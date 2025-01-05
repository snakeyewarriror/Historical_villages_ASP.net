using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final_project
{
    public partial class create_account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string data = DateTime.Now.AddYears(-18).ToShortDateString();
                validarDataNascimento.ErrorMessage = "Defina data igual ou anterior a " + data;
                validarDataNascimento.Type = ValidationDataType.Date;
                validarDataNascimento.Operator = ValidationCompareOperator.LessThan;
                validarDataNascimento.ValueToCompare = data;
                validarDataNascimento.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC0000");

                var provider = Membership.Provider as SqlMembershipProvider;
                if (provider != null)
                {
                    int minPasswordLength = provider.MinRequiredPasswordLength;
                    int minNonAlphanumericChars = provider.MinRequiredNonAlphanumericCharacters;
                    string passwordRegex = provider.PasswordStrengthRegularExpression;

                    Response.Write($"Min Password Length: {minPasswordLength}, Min Non-Alphanumeric: {minNonAlphanumericChars}, Regex: {passwordRegex}");
                }
            }
        }

        protected void buttonCriarConta_Click(object sender, EventArgs e)
        {
            //Criar conta - Membership
            Membership.CreateUser(TextBoxUsername.Text, password.Text, TextBoxEmail.Text);

            //obter o UserID
            MembershipUser user = Membership.GetUser(TextBoxUsername.Text);
            object user_id = user.ProviderUserKey;


            using (SqlConnection connection = new SqlConnection(@"data source=.\Sqlexpress; initial catalog = Locais; integrated security = true;"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "INSERT Utilizador(Id, Nome, Email, DataNascimento) " +
                        "VALUES( @user_id, @nome, @email, @data)";

                    // Add the values to the query
                    command.Parameters.Add("@user_id", SqlDbType.NVarChar).Value = user_id.ToString();
                    command.Parameters.Add("@nome", SqlDbType.NVarChar, 50).Value = TextBoxUsername.Text;
                    command.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = TextBoxEmail.Text;
                    command.Parameters.Add("@data", SqlDbType.DateTime).Value = DateTime.Parse(TextBoxDate.Text);


                    connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();


                }
            }

            // Redirects to the login page
            Response.Redirect("login.aspx");
        }

    }
}