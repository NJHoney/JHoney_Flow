using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JHoney_Flow.Util.Loading
{
    class LoadingViewModel:BindableBase
    {
        #region 프로퍼티
        public Visibility Visibility
        {
            get { return _visibility; }
            set { _visibility = value; RaisePropertyChanged("Visibility"); }
        }
        private Visibility _visibility = Visibility.Collapsed;

        public int ProgressMin
        {
            get { return _progressMin; }
            set { _progressMin = value; RaisePropertyChanged("ProgressMin"); }
        }
        private int _progressMin = 0;
        public int ProgressMax
        {
            get { return _progressMax; }
            set { _progressMax = value; RaisePropertyChanged("ProgressMax"); }
        }
        private int _progressMax = 1;

        public int ProgressCurrent
        {
            get { return _progressCurrent; }
            set { _progressCurrent = value; RaisePropertyChanged("ProgressCurrent"); }
        }
        private int _progressCurrent = 1;
        public string LoadingText
        {
            get { return _loadingText; }
            set { _loadingText = value; RaisePropertyChanged("LoadingText"); }
        }
        private string _loadingText = "Now Loading";
        #endregion
        #region 커맨드
        //public RelayCommand<object> MyCommand { get; private set; }
        #endregion

        #region 초기화
        public LoadingViewModel()
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
        public void SetMax(int Progress_Max)
        {
            ProgressMax = Progress_Max;
        }

        public void SetCurrent(int Value)
        {
            ProgressCurrent = Value;
        }
        /*
        private void OnMyCommand(object param)
            {

            }
            */
        #endregion
    }
}
