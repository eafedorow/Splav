using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MVVM;
using Splav2.Abstractions;

namespace Splav2.Models
{
    internal class CurrentPageModel: BindableBase, ICurrentPage
    {
        private Page _currentPage;
        public Page CurrentPage 
        {
            get => _currentPage; 
            set => SetProperty(ref _currentPage, value);
        }
    }
}
