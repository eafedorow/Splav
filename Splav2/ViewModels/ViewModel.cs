using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MVVM;
using MVVM.Extensions;
using Splav2.Abstractions;
using Splav2.Models;

namespace Splav2.ViewModel
{
    /// <summary>
    /// Перенести page в модель и поместить их в массив
    /// Проверку делать на равенство первому или последнему элементу списка 
    /// По идеи все должно быть cool
    /// </summary>
    internal class ViewModel: BindableBase
    { 
        /// <summary>
        /// Заменил CurrentPage на Win чтобы не путаться! 
        /// </summary>
        private List<Page> _pageList = new List<Page>();
        public ICurrentPage Win { get; } 
        public ICommand NextPage { get; }   
        public ICommand PreviousPage { get; }
        

        public ViewModel()
        {
            _pageList.Add(new Views.Page1());
            _pageList.Add(new Views.Page2());
            _pageList.Add(new Views.Page3());
            Win = new CurrentPageModel();
            Win.CurrentPage = _pageList[0];
            NextPage = new RelayCommand(Next, CanNext);
            PreviousPage = new RelayCommand(Previous, CanPrevious);
            Win.PropertyChanged += Win_PropertyChanged;
        }

        private void Next() {
            if (!CanNext()) return;
            Win.CurrentPage = _pageList[_pageList.IndexOf(Win.CurrentPage) + 1];
        }
        private bool CanNext() => 
            _pageList.IndexOf(Win.CurrentPage) != (_pageList.Count - 1); // проверка на последний элемент
        private void Previous() {
            if (!CanPrevious()) return;
            Win.CurrentPage = _pageList[_pageList.IndexOf(Win.CurrentPage) - 1];
        }
        private bool CanPrevious() => 
            _pageList.IndexOf(Win.CurrentPage) != 0;
        private void Win_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NextPage.RaiseCanExecuteChanged();
            PreviousPage.RaiseCanExecuteChanged();
        }

    }
}
