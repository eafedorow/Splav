using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MVVM;
using Splav2.Abstractions;

namespace Splav2.Models
{
    internal class ProjectModel: BindableBase, IProjectPage
    {
        private string _dataBasepath = "";
        private string _pyScriptpath = "";
        
        public string DataBasepath
        {
            get => _dataBasepath;
            set => SetProperty(ref _dataBasepath, value);
        }
        public string PyScriptpath
        {
            get => _pyScriptpath;
            set=> SetProperty(ref _pyScriptpath, value);
        }
    }
}
