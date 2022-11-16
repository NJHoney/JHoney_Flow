using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHoney_Flow.Model
{
    class FileIOModel:BindableBase
    {
        public FileIOModel() { }
        public FileIOModel(string FileFullPath)
        {
            if (FileFullPath == "")
            {
                return;
            }
            FileName_Full = FileFullPath;
            MakeProperty();
        }


        #region ---［ 프로퍼티 ］---------------------------------------------------------------------
        /// <summary>
        /// Path+SafeFileName : C:\temp\temp.jpg
        /// </summary>
        public string FileName_Full
        {
            get { return _fileNameFull; }
            set { _fileNameFull = value; RaisePropertyChanged("FileNameFull"); }
        }
        private string _fileNameFull = "";

        /// <summary>
        /// Only Path : C:\temp\
        /// </summary>
        public string FileName_Path
        {
            get { return _fileNamePath; }
            set { _fileNamePath = value; RaisePropertyChanged("FileNamePath"); }
        }
        private string _fileNamePath = "";

        /// <summary>
        /// Only FileName + Extension : temp.jpg
        /// </summary>
        public string FileName_Safe
        {
            get { return _fileNameSafe; }
            set { _fileNameSafe = value; RaisePropertyChanged("FileNameSafe"); }
        }
        private string _fileNameSafe = "";

        /// <summary>
        /// Only FileName + Extension : temp
        /// </summary>
        public string FileName_OnlyName
        {
            get { return _fileName_OnlyName; }
            set { _fileName_OnlyName = value; RaisePropertyChanged("FileName_OnlyName"); }
        }
        private string _fileName_OnlyName = "";

        /// <summary>
        /// Extension : jpg
        /// </summary>
        public string FileName_Extension
        {
            get { return _fileName_Extension; }
            set { _fileName_Extension = value; RaisePropertyChanged("FileName_Extension"); }
        }
        private string _fileName_Extension = "";
        #endregion ---------------------------------------------------------------------------------

        #region ---［ Private 내부로직 ］---------------------------------------------------------------------
        private string GetOnlyPath(string FullName)
        {
            string OnlyPath = "";

            OnlyPath = FullName.Substring
                (
                0,
                FullName.LastIndexOf("\\") + 1
                );

            return OnlyPath;
        }

        private string GetSafeFileName(string FullName)
        {
            string SafeFileName = "";

            SafeFileName = FullName.Substring
                (
                FullName.LastIndexOf("\\") + 1,
                FullName.Length - FullName.LastIndexOf("\\") - 1
                );

            return SafeFileName;
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
        #endregion ---------------------------------------------------------------------------------

        public string GetDirectoryName(int Iteration)
        {
            string DirName = FileName_Path;
            DirName = DirName.Substring(0, DirName.Length - 1);

            for (int iLoopCount = 0; iLoopCount < Iteration; iLoopCount++)
            {

                DirName = DirName.Substring(0, DirName.LastIndexOf("\\"));
            }
            DirName = DirName.Substring(DirName.LastIndexOf("\\") + 1, DirName.Length - DirName.LastIndexOf("\\") - 1);

            return DirName;
        }
        public void MakeProperty()
        {
            FileName_Path = GetOnlyPath(FileName_Full);
            FileName_Safe = GetSafeFileName(FileName_Full);
            FileName_OnlyName = GetOnlyName(FileName_Full);
            FileName_Extension = GetExtension(FileName_Full);
        }
    }
}
