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
using MVVM.Extensions;

namespace Splav2.ViewModels
{
    internal class ViewModeOutputPath: BindableBase
    {
        private CancellationTokenSource source;
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
            StopCommand = new RelayCommand(StopExamination, CanStop);
            this.WhenPropertyChanged(x => x.Stop, OnStop);
        }   

        /// <summary>
        /// Сама кнопка есть, надо настроить ее видимость, скорее всего через costum nastr
        /// </summary>
        private async void StartExamination() {
            source = new CancellationTokenSource();
            var model = ProjectModel.Instance;
            string dbpath = model.DataBasepath;
            string scriptpath = model.PyScriptpath;
            if (dbpath != "" && scriptpath != "")
            {
                Process? proc = null;
                try
                {
                    Stop = true;
                    string processName = $"\"C:\\Windows\\py.exe {scriptpath} {dbpath}\"";
                    proc = Process.Start("cmd", $"/c {processName}");
                    await proc.WaitForExitAsync(source.Token);
                    await Task.Delay(3000);
                    MessageBox.Show("Complit script!"); // Впринципи это можно убрать (уточнить вопрос про /q echo off)
                }
                catch (System.Threading.Tasks.TaskCanceledException)
                {
                    if (proc != null) {
                        if (!proc.HasExited) {
                            proc.Kill();
                        }
                    }
                }
            }
            else MessageBox.Show("Отсутствует путь к бд или скрипту!!!");
            Stop = false;
        }
        private void StopExamination()
        {
            source?.Cancel();
        }
        private bool CanStop() => Stop;
        private void OnStop()
        {
            StopCommand.RaiseCanExecuteChanged();
        }
    }
}
