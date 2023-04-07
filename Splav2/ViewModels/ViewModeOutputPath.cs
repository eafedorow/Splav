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

namespace Splav2.ViewModels
{
    internal class ViewModeOutputPath: BindableBase
    {
        private string _outputPath = "";
        public string OutputPath 
        { 
            get => _outputPath; 
            set => SetProperty(ref _outputPath, value); 
        }

        public ViewModeOutputPath() {
            var model = ProjectModel.GetProjectModel();
            string dbpath = model.DataBasepath;
            string scriptpath = model.PyScriptpath;
            if (dbpath != "" && scriptpath != "")
            {
                GoScript(dbpath, scriptpath); // Вызов обработки файла 
            }
        }

        private void GoScript(string dbpath, string scriptpath)
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            scope.SetVariable("dbpath", dbpath);
            engine.ExecuteFile(scriptpath, scope);
            OutputPath = scope.GetVariable("modelpath");
        }
    }
}
