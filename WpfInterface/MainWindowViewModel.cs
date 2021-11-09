using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Utilities.ViewModels.Base;

namespace WpfInterface
{
    public class MainWindowViewModel : ViewModel
    {
        private string _Title = "Главное окно";

        //public string Title
        //{
        //    get => _Title;
        //    set
        //    {
        //        if(Equals(_Title, value)) return;
        //        _Title = value;
        //        OnPropertyChanged("Title");
        //    }
        //}

        public string Title { get => _Title; set => Set(ref _Title, value); }

        public DateTime CurrentTime => DateTime.Now;

        private Task _UpdateTimeTask;

        public MainWindowViewModel()
        {
            _UpdateTimeTask = UpdateTime();
        }

        protected async Task UpdateTime()
        {
            while (true)
            {
                await Task.Delay(100);
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        protected override void OnPropertyChanged(string PropertyName = null)
        {
            switch (PropertyName)
            {
                case nameof(Title):
                    Debug.WriteLine(Title);
                    break;
            }

            if (PropertyName != nameof(CurrentTime))
                base.OnPropertyChanged(PropertyName);
        }
    }
}
