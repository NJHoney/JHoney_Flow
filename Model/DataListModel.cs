using JHoney_Flow.Language;
using JHoney_Flow.ViewModel.RepoLv2;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHoney_Flow.Model
{
    class DataListModel:BindableBase
    {
        
        
        public DataListModel()
        {

        }
        public DataListModel(bool IsUsing, string FilePath, DataListViewModel DataListViewModel)
        {
            IsChecked = IsUsing;

            PathString = new FolderIOModel(FilePath);

            //AddableComboViewModel._pathListViewModel = PathListViewModel;
        }

        public void CalcTrainValidationTest()
        {
            double TotalRatio = TrainRatio + ValidationRatio + TestRatio;
            int TotalImageCount = ImagePathList.Count;

            TrainImageCount = Math.Round((double)(TotalImageCount * (TrainRatio / TotalRatio)));
            ValidationImageCount = Math.Round((double)(TotalImageCount * (ValidationRatio / TotalRatio)));
            TestImageCount = Math.Round((double)(TotalImageCount * (TestRatio / TotalRatio)));

            TrainPercent = ((TrainRatio / TotalRatio) * 100);
            ValidationPercent = ((ValidationRatio / TotalRatio) * 100);
            TestPercent = ((TestRatio / TotalRatio) * 100);

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

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; RaisePropertyChanged("IsChecked"); }
        }
        private bool _isChecked = false;


        public bool IsFindSubFolder
        {
            get { return _isFindSubFolder; }
            set { _isFindSubFolder = value; RaisePropertyChanged("IsFindSubFolder"); }
        }
        private bool _isFindSubFolder = false;
        public FolderIOModel PathString
        {
            get { return _pathString; }
            set { _pathString = value; RaisePropertyChanged("PathString"); }
        }
        private FolderIOModel _pathString = new FolderIOModel();

        public ObservableCollection<FileIOModel> ImagePathList
        {
            get { return _imagePathList; }
            set { _imagePathList = value; RaisePropertyChanged("ImagePathList"); }
        }
        private ObservableCollection<FileIOModel> _imagePathList = new ObservableCollection<FileIOModel>();

        //public AddableComboViewModel AddableComboViewModel
        //{
        //    get { return _addableComboViewModel; }
        //    set { _addableComboViewModel = value; RaisePropertyChanged("AddableComboViewModel"); }
        //}
        //private AddableComboViewModel _addableComboViewModel = new AddableComboViewModel();

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
    }


}


