using JHoney_Flow.Language;
using JHoney_Flow.Util.Loading;
using JHoney_Flow.ViewModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JHoney_Flow
{
    class MainWindowViewModel:BindableBase
    {

        #region 프로퍼티


        #region ---［ String ］---------------------------------------------------------------------

        public string Menu_File
        {
            get { return _menu_File; }
            set { _menu_File = value; RaisePropertyChanged("Menu_File"); }
        }
        private string _menu_File;

        public string SubMenu_NewRepository
        {
            get { return _subMenu_NewRepository; }
            set { _subMenu_NewRepository = value; RaisePropertyChanged("SubMenu_NewRepository"); }
        }
        private string _subMenu_NewRepository;

        public string SubMenu_SaveRepository
        {
            get { return _subMenu_SaveRepository; }
            set { _subMenu_SaveRepository = value; RaisePropertyChanged("SubMenu_SaveRepository"); }
        }
        private string _subMenu_SaveRepository;

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

        public IndexViewModel IndexViewModel
        {
            get { return _indexViewModel; }
            set { _indexViewModel = value; RaisePropertyChanged("IndexViewModel"); }
        }
        private IndexViewModel _indexViewModel = new IndexViewModel();

        public RepositoryViewModel RepositoryViewModel
        {
            get { return _repositoryViewModel; }
            set { _repositoryViewModel = value; RaisePropertyChanged("RepositoryViewModel"); }
        }
        private RepositoryViewModel _repositoryViewModel = new RepositoryViewModel();

        public LoadingViewModel LoadingViewModel
        {
            get { return _loadingViewModel; }
            set { _loadingViewModel = value; RaisePropertyChanged("LoadingViewModel"); }
        }
        private LoadingViewModel _loadingViewModel = new LoadingViewModel();

        #endregion ---------------------------------------------------------------------------------


        #endregion
        #region 커맨드
        public DelegateCommand<object> LanguageChange { get; private set; }
        #endregion

        #region 초기화
        public MainWindowViewModel()
        {
            InitData();
            InitCommand();
            InitEvent();
        }

        void InitData()
        {
            RepositoryViewModel._loadingViewModel = LoadingViewModel;
            RepositoryViewModel.DataListViewModel._loadingViewModel = LoadingViewModel;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            SetTextLanguage();
        }

        void InitCommand()
        {
            LanguageChange = new DelegateCommand<object>((param) => OnLanguageChange(param));
        }

        void InitEvent()
        {

        }
        #endregion

        #region 이벤트

        private void OnLanguageChange(object param)
        {
            if(param.ToString() =="kr")
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ko-KR");
                SetTextLanguage();
                
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                SetTextLanguage();
            }
        }

        void SetTextLanguage()
        {
            Menu_File = Str.File;
            SubMenu_Exit = Str.Exit;
            SubMenu_NewRepository = Str.New_Repository;
            SubMenu_SaveRepository = Str.Save_Repository;
            Menu_Language = Str.Language;
            SubMenu_Lang_En = Str.English;
            SubMenu_Lang_Ko = Str.Korean;

            RepositoryViewModel.SetTextLanguage();
            RepositoryViewModel.DataListViewModel.SetTextLanguage();
        }
        #endregion

    }
}
