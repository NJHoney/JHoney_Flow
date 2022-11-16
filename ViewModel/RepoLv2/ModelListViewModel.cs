using JHoney_Flow.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JHoney_Flow.ViewModel.RepoLv2
{
    class ModelListViewModel:BindableBase
    {

        #region 프로퍼티
        Thread AddFileThread;
        #region ---［ Pagging ］---------------------------------------------------------------------
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; RaisePropertyChanged("CurrentPage"); }
        }
        private int _currentPage = 1;

        public int MaxPage
        {
            get { return _maxPage; }
            set { _maxPage = value; RaisePropertyChanged("MaxPage"); }
        }
        private int _maxPage = 1;

        public int PagingSize
        {
            get { return _pagingSize; }
            set { _pagingSize = value; RaisePropertyChanged("PagingSize"); }
        }
        private int _pagingSize = 30;

        public ObservableCollection<PagingButtonModel> SelectNumPageList
        {
            get { return _selectNumPageList; }
            set { _selectNumPageList = value; RaisePropertyChanged("SelectNumPageList"); }
        }
        private ObservableCollection<PagingButtonModel> _selectNumPageList = new ObservableCollection<PagingButtonModel>();
        #endregion ---------------------------------------------------------------------------------

        public ObservableCollection<FileIOModel> LoadModelListCurrent
        {
            get { return _loadModelListCurrent; }
            set { _loadModelListCurrent = value; RaisePropertyChanged("LoadModelListCurrent"); }
        }
        private ObservableCollection<FileIOModel> _loadModelListCurrent = new ObservableCollection<FileIOModel>();

        public ObservableCollection<FileIOModel> LoadModelListAll
        {
            get { return _loadModelListAll; }
            set { _loadModelListAll = value; RaisePropertyChanged("LoadModelListAll"); }
        }
        private ObservableCollection<FileIOModel> _loadModelListAll = new ObservableCollection<FileIOModel>();

        public ListBox ImageListBox
        {
            get { return _imageListBox; }
            set { _imageListBox = value; RaisePropertyChanged("ImageListBox"); }
        }
        private ListBox _imageListBox = new ListBox();

        public string SelectedModelPath
        {
            get { return _selectedModelPath; }
            set { _selectedModelPath = value; RaisePropertyChanged("SelectedModelPath"); }
        }
        private string _selectedModelPath = "";
        #region ---［ Menu ］---------------------------------------------------------------------

        public int FileOpenSelectedIndex
        {
            get { return _fileOpenSelectedIndex; }
            set { _fileOpenSelectedIndex = value; RaisePropertyChanged("FileOpenSelectedIndex"); }
        }
        private int _fileOpenSelectedIndex = 0;


        public ObservableCollection<string> FileOpenMenuList
        {
            get { return _fileOpenMenuList; }
            set { _fileOpenMenuList = value; RaisePropertyChanged("FileOpenMenuList"); }
        }
        private ObservableCollection<string> _fileOpenMenuList = new ObservableCollection<string>();

        public int FileDelSelectedIndex
        {
            get { return _fileDelSelectedIndex; }
            set { _fileDelSelectedIndex = value; RaisePropertyChanged("FileDelSelectedIndex"); }
        }
        private int _fileDelSelectedIndex = 0;
        public ObservableCollection<string> FileDelMenuList
        {
            get { return _fileDelMenuList; }
            set { _fileDelMenuList = value; RaisePropertyChanged("FileDelMenuList"); }
        }
        private ObservableCollection<string> _fileDelMenuList = new ObservableCollection<string>();
        #endregion ---------------------------------------------------------------------------------

        #endregion
        #region 커맨드
        //public RelayCommand<object> MyCommand { get; private set; }
        public DelegateCommand<object> CommandOpenMenu { get; private set; }
        public DelegateCommand<object> CommandDelMenu { get; private set; }
        public DelegateCommand<object> CommandSetPage { get; private set; }
        public DelegateCommand<object> CommandLoaded { get; private set; }
        public DelegateCommand<object> CommandSelectImage { get; private set; }
        #endregion

        #region 초기화
        public ModelListViewModel()
        {
            InitData();
            InitCommand();
            InitEvent();
        }

        void InitData()
        {
            FileOpenMenuList.Add("Open File ");
            FileOpenMenuList.Add("Open Folder ");
            FileOpenMenuList.Add("Open Folder-All");

            FileDelMenuList.Add("Del Selected");
            FileDelMenuList.Add("Del All");

            SelectNumPageList.Add(new PagingButtonModel() { PageNum = "1", IsEnabled = false });
        }

        void InitCommand()
        {
            
            CommandOpenMenu = new DelegateCommand<object>((param) => OnCommandOpenMenu(param));
            CommandDelMenu = new DelegateCommand<object>((param) => OnCommandDelMenu(param));
            CommandSetPage = new DelegateCommand<object>((param) => OnCommandSetPage(param));
            CommandLoaded = new DelegateCommand<object>((param) => OnCommandLoaded(param));

            CommandSelectImage = new DelegateCommand<object>((param) => OnCommandSelectImage(param));
            
        }

        void InitEvent()
        {

        }
        #endregion

        #region 이벤트
        private void OnCommandOpenMenu(object param)
        {
            
            DirectoryInfo di;
            WPFFolderBrowser.WPFFolderBrowserDialog fbd;

            switch (FileOpenSelectedIndex)
            {
                case 0:
                    Microsoft.Win32.OpenFileDialog Dialog = new Microsoft.Win32.OpenFileDialog();
                    Dialog.DefaultExt = ".txt";
                    Dialog.Filter = "Model Files (*.model),|*.model;|All Files (*.*)|*.*";
                    Dialog.Multiselect = true;
                    bool? result = Dialog.ShowDialog();

                    if (result == true)
                    {
                        LoadModelListCurrent.Clear();
                        SelectNumPageList.Clear();
                        AddFileThread = new Thread(() => AddFileThreadMethod(Dialog.FileNames));
                        AddFileThread.Start();
                        AddFileThread.Join();
                        PageListExtract("");
                        ListNumRefresh();
                    }
                    break;
                case 1:
                    fbd = new WPFFolderBrowser.WPFFolderBrowserDialog();
                    bool? resfolder = fbd.ShowDialog();

                    if (resfolder == true)
                    {
                        LoadModelListCurrent.Clear();
                        SelectNumPageList.Clear();
                        di = new DirectoryInfo(fbd.FileName);
                        AddFileThread = new Thread(() => AddFileThreadMethod(di.GetFiles()));
                        AddFileThread.Start();
                        AddFileThread.Join();
                        PageListExtract("");
                        ListNumRefresh();
                    }
                    break;
                case 2:
                    fbd = new WPFFolderBrowser.WPFFolderBrowserDialog();
                    bool? resfolder2 = fbd.ShowDialog();

                    if (resfolder2 == true)
                    {
                        LoadModelListCurrent.Clear();
                        SelectNumPageList.Clear();
                        di = new DirectoryInfo(fbd.FileName);
                        AddFileThread = new Thread(() => AddFileThreadMethod(di.GetFiles("*.*", SearchOption.AllDirectories)));
                        AddFileThread.Start();
                        AddFileThread.Join();
                        PageListExtract("");
                        ListNumRefresh();
                    }
                    break;
            }
        }
        private void OnCommandDelMenu(object param)
        {
            switch (FileDelSelectedIndex)
            {
                case 0:
                    if (ImageListBox.SelectedItems.Count < 1)
                    {
                        if (LoadModelListCurrent.Count < 1)
                        {
                            return;
                        }
                        LoadModelListAll.RemoveAt(0);
                        LoadModelListCurrent.RemoveAt(0);
                    }
                    else
                    {
                        int LoofCount = ImageListBox.SelectedItems.Count;
                        for (int iLoofCount = 0; iLoofCount < LoofCount; iLoofCount++)
                        {
                            LoadModelListAll.Remove((FileIOModel)ImageListBox.SelectedItems[0]);
                            LoadModelListCurrent.Remove((FileIOModel)ImageListBox.SelectedItems[0]);
                        }
                    }
                    break;
                case 1:
                    LoadModelListCurrent.Clear();
                    LoadModelListAll.Clear();
                    CurrentPage = 1;
                    MaxPage = 1;
                    PageListExtract("");
                    ListNumRefresh();
                    break;
            }
        }
        public void PageListExtract(string findPageCommand)
        {
            if (findPageCommand == "First")
            {
                CurrentPage = 1;
                //var k = LoadModelListAll.Skip((CurrentPage - 1) * PagingSize).Take(PagingSize);
                LoadModelListCurrent = new ObservableCollection<FileIOModel>(LoadModelListAll.Skip((CurrentPage - 1) * PagingSize).Take(PagingSize));
                return;
            }

            if (findPageCommand == "Last")
            {
                CurrentPage = MaxPage;
                LoadModelListCurrent = new ObservableCollection<FileIOModel>(LoadModelListAll.Skip((CurrentPage - 1) * PagingSize).Take(PagingSize));
                return;
            }

            if (findPageCommand == "Next")
            {
                if (LoadModelListAll.Count < (CurrentPage) * PagingSize)
                {

                    return;
                }
            }
            else if (findPageCommand == "Back")
            {
                if (CurrentPage <= 1)
                {

                    return;
                }
            }

            if (findPageCommand == "Next")
            {
                CurrentPage++;
            }
            else if (findPageCommand == "Back")
            {
                CurrentPage--;
            }
            if (LoadModelListCurrent.Count > 0)
            {
                LoadModelListCurrent.Clear();
            }

            LoadModelListCurrent = new ObservableCollection<FileIOModel>(LoadModelListAll.Skip((CurrentPage - 1) * PagingSize).Take(PagingSize));
        }

        private void OnCommandSetPage(object param)
        {
            if (param.ToString() == "BackList")
            {
                if (CurrentPage - 5 <= 1)
                {
                    CurrentPage = 1;

                }
                else if (CurrentPage - 5 > 1)
                {
                    CurrentPage = CurrentPage - 5;
                }
            }

            if (param.ToString() == "NextList")
            {
                if (CurrentPage + 5 >= MaxPage)
                {
                    CurrentPage = MaxPage;

                }
                else if (CurrentPage + 5 < MaxPage)
                {
                    CurrentPage = CurrentPage + 5;
                }
            }

            int TargetPage = 0;
            if (int.TryParse(param.ToString(), out TargetPage))
            {
                if (TargetPage == 0)
                {
                    return;
                }
                CurrentPage = TargetPage;
            }
            PageListExtract(param.ToString());
            ListNumRefresh();
        }
        public void ListNumRefresh()
        {
            SelectNumPageList.Clear();
            int TempPage = (((CurrentPage - 1) / 5) * 5) + 1;
            for (int iLoofCount = 0; iLoofCount < 5; iLoofCount++)
            {
                if ((TempPage + iLoofCount) <= MaxPage)
                {
                    SelectNumPageList.Add(new PagingButtonModel() { PageNum = (TempPage + iLoofCount).ToString(), IsEnabled = true });
                }
            }
            var a = from b in SelectNumPageList
                    where b.PageNum == (CurrentPage).ToString()
                    select b;
            if (a.Count() > 0)
            {
                int TempCount = SelectNumPageList.IndexOf(a.First());
                SelectNumPageList[TempCount].IsEnabled = false;
            }
        }
        public void AddFileThreadMethod(string[] files)
        {
            for (int iLoofCount = 0; iLoofCount < files.Count(); iLoofCount++)
            {
                FileAttributes attr = File.GetAttributes(files[iLoofCount]);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    for (int iLoopCount = 0; iLoopCount < files.Count(); iLoopCount++)
                    {
                        DirectoryInfo di = new DirectoryInfo(files[iLoofCount]);
                        AddFileThreadMethod(di.GetFiles("*", SearchOption.AllDirectories));
                    }
                    return;
                }
                else
                {
                    FileIOModel TempFileIO = new FileIOModel(files[iLoofCount]);
                    if (TempFileIO.FileName_Extension.ToLower() == "model")
                    {
                        LoadModelListAll.Add(new FileIOModel(files[iLoofCount]));
                    }
                }
            }

            MaxPage = (LoadModelListAll.Count() / PagingSize) + 1;
            PageListExtract("");
        }
        public void AddFileThreadMethod(FileInfo[] files)
        {
            for (int iLoofCount = 0; iLoofCount < files.Count(); iLoofCount++)
            {
                if (files[iLoofCount].Extension != ".db")
                {
                    FileIOModel TempFileIO = new FileIOModel(files[iLoofCount].FullName);
                    if (TempFileIO.FileName_Extension.ToLower() == "h5")
                    {
                        LoadModelListAll.Add(new FileIOModel(files[iLoofCount].FullName));
                    }
                    //LoadModelListAll.Add(new FileIOModel(files[iLoofCount].FullName));
                }
            }

            MaxPage = (LoadModelListAll.Count() / PagingSize) + 1;
            PageListExtract("");
        }

        public void AddFileThreadMethod(string folder)
        {
            DirectoryInfo di = new DirectoryInfo(folder);
            
            AddFileThreadMethod(di.GetFiles("*", SearchOption.AllDirectories));
        }
        private void OnCommandLoaded(object param)
        {
            if (param.GetType().Name == "ListBox")
            {
                ImageListBox = param as ListBox;
                ImageListBox.SelectionMode = SelectionMode.Extended;
            }
        }


        private void OnCommandSelectImage(object param)
        {
            if(param!=null)
            {
                if(param.GetType().Name!="ListBox")
                {
                    return;
                }
                ListBox templistbox = param as ListBox;
                FileIOModel tempPath = templistbox.SelectedItem as FileIOModel;
                if(tempPath==null)
                {
                    SelectedModelPath = "";
                    return;
                }
                SelectedModelPath = tempPath.FileName_Full;
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
