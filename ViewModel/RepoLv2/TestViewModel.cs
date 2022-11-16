using JHoney_Flow.Keras_Model;
using JHoney_Flow.View.RepoLv2.DataListRootPath;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JHoney_Flow.ViewModel.RepoLv2
{
    class TestViewModel:BindableBase
    {
        //public Resnet50 resnet50 = new Resnet50();
        public GenericIdentity _identity;
        #region 프로퍼티
        public ModelListViewModel ModelListViewModel
        {
            get { return _modelListViewModel; }
            set { _modelListViewModel = value; RaisePropertyChanged("ModelListViewModel"); }
        }
        private ModelListViewModel _modelListViewModel;

        public DataListRootPathViewModel DataListRootPathViewModel
        {
            get { return _dataListRootPathViewModel; }
            set { _dataListRootPathViewModel = value; RaisePropertyChanged("DataListRootPathViewModel"); }
        }
        private DataListRootPathViewModel _dataListRootPathViewModel;

        public CartesianChart CartesianChart
        {
            get { return _cartesianChart; }
            set { _cartesianChart = value; RaisePropertyChanged("CartesianChart"); }
        }
        private CartesianChart _cartesianChart;

        public HeatSeries HeatSeries_ConfusionMatrix
        {
            get { return _heatSeries_ConfusionMatrix; }
            set { _heatSeries_ConfusionMatrix = value; RaisePropertyChanged("HeatSeries_ConfusionMatrix"); }
        }
        private HeatSeries _heatSeries_ConfusionMatrix = new HeatSeries() { Title = "Confusion Matrix" };

        public ChartValues<HeatPoint> ChartValue1
        {
            get { return _chartValue1; }
            set { _chartValue1 = value; RaisePropertyChanged("ChartValue1"); }
        }
        private ChartValues<HeatPoint> _chartValue1 = new ChartValues<HeatPoint>();

        public double ProgressValue
        {
            get { return _progressValue; }
            set { _progressValue = value; RaisePropertyChanged("ProgressValue"); }
        }
        private double _progressValue = 0;

        public double ProgressMax
        {
            get { return _progressMax; }
            set { _progressMax = value; RaisePropertyChanged("ProgressMax"); }
        }
        private double _progressMax = 100;

        public double EpochperMicroSec
        {
            get { return _epochperMicroSec; }
            set { _epochperMicroSec = value; RaisePropertyChanged("EpochperMicroSec"); }
        }
        private double _epochperMicroSec = 0;

        public float ProgressPercent
        {
            get { return _progressPercent; }
            set { _progressPercent = value; RaisePropertyChanged("ProgressPercent"); }
        }
        private float _progressPercent = 0.0f;

        public double ProgressNext
        {
            get { return _progressNext; }
            set { _progressNext = value; RaisePropertyChanged("ProgressNext"); }
        }
        private double _progressNext = 0;
        #region ---［ Test Parameter ］---------------------------------------------------------------------
        public bool UsingTrainSet
        {
            get { return _usingTrainSet; }
            set { _usingTrainSet = value; RaisePropertyChanged("UsingTrainSet"); }
        }
        private bool _usingTrainSet = false;

        public bool UsingValidationSet
        {
            get { return _usingValidationSet; }
            set { _usingValidationSet = value; RaisePropertyChanged("UsingValidationSet"); }
        }
        private bool _usingValidationSet = false;

        public bool UsingTestSet
        {
            get { return _usingTestSet; }
            set { _usingTestSet = value; RaisePropertyChanged("UsingTestSet"); }
        }
        private bool _usingTestSet = true;

        public string StopSignal
        {
            get { return _stopSignal; }
            set { _stopSignal = value; RaisePropertyChanged("StopSignal"); }
        }
        private string _stopSignal = "";

        #endregion ---------------------------------------------------------------------------------

        public ObservableCollection<string> LogAll
        {
            get { return _logAll; }
            set { _logAll = value; RaisePropertyChanged("LogAll"); }
        }
        private ObservableCollection<string> _logAll = new ObservableCollection<string>();
        public bool IsTraining
        {
            get { return _isTraining; }
            set { _isTraining = value; RaisePropertyChanged("IsTraining"); }
        }
        private bool _isTraining = false;
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
        //public RelayCommand<object> MyCommand { get; private set; }
        public DelegateCommand<object> LoadedEvent { get; private set; }
        public DelegateCommand<object> CommandTest { get; private set; }

        #endregion

        #region 초기화
        public TestViewModel()
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
            LoadedEvent = new DelegateCommand<object>((param) => OnLoadedEvent(param));
            CommandTest = new DelegateCommand<object>((param) => OnCommandTest(param));
            //MyCommand = new RelayCommand<object>((param) => OnMyCommand(param));
        }


        void InitEvent()
        {

        }
        #endregion

        #region 이벤트
        private void OnLoadedEvent(object param)
        {
            if (param != null && CartesianChart == null)
            {
                CartesianChart = param as CartesianChart;
                HeatSeries_ConfusionMatrix.DataLabels = true;
                HeatSeries_ConfusionMatrix.FontSize = 30;
                
                CartesianChart.Series.Add(HeatSeries_ConfusionMatrix);
                CartesianChart.Series[0].Values = ChartValue1;
                
            }
        }
        public void MakeConfusionMatrix(List<string> Classes, List<(int,int,int)> Result)
        {
            ChartValue1.Clear();
            List<string> revClasses = new List<string>(Classes);
            for (int i = 0; i < Classes.Count; i++)
            {
                Classes[i] = "Predict " + Classes[i];
            }
            for (int i = 0; i < revClasses.Count; i++)
            {
                revClasses[i] = "Actual " + revClasses[i];
            }

            revClasses.Reverse();
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                //Classes.Reverse();
                CartesianChart.AxisX[0].Labels = Classes;
                CartesianChart.AxisX[0].Position = AxisPosition.RightTop;
                CartesianChart.AxisY[0].Labels = revClasses;
                CartesianChart.AxisX[0].FontSize = 30;
                
                CartesianChart.AxisY[0].FontSize = 30;
                for (int iLoopCount = 0; iLoopCount < Classes.Count; iLoopCount++)
                {
                    for (int jLoopCount = 0; jLoopCount < Classes.Count; jLoopCount++)
                    {
                        ChartValue1.Add(new HeatPoint(iLoopCount, jLoopCount, 0));
                    }
                }
                
                for (int iLoopCount = 0; iLoopCount < Result.Count; iLoopCount++)
                {
                    int idxMatrix = (Classes.Count - 1 - Result[iLoopCount].Item1) + Result[iLoopCount].Item2 * Classes.Count;
                    
                    ChartValue1[idxMatrix].Weight += 1;
                }
            }));
        }

        private void OnCommandTest(object param)
        {
            if (DataListRootPathViewModel.DataSetRootPath == "")
            {
                SendLog("", "No selected data-set root path");
                return;
            }

            if(ModelListViewModel.SelectedModelPath=="")
            {
                SendLog("", "No selected model");
                return;
            }
            if(!UsingTrainSet && !UsingValidationSet && !UsingTestSet)
            {
                SendLog("", "Select Data-set for evaluation");
            }

            SendLog("", "Test Start!");
            IsTraining = true;
            DataListRootPathViewModel.IsTraining = IsTraining;
            Resnet50 resnet50 = new Resnet50();
            resnet50._testViewModel = this;
            Task thr = new Task(() => resnet50.Evaluation(DataListRootPathViewModel.ImageHeight, DataListRootPathViewModel.ImageWidth, DataListRootPathViewModel.ImageChannel, DataListRootPathViewModel.DataSetRootPath,ModelListViewModel.SelectedModelPath
                ,UsingTrainSet,UsingValidationSet,UsingTestSet,32), TaskCreationOptions.DenyChildAttach);
            thr.Start();
        }
        public void SendLog(string LogType, string Value)
        {
            if (LogAll.Count > 1001)
            {
                for (int iLoofCount = LogAll.Count - 1; iLoofCount > 100; --iLoofCount)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        LogAll.RemoveAt(LogAll.Count - 1);
                    }));
                }
            }

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                LogAll.Insert(0, DateTime.Now.ToLongTimeString() + " : " + Value);
            }));
        }
        /*
        private void OnMyCommand(object param)
            {

            }
            */
        #endregion

    }
}
