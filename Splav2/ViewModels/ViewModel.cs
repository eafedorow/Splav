using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Navigation;

namespace ProjectForModel.ViewModels
{
    /// <summary>
    /// Перенести page в модель и поместить их в массив
    /// Проверку делать на равенство первому или последнему элементу списка 
    /// По идеи все должно быть cool
    /// </summary>
    class ViewModel: BaseViewModel { 
        
        private List<Page> _pageList = new List<Page>();
        private Page _currentPage;
        public ICommand NextPage { get; }
        public ICommand PreviousPage { get; }
        public Page CurrentPage {
            get
            {
                return _currentPage;
            }
            set
            {
                if (_currentPage == value) return;
                SetProperty(ref _currentPage, value);
                
            }
        }

        public ViewModel()
        {
            _pageList.Add(new Views.Page1());
            _pageList.Add(new Views.Page2());
            CurrentPage = _pageList[0];
            NextPage = new RelayCommand(Next, CanNext);
            PreviousPage = new RelayCommand(Previous, CanPrevious);
        }
        private void Next() {
            var indexOf = _pageList.IndexOf(CurrentPage);
            var b = (_pageList.IndexOf(CurrentPage) != 0) ? true : false;
            CurrentPage = _pageList[indexOf == _pageList.Count - 1 ? 0 : indexOf + 1];
            
        }
        private bool CanNext() => (_pageList.IndexOf(CurrentPage) != _pageList.Count - 1) ? true : false; // проверка на последний элемент
        private void Previous() {
            var indexOf = _pageList.IndexOf(CurrentPage);
            var b = (_pageList.IndexOf(CurrentPage) != 0) ? true : false;
            CurrentPage = _pageList[indexOf == 0 ? 0 : indexOf - 1];
        }

        private bool CanPrevious() => (_pageList.IndexOf(CurrentPage) != 0) ? true : true; 

    }
}
