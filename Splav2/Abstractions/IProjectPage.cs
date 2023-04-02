using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Splav2.Abstractions
{
    internal interface IProjectPage: INotifyPropertyChanged
    {
        Page CurrentPage { get; set; }
        String DataBasepath { get; set; }
        String PyScriptpath { get; set; }
    }
}
