using Dapper;
using driver_helper_dotnet.Helper;
using driver_helper_dotnet.Model;
using driver_helper_dotnet.Repository;
using Npgsql;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace driver_helper_dotnet
{
    public partial class DriverHelper : Form
    {
        private CancellationTokenSource cancellationTokenSource;
        public DriverHelper()
        {
            InitializeComponent();
            View.FormView.ProgressUpdated += SetProgressLabel;
            View.FormView.StatusUpdated += SetStatusLabel;
            View.FormView.CancelBtnEnabledUpdated += SetCancelBtn;
            View.FormView.ProgressLblVisibleUpdated += SetProgressLblVisible;
            View.FormView.CheckBtnEnabledUpdated += SetCheckBtn;
        }

        private void DriverHelper_Load(object sender, EventArgs e)
        {

        }

        private async void readBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(groupNametxt.Text))
            {
                MessageBox.Show("請輸入群組名稱！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            

            try
            {
                openFileDialog1.ShowDialog();
                string fileName = openFileDialog1.FileName;
                string[] lines = File.ReadAllLines(fileName);
                SetView(lines.Length);
                cancellationTokenSource = new CancellationTokenSource();

                // Start Loading
                await Task.Run(() =>
                {
                    if (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        // filter by format
                        View.FormView.Status = "掃描中";
                        FilterHelper filter = new FilterHelper();
                        List<Order> orderList = filter.GetOrdersByFilter(lines, groupNametxt.Text, cancellationTokenSource.Token);

                        if (cancellationTokenSource.Token.IsCancellationRequested)
                        {
                            View.FormView.Status = "任務中止";
                            return;
                        }
                        // save into DB
                        View.FormView.Status = "儲存中";
                        HideProgressLbl();
                        var repo = new Repository.OrderRepo();
                        repo.SaveToDB(orderList);

                        View.FormView.Status = "儲存完畢";


                        MessageBox.Show("已儲存完畢", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }, cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                // log
                Debug.WriteLine(ex);
            }

            ShowProgressLbl();
            SetClear();
        }

        private void StopTask()
        {
            cancellationTokenSource?.Cancel();
        }
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            StopTask();
        }

        private void SetView(int lineCount)
        {
            View.FormView.TotalLine = lineCount;
            readBtn.Enabled = false;
            View.FormView.CheckBtnEnabled = false;
        }

        private void SetClear()
        {
            View.FormView.CurrentLine = 0;
            readBtn.Enabled = true;
            cancelBtn.Enabled = true;
            View.FormView.CheckBtnEnabled = true;
        }

        private void HideProgressLbl()
        {
            View.FormView.CancelBtnEnabled = false;
        }
        private void ShowProgressLbl()
        {
            View.FormView.CancelBtnEnabled = true;
        }
        public void SetProgresslbl(string txt)
        {
            progresslbl.Text = txt;
        }
        private void SetProgressLabel(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(SetProgressLabel), text);
                return;
            }

            progresslbl.Text = text;
        }

        private void SetStatusLabel(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(SetStatusLabel), text);
                return;
            }

            statuslbl.Text = text;
        }
        private void SetCancelBtn(bool enabled)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(SetCancelBtn), enabled);
                return;
            }

            cancelBtn.Enabled = enabled;
        }

        private void SetProgressLblVisible(bool visible)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(SetProgressLblVisible), visible);
                return;
            }

            progresslblM.Enabled = visible;
            progresslbl.Enabled = visible;
        }

        private void SetCheckBtn(bool enabled)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(SetCheckBtn), enabled);
                return;
            }

            checkConBtn.Enabled = enabled;
        }

        private void checkConBtn_Click(object sender, EventArgs e)
        {
            string connStr = new SettingsHelper().GetConnectionString();

            try
            {
                using (var conn = new NpgsqlConnection(connStr))
                {
                    conn.Open();
                    MessageBox.Show("資料庫連線正常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (NpgsqlException)
            {
                MessageBox.Show("資料庫連線異常", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}