using MVVM;
using Splav2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Resources;

namespace Splav2.ViewModels
{
    internal class ViewModelPrintPyScr : BindableBase
    {
        private string _pythonScriptText = "";
        public string PythonScriptText 
        {
            get => _pythonScriptText; 
            set => SetProperty(ref _pythonScriptText, value); 
        }

        public ViewModelPrintPyScr()
        {
            var model = ProjectModel.GetProjectModel();
            ReadPythonFile(model.PyScriptpath);
        }
        private async void ReadPythonFile(string filepath)
        {
            using (StreamReader read = new(filepath))
            {
                PythonScriptText = await read.ReadToEndAsync();
            }
        }
    }
}
