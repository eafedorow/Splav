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
using System.IO;
using System.Threading;

namespace Splav2.ViewModels
{
    internal class ViewModeOutputPath: BindableBase
    {
        private CancellationTokenSource source = new CancellationTokenSource();
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        private string _outputPath = "";
        public string OutputPath 
        { 
            get => _outputPath; 
            set => SetProperty(ref _outputPath, value); 
        }

        public ViewModeOutputPath() {
            StartCommand = new RelayCommand(StartExamination);
            StopCommand = new RelayCommand(StopExamination);

        }

        /// <summary>
        /// Сама кнопка есть, надо настроить ее видимость, скорее всего через costum nastr
        /// </summary>
        private async void StartExamination() {
            var model = ProjectModel.Instance;
            string dbpath = model.DataBasepath;
            string scriptpath = model.PyScriptpath;
            if (dbpath != "" && scriptpath != "")
            {
                string processName = $"C:\\Windows\\py.exe \"{scriptpath} {dbpath}\"";
                var proc = System.Diagnostics.Process.Start(processName);
                await proc.WaitForExitAsync(source.Token);
                MessageBox.Show("Complit script!");
            }
            else MessageBox.Show("Отсутствует путь к бд или скрипту!!!");
        }
        private void StopExamination()
        {
            source.Cancel();
        }
    }
}
