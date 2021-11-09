using System.Diagnostics;

namespace WpfInterface
{
    public class TestMainWindowViewModel : MainWindowViewModel
    {
        protected override void OnPropertyChanged(string PropertyName = null)
        {
            switch (PropertyName)
            {
                case nameof(Title):
                    Debug.WriteLine(Title);
                    break;
            }

            base.OnPropertyChanged(PropertyName);
        }
    }
}
