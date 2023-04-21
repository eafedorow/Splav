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
using System.Diagnostics;
using static IronPython.Modules._ast;
using MVVM.Extensions;

namespace Splav2.ViewModels
{
    internal class ViewModeOutputPath: BindableBase
    {
        private CancellationTokenSource source = new CancellationTokenSource();
        private bool _stop = false;
        public bool Stop 
        {
            get=>_stop;
            set => SetProperty(ref _stop, value);
        }
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
            StopCommand = new RelayCommand(StopExamination,CanStop);
            this.WhenPropertyChanged(x => x.Stop, OnStop);
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
                try
                {
                    Stop = true;
                    string processName = $"\"C:\\Windows\\py.exe {scriptpath} {dbpath}\"";
                    var proc = Process.Start("cmd", $"/c {processName}");
                    await proc.WaitForExitAsync(source.Token);
                    await Task.Delay(3000);
                    MessageBox.Show("Complit script!"); // Впринципи это можно убрать (уточнить вопрос про /q echo off)
                }
                catch (System.Threading.Tasks.TaskCanceledException)
                {
                    /// Надо ли делать тут что-либо! Хотя зачем....
                }
                Stop = false;
            }
            else MessageBox.Show("Отсутствует путь к бд или скрипту!!!");
        }
        private void StopExamination()
        {
            source.Cancel();
        }
        private bool CanStop() => Stop;
        private void OnStop()
        {
            StopCommand.RaiseCanExecuteChanged();
        }
    }
}
