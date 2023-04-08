using Microsoft.Win32;
using MVVM;
using MVVM.Commands;
using Splav2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Splav2.ViewModels
{
    internal class ViewModelOpenScript : BindableBase
    {
        public ViewModelOpenScript() { }

        private RelayCommand openScript;
        public ICommand OpenScript => openScript ??= new RelayCommand(PerformOpenScript);

        private void PerformOpenScript()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Python Scripts (*.py)|*.py";

            if (openFileDialog.ShowDialog() == true)
            {
                var model = ProjectModel.GetProjectModel();
                //string scriptText = File.ReadAllText(openFileDialog.FileName);
                string filename = openFileDialog.FileName;
                model.PyScriptpath = filename;
            }
        }
    }
}
