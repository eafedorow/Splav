using MVVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Resources;

namespace Splav2.ViewModels
{
    internal class ViewModelPage : BindableBase
    {
        private string _pythonScriptText = "";
        public string PythonScriptText 
        {
            get => _pythonScriptText; 
            set => SetProperty(ref _pythonScriptText, value); 
        }

        public ViewModelPage()
        {
            ReadPythonFile();
        }
        private async void ReadPythonFile()
        {
            string filepath = "main.py";
            using (StreamReader read = new StreamReader(filepath))
            {
                PythonScriptText = await read.ReadToEndAsync();
            }
        }
    }
}
