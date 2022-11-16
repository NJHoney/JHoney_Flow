using JHoney_Flow.Keras_Model;
using JHoney_Flow.Model;
using JHoney_Flow.View.RepoLv2.DataListRootPath;
using LiveCharts;
using LiveCharts.Wpf;
using MahApps.Metro.Controls;
using OpenCvSharp;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace JHoney_Flow.ViewModel.RepoLv2
{
    class TrainViewModel : BindableBase
    {

        #region 프로퍼티
        //public Resnet50 resnet50 = new Resnet50();
        Stopwatch tempwatch = new Stopwatch();
        public Thread ProgressBarThread;
        TrainSettingModel tsm = new TrainSettingModel();
        public GenericIdentity _identity;

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


        public bool IsTraining
        {
            get { return _isTraining; }
            set { _isTraining = value; RaisePropertyChanged("IsTraining"); }
        }
        private bool _isTraining = false;

        public CartesianChart CartesianChart
        {
            get { return _cartesianChart; }
            set { _cartesianChart = value; RaisePropertyChanged("CartesianChart"); }
        }
        private CartesianChart _cartesianChart;

        public LineSeries LineSeries_ValLoss
        {
            get { return _lineSeries_ValLoss; }
            set { _lineSeries_ValLoss = value; RaisePropertyChanged("LineSeries_ValLoss"); }
        }
        private LineSeries _lineSeries_ValLoss = new LineSeries() { Title = "Cross Entropy Loss" };

        public LineSeries LineSeries_ValAcc
        {
            get { return _lineSeries_ValAcc; }
            set { _lineSeries_ValAcc = value; RaisePropertyChanged("LineSeries_ValAcc"); }
        }
        private LineSeries _lineSeries_ValAcc = new LineSeries() { Title = "Validation_Recall" };

        
        
        public ChartValues<double> ChartValue1
        {
            get { return _chartValue1; }
            set { _chartValue1 = value; RaisePropertyChanged("ChartValue1"); }
        }
        private ChartValues<double> _chartValue1 = new ChartValues<double>();

        public ChartValues<double> ChartValue_ValAcc        
        { 
            get { return _chartValue_ValAcc; }
            set { _chartValue_ValAcc = value; RaisePropertyChanged("ChartValue_ValAcc"); }
        }
        private ChartValues<double> _chartValue_ValAcc = new ChartValues<double>();

        public ObservableCollection<string> LogAll
        {
            get { return _logAll; }
            set { _logAll = value; RaisePropertyChanged("LogAll"); }
        }
        private ObservableCollection<string> _logAll = new ObservableCollection<string>();

        public ObservableCollection<string> OptimizerList
        {
            get { return _optimizerList; }
            set { _optimizerList = value; RaisePropertyChanged("OptimizerList"); }
        }
        private ObservableCollection<string> _optimizerList = new ObservableCollection<string>();

        public string OptimizerSelected
        {
            get { return _optimizerSelected; }
            set { _optimizerSelected = value; RaisePropertyChanged("OptimizerSelected"); }
        }
        private string _optimizerSelected = "Adam";
        public ObservableCollection<string> PreTrainedList
        {
            get { return _preTrainedList; }
            set { _preTrainedList = value; RaisePropertyChanged("PreTrainedList"); }
        }
        private ObservableCollection<string> _preTrainedList = new ObservableCollection<string>();

        public string PreTrainedSelected
        {
            get { return _preTrainedSelected; }
            set { _preTrainedSelected = value; RaisePropertyChanged("PreTrainedSelected"); }
        }
        private string _preTrainedSelected = "ResNet152";


        
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


        #region ---［ Train Parameter ］---------------------------------------------------------------------

        public int Epoch
        {
            get { return _epoch; }
            set { _epoch = value; RaisePropertyChanged("Epoch"); }
        }
        private int _epoch = 100;

        public int MiniBatch
        {
            get { return _miniBatch; }
            set { _miniBatch = value; RaisePropertyChanged("MiniBatch"); }
        }
        private int _miniBatch = 32;

        public string LearningRate
        {
            get { return _learningRate; }
            set { _learningRate = value; RaisePropertyChanged("LearningRate"); }
        }
        private string _learningRate = "0.001";

        

        public string StopSignal
        {
            get { return _stopSignal; }
            set { _stopSignal = value; RaisePropertyChanged("StopSignal"); }
        }
        private string _stopSignal = "";

        public bool Feature_wise
        {
            get { return _feature_wise; }
            set { _feature_wise = value; RaisePropertyChanged("Feature_wise"); }
        }
        private bool _feature_wise = false;

        public bool Sample_wise
        {
            get { return _sample_wise; }
            set { _sample_wise = value; RaisePropertyChanged("Sample_wise"); }
        }
        private bool _sample_wise = false;

        public bool Feature_std_norm
        {
            get { return _feature_std_norm; }
            set { _feature_std_norm = value; RaisePropertyChanged("Feature_std_norm"); }
        }
        private bool _feature_std_norm = false;

        public bool Sample_std_norm
        {
            get { return _sample_std_norm; }
            set { _sample_std_norm = value; RaisePropertyChanged("Sample_std_norm"); }
        }
        private bool _sample_std_norm = false;

        public bool ZCA_whitening
        {
            get { return _zCA_whitening; }
            set { _zCA_whitening = value; RaisePropertyChanged("ZCA_whitening"); }
        }
        private bool _zCA_whitening = false;

        public bool Horizontal_Flip
        {
            get { return _horizontal_Flip; }
            set { _horizontal_Flip = value; RaisePropertyChanged("Horizontal_Flip"); }
        }
        private bool _horizontal_Flip = false;

        public bool Vertical_Flip
        {
            get { return _vertical_Flip; }
            set { _vertical_Flip = value; RaisePropertyChanged("Vertical_Flip"); }
        }
        private bool _vertical_Flip = false;

        
        #endregion ---------------------------------------------------------------------------------



        #endregion
        #region 커맨드
        
        public DelegateCommand<object> LoadedEvent { get; private set; }
        public DelegateCommand<object> CommandTrain { get; private set; }
        public DelegateCommand<object> CommandStop { get; private set; }


        #endregion

        #region 초기화
        public TrainViewModel()
        {
            InitData();
            InitCommand();
            InitEvent();
        }

        void InitData()
        {
            

            OptimizerList.Add("Adadelta");
            OptimizerList.Add("Adagrad");
            OptimizerList.Add("Adam");
            OptimizerList.Add("Adamax");
            OptimizerList.Add("Nadam");
            OptimizerList.Add("RMSprop");
            OptimizerList.Add("SGD");


            PreTrainedList.Add("DenseNet121");
            PreTrainedList.Add("DenseNet169");
            PreTrainedList.Add("DenseNet201");
            PreTrainedList.Add("InceptionV3");
            PreTrainedList.Add("MobileNet");
            PreTrainedList.Add("ResNet50");
            PreTrainedList.Add("ResNet101");
            PreTrainedList.Add("ResNet152");
            PreTrainedList.Add("VGG16");
            PreTrainedList.Add("VGG19");

        }

        void InitCommand()
        {
            
            LoadedEvent = new DelegateCommand<object>((param) => OnLoadedEvent(param));
            CommandTrain = new DelegateCommand<object>((param) => OnCommandTrain(param));
            CommandStop = new DelegateCommand<object>((param) => OnCommandStop(param));
        }

        void InitEvent()
        {

        }
        #endregion

        #region 이벤트
        private void OnLoadedEvent(object param)
        {
            if (param != null && CartesianChart==null)
            {
                CartesianChart = param as CartesianChart;
                CartesianChart.AxisY.Add(new Axis());
                CartesianChart.AxisY.Add(new Axis());
                CartesianChart.Series.Add(LineSeries_ValLoss);
                CartesianChart.Series.Add(LineSeries_ValAcc);
                
                CartesianChart.Series[0].Values = ChartValue1;
                CartesianChart.Series[1].Values = ChartValue_ValAcc;
                CartesianChart.AxisY[0].Title= "Cross Entropy Loss";
                CartesianChart.AxisY[0].Foreground = Brushes.Blue;
                CartesianChart.AxisY[1].Title = "Recall";
                CartesianChart.AxisY[1].Foreground = Brushes.IndianRed;
                CartesianChart.AxisY[1].Position = AxisPosition.RightTop;
                CartesianChart.Series[0].ScalesYAt = 0;
                CartesianChart.Series[1].ScalesYAt = 1;
                CartesianChart.FontSize = 15;
                CartesianChart.AxisY[0].FontSize = 20;
                CartesianChart.AxisY[1].FontSize = 20;
                CartesianChart.DisableAnimations = true;
                CartesianChart.Hoverable = false;
                CartesianChart.DataTooltip = null;
            }
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

        public void CalcProgressBar(double epochpertime, int currentepoch, int totalepoch)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                EpochperMicroSec = epochpertime;
                ProgressMax = epochpertime * totalepoch;
                ProgressNext = ProgressMax * ((double)currentepoch / (double)totalepoch);
                ProgressValue = ProgressMax * (((double)currentepoch-1) / (double)totalepoch);
            }));

            
        }

        public void ProgressCounter()
        {
            tempwatch.Start();
             ProgressBarThread = new Thread(() =>
            {
                while (true)
                {
                    if(ProgressNext<ProgressValue && EpochperMicroSec !=0)
                    {
                        ProgressValue = ProgressNext - 1;
                        ProgressPercent = (float)(ProgressValue / ProgressMax) * 100;
                    }

                    if(ProgressNext<ProgressMax && EpochperMicroSec != 0)
                    {
                        if (ProgressNext >= ProgressValue && EpochperMicroSec!=0)
                        {
                            if (tempwatch.ElapsedMilliseconds >= 100)
                            {
                                tempwatch.Restart();
                                ProgressValue += (EpochperMicroSec)/50;
                                ProgressPercent = (float)(ProgressValue / ProgressMax) * 100;
                            }
                            //ProgressValue += (double.Parse(tempwatch.ElapsedMilliseconds.ToString()));
                            
                        }
                    }

                    if (ProgressNext >= ProgressMax && EpochperMicroSec != 0)
                    {
                        ProgressNext = ProgressMax;
                        //ProgressMax = ProgressValue = ProgressNext = 100;
                        ProgressPercent = (float)(ProgressValue / ProgressMax) * 100;
                        break;
                    }
                }
                tempwatch.Stop();
            });
            ProgressBarThread.Start();
        }


        


        private void OnCommandTrain(object param)
        {
            
            if(DataListRootPathViewModel.DataSetRootPath =="")
            {
                return;
            }
            SendLog("", "Train Initialize!");
            tsm.SetParam(Feature_wise, Sample_wise, Feature_std_norm, Sample_std_norm, ZCA_whitening, Horizontal_Flip, Vertical_Flip);
            IsTraining = true;
            DataListRootPathViewModel.IsTraining = IsTraining;

            ChartValue1.Clear();
            ChartValue_ValAcc.Clear();
            ProgressValue = ProgressNext = 0;
            ProgressMax = 100;

            Resnet50 resnet50 = new Resnet50();
            resnet50._trainviewmodel = this;
            Task task = new Task(() => resnet50.TrainNew_UsingGenerator(DataListRootPathViewModel.ImageHeight, DataListRootPathViewModel.ImageWidth, DataListRootPathViewModel.ImageChannel, DataListRootPathViewModel.DataSetRootPath, tsm, PreTrainedSelected, float.Parse(LearningRate), Epoch, MiniBatch),TaskCreationOptions.DenyChildAttach);
            task.Start();
            //Thread thr = new Thread(() => resnet50.TrainNew_UsingGenerator(DataListRootPathViewModel.ImageHeight, DataListRootPathViewModel.ImageWidth, DataListRootPathViewModel.ImageChannel, DataListRootPathViewModel.DataSetRootPath,tsm,PreTrainedSelected,float.Parse(LearningRate),Epoch,MiniBatch));
            //thr.Start();
        }

        private void OnCommandStop(object param)
        {
            if(StopSignal =="Stop")
            {
                StopSignal = "";
                SendLog("", "User Stop Canceled");
            }
            else
            {
                StopSignal = "Stop";
                SendLog("", "User Stop Reservation");
            }

            
        }

        #endregion

    }
}
