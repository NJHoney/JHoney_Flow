using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHoney_Flow.Model
{
    class FolderIOModel:BindableBase
    {
        public FolderIOModel() { }

        public FolderIOModel(string FullPath)
        {
            if (FullPath == "" || FullPath == null)
            {
                return;
            }
            MakeProperty(FullPath);
        }


        #region ---［ 프로퍼티 ］---------------------------------------------------------------------
        public bool IsNetworkFolder
        {
            get { return _isNetworkFolder; }
            set { _isNetworkFolder = value; RaisePropertyChanged("IsNetworkFolder"); }
        }
        private bool _isNetworkFolder = false;

        /// <summary>
        /// FullPath : C:\First\Second\
        /// </summary>
        public string FolderPath_Full
        {
            get { return _folderPath_Full; }
            set { _folderPath_Full = value; RaisePropertyChanged("FolderPath_Full"); }
        }
        private string _folderPath_Full = "";

        public string FolderPath_LastFolderName
        {
            get { return _folderPath_LastFolderName; }
            set { _folderPath_LastFolderName = value; RaisePropertyChanged("FolderPath_LastFolderName"); }
        }
        private string _folderPath_LastFolderName = "";

        public string FolderPath_ClassFolderName
        {
            get { return _folderPath_ClassFolderName; }
            set { _folderPath_ClassFolderName = value; RaisePropertyChanged("FolderPath_ClassFolderName"); }
        }
        private string _folderPath_ClassFolderName = "";

        public string SubClassName
        {
            get { return _subClassName; }
            set { _subClassName = value; RaisePropertyChanged("SubClassName"); }
        }
        private string _subClassName = "";

        public string Path_Root
        {
            get { return _path_Root; }
            set { _path_Root = value; RaisePropertyChanged("Path_Root"); }
        }
        private string _path_Root = "";

        public ObservableCollection<string> EachFolderName
        {
            get { return _eachFolderName; }
            set { _eachFolderName = value; RaisePropertyChanged("EachFolderName"); }
        }
        private ObservableCollection<string> _eachFolderName = new ObservableCollection<string>();
        #endregion ---------------------------------------------------------------------------------

        #region ---［ Private 내부로직 ］---------------------------------------------------------------------
        private string GetRootFolderName(string FullName)
        {
            string SafeFileName = "";

            if (FullName.Substring(0, 1) == "\\")
            {
                IsNetworkFolder = true;
            }
            else
            {
                IsNetworkFolder = false;
                Path_Root = FullName.Substring(0, FullName.IndexOf("\\") + 1);
            }

            return SafeFileName;
        }

        private void GetEachFolderName(string FullName)
        {
            EachFolderName.Clear();
            int Counter = 0;
            string FolderName = "";

            if (IsNetworkFolder)
            {

            }
            else
            {
                while (true)
                {
                    ++Counter;
                    FolderName = FindStr(FullName, "\\", Counter);
                    if (FolderName == "" || FolderName == null)
                    {
                        break;
                    }
                    EachFolderName.Add(FolderName);
                }
            }
        }

        private string GetOnlyName(string FullName)
        {
            string OnlyName = "";

            OnlyName = FullName.Substring
                (
                FullName.LastIndexOf("\\") + 1,
                FullName.LastIndexOf(".") - FullName.LastIndexOf("\\") - 1
                );

            return OnlyName;
        }

        private string GetExtension(string FullName)
        {
            string Extension = "";

            Extension = FullName.Substring
                (
                FullName.LastIndexOf(".") + 1,
                FullName.Length - FullName.LastIndexOf(".") - 1
                );

            return Extension;
        }

        string FindStr(string raw, string search, int repeat)
        {

            for (int iLoofCount = 0; iLoofCount < repeat; iLoofCount++)
            {
                if (raw.LastIndexOf(search) == -1)
                {
                    return "";
                }

                raw = raw.Substring(raw.IndexOf(search) + 1, raw.Length - raw.IndexOf(search) - 1);



            }
            if (raw.IndexOf(search) < 0)
            {
                return raw;
            }
            raw = raw.Substring(0, raw.IndexOf(search));

            return raw;
        }
        #endregion ---------------------------------------------------------------------------------

        public void SetFullPath(string FullPath)
        {
            MakeProperty(FullPath);
        }

        private void MakeProperty(string FullPath)
        {
            FolderPath_Full = FullPath;
            GetRootFolderName(FullPath);
            GetEachFolderName(FullPath);
            if (EachFolderName.Count > 0)
            {
                FolderPath_LastFolderName = EachFolderName[EachFolderName.Count - 1];
                FolderPath_ClassFolderName = FolderPath_LastFolderName;
                SubClassName = FolderPath_ClassFolderName;
            }

        }
    }
}
