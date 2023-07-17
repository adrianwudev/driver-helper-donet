using driver_helper_dotnet.Helper;

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

            openFileDialog1.ShowDialog();
            string fileName = openFileDialog1.FileName;
            string[] lines = File.ReadAllLines(fileName);



            // filter by format
            FilterHelper filter = new FilterHelper();
            filter.GetOrdersByFilter(lines);

            //FormatFilter formatFilter = new FormatFilter();
            //OrderFormats orderFormats = formatFilter.GetByFormatFilter(readFile);


            //Debug.WriteLine("PickUpTimeDate: " + orderFormats.PickUpTimeDate);
            //Debug.WriteLine("PickUpTime: " + orderFormats.PickUpTime);
            //Debug.WriteLine("AddressUp: " + orderFormats.AddressUp);
            //Debug.WriteLine("AddressOff: " + orderFormats.AddressOff);
            // save into DB
        }


    }
}