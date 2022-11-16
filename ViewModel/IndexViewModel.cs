using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JHoney_Flow.ViewModel
{
    class IndexViewModel:BindableBase
    {
        #region 프로퍼티

        public Visibility Visibility
        {
            get { return _visibility; }
            set { _visibility = value; RaisePropertyChanged("Visibility"); }
        }
        private Visibility _visibility = Visibility.Collapsed;

        #endregion
        #region 커맨드
        //public RelayCommand<object> MyCommand { get; private set; }
        #endregion

        #region 초기화
        public IndexViewModel()
        {
            InitData();
            InitCommand();
            InitEvent();
        }

        void InitData()
        {

        }

        void InitCommand()
        {
            //MyCommand = new RelayCommand<object>((param) => OnMyCommand(param));
        }

        void InitEvent()
        {

        }
        #endregion

        #region 이벤트
        /*
        private void OnMyCommand(object param)
            {

            }
            */
        #endregion

    }
}
