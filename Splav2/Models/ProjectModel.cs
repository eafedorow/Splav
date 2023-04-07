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
        private static ProjectModel? model;
        private string _dataBasepath = "";
        private string _pyScriptpath = "";
        private UserControl _currentPage;
        public UserControl CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

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
        private ProjectModel() { }
        public static ProjectModel GetProjectModel()
        {
            model ??= new ProjectModel();
            return model;
        }
    }
}
