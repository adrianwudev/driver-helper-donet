using driver_helper_dotnet.Helper;
using driver_helper_dotnet.Model;
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

            //FormatFilter formatFilter = new FormatFilter();
            //OrderFormats orderFormats = formatFilter.GetByFormatFilter(readFile);


            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            foreach (Order order in orderList)
            {
                Debug.WriteLine("=====");
                var orderJson = JsonSerializer.Serialize(order, options);
                Debug.WriteLine(orderJson);
            }
            // save into DB
        }


    }
}