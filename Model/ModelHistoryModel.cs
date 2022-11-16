using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHoney_Flow.Model
{
    class ModelHistoryModel:BindableBase
    {

        public List<double> Loss
        {
            get { return _loss; }
            set { _loss = value; RaisePropertyChanged("Loss"); }
        }
        private List<double> _loss = new List<double>();

        public List<double> Val_Loss
        {
            get { return _val_Loss; }
            set { _val_Loss = value; RaisePropertyChanged("Val_Loss"); }
        }
        private List<double> _val_Loss = new List<double>();

        public List<double> Accuracy
        {
            get { return _accuracy; }
            set { _accuracy = value; RaisePropertyChanged("Accuracy"); }
        }
        private List<double> _accuracy = new List<double>();

        public List<double> Val_Accuracy
        {
            get { return _val_Accuracy; }
            set { _val_Accuracy = value; RaisePropertyChanged("Val_Accuracy"); }
        }
        private List<double> _val_Accuracy = new List<double>();

    }
}
