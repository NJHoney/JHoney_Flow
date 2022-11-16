using JHoney_Flow.ViewModel.RepoLv2;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JHoney_Flow.View.RepoLv2.RatioSetDialog
{
    class RatioSetDialogViewModel:BindableBase
    {
        #region 프로퍼티
        public Visibility Visibility
        {
            get { return _visibility; }
            set { _visibility = value; RaisePropertyChanged("Visibility"); }
        }
        private Visibility _visibility = Visibility.Collapsed;

        public DataListViewModel _dataListViewModel;

        public double TrainPercent
        {
            get { return _trainPercent; }
            set { _trainPercent = value; RaisePropertyChanged("TrainPercent"); }
        }
        private double _trainPercent = 0;
        public double TrainRatio
        {
            get { return _trainRatio; }
            set { _trainRatio = value; RaisePropertyChanged("TrainRatio"); }
        }
        private double _trainRatio = 6.4;

        public double TrainImageCount
        {
            get { return _trainImageCount; }
            set { _trainImageCount = value; RaisePropertyChanged("TrainImageCount"); }
        }
        private double _trainImageCount = 0;

        public double ValidationPercent
        {
            get { return _validationPercent; }
            set { _validationPercent = value; RaisePropertyChanged("ValidationPercent"); }
        }
        private double _validationPercent = 0;

        public double ValidationRatio
        {
            get { return _validationRatio; }
            set { _validationRatio = value; RaisePropertyChanged("ValidationRatio"); }
        }
        private double _validationRatio = 1.6;

        public double ValidationImageCount
        {
            get { return _validationImageCount; }
            set { _validationImageCount = value; RaisePropertyChanged("ValidationImageCount"); }
        }
        private double _validationImageCount = 0;

        public double TestPercent
        {
            get { return _testPercent; }
            set { _testPercent = value; RaisePropertyChanged("TestPercent"); }
        }
        private double _testPercent = 0;

        public double TestRatio
        {
            get { return _testRatio; }
            set { _testRatio = value; RaisePropertyChanged("TestRatio"); }
        }
        private double _testRatio = 2;

        public double TestImageCount
        {
            get { return _testImageCount; }
            set { _testImageCount = value; RaisePropertyChanged("TestImageCount"); }
        }
        private double _testImageCount = 0;


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

        #endregion
        #region 커맨드
        public DelegateCommand<object> CommandSetRatio { get; private set; }
        public DelegateCommand<object> CommandCancel { get; private set; }
        public DelegateCommand<object> CommandRatioChanged { get; private set; }
        #endregion

        #region 초기화
        public RatioSetDialogViewModel()
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
            CommandSetRatio = new DelegateCommand<object>((param) => OnCommandSetRatio(param));
            CommandCancel = new DelegateCommand<object>((param) => OnCommandCancel(param));
            CommandRatioChanged = new DelegateCommand<object>((param) => OnCommandRatioChanged(param));
        }

        void InitEvent()
        {

        }
        #endregion

        #region 이벤트

        private void OnCommandSetRatio(object param)
        {
            _dataListViewModel.SetAllRatio(TrainRatio, ValidationRatio, TestRatio);
            Visibility = Visibility.Collapsed;
            _dataListViewModel.IsEnabledMainGrid = true;
        }


        private void OnCommandCancel(object param)
        {

            Visibility = Visibility.Collapsed;
            _dataListViewModel.IsEnabledMainGrid = true;
        }
        public void CalcTrainValidationTest()
        {
            double TotalRatio = TrainRatio + ValidationRatio + TestRatio;



            TrainImageCount = Math.Round((double)(TotalImageCount * (TrainRatio / TotalRatio)));
            ValidationImageCount = Math.Round((double)(TotalImageCount * (ValidationRatio / TotalRatio)));
            TestImageCount = Math.Round((double)(TotalImageCount * (TestRatio / TotalRatio)));

            TrainPercent = (int)((TrainRatio / TotalRatio) * 100);
            ValidationPercent = (int)((ValidationRatio / TotalRatio) * 100);
            TestPercent = (int)((TestRatio / TotalRatio) * 100);

            if (TotalImageCount != (TrainImageCount + ValidationImageCount + TestImageCount))
            {
                double MaxValue = Math.Max(TrainRatio, ValidationRatio);
                MaxValue = Math.Max(MaxValue, TestRatio);

                if (MaxValue == TrainRatio)
                {
                    TrainImageCount += (TotalImageCount - (TrainImageCount + ValidationImageCount + TestImageCount));
                }
                else if (MaxValue == ValidationRatio)
                {
                    ValidationImageCount += (TotalImageCount - (TrainImageCount + ValidationImageCount + TestImageCount));
                }
                else
                {
                    TestImageCount += (TotalImageCount - (TrainImageCount + ValidationImageCount + TestImageCount));
                }
            }

            //예전 방식
            ////ValidationImageCount = Math.Round((double)(TotalImageCount * (ValidationRatio / TotalRatio)));
            ////TestImageCount = Math.Round((double)(TotalImageCount * (TestRatio / TotalRatio)));
        }

        private void OnCommandRatioChanged(object param)
        {
            CalcTrainValidationTest();
        }
        #endregion
    }
}
