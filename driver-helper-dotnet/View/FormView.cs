using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace driver_helper_dotnet.View
{
    public static class FormView
    {
        private static int _currentLine = 0;
        public static int CurrentLine
        {
            get => _currentLine;
            set
            {
                _currentLine = value;
                UpdateProgress();
            }
        }
        public static int TotalLine { get; set; }
        public static event Action<string> ProgressUpdated;

        public static void UpdateProgress()
        {
            if (TotalLine == 0)
                return;

            double percentage = (double)CurrentLine / TotalLine * 100;
            ProgressUpdated?.Invoke($"{percentage:F2} %");
        }

        public static event Action<string> StatusUpdated;
        private static string _status = "準備";
        public static string Status 
        { 
            get => _status; 
            set 
            { 
                _status = value;
                UpdateStatus();
            } 
        }

        private static void UpdateStatus()
        {
            string status = Status;
            StatusUpdated?.Invoke(status);
        }

        public static event Action<bool> CancelBtnEnabledUpdated;
        private static bool _cancelBtnEnabled = true;
        public static bool CancelBtnEnabled
        {
            get => _cancelBtnEnabled;
            set
            {
                _cancelBtnEnabled = value;
                UpdateCancelBtnEnabled();
            }
        }

        private static void UpdateCancelBtnEnabled()
        {
            bool enabled = CancelBtnEnabled;
            CancelBtnEnabledUpdated?.Invoke(enabled);
        }

        public static event Action<bool> ProgressLblVisibleUpdated;
        private static bool _progressLblVisible = true;
        public static bool ProgressLblVisible
        {
            get => _progressLblVisible;
            set
            {
                _progressLblVisible = value;
                UpdateProgressLblVisible();
            }
        }

        private static void UpdateProgressLblVisible()
        {
            bool visible = ProgressLblVisible;
            ProgressLblVisibleUpdated?.Invoke(visible);
        }

        public static event Action<bool> CheckBtnEnabledUpdated;
        private static bool _checkBtnEnabled = true;
        public static bool CheckBtnEnabled
        {
            get => _checkBtnEnabled;
            set
            {
                _checkBtnEnabled = value;
                UpdateCheckBtnEnabled();
            }
        }

        private static void UpdateCheckBtnEnabled()
        {
            bool enabled = CheckBtnEnabled;
            CheckBtnEnabledUpdated?.Invoke(enabled);
        }
    }
}
