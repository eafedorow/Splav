using Aspose.Cells;
using ExcelDataReader;
using Microsoft.Win32;
using MVVM;
using MVVM.Commands;
using OfficeOpenXml;
using Splav2.Models;
using Splav2.Views;
using System.Data;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;


namespace Splav2.ViewModels
{
    internal class ViewModelOpenDB : BindableBase
    {


        public ViewModelOpenDB()
        { }

        private int _rowCount;
        private int _columnCount;

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



        private DataView ReadExcelData(string filePath)
        {
            Workbook wb = new Workbook(filePath);
            WorksheetCollection collection = wb.Worksheets;

            for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
            { 
                Worksheet worksheet = collection[worksheetIndex];

            }
            /*// Читаем, получаем DataView и работаем с ним как обычно.
            DataSet dataSet = edr.AsDataSet(conf);
            DataView dtView = dataSet.Tables[0].AsDataView();

            // После завершения чтения освобождаем ресурсы.
            edr.Close();
            return dtView;*/
        }

        private RelayCommand openExcel;
        public ICommand OpenExcel => openExcel ??= new RelayCommand(PerformOpenExcel);

        private void PerformOpenExcel()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "EXCEL Files (*.xlsx)|*.xlsx|EXCEL Files 2003 (*.xls)|*.xls|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != true)
                return;
            var model = ProjectModel.GetProjectModel();
            var filename = openFileDialog.FileName;
            model.DataBasepath = filename;
            ReadExcelData(filename);
        }


        /*private void PerformOpenExcel()
          {
              OpenFileDialog openFileDialog = new OpenFileDialog();
              openFileDialog.Filter = "EXCEL Files (*.xlsx)|*.xlsx|EXCEL Files 2003 (*.xls)|*.xls|All files (*.*)|*.*";
              if (openFileDialog.ShowDialog() != true)
                  return;

               = ReadExcelData(openFileDialog.FileName);

              /*if (openFileDialog.ShowDialog() == true)
              {
                  _filePath = openFileDialog.FileName;
                  _excelData = ReadExcelData(openFileDialog.FileName);
                  _rowCount = _excelData.Rows.Count;
                  _columnCount = _excelData.Columns.Count;
              }//
          }*/

    }
}
