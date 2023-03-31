using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Splav2.Abstractions
{
    internal interface ICurrentPage: INotifyPropertyChanged
    {
        Page CurrentPage { get; set; }
    }
}
