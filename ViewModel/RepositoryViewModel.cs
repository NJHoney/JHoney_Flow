using JHoney_Flow.Language;
using JHoney_Flow.Util.Loading;
using JHoney_Flow.View.RepoLv2.DataListRootPath;
using JHoney_Flow.ViewModel.RepoLv2;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JHoney_Flow.ViewModel
{
    class RepositoryViewModel:BindableBase
    {

        #region 프로퍼티
        GenericIdentity identity = new GenericIdentity("pythonId");

        public LoadingViewModel _loadingViewModel;
        public Visibility Visibility
        {
            get { return _visibility; }
            set { _visibility = value; RaisePropertyChanged("Visibility"); }
        }
        private Visibility _visibility = Visibility.Visible;


        
        #region ---［ String ］---------------------------------------------------------------------

        public string Tab_Data
        {
            get { return _tab_Data; }
            set { _tab_Data = value; RaisePropertyChanged("Tab_Data"); }
        }
        private string _tab_Data;

        public string Tab_Train
        {
            get { return _tab_Train; }
            set { _tab_Train = value; RaisePropertyChanged("Tab_Train"); }
        }
        private string _tab_Train;

        public string Tab_Test
        {
            get { return _tab_Test; }
            set { _tab_Test = value; RaisePropertyChanged("Tab_Test"); }
        }
        private string _tab_Test;

        public string SubMenu_Exit
        {
            get { return _subMenu_Exit; }
            set { _subMenu_Exit = value; RaisePropertyChanged("SubMenu_Exit"); }
        }
        private string _subMenu_Exit;

        public string Menu_Language
        {
            get { return _menu_Language; }
            set { _menu_Language = value; RaisePropertyChanged("Menu_Language"); }
        }
        private string _menu_Language;

        public string SubMenu_Lang_En
        {
            get { return _subMenu_Lang_En; }
            set { _subMenu_Lang_En = value; RaisePropertyChanged("SubMenu_Lang_En"); }
        }
        private string _subMenu_Lang_En;

        public string SubMenu_Lang_Ko
        {
            get { return _subMenu_Lang_Ko; }
            set { _subMenu_Lang_Ko = value; RaisePropertyChanged("SubMenu_Lang_Ko"); }
        }
        private string _subMenu_Lang_Ko;
        #endregion ---------------------------------------------------------------------------------

        #region ---［ ViewModel ］---------------------------------------------------------------------


        public DataListViewModel DataListViewModel
        {
            get { return _dataListViewModel; }
            set { _dataListViewModel = value; RaisePropertyChanged("DataListViewModel"); }
        }
        private DataListViewModel _dataListViewModel = new DataListViewModel();

        public TrainViewModel TrainViewModel
        {
            get { return _trainViewModel; }
            set { _trainViewModel = value; RaisePropertyChanged("TrainViewModel"); }
        }
        private TrainViewModel _trainViewModel = new TrainViewModel();

        public TestViewModel TestViewModel
        {
            get { return _testViewModel; }
            set { _testViewModel = value; RaisePropertyChanged("TestViewModel"); }
        }
        private TestViewModel _testViewModel = new TestViewModel();

        public ModelListViewModel ModelListViewModel
        {
            get { return _modelListViewModel; }
            set { _modelListViewModel = value; RaisePropertyChanged("ModelListViewModel"); }
        }
        private ModelListViewModel _modelListViewModel = new ModelListViewModel();

        public DataListRootPathViewModel DataListRootPathViewModel
        {
            get { return _dataListRootPathViewModel; }
            set { _dataListRootPathViewModel = value; RaisePropertyChanged("DataListRootPathViewModel"); }
        }
        private DataListRootPathViewModel _dataListRootPathViewModel = new DataListRootPathViewModel();
        #endregion ---------------------------------------------------------------------------------


        #endregion
        #region 커맨드
        //public RelayCommand<object> MyCommand { get; private set; }
        #endregion

        #region 초기화
        public RepositoryViewModel()
        {
            InitData();
            InitCommand();
            InitEvent();
        }

        void InitData()
        {
            TrainViewModel.ModelListViewModel = ModelListViewModel;
            TrainViewModel.DataListRootPathViewModel = DataListRootPathViewModel;
            TestViewModel.ModelListViewModel = ModelListViewModel;
            TestViewModel.DataListRootPathViewModel = DataListRootPathViewModel;
            DataListRootPathViewModel.ModelListViewModel = ModelListViewModel;

            TrainViewModel._identity = identity;
            TestViewModel._identity = identity;
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
        public void SetTextLanguage()
        {
            Tab_Data = Str.Data;
            Tab_Train = Str.Train;
            Tab_Test = Str.Test;
            //SubMenu_SaveRepository = Str.Save_Repository;
            //Menu_Language = Str.Language;
            //SubMenu_Lang_En = Str.English;
            //SubMenu_Lang_Ko = Str.Korean;
        }
        /*
        private void OnMyCommand(object param)
            {

            }
            */
        #endregion

    }
}
