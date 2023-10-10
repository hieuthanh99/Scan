using Scan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.App
{
    public class HistoryLoginItemViewModel : BaseViewModel
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _FullName;
        public string FullName
        {
            get { return _FullName; }
            set
            {
                _FullName = value;
                OnPropertyChanged("FullName");
            }
        }

        private DateTime _LoginTime;
        public DateTime LoginTime
        {
            get { return _LoginTime; }
            set
            {
                _LoginTime = value;
                OnPropertyChanged("LoginTime");
            }
        }

        private bool _IsSelect;
        public bool IsSelect
        {
            get
            {
                return _IsSelect;
            }
            set
            {
                _IsSelect = value;
                OnPropertyChanged("IsSelect");
            }
        }
    }
}
