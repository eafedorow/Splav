using MVVM;
using MVVM.Commands;
using Splav2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Resources;

namespace Splav2.ViewModels
{
    internal class ViewModelPrintPyScr : BindableBase
    {
        public ICommand PrintCommand { get; }
        private string _pythonScriptText = "";
        public string PythonScriptText 
        {
            get => _pythonScriptText; 
            set => SetProperty(ref _pythonScriptText, value); 
        }

        public ViewModelPrintPyScr()
        {
            PrintCommand = new RelayCommand(PrintPyScript);
        }
        private void PrintPyScript() {
            var model = ProjectModel.Instance;
            string filepath = model.PyScriptpath;
            if (filepath != "") ReadPythonFile(model.PyScriptpath);
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
