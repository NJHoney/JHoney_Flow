using JHoney_Flow.Keras_Model;
using JHoney_Flow.Language;
using JHoney_Flow.Model;
using JHoney_Flow.Util.Loading;
using JHoney_Flow.View.RepoLv2.RatioSetDialog;
using MahApps.Metro.Controls.Dialogs;
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
using System.Windows;
using System.Windows.Controls;

namespace JHoney_Flow.ViewModel.RepoLv2
{
    class DataListViewModel:BindableBase
    {
        #region 프로퍼티
        Thread ImageLoadThread;
        ManualResetEvent doneEvent = new ManualResetEvent(false);

        private DialogCoordinator _dialogCoordinator;
        public LoadingViewModel _loadingViewModel;

        public RatioSetDialogViewModel RatioSetDialogViewModel
        {
            get { return _ratioSetDialogViewModel; }
            set { _ratioSetDialogViewModel = value; RaisePropertyChanged("RatioSetDialogViewModel"); }
        }
        private RatioSetDialogViewModel _ratioSetDialogViewModel = new RatioSetDialogViewModel();
        #region ---［ String ］---------------------------------------------------------------------
        public string DataColumn_CheckSubFolder
        {
            get { return _dataColumn_CheckSubFolder; }
            set { _dataColumn_CheckSubFolder = value; RaisePropertyChanged("DataColumn_CheckSubFolder"); }
        }
        private string _dataColumn_CheckSubFolder;


        public string DataColumn_ClassName
        {
            get { return _dataColumn_ClassName; }
            set { _dataColumn_ClassName = value; RaisePropertyChanged("DataColumn_ClassName"); }
        }
        private string _dataColumn_ClassName;

        public string DataColumn_DataPath
        {
            get { return _dataColumn_DataPath; }
            set { _dataColumn_DataPath = value; RaisePropertyChanged("DataColumn_DataPath"); }
        }
        private string _dataColumn_DataPath;

        public string DataColumn_DataPathSet
        {
            get { return _dataColumn_DataPathSet; }
            set { _dataColumn_DataPathSet = value; RaisePropertyChanged("DataColumn_DataPathSet"); }
        }
        private string _dataColumn_DataPathSet;

        public string DataColumn_TotalImageCount
        {
            get { return _dataColumn_TotalImageCount; }
            set { _dataColumn_TotalImageCount = value; RaisePropertyChanged("DataColumn_TotalImageCount"); }
        }
        private string _dataColumn_TotalImageCount;

        public string DataColumn_Train
        {
            get { return _dataColumn_Train; }
            set { _dataColumn_Train = value; RaisePropertyChanged("DataColumn_Train"); }
        }
        private string _dataColumn_Train;

        public string DataColumn_Validation
        {
            get { return _dataColumn_Validation; }
            set { _dataColumn_Validation = value; RaisePropertyChanged("DataColumn_Validation"); }
        }
        private string _dataColumn_Validation;

        public string DataColumn_Test
        {
            get { return _dataColumn_Test; }
            set { _dataColumn_Test = value; RaisePropertyChanged("DataColumn_Test"); }
        }
        private string _dataColumn_Test;

        public string DataColumn_SubClassType
        {
            get { return _dataColumn_SubClassType; }
            set { _dataColumn_SubClassType = value; RaisePropertyChanged("DataColumn_SubClassType"); }
        }
        private string _dataColumn_SubClassType;

        public string Setting_SetAllRatio
        {
            get { return _setting_SetAllRatio; }
            set { _setting_SetAllRatio = value; RaisePropertyChanged("Setting_SetAllRatio"); }
        }
        private string _setting_SetAllRatio;

