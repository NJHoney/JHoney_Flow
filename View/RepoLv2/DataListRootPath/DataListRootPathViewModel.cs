using JHoney_Flow.ViewModel.RepoLv2;
using OpenCvSharp;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHoney_Flow.View.RepoLv2.DataListRootPath
{
    class DataListRootPathViewModel:BindableBase
    {

        #region 프로퍼티
        public string DataSetRootPath
        {
            get { return _dataSetRootPath; }
            set { _dataSetRootPath = value; RaisePropertyChanged("DataSetRootPath"); }
        }
        private string _dataSetRootPath = "";

        public bool IsTraining
        {
            get { return _isTraining; }
            set { _isTraining = value; RaisePropertyChanged("IsTraining"); }
        }
        private bool _isTraining;

        public int ImageWidth
        {
            get { return _imageWidth; }
            set { _imageWidth = value; RaisePropertyChanged("ImageWidth"); }
        }
        private int _imageWidth = 0;

        public int ImageHeight
        {
            get { return _imageHeight; }
            set { _imageHeight = value; RaisePropertyChanged("ImageHeight"); }
        }
        private int _imageHeight = 0;

        public int ImageChannel
        {
            get { return _imageChannel; }
            set { _imageChannel = value; RaisePropertyChanged("ImageChannel"); }
        }
        private int _imageChannel = 0;

        public ModelListViewModel ModelListViewModel
        {
            get { return _modelListViewModel; }
            set { _modelListViewModel = value; RaisePropertyChanged("ModelListViewModel"); }
        }
        private ModelListViewModel _modelListViewModel;
        /*
        public int MyVariable
          {
              get { return _myVariable; }
              set { _myVariable = value; RaisePropertyChanged("MyVariable"); }
          }
          private int _myVariable;
          */
        #endregion
        #region 커맨드
        public DelegateCommand<object> OpenRootPathCommand { get; private set; }
        //public RelayCommand<object> MyCommand { get; private set; }
        #endregion

        #region 초기화
        public DataListRootPathViewModel()
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
            OpenRootPathCommand = new DelegateCommand<object>((param) => OnOpenRootPathCommand(param));
            //MyCommand = new RelayCommand<object>((param) => OnMyCommand(param));
        }

        void InitEvent()
        {

        }
        #endregion

        #region 이벤트
        private void OnOpenRootPathCommand(object param)
        {
            WPFFolderBrowser.WPFFolderBrowserDialog fbd = new WPFFolderBrowser.WPFFolderBrowserDialog();
            bool? resfolder = fbd.ShowDialog();

            if (resfolder == true)
            {
                DataSetRootPath = fbd.FileName;
                DirectoryInfo di = new DirectoryInfo(DataSetRootPath + "\\Train");

                Mat TempMat = new Mat(di.GetFiles("*.*", SearchOption.AllDirectories)[0].FullName, ImreadModes.Unchanged);
                ImageWidth = TempMat.Width;
                ImageHeight = TempMat.Height;
                ImageChannel = TempMat.Channels();
                TempMat.Dispose();



                di = new DirectoryInfo(DataSetRootPath + "\\Models\\");
                if (di.Exists)
                {
                    ModelListViewModel.LoadModelListCurrent.Clear();
                    ModelListViewModel.LoadModelListAll.Clear();

                    ModelListViewModel.AddFileThreadMethod(di.GetFiles("*.h5"));
                    ModelListViewModel.PageListExtract("");
                    ModelListViewModel.ListNumRefresh();
                }

            }
        }
        /*
        private void OnMyCommand(object param)
            {

            }
            */
        #endregion

    }
}
