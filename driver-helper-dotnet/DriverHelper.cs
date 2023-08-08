using Dapper;
using driver_helper_dotnet.Helper;
using driver_helper_dotnet.Model;
using driver_helper_dotnet.Repository;
using Npgsql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace driver_helper_dotnet
{
    public partial class DriverHelper : Form
    {
        
        public DriverHelper()
        {
            InitializeComponent();
        }

        private void DriverHelper_Load(object sender, EventArgs e)
        {

        }

        private void readBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(groupNametxt.Text))
            {
                MessageBox.Show("請輸入群組名稱！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            openFileDialog1.ShowDialog();
            string fileName = openFileDialog1.FileName;
            string[] lines = File.ReadAllLines(fileName);


            // filter by format
            FilterHelper filter = new FilterHelper();
            List<Order> orderList = filter.GetOrdersByFilter(lines, groupNametxt.Text);

            // save into DB
            var repo = new Repository.OrderRepo();
            repo.SaveToDB(orderList);


            MessageBox.Show("已儲存完畢", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}