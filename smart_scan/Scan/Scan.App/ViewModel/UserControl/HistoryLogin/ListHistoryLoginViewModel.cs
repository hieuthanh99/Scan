using Scan.Core.Services;
using Scan.Core.ViewModel.Base;
using Scan.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scan.App
{
    public class ListHistoryLoginViewModel : BaseViewModel
    {
        private readonly WrapperServices services;
        public ObservableCollection<HistoryLogin> Items { get; set; }
        public ICommand onChooseUser { get; set; }


        public ListHistoryLoginViewModel()
        {
            services = WrapperServices.Instance();

            onChooseUser = new RelayParameterizedCommand((parameter) => HandleChooseUser(parameter));
            Items = LoadHistory();

            if (Items.Count > 0)
            {
                Items[0].IsSelect = true;
                IoC.Get<ApplicationViewModel>().UserHistoryChoosed = Items.FirstOrDefault(x => x.IsSelect = true).UserName;
            }
        }

        private void HandleChooseUser(object parameter)
        {
            var userHistory = parameter as HistoryLogin;
            Items = Items.Aggregate(new ObservableCollection<HistoryLogin>(), (currentVal, val) =>
            {
                val.IsSelect = false;
                if (val.UserName == userHistory.UserName)
                    val.IsSelect = true;

                currentVal.Add(val);
                return currentVal;
            });

            OnPropertyChanged("Items");
            string choosenUserName = "";
            foreach (HistoryLogin item in Items)
            {
                if (item.IsSelect)
                {
                    choosenUserName = item.UserName;
                    break;
                }
            }

            IoC.Get<ApplicationViewModel>().UserHistoryChoosed = choosenUserName;
        }

        private ObservableCollection<HistoryLogin> LoadHistory()
        {
            //var histories = services.User.GetLoginHistory();

            //Data Test
            List<HistoryLogin> histories = new List<HistoryLogin>();

            histories.Add(new HistoryLogin
            {
                UserName = "testscan01",
                FullName = "testscan01",
                LoginTime = "2023-10-04 09:00:00",
                IsSelect = true
            });

            var items = new ObservableCollection<HistoryLogin>();
            foreach (var item in histories)
            {
                items.Add(item);
            }

            return items;
        }
    }
}
