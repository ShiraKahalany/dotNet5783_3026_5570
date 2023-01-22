using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO;

public partial class Now : Window, INotifyPropertyChanged
{
    private DateTime _now;
    public DateTime now
    {
        get { return _now; }
        set
        {
            _now = value;
            OnPropertyChanged("now");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // ...
}
