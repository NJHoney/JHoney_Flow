using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHoney_Flow.Model
{
    class PagingButtonModel:BindableBase
    {
        public string PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value; RaisePropertyChanged("PageNum"); }
        }
        private string _pageNum = "";

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; RaisePropertyChanged("IsEnabled"); }
        }
        private bool _isEnabled = true;
    }
}
