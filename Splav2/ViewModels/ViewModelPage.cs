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
            set => _pythonScriptText = value; 
        }

        public ViewModelPage()
        {
            string filepath = "main.py";
            using (StreamReader read = new StreamReader(filepath))
            {
               //string text = await read.ReadToEndAsync();

            }

        }
    }
}
