using Aspose.Cells;
using ExcelDataReader;
using Microsoft.Win32;
using MVVM;
using MVVM.Commands;
using Splav2.Models;
using Splav2.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;


namespace Splav2.ViewModels
{
    internal class ViewModelOpenDB : BindableBase
    {
        private DataTable _databases = new DataTable();
        public DataTable Databases
        {
            get { return _databases; }
            set
            {
                if (_databases != value)
                {
                    SetProperty(ref _databases, value);
                }
            }
        }

        public ViewModelOpenDB()
        {
            
        }

        private int _rowCount;
        private int _columnCount;

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

        public int RowCount
        {
            get { return _rowCount; }
            set
            {
                if (_rowCount != value)
                {
                    SetProperty(ref _rowCount, value);
                }
            }
        }


        public int ColumnCount
        {
            get { return _columnCount; }
            set
            {
                if (_columnCount != value)
                {
                    SetProperty(ref _columnCount, value);
                }
            }
        }


        private DataTable _excelData;
        IExcelDataReader edr;



       /* private DataView ReadExcelData(string filePath)
        {
            Workbook wb = new Workbook(filePath);
            WorksheetCollection collection = wb.Worksheets;

            for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
            { 
                Worksheet worksheet = collection[worksheetIndex];

            }
            // Читаем, получаем DataView и работаем с ним как обычно.
            DataSet dataSet = edr.AsDataSet(conf);
            DataView dtView = dataSet.Tables[0].AsDataView();

            // После завершения чтения освобождаем ресурсы.
            edr.Close();
            return dtView;
        }*/

        private RelayCommand openExcel;
        public ICommand OpenExcel => openExcel ??= new RelayCommand(PerformOpenExcel);

        private void PerformOpenExcel()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "EXCEL Files (*.xlsx)|*.xlsx|EXCEL Files 2003 (*.xls)|*.xls|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != true)
                return;
            FilePath = openFileDialog.FileName;
            var model = ProjectModel.Instance;
            model.DataBasepath = FilePath;
            var task = Task.Run(()=>ReadExcel());
            //ReadExcelData(filename);
            //Workbook workbook = new Workbook(FilePath);
            //Worksheet worksheet = workbook.Worksheets[0];
            // Получить количество строк и столбцов
            //RowCount = worksheet.Cells.MaxDataRow;
            //ColumnCount = worksheet.Cells.MaxDataColumn;
            //DataTable = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, true);
            //dataGrid.ItemsSource = dataTable.DefaultView;


        }

        public void ReadExcel()
        {
            Workbook workbook = new Workbook(FilePath);
            Worksheet worksheet = workbook.Worksheets[0];
            // Получить количество строк и столбцов
            RowCount = worksheet.Cells.MaxDataRow;
            ColumnCount = worksheet.Cells.MaxDataColumn;
            Databases = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, true); // Записал на прямую и сделал попытку ленивой загрузки
            //Databases = dataTable; // DataTable dataTable
        }



    }
}