        public string Setting_SetRandomDistribute
        {
            get { return _setting_SetRandomDistribute; }
            set { _setting_SetRandomDistribute = value; RaisePropertyChanged("Setting_SetRandomDistribute"); }
        }
        private string _setting_SetRandomDistribute;
        #endregion ---------------------------------------------------------------------------------
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; RaisePropertyChanged("IsEnabled"); }
        }
        private bool _isEnabled = true;
        public bool IsEnabledMainGrid
        {
            get { return _isEnabledMainGrid; }
            set { _isEnabledMainGrid = value; RaisePropertyChanged("IsEnabledMainGrid"); }
        }
        private bool _isEnabledMainGrid = true;

        public bool IsCheckedRandomDistribute
        {
            get { return _isCheckedRandomDistribute; }
            set { _isCheckedRandomDistribute = value; RaisePropertyChanged("IsCheckedRandomDistribute"); }
        }
        private bool _isCheckedRandomDistribute = false;

        public int TotalImageCount
        {
            get { return _totalImageCount; }
            set { _totalImageCount = value; RaisePropertyChanged("TotalImageCount"); }
        }
        private int _totalImageCount = 0;

        public int TotalTrainCount
        {
            get { return _totalTrainCount; }
            set { _totalTrainCount = value; RaisePropertyChanged("TotalTrainCount"); }
        }
        private int _totalTrainCount = 0;

        public int TotalValCount
        {
            get { return _totalValCount; }
            set { _totalValCount = value; RaisePropertyChanged("TotalValCount"); }
        }
        private int _totalValCount = 0;

        public int TotalTestCount
        {
            get { return _totalTestCount; }
            set { _totalTestCount = value; RaisePropertyChanged("TotalTestCount"); }
        }
        private int _totalTestCount = 0;

        public ObservableCollection<DataListModel> DataList
        {
            get { return _dataList; }
            set { _dataList = value; RaisePropertyChanged("DataList"); }
        }
        private ObservableCollection<DataListModel> _dataList = new ObservableCollection<DataListModel>();


        public DataGrid DataGrid
        {
            get { return _dataGrid; }
            set { _dataGrid = value; RaisePropertyChanged("DataGrid"); }
        }
        private DataGrid _dataGrid = new DataGrid();
        #endregion
        #region 커맨드
        public DelegateCommand<object> LoadedEvent { get; private set; }
        public DelegateCommand<object> CommandAddPathList { get; private set; }
        public DelegateCommand<object> CommandRatioChanged { get; set; }
        public DelegateCommand<object> CommandSetPathButton { get; private set; }
        public DelegateCommand<object> CommandSingleCheckedChange { get; set; }
        public DelegateCommand<object> CommandRemovePathList { get; private set; }
        public DelegateCommand<object> CommandAddGroupPathList { get; private set; }
        public DelegateCommand<object> CommandRemoveGroupPathList { get; private set; }
        public DelegateCommand<object> CommandApplyAllRatio { get; private set; }

        public DelegateCommand<object> CommandSaveFiles { get; private set; }

        #endregion

        #region 초기화
        public DataListViewModel()
        {
            InitData();
            InitCommand();
            InitEvent();
        }

        void InitData()
        {
            RatioSetDialogViewModel._dataListViewModel = this;
        }

        void InitCommand()
        {
            LoadedEvent = new DelegateCommand<object>((param) => OnLoadedEvent(param));
            CommandAddPathList = new DelegateCommand<object>((param) => OnCommandAddPathList(param));
            CommandRatioChanged = new DelegateCommand<object>((param) => OnCommandRatioChanged(param));
            CommandSetPathButton = new DelegateCommand<object>((param) => OnCommandSetPathButton(param));
            CommandSingleCheckedChange = new DelegateCommand<object>((param) => OnCommandSingleCheckedChange(param));
            CommandRemovePathList = new DelegateCommand<object>((param) => OnCommandRemovePathList(param));
            CommandAddGroupPathList = new DelegateCommand<object>((param) => OnCommandAddGroupPathList(param));
            CommandRemoveGroupPathList = new DelegateCommand<object>((param) => OnCommandRemoveGroupPathList(param));
            CommandApplyAllRatio = new DelegateCommand<object>((param) => OnCommandApplyAllRatio(param));
            CommandSaveFiles = new DelegateCommand<object>((param) => OnCommandSaveFiles(param));
        }

        void InitEvent()
        {

        }
        #endregion

        #region 이벤트
        public void SetTextLanguage()
        {
            if (DataGrid.Columns.Count != 0)
            {
                DataGrid.Columns[0].HeaderTemplate.LoadContent().SetCurrentValue(CheckBox.IsCheckedProperty, true);
                DataGrid.Columns[0].Header = Str.Using_subfolders;
                DataGrid.Columns[1].Header = Str.ClassName;
                DataGrid.Columns[2].Header = Str.DataPath;
                DataGrid.Columns[3].Header = Str.Set_Path;
                DataGrid.Columns[4].Header = Str.TotalImageCount;
                DataGrid.Columns[5].Header = Str.Train;
                DataGrid.Columns[6].Header = Str.Validation;
                DataGrid.Columns[7].Header = Str.Test;
                DataGrid.Columns[8].Header = Str.SubClassType;
            }
            Setting_SetAllRatio = Str.Set_all_ratio;
            Setting_SetRandomDistribute = Str.Using_Random_Distribute;
            DataColumn_TotalImageCount = Str.TotalImageCount;
            DataColumn_Train = Str.Train;
            DataColumn_Validation = Str.Validation;
            DataColumn_Test = Str.Test;
        }
        private void OnCommandAddPathList(object param)
        {
            DataList.Add(new DataListModel(true, "", this));
            DataList[DataList.Count - 1].CalcTrainValidationTest();
        }

        private void OnLoadedEvent(object param)
        {   
            DataGrid = param as DataGrid;
            DataGrid.ItemsSource = DataList;
            var checkHeader = DataGrid.Columns[0].HeaderTemplate.LoadContent() as CheckBox;
            checkHeader.Content = Str.All;
            DataGrid.Columns[0].Header = Str.Using_subfolders;
            DataGrid.Columns[1].Header = Str.ClassName;
            DataGrid.Columns[2].Header = Str.DataPath;
            DataGrid.Columns[3].Header = Str.Set_Path;
            DataGrid.Columns[4].Header = Str.TotalImageCount;
            DataGrid.Columns[5].Header = Str.Train;
            DataGrid.Columns[6].Header = Str.Validation;
            DataGrid.Columns[7].Header = Str.Test;
            DataGrid.Columns[8].Header = Str.SubClassType;
        }

        private void OnCommandSingleCheckedChange(object param)
        {
            if (param != null)
            {
                DataListModel k = param as DataListModel;
                //k.IsFindSubFolder = !k.IsFindSubFolder;
                ThreadLoadImageFiles(k);
            }
        }

        private void OnCommandSetPathButton(object param)
        {
            WPFFolderBrowser.WPFFolderBrowserDialog fbd = new WPFFolderBrowser.WPFFolderBrowserDialog();
            bool? resfolder = fbd.ShowDialog();

            if (resfolder == true)
            {
                //run 
                DataListModel temp = param as DataListModel;
                temp.PathString.SetFullPath(fbd.FileName);

                IsEnabled = false;
                ImageLoadThread = new Thread(() => ThreadLoadImageFiles(temp));
                ImageLoadThread.Start();
            }
        }

        void ThreadLoadImageFiles(object obj)
        {
            DataListModel tempList = obj as DataListModel;
            if (tempList.PathString.FolderPath_Full == "" || tempList.PathString.FolderPath_Full == null)
            {
                _loadingViewModel.Visibility = System.Windows.Visibility.Collapsed;
                IsEnabled = true;
                return;
            }

            tempList.ImagePathList.Clear();
            _loadingViewModel.Visibility = System.Windows.Visibility.Visible;

            DirectoryInfo di = new DirectoryInfo(tempList.PathString.FolderPath_Full);
            FileInfo[] files;
            //List<FileInfo> NewfileList = new List<FileInfo>();

            //if (tempList.AddableComboViewModel.ExtensionList.Count < 1)
            //{
            //    _loadingViewModel.Visibility = System.Windows.Visibility.Collapsed;
            //    IsEnabled = true;
            //    return;
            //}
            if (tempList.IsFindSubFolder)
            {
                files = di.GetFiles("*.*", SearchOption.AllDirectories);
            }
            else
            {
                files = di.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            }

            if (files.Count() < 1)
            {
                _loadingViewModel.Visibility = System.Windows.Visibility.Collapsed;
                IsEnabled = true;
                tempList.CalcTrainValidationTest();
                RefleshTotalCount();
                return;
            }

            //for (int iLoopCount = 0; iLoopCount < tempList.AddableComboViewModel.ExtensionList.Count; iLoopCount++)
            //{
            //    NewfileList.AddRange(files.Where(x => x.FullName.EndsWith("." + tempList.AddableComboViewModel.ExtensionList[iLoopCount])).ToList());
            //}


            _loadingViewModel.SetMax(files.Count());

            for (int i = 0; i < files.Count(); i++)
            {
                tempList.ImagePathList.Add(new FileIOModel(files[i].FullName));
                _loadingViewModel.SetCurrent(i + 1);
            }

            tempList.CalcTrainValidationTest();

            _loadingViewModel.Visibility = System.Windows.Visibility.Collapsed;
            IsEnabled = true;

            doneEvent.Set();

            RefleshTotalCount();
        }

        private void OnCommandRemovePathList(object param)
        {
            if (param == null)
            {
                return;
            }
            DataListModel temp = param as DataListModel;
            DataList.Remove(temp);
            RefleshTotalCount();
        }
        private void OnCommandRemoveGroupPathList(object param)
        {
            DataList.Clear();
            TotalImageCount = 0;
            RefleshTotalCount();
        }

        private void OnCommandAddGroupPathList(object param)
        {
            WPFFolderBrowser.WPFFolderBrowserDialog fbd = new WPFFolderBrowser.WPFFolderBrowserDialog();
            bool? resfolder = fbd.ShowDialog();

            if (resfolder == true)
            {
                DirectoryInfo di = new DirectoryInfo(fbd.FileName);
                DirectoryInfo[] folders;
                List<string> SubFolderList = new List<string>();

                folders = di.GetDirectories();


                for (int iLoopCount = 0; iLoopCount < folders.Count(); iLoopCount++)
                {
                    DataList.Add(new DataListModel(true, folders[iLoopCount].FullName, this));

                    ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadLoadImageFiles), DataList[iLoopCount]);
                }
            }
        }

        private void OnCommandSaveFiles(object param)
        {
            if (DataList.Count < 1)
            {
                return;
            }

            WPFFolderBrowser.WPFFolderBrowserDialog fbd = new WPFFolderBrowser.WPFFolderBrowserDialog();
            bool? resfolder = fbd.ShowDialog();

            if (resfolder == true)
            {
                IsEnabled = false;
                ImageLoadThread = new Thread(() => ThreadSaveImageFiles(fbd.FileName));
                ImageLoadThread.Start();
            }
        }
        void ThreadSaveImageFiles(string SavePath)
        {
            _loadingViewModel.Visibility = System.Windows.Visibility.Visible;

            DirectoryInfo di;// = new DirectoryInfo(SavePath);

            

            for (int iLoopCount = 0; iLoopCount < DataList.Count; iLoopCount++)
            {
                bool usingSubClass = DataList[iLoopCount].PathString.SubClassName!="" ? true : false;
                string baseTrainDir = "";
                string baseValDir = "";
                string baseTestDir = "";
                if (double.IsNaN(DataList[iLoopCount].TrainImageCount) || double.IsNaN(DataList[iLoopCount].ValidationImageCount) || double.IsNaN(DataList[iLoopCount].TestImageCount))
                {
                    continue;
                }
                _loadingViewModel.SetMax(DataList[iLoopCount].ImagePathList.Count);//UIthread 충돌 가능성 있음
                if (!usingSubClass)
                {
                    di = new DirectoryInfo(SavePath + "\\Train\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    baseTrainDir = SavePath + "\\Train\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName;

                    di = new DirectoryInfo(SavePath + "\\Validation\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    baseValDir = SavePath + "\\Validation\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName;

                    di = new DirectoryInfo(SavePath + "\\Test\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    baseTestDir = SavePath + "\\Test\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName;
                }
                else
                {
                    di = new DirectoryInfo(SavePath + "\\Train\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName + "\\" + DataList[iLoopCount].PathString.SubClassName);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    baseTrainDir = SavePath + "\\Train\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName + "\\" + DataList[iLoopCount].PathString.SubClassName;

                    di = new DirectoryInfo(SavePath + "\\Validation\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName + "\\" + DataList[iLoopCount].PathString.SubClassName);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    baseValDir = SavePath + "\\Validation\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName + "\\" + DataList[iLoopCount].PathString.SubClassName;

                    di = new DirectoryInfo(SavePath + "\\Test\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName + "\\" + DataList[iLoopCount].PathString.SubClassName);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    baseTestDir = SavePath + "\\Test\\" + DataList[iLoopCount].PathString.FolderPath_ClassFolderName + "\\" + DataList[iLoopCount].PathString.SubClassName;
                }
                

                double TrainRemain = DataList[iLoopCount].TrainImageCount;
                double ValidationRemain = DataList[iLoopCount].ValidationImageCount;
                double TestRemain = DataList[iLoopCount].TestImageCount;

                int LoopIndex = 0;

                ObservableCollection<FileIOModel> DupImageList = new ObservableCollection<FileIOModel>(DataList[iLoopCount].ImagePathList);


                int CheckExistOverlabCount = 1;

                if (IsCheckedRandomDistribute)
                {
                    //랜덤 적용 시   
                    Random rnd = new Random();
                    int CurrentRandom = 0;

                    while (DupImageList.Count > 0)
                    {
                        CurrentRandom = rnd.Next(0, DupImageList.Count - 1);

                        if (TrainRemain > 0)
                        {
                            if (CurrentRandom == DupImageList.Count)
                            {
                                --CurrentRandom;
                            }
                            if (File.Exists(baseTrainDir + "\\" + DupImageList[CurrentRandom].FileName_Safe))
                            {
                                while (true)
                                {
                                    if (!File.Exists(baseTrainDir + DupImageList[CurrentRandom].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[CurrentRandom].FileName_Extension))
                                    {
                                        File.Copy(DupImageList[CurrentRandom].FileName_Full,  baseTrainDir + "\\" + DupImageList[CurrentRandom].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[CurrentRandom].FileName_Extension, false);
                                        break;
                                    }
                                    ++CheckExistOverlabCount;
                                }
                                CheckExistOverlabCount = 1;
                            }
                            else
                            {
                                File.Copy(DupImageList[CurrentRandom].FileName_Full, baseTrainDir + "\\" + DupImageList[CurrentRandom].FileName_Safe, false);
                            }

                            DupImageList.RemoveAt(CurrentRandom);
                            --TrainRemain;
                            ++LoopIndex;
                            _loadingViewModel.SetCurrent(LoopIndex);
                        }
                        if (ValidationRemain > 0)
                        {
                            if (CurrentRandom == DupImageList.Count)
                            {
                                --CurrentRandom;
                            }
                            if (File.Exists(baseValDir + "\\" + DupImageList[CurrentRandom].FileName_Safe))
                            {
                                while (true)
                                {
                                    if (!File.Exists(baseValDir + "\\" + DupImageList[CurrentRandom].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[CurrentRandom].FileName_Extension))
                                    {
                                        File.Copy(DupImageList[CurrentRandom].FileName_Full, baseValDir + "\\" + DupImageList[CurrentRandom].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[CurrentRandom].FileName_Extension, false);
                                        break;
                                    }
                                    ++CheckExistOverlabCount;
                                }
                                CheckExistOverlabCount = 1;
                            }
                            else
                            {
                                File.Copy(DupImageList[CurrentRandom].FileName_Full, baseValDir + "\\" + DupImageList[CurrentRandom].FileName_Safe, false);
                            }
                            DupImageList.RemoveAt(CurrentRandom);
                            --ValidationRemain;
                            ++LoopIndex;
                            _loadingViewModel.SetCurrent(LoopIndex);
                        }
                        if (TestRemain > 0)
                        {
                            if (CurrentRandom == DupImageList.Count)
                            {
                                --CurrentRandom;
                            }
                            if (File.Exists(baseTestDir + "\\" + DupImageList[CurrentRandom].FileName_Safe))
                            {
                                while (true)
                                {
                                    if (!File.Exists(baseTestDir + "\\" + DupImageList[CurrentRandom].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[CurrentRandom].FileName_Extension))
                                    {
                                        File.Copy(DupImageList[CurrentRandom].FileName_Full, baseTestDir + "\\" + DupImageList[CurrentRandom].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[CurrentRandom].FileName_Extension, false);
                                        break;
                                    }
                                    ++CheckExistOverlabCount;
                                }
                                CheckExistOverlabCount = 1;
                            }
                            else
                            {
                                File.Copy(DupImageList[CurrentRandom].FileName_Full, baseTestDir + "\\" + DupImageList[CurrentRandom].FileName_Safe, false);
                            }
                            DupImageList.RemoveAt(CurrentRandom);
                            --TestRemain;
                            ++LoopIndex;
                            _loadingViewModel.SetCurrent(LoopIndex);
                        }
                    }

                }
                else
                {

                    ////이미지 수만큼 루프
                    while (DupImageList.Count != LoopIndex)
                    {
                        if (TrainRemain > 0)
                        {
                            if (File.Exists(baseTrainDir + "\\" + DupImageList[LoopIndex].FileName_Safe))
                            {
                                while (true)
                                {
                                    if (!File.Exists(baseTrainDir + "\\" + DupImageList[LoopIndex].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[LoopIndex].FileName_Extension))
                                    {
                                        File.Copy(DupImageList[LoopIndex].FileName_Full,baseTrainDir + "\\" + DupImageList[LoopIndex].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[LoopIndex].FileName_Extension, false);
                                        break;
                                    }
                                    ++CheckExistOverlabCount;
                                }
                                CheckExistOverlabCount = 1;
                            }
                            else
                            {
                                File.Copy(DupImageList[LoopIndex].FileName_Full, baseTrainDir + "\\" + DupImageList[LoopIndex].FileName_Safe, false);
                            }

                            --TrainRemain;
                            ++LoopIndex;
                            _loadingViewModel.SetCurrent(LoopIndex);
                        }
                        if (ValidationRemain > 0)
                        {
                            if (File.Exists(baseValDir + "\\" + DupImageList[LoopIndex].FileName_Safe))
                            {
                                while (true)
                                {
                                    if (!File.Exists(baseValDir + "\\" + DupImageList[LoopIndex].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[LoopIndex].FileName_Extension))
                                    {
                                        File.Copy(DupImageList[LoopIndex].FileName_Full, baseValDir + "\\" + DupImageList[LoopIndex].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[LoopIndex].FileName_Extension, false);
                                        break;
                                    }
                                    ++CheckExistOverlabCount;
                                }
                                CheckExistOverlabCount = 1;
                            }
                            else
                            {
                                File.Copy(DupImageList[LoopIndex].FileName_Full, baseValDir + "\\" + DupImageList[LoopIndex].FileName_Safe, false);
                            }

                            --ValidationRemain;
                            ++LoopIndex;
                            _loadingViewModel.SetCurrent(LoopIndex);
                        }
                        if (TestRemain > 0)
                        {
                            if (File.Exists(baseTestDir + "\\" + DupImageList[LoopIndex].FileName_Safe))
                            {
                                while (true)
                                {
                                    if (!File.Exists(baseTestDir + "\\" + DupImageList[LoopIndex].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[LoopIndex].FileName_Extension))
                                    {
                                        File.Copy(DupImageList[LoopIndex].FileName_Full, baseTestDir + "\\" + DupImageList[LoopIndex].FileName_OnlyName + "(" + CheckExistOverlabCount + ")." + DupImageList[LoopIndex].FileName_Extension, false);
                                        break;
                                    }
                                    ++CheckExistOverlabCount;
                                }
                                CheckExistOverlabCount = 1;
                            }
                            else
                            {
                                File.Copy(DupImageList[LoopIndex].FileName_Full, baseTestDir + "\\" + DupImageList[LoopIndex].FileName_Safe, false);
                            }
                            --TestRemain;
                            ++LoopIndex;
                            _loadingViewModel.SetCurrent(LoopIndex);
                        }
                    }
                }


            }


            _loadingViewModel.Visibility = System.Windows.Visibility.Collapsed;
            IsEnabled = true;

        }

        public void SetAllRatio(double SetTrainRatio, double SetValRatio, double SetTestRatio)
        {
            for (int iLoopCount = 0; iLoopCount < DataList.Count; iLoopCount++)
            {
                DataList[iLoopCount].TrainRatio = SetTrainRatio;
                DataList[iLoopCount].ValidationRatio = SetValRatio;
                DataList[iLoopCount].TestRatio = SetTestRatio;

                DataList[iLoopCount].CalcTrainValidationTest();
            }
            RefleshTotalCount();
        }

        private void OnCommandApplyAllRatio(object param)
        {
            RatioSetDialogViewModel.TotalImageCount = TotalImageCount;
            RatioSetDialogViewModel.CalcTrainValidationTest();
            IsEnabledMainGrid = false;
            RatioSetDialogViewModel.Visibility = Visibility.Visible;
        }

        private void OnCommandRatioChanged(object param)
        {
            DataListModel temp = param as DataListModel;
            temp.CalcTrainValidationTest();
            RefleshTotalCount();
        }
        void RefleshTotalCount()
        {
            TotalImageCount = 0;
            TotalTrainCount = 0;
            TotalValCount = 0;
            TotalTestCount = 0;
            for (int iLoopCount = 0; iLoopCount < DataList.Count; iLoopCount++)
            {
                TotalImageCount += DataList[iLoopCount].ImagePathList.Count;
                TotalTrainCount += (int)DataList[iLoopCount].TrainImageCount;
                TotalValCount += (int)DataList[iLoopCount].ValidationImageCount;
                TotalTestCount += (int)DataList[iLoopCount].TestImageCount;
            }
        }
        #endregion

    }
}
