using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final_project
{
    public partial class Main_page : System.Web.UI.Page
    {
        private int currentPage;
        private string connection_string = @"data source=.\sqlexpress; initial catalog = Locais; integrated security = true;";

        protected void Page_Load(object sender, EventArgs e)
        {

            using(SqlConnection connection = new SqlConnection(connection_string))
            {
                using (SqlCommand command = new SqlCommand("GetTop10Locais", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // colocar dados no DataList
                        listLocaisTop.DataSource = reader;
                        listLocaisTop.DataBind();
                    }
                    connection.Close();
                }
            }


            if (!IsPostBack)
            {

                GetLocais();
                ViewState["contador"] = 0;
            }

            currentPage = (int)ViewState["contador"];
        }

        void GetLocais()
        {
            SqlConnection connection = new SqlConnection
            (connection_string);

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
            GetLocais();
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
            currentPage = (int)ViewState["total"] - 1;
            ViewState["contador"] = currentPage;
            BindListLocais((DataTable)ViewState["dataSource"]);
        }
    }
}