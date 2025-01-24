using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final_project
{
    public partial class Search : System.Web.UI.Page
    {

        private int currentPage;

        private string connectionString = @"data source=.\sqlexpress; initial catalog = Locais; integrated security=true;";

        void LoadDistritos()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nome FROM Distrito", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                listDistrito.DataSource = reader;
                listDistrito.DataTextField = "Nome";
                listDistrito.DataValueField = "Id";
                listDistrito.DataBind();

                // Add initial item
                listDistrito.Items.Insert(0, "Selecione um Distrito");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                listConcelho.Items.Insert(0, "Escolha um Distrito primeiro");
                GetLocais();
                LoadDistritos();
                ViewState["contador"] = 0;
            }

            currentPage = (int)ViewState["contador"];
        }

        private void LoadConcelhos(int distritoId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nome FROM Concelho " +
                "WHERE Distrito = @DistritoId", conn);
                cmd.Parameters.AddWithValue("@DistritoId", distritoId);
                SqlDataReader reader = cmd.ExecuteReader();
                listConcelho.DataSource = reader;
                listConcelho.DataTextField = "Nome";
                listConcelho.DataValueField = "Id";
                listConcelho.DataBind();

                // Add inital item
                listConcelho.Items.Insert(0, "Selecione um Concelho");
            }
        }

        protected void listDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listDistrito.SelectedValue != "Selecione um Distrito")
            {
                LoadConcelhos(int.Parse(listDistrito.SelectedValue));
            }
            else
            {
                listConcelho.Items.Clear();
                listConcelho.Items.Insert(0, "Selecione um Concelho");
            }
        }

        protected void buttonPesquisar_Click(object sender, EventArgs e)
        {

            string command = "SELECT L.Id AS LocalID, L.Nome AS NomeLocal, C.Nome AS Concelho, " +
                            "D.Nome AS Distrito, F.Ficheiro AS PrimeiraFoto " +
                            "FROM Local L INNER JOIN Concelho C ON L.Concelho = C.Id " +
                            "INNER JOIN Distrito D ON C.Distrito = D.Id " +
                            "LEFT JOIN (SELECT Local, MIN(Id) AS PrimeiraFotoId FROM Foto " +
                            "GROUP BY Local) FP ON L.Id = FP.Local " +
                            "LEFT JOIN Foto F ON FP.PrimeiraFotoId = F.Id ";

            List<SqlParameter> parameters = new List<SqlParameter>();


            if (!string.IsNullOrWhiteSpace(textNome.Text) ||
                listDistrito.SelectedValue != "Selecione um Distrito" ||
                (!string.IsNullOrEmpty(listConcelho.SelectedValue) && listConcelho.SelectedValue != "Escolha um Distrito primeiro"))
            {

                command += " WHERE ";
                if (!string.IsNullOrWhiteSpace(textNome.Text))
                {

                    command += "L.Nome LIKE @Nome";
                    parameters.Add(new SqlParameter("@Nome", "%" + textNome.Text.Trim() + "%"));
                }

                if (listDistrito.SelectedValue != "Selecione um Distrito")
                {
                    if (parameters.Count > 0) command += " AND ";
                    command += "D.Id = @DistritoId";
                    parameters.Add(new SqlParameter("@DistritoId", listDistrito.SelectedValue));
                }

                if (listConcelho.SelectedValue != "Selecione um Distrito" && listConcelho.SelectedValue != "Escolha um Distrito primeiro")
                {
                    if (parameters.Count > 0) command += " AND ";
                    command += "C.Id = @ConcelhoId";
                    parameters.Add(new SqlParameter("@ConcelhoId", listConcelho.SelectedValue));
                }
            }

            command += " order by LocalID DESC";

            DataTable resultadoPesquisa = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(command, connection);
                cmd.Parameters.AddRange(parameters.ToArray());
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                resultadoPesquisa.Load(reader);
                reader.Close();
            }
            BindListLocais(resultadoPesquisa);
        }


        // DataList

        void GetLocais()
        {
            SqlConnection connection = new SqlConnection
            (@"data source=.\sqlexpress; initial catalog = Locais; integrated security=true;");

            SqlCommand command = new SqlCommand();
            command.CommandText = "GetLocais";
            command.CommandType = CommandType.StoredProcedure;

            command.Connection = connection;
            SqlDataReader reader;
            DataTable table = new DataTable();
            connection.Open();
            reader = command.ExecuteReader();
            table.Load(reader);
            connection.Close();
            BindListLocais(table);
        }

        void BindListLocais(DataTable table)
        {
            PagedDataSource paged = new PagedDataSource();
            paged.DataSource = table.DefaultView;
            paged.PageSize = 6;
            paged.AllowPaging = true;
            paged.CurrentPageIndex = currentPage;

            linkFirst.Enabled = !paged.IsFirstPage;
            linkPrevious.Enabled = !paged.IsFirstPage;
            linkNext.Enabled = !paged.IsLastPage;
            linkLast.Enabled = !paged.IsLastPage;

            ViewState["total"] = paged.PageCount;
            listLocais.DataSource = paged;
            listLocais.DataBind();
            ViewState["dataSource"] = table;
        }


        protected void linkFirst_click(object sender, EventArgs e)
        {
            currentPage = 0;
            ViewState["contador"] = currentPage;
            buttonPesquisar_Click(null, null);
        }

        protected void linkPrevious_click(object sender, EventArgs e)
        {
            currentPage = (int)ViewState["contador"];
            currentPage -= 1;
            ViewState["contador"] = currentPage;
            BindListLocais((DataTable)ViewState["dataSource"]);
        }

        protected void linkNext_click(object sender, EventArgs e)
        {
            currentPage = (int)ViewState["contador"];
            currentPage += 1;
            ViewState["contador"] = currentPage;
            BindListLocais((DataTable)ViewState["dataSource"]);
        }

        protected void linkLast_click(object sender, EventArgs e)
        {
            currentPage = (int)ViewState["total"] - 2;
            currentPage += 1;
            ViewState["contador"] = currentPage;
            BindListLocais((DataTable)ViewState["dataSource"]);
        }

    }
}