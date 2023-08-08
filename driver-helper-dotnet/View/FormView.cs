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
    }
}
