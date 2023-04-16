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

        private RelayCommand openScriptPy;

        private string _filePath = "";

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    SetProperty(ref _filePath, value);
                }
            }
        }
        public ICommand OpenScriptPy => openScriptPy ??= new RelayCommand(PerformOpenScriptPy);

        private void PerformOpenScriptPy()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Python Scripts (*.py)|*.py";

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                var model = ProjectModel.Instance;
                model.PyScriptpath = FilePath;
            }
        }
    }
}
