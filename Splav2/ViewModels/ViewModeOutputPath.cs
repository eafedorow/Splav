using IronPython.Compiler.Ast;
using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Windows;
using Splav2.Models;
using System.Windows.Input;
using MVVM.Commands;

namespace Splav2.ViewModels
{
    internal class ViewModeOutputPath: BindableBase
    {
        public ICommand StartCommand { get; }
        private string _outputPath = "";
        public string OutputPath 
        { 
            get => _outputPath; 
            set => SetProperty(ref _outputPath, value); 
        }

        public ViewModeOutputPath() {
            StartCommand = new RelayCommand(StartExamination);
        }

        private void StartExamination() {
            var model = ProjectModel.Instance;
            string dbpath = model.DataBasepath;
            string scriptpath = model.PyScriptpath;
            if (dbpath != "" && scriptpath != "")
            {
                GoScript(dbpath, scriptpath); // Вызов обработки файла 
            }
            else MessageBox.Show("Отсутствует путь к бд или скриату!!!");

        }

        private void GoScript(string dbpath, string scriptpath)
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            scope.SetVariable("db_path", dbpath);
            engine.ExecuteFile(scriptpath, scope);
            OutputPath = scope.GetVariable("model_path");
        }
    }
}
