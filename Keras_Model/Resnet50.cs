using JHoney_Flow.Model;
using JHoney_Flow.ViewModel.RepoLv2;
using LiveCharts.Wpf;
using Numpy;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JHoney_Flow.Keras_Model
{
    class Resnet50:IDisposable
    {
        private bool IsInitialize = false;
        public TrainViewModel _trainviewmodel;
        public TestViewModel _testViewModel;
        public List<double> Val_LossList = new List<double>();
        public List<double> Val_AccList = new List<double>();
        int[] imageDims = new int[] { 224, 224, 3 };



        Stopwatch stopwatch = new Stopwatch();


        private void Initialize()
        {

            
            
            if(!IsInitialize)
            {
                string PYTHON_HOME = @"C:\Users\jona\AppData\Local\Programs\Python\Python38\";
                PythonEngine.PythonHome = PYTHON_HOME;
                PythonEngine.PythonPath += ";C:\\Users\\jona\\AppData\\Local\\Programs\\Python\\Python38\\Lib\\site-packages;";
                PythonEngine.PythonPath += "C:\\Program Files\\NVIDIA GPU Computing Toolkit\\CUDA\\v10.1\\;";
                PythonEngine.PythonPath += "C:\\Program Files\\NVIDIA GPU Computing Toolkit\\CUDA\\v10.1\\bin\\;";
                PythonEngine.PythonPath += "C:\\Program Files\\NVIDIA GPU Computing Toolkit\\CUDA\\v10.1\\bin\\libnvvp\\;";
            }
            

            PythonEngine.Initialize();
            if(!IsInitialize)
            {
                PythonEngine.RunSimpleString(@"import keras" + Environment.NewLine +
                "import gc" + Environment.NewLine +
                    "global TempTrainSavePath" + Environment.NewLine +
                    //"global TempTestSavePath" + Environment.NewLine +
                    "class CustomCallBack(keras.callbacks.Callback):" + Environment.NewLine +
                    "\tdef init(self):" + Environment.NewLine +
                    "\t\tpass" + Environment.NewLine +
                    "\tdef on_epoch_end(self, epoch, logs=None):" + Environment.NewLine +
                    "\t\tkeys = list(logs.keys())" + Environment.NewLine +
                    "\t\tf = open(TempTrainSavePath, 'w')" + Environment.NewLine +
                    "\t\tdata = str(epoch+1) + \", \" + str(logs['loss']) + \", \" + str(logs['val_accuracy'])" + Environment.NewLine +
                    "\t\tf.write(data)" + Environment.NewLine +
                    "\t\tf.close()" + Environment.NewLine +

                    "class TestCallback(keras.callbacks.Callback):" + Environment.NewLine +
                    "\tdef init(self):" + Environment.NewLine +
                    "\t\tpass" + Environment.NewLine +
                    "\tdef on_predict_batch_end(self, batch, logs=None):" + Environment.NewLine +
                    "\t\tf = open(TempTestSavePath + str(batch + 1) + '.txt', 'w')" + Environment.NewLine +
                    "\t\tdata = str(logs['outputs'][0])" + Environment.NewLine +
                    "\t\tf.write(data)" + Environment.NewLine +
                    "\t\tf.close()"
                    );
            }
            

            //string pythoncode = "gpus = keras.backend.tensorflow_backend.tf.config.experimental.list_physical_devices('GPU')"+ Environment.NewLine+
            //    "keras.backend.tensorflow_backend.tf.config.experimental.set_virtual_device_configuration(gpus[0], [keras.backend.tensorflow_backend.tf.config.experimental.VirtualDeviceConfiguration(memory_limit=4096)])"+ Environment.NewLine+
            //    "keras.backend.tensorflow_backend.tf.config.experimental.set_memory_growth(gpus[0], True)";
            //using (Py.GIL())
            //{
            //    PythonEngine.RunSimpleString(pythoncode);
            //}

            IsInitialize = true;
        }

        public void TrainNew_UsingGenerator(int img_rows, int img_cols, int img_ch, string DataSetRootPath, TrainSettingModel tsm, string PreTrained, float LearningRate = 0.001f, int epoch = 50, int minibatch = 16)
        {
            //if(!IsInitialize)
            //{
                Initialize();
            //}
            

            //Train Data
            DirectoryInfo di = new DirectoryInfo(DataSetRootPath + "\\Train\\");
            int ClassNum = di.GetDirectories().Count();
            _trainviewmodel.SendLog("", "Train Data Serialized");
            //Validation Data
            di = new DirectoryInfo(DataSetRootPath + "\\Validation\\");

            //Model Construct
            string pythoncode = "base_Model = keras.applications." + PreTrained + "(weights='imagenet', input_shape=(224,224,3), include_top=False)" + Environment.NewLine +
                "inputs = keras.Input(shape=(224,224,3))" + Environment.NewLine +
                "x = base_Model(inputs)" + Environment.NewLine +
                //Classifier
                "x = keras.layers.Flatten()(x)" + Environment.NewLine +
                "x = keras.layers.Dense(896, activation='relu')(x)" + Environment.NewLine +
                "x = keras.layers.BatchNormalization()(x)" + Environment.NewLine +
                "x = keras.layers.Dense(448, activation='relu')(x)" + Environment.NewLine +
                "x = keras.layers.BatchNormalization()(x)" + Environment.NewLine +
                "x = keras.layers.Dense(224, activation='relu')(x)" + Environment.NewLine +
                "x = keras.layers.BatchNormalization()(x)" + Environment.NewLine +
                "outputs = keras.layers.Dense(" + ClassNum + ", activation='softmax')(x)" + Environment.NewLine +
                "model = keras.Model(inputs, outputs)" + Environment.NewLine +
                "for layer in base_Model.layers:" + Environment.NewLine +
                "\tlayer.trainable = False";
            _trainviewmodel.SendLog("", "Model Constructing...");
            PythonEngine.RunSimpleString(pythoncode);
            //using (Py.GIL())
            //{
                
            //}
            _trainviewmodel.ProgressCounter();

            pythoncode = "trainDataGen = keras.preprocessing.image.ImageDataGenerator(rescale=1./255, horizontal_flip=True, vertical_flip=True, data_format='channels_last')" + Environment.NewLine +
                "trainData = trainDataGen.flow_from_directory(directory='" + DataSetRootPath + "/Train" + "', target_size=(224, 224), color_mode='rgb', batch_size=16, shuffle=True)";
            _trainviewmodel.SendLog("", "Train Data Setting");
            PythonEngine.RunSimpleString(pythoncode);
            //using (Py.GIL())
            //{
                
            //}
            pythoncode = "valDataGen = keras.preprocessing.image.ImageDataGenerator(rescale=1./255, data_format='channels_last')" + Environment.NewLine +
                "valData = valDataGen.flow_from_directory(directory='" + DataSetRootPath + "/Validation" + "', target_size=(224, 224), color_mode='rgb', batch_size=16, shuffle=True)";
            _trainviewmodel.SendLog("", "Validation Data Setting");
            PythonEngine.RunSimpleString(pythoncode);
            //using (Py.GIL())
            //{
                
            //}

            //pythoncode = "cb_checkpoint = keras.callbacks.ModelCheckpoint(filepath='" + DataSetRootPath + "/best.h5', monitor='val_accuracy', save_best_only=True)";
            //PythonEngine.RunSimpleString(pythoncode);
            //using (Py.GIL())
            //{
                
            //}
            pythoncode = "opt = keras.optimizers.Adam(lr=" + LearningRate + ", decay=" + LearningRate + " / " + epoch + ")";
            PythonEngine.RunSimpleString(pythoncode);
            //using (Py.GIL())
            //{
                
            //}
            pythoncode = "model.compile(optimizer=opt, loss=keras.losses.CategoricalCrossentropy(), metrics=['accuracy'])";
            PythonEngine.RunSimpleString(pythoncode);
            //using (Py.GIL())
            //{
                
            //}

            _trainviewmodel.SendLog("", "Train Start : First epoch takes some time...");

            double epochpertime = 0;
            double CurrentLoss = 0;
            double BestLoss = 100;
            double CurrentValAccuracy = 0;
            double BestValAccuracy = 0;
            double BestEpoch = 0;

            
            di = new DirectoryInfo(DataSetRootPath + "/models");
            if (!di.Exists)
            {
                di.Create();
            }
            di = new DirectoryInfo(DataSetRootPath + "\\TrainTemp");
            if(!di.Exists)
            {
                di.Create();
            }
            for (int epochCounter = 0; epochCounter<epoch;++epochCounter)
            {
                if (_trainviewmodel.StopSignal == "Stop")
                {
                    _trainviewmodel.SendLog("", "User Stop");
                    _trainviewmodel.CalcProgressBar(100, epoch, epoch);
                    break;
                }

                pythoncode = "TempTrainSavePath = '" + DataSetRootPath + "/TrainTemp/trainTemp" + (epochCounter+1) + ".txt'";
                PythonEngine.RunSimpleString(pythoncode);
                //using (Py.GIL())
                //{
                    
                //}

                pythoncode = "hist = model.fit_generator(trainData, epochs=1, validation_data=valData, callbacks=[CustomCallBack()])";
                PythonEngine.RunSimpleString(pythoncode);
                //using (Py.GIL())
                //{
                    
                //}

                string[] result = new string[3];
                while (!CanFileAccess(DataSetRootPath + "/TrainTemp/trainTemp" + (epochCounter + 1) + ".txt"))
                {
                    Thread.Sleep(10);
                }
                
                    result = File.ReadAllText(DataSetRootPath + "/TrainTemp/trainTemp" + (epochCounter + 1) + ".txt").Split(',');
                    File.Delete(DataSetRootPath + "/TrainTemp/trainTemp" + (epochCounter + 1) + ".txt");
                
                    stopwatch.Stop();
                    if (epochpertime == 0)
                    {
                        epochpertime = double.Parse(stopwatch.ElapsedMilliseconds.ToString());
                    }
                    else
                    {
                        epochpertime += double.Parse(stopwatch.ElapsedMilliseconds.ToString());
                        epochpertime /= 2;
                        _trainviewmodel.CalcProgressBar(epochpertime, epochCounter, epoch);
                    }
                
                if (epochCounter > 1)
                {
                    stopwatch.Restart();
                }

                CurrentLoss = double.Parse(result[1]);
                CurrentValAccuracy = double.Parse(result[2]);

                if (BestValAccuracy < CurrentValAccuracy)
                {
                    BestEpoch = epochCounter;
                    BestLoss = CurrentLoss;
                    BestValAccuracy = CurrentValAccuracy;
                    if(File.Exists(DataSetRootPath + "/TrainTemp/models/best.h5"))
                    {
                        while (!CanFileAccess(DataSetRootPath + "/TrainTemp/models/best.h5"))
                        {
                            Thread.Sleep(10);
                        }
                    }
                    pythoncode = "model.save('" + DataSetRootPath + "/models/best.h5" + "', overwrite=True)";
                    PythonEngine.RunSimpleString(pythoncode);
                    //using (Py.GIL())
                    //{
                        
                    //}

                    _trainviewmodel.SendLog("All", "Updated Best Model!");
                }

                Thread thread = new Thread(() =>
                {
                    _trainviewmodel.ChartValue1.Add((double)CurrentLoss);
                    _trainviewmodel.ChartValue_ValAcc.Add((double)CurrentValAccuracy);
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();

                _trainviewmodel.SendLog("All", "-----------------------------------------------");
                _trainviewmodel.SendLog("All", "best epoch : " + BestEpoch + "  Best Validation Accuracy : " + BestValAccuracy);
                _trainviewmodel.SendLog("All", "-----------------------------------------------");
                _trainviewmodel.SendLog("All", "Train-Loss : " + CurrentLoss);
                _trainviewmodel.SendLog("All", "Val-Accuracy : " + CurrentValAccuracy);
                _trainviewmodel.SendLog("All", "Epoch ( " + (epochCounter+1) + " / " + epoch + " )");
                _trainviewmodel.SendLog("All", "-----------------------------------------------");
            }
            // save the model
            if (File.Exists(DataSetRootPath + "\\Models\\best.h5"))
            {
                string ThisTime = DateTime.Now.Year.ToString() + "_" +
                    DateTime.Now.Month.ToString() + "_" +
                    DateTime.Now.Day.ToString() + "_" +
                    DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                File.Move(DataSetRootPath + "\\Models\\best.h5", DataSetRootPath + "\\Models\\" + PreTrained + "_" + ThisTime + ".h5");
            }

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                _trainviewmodel.ModelListViewModel.LoadModelListCurrent.Clear();
                _trainviewmodel.ModelListViewModel.LoadModelListAll.Clear();
                _trainviewmodel.ModelListViewModel.SelectNumPageList.Clear();
                _trainviewmodel.ModelListViewModel.AddFileThreadMethod(DataSetRootPath + "\\Models");

                _trainviewmodel.ModelListViewModel.PageListExtract("");
                _trainviewmodel.ModelListViewModel.ListNumRefresh();
            }));

            _trainviewmodel.CalcProgressBar(100, epoch, epoch);
            _trainviewmodel.SendLog("", "Train Complate");
            _trainviewmodel.IsTraining = false;
            _trainviewmodel.DataListRootPathViewModel.IsTraining = false;
            _trainviewmodel.StopSignal = "";

            pythoncode = "keras.backend.tensorflow_backend.clear_session()" + Environment.NewLine+
                "gc.collect()";
            PythonEngine.RunSimpleString(pythoncode);
            //using (Py.GIL())
            //{
                
            //}
            this.Dispose();
        }

        public void TrainNew_UsingGenerator_Backup(int img_rows, int img_cols, int img_ch, string DataSetRootPath, TrainSettingModel tsm, string PreTrained, float LearningRate = 0.001f, int epoch = 50, int minibatch = 16)
        {
            Initialize();

            //Train Data
            DirectoryInfo di = new DirectoryInfo(DataSetRootPath + "\\Train\\");
            int ClassNum = di.GetDirectories().Count();
            _trainviewmodel.SendLog("", "Train Data Serialized");
            //Validation Data
            di = new DirectoryInfo(DataSetRootPath + "\\Validation\\");

            //Model Construct
            string pythoncode = "base_Model = keras.applications." + PreTrained + "(weights='imagenet', input_shape=(224,224,3), include_top=False)" + Environment.NewLine +
                "inputs = keras.Input(shape=(224,224,3))" + Environment.NewLine +
                "x = base_Model(inputs)" + Environment.NewLine +
                "x = keras.layers.GlobalAveragePooling2D()(x)" + Environment.NewLine +
                "outputs = keras.layers.Dense(" + ClassNum + ", activation='softmax')(x)" + Environment.NewLine +
                "model = keras.Model(inputs, outputs)" + Environment.NewLine +
                "for layer in base_Model.layers:" + Environment.NewLine +
                "\tlayer.trainable = False";
            _trainviewmodel.SendLog("", "Model Constructing...");
            using (Py.GIL())
            {
                PythonEngine.RunSimpleString(pythoncode);
            }
            _trainviewmodel.ProgressCounter();

            pythoncode = "trainDataGen = keras.preprocessing.image.ImageDataGenerator(rescale=1./255, horizontal_flip=True, vertical_flip=True, data_format='channels_last')" + Environment.NewLine +
                "trainData = trainDataGen.flow_from_directory(directory='" + DataSetRootPath + "/Train" + "', target_size=(224, 224), color_mode='rgb', batch_size=16, shuffle=True)";
            _trainviewmodel.SendLog("", "Train Data Setting");
            using (Py.GIL())
            {
                PythonEngine.RunSimpleString(pythoncode);
            }
            pythoncode = "valDataGen = keras.preprocessing.image.ImageDataGenerator(rescale=1./255, horizontal_flip=True, vertical_flip=True, data_format='channels_last')" + Environment.NewLine +
                "valData = valDataGen.flow_from_directory(directory='" + DataSetRootPath + "/Validation" + "', target_size=(224, 224), color_mode='rgb', batch_size=16, shuffle=True)";
            _trainviewmodel.SendLog("", "Validation Data Setting");
            using (Py.GIL())
            {
                PythonEngine.RunSimpleString(pythoncode);
            }

            pythoncode = "cb_checkpoint = keras.callbacks.ModelCheckpoint(filepath='" + DataSetRootPath + "/best.h5', monitor='val_accuracy', save_best_only=True)";
            using (Py.GIL())
            {
                PythonEngine.RunSimpleString(pythoncode);
            }
            pythoncode = "opt = keras.optimizers.Adam(lr=" + LearningRate + ", decay=" + LearningRate + " / " + epoch + ")";
            using (Py.GIL())
            {
                PythonEngine.RunSimpleString(pythoncode);
            }
            pythoncode = "model.compile(optimizer=opt, loss=keras.losses.CategoricalCrossentropy(), metrics=['accuracy'])";
            using (Py.GIL())
            {
                PythonEngine.RunSimpleString(pythoncode);
            }

            _trainviewmodel.SendLog("", "Train Start : First epoch takes some time...");

            double epochpertime = 0;

            Thread trainThread = new Thread(() =>
            {
                int preepoch = 0;
                int currentepoch = 0;
                int nextepoch = 1;
                double CurrentLoss = 0;
                double BestLoss = 100;
                double CurrentValAccuracy = 0;
                double BestValAccuracy = 100;
                double BestEpoch = 0;
                while (true)
                {
                    if (_trainviewmodel.StopSignal == "Stop")
                    {
                        _trainviewmodel.SendLog("", "User Stop");
                        _trainviewmodel.CalcProgressBar(100, epoch, epoch);

                        PythonEngine.Shutdown();
                        break;
                    }
                    di = new DirectoryInfo(@"C:\");

                    if (di.GetFiles("JHoneyTemp*", SearchOption.TopDirectoryOnly).Count() < 1)
                    {
                        continue;
                    }
                    string[] result = new string[3];
                    if (CanFileAccess(@"C:\JHoneyTemp" + nextepoch + ".txt"))
                    {
                        result = File.ReadAllText(@"C:\JHoneyTemp" + nextepoch + ".txt").Split(',');
                        File.Delete(@"C:\JHoneyTemp" + nextepoch + ".txt");
                    }
                    else
                    {
                        continue;
                    }

                    if (int.Parse(result[0]) == nextepoch)
                    {
                        ++preepoch;
                        ++currentepoch;
                        ++nextepoch;
                        stopwatch.Stop();
                        if (epochpertime == 0)
                        {
                            epochpertime = double.Parse(stopwatch.ElapsedMilliseconds.ToString());
                        }
                        else
                        {
                            epochpertime += double.Parse(stopwatch.ElapsedMilliseconds.ToString());
                            epochpertime /= 2;
                            _trainviewmodel.CalcProgressBar(epochpertime, currentepoch, epoch);
                        }
                    }
                    else
                    {
                        continue;
                    }

                    if (currentepoch > 1)
                    {
                        stopwatch.Restart();
                    }

                    CurrentLoss = double.Parse(result[1]);
                    CurrentValAccuracy = double.Parse(result[2]);

                    if (BestValAccuracy < CurrentValAccuracy)
                    {
                        BestEpoch = currentepoch;
                        BestLoss = CurrentLoss;
                        BestValAccuracy = CurrentValAccuracy;
                        _trainviewmodel.SendLog("All", "Updated Best Model!");

                    }

                    Thread thread = new Thread(() =>
                    {
                        _trainviewmodel.ChartValue1.Add((double)CurrentLoss);
                        _trainviewmodel.ChartValue_ValAcc.Add((double)CurrentValAccuracy);
                    });
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();

                    _trainviewmodel.SendLog("All", "-----------------------------------------------");
                    _trainviewmodel.SendLog("All", "best epoch : " + BestEpoch + "  Best Validation Accuracy : " + BestValAccuracy);
                    _trainviewmodel.SendLog("All", "-----------------------------------------------");
                    _trainviewmodel.SendLog("All", "Train-Loss : " + CurrentLoss);
                    _trainviewmodel.SendLog("All", "Val-Accuracy : " + CurrentValAccuracy);
                    _trainviewmodel.SendLog("All", "Epoch ( " + (currentepoch) + " / " + epoch + " )");
                    _trainviewmodel.SendLog("All", "-----------------------------------------------");
                }
                // save the model
                if (File.Exists(DataSetRootPath + "\\Models\\best.h5"))
                {
                    string ThisTime = DateTime.Now.Year.ToString() + "_" +
                        DateTime.Now.Month.ToString() + "_" +
                        DateTime.Now.Day.ToString() + "_" +
                        DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    File.Move(DataSetRootPath + "\\Models\\best.h5", DataSetRootPath + "\\Models\\" + PreTrained + "_" + ThisTime + ".h5");
                }

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    _trainviewmodel.ModelListViewModel.LoadModelListCurrent.Clear();
                    _trainviewmodel.ModelListViewModel.LoadModelListAll.Clear();
                    _trainviewmodel.ModelListViewModel.SelectNumPageList.Clear();
                    _trainviewmodel.ModelListViewModel.AddFileThreadMethod(DataSetRootPath + "\\Models");

                    _trainviewmodel.ModelListViewModel.PageListExtract("");
                    _trainviewmodel.ModelListViewModel.ListNumRefresh();
                }));

                _trainviewmodel.SendLog("", "Train Complate");
                _trainviewmodel.IsTraining = false;
                _trainviewmodel.DataListRootPathViewModel.IsTraining = false;
                _trainviewmodel.StopSignal = "";

            });
            trainThread.Start();

            pythoncode = "hist = model.fit_generator(trainData, epochs=" + epoch + ", validation_data=valData, callbacks=[cb_checkpoint, CustomCallBack()])";
            using (Py.GIL())
            {
                PythonEngine.RunSimpleString(pythoncode);
            }

        }

        bool CanFileAccess(string FilePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch
            {
                _trainviewmodel.SendLog("", "Waiting for save model");
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return true;
        }

        public void Evaluation(int img_rows, int img_cols, int img_ch, string DataSetRootPath, string SelectedModelPath, bool UsingTrainSet, bool UsingValSet, bool UsingTestSet, int minibatch = 16)
        {
            Initialize();
            List<(int, int, int)> resultList = new List<(int, int, int)>();
            DirectoryInfo di = new DirectoryInfo(DataSetRootPath + "/TestTemp/");
            if(!di.Exists)
            {
                di.Create();
            }
            List<string> classes = new List<string>();

            _testViewModel.ProgressMax = 100;
            _testViewModel.ProgressNext = 0;
            _testViewModel.ProgressValue = 0;
            _testViewModel.ProgressPercent = 0f;
            

            _testViewModel.SendLog("", "Model Construct");
            string pythoncode = "model = keras.models.load_model('" + SelectedModelPath + "')";
            PythonEngine.RunSimpleString(pythoncode);
            //using (Py.GIL())
            //{
                
            //}

            _testViewModel.SendLog("", "Set SavePath");
            pythoncode = "TempTestSavePath = '" + DataSetRootPath + "/TestTemp/'";
            PythonEngine.RunSimpleString(pythoncode);
            //using (Py.GIL())
            //{
                
            //}

            _testViewModel.SendLog("", "Make Data Gen");
            pythoncode = "testDataGen = keras.preprocessing.image.ImageDataGenerator(rescale=1./255)";
            PythonEngine.RunSimpleString(pythoncode);
            //using (Py.GIL())
            //{
                
            //}

            if (UsingTrainSet)
            {
                pythoncode = "testData = testDataGen.flow_from_directory(directory='" + DataSetRootPath + "\\Train" + "', target_size=(224, 224), color_mode='rgb', batch_size=1, shuffle=False)";
                PythonEngine.RunSimpleString(pythoncode);
                //using (Py.GIL())
                //{
                    
                //}
                di = new DirectoryInfo(DataSetRootPath + "\\Train");
                var dirs = Directory.GetDirectories(DataSetRootPath + "\\Train");

                for (int iLoopCount = 0; iLoopCount < di.GetDirectories().Count(); iLoopCount++)
                {
                    classes.Add(di.GetDirectories()[iLoopCount].Name);
                }
                _testViewModel.ProgressMax = di.GetFiles("*", SearchOption.AllDirectories).Count();
                for (int iLoopCount = 0; iLoopCount < di.GetFiles("*", SearchOption.AllDirectories).Count(); iLoopCount++)
                {
                    _testViewModel.ProgressValue = iLoopCount+1;
                    _testViewModel.ProgressPercent = (float)_testViewModel.ProgressValue / (float)_testViewModel.ProgressMax;
                    pythoncode = "hist = model.predict(testData[" + iLoopCount + "][0])";
                    PythonEngine.RunSimpleString(pythoncode);
                    //using (Py.GIL())
                    //{
                        
                    //}
                    pythoncode = "f = open(TempTestSavePath + str(" + iLoopCount + ") + '.txt', 'w')" + Environment.NewLine +
                        "data = str(hist)" + Environment.NewLine +
                        "f.write(data)" + Environment.NewLine +
                        "f.close()";
                    PythonEngine.RunSimpleString(pythoncode);
                    //using (Py.GIL())
                    //{
                        
                    //}
                    while (!CanFileAccess(DataSetRootPath + "/TestTemp/" + iLoopCount + ".txt"))
                    {
                        Thread.Sleep(5);
                    }
                    string[] result = new string[classes.Count];
                    double[] resultParse = new double[classes.Count];
                    result = File.ReadAllText(DataSetRootPath + "/TestTemp/" + (iLoopCount) + ".txt").Replace("[", "").Replace("]", "").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var maximum = result.Max(x => double.Parse(x));

                    for (int jLoopCount = 0; jLoopCount < result.Count(); jLoopCount++)
                    {
                        resultParse[jLoopCount] = double.Parse(result[jLoopCount]);
                    }

                    var clsNum = Array.IndexOf(resultParse, maximum);

                    int ActualCount = Array.IndexOf(dirs, di.GetFiles("*", SearchOption.AllDirectories)[iLoopCount].Directory.FullName);
                    File.Delete(DataSetRootPath + "/TestTemp/" + iLoopCount + ".txt");
                    _testViewModel.SendLog("", di.GetFiles("*", SearchOption.AllDirectories)[iLoopCount].Name +
                        " : Predict : " + di.GetDirectories()[clsNum] + "\t Score : " + maximum + "\t Actual : " + di.GetDirectories()[ActualCount]);
                    resultList.Add((ActualCount, clsNum, 0));//actual/predict/0
                }
            }
            
            if (UsingValSet)
            {
                pythoncode = "testData = testDataGen.flow_from_directory(directory='" + DataSetRootPath + "\\Validation" + "', target_size=(224, 224), color_mode='rgb', batch_size=1, shuffle=False)";
                PythonEngine.RunSimpleString(pythoncode);
                //using (Py.GIL())
                //{
                    
                //}
                di = new DirectoryInfo(DataSetRootPath + "\\Validation");
                var dirs = Directory.GetDirectories(DataSetRootPath + "\\Validation");
                if(classes.Count==0)
                {
                    for (int iLoopCount = 0; iLoopCount < di.GetDirectories().Count(); iLoopCount++)
                    {
                        classes.Add(di.GetDirectories()[iLoopCount].Name);
                    }
                }
                _testViewModel.ProgressMax = di.GetFiles("*", SearchOption.AllDirectories).Count();
                for (int iLoopCount = 0; iLoopCount < di.GetFiles("*", SearchOption.AllDirectories).Count(); iLoopCount++)
                {
                    _testViewModel.ProgressValue = iLoopCount+1;
                    _testViewModel.ProgressPercent = (float)_testViewModel.ProgressValue / (float)_testViewModel.ProgressMax;
                    pythoncode = "hist = model.predict(testData[" + iLoopCount + "][0])";
                    PythonEngine.RunSimpleString(pythoncode);
                    //using (Py.GIL())
                    //{
                        
                    //}
                    pythoncode = "f = open(TempTestSavePath + str(" + iLoopCount + ") + '.txt', 'w')" + Environment.NewLine +
                        "data = str(hist)" + Environment.NewLine +
                        "f.write(data)" + Environment.NewLine +
                        "f.close()";
                    PythonEngine.RunSimpleString(pythoncode);
                    //using (Py.GIL())
                    //{
                        
                    //}
                    while (!CanFileAccess(DataSetRootPath + "/TestTemp/" + iLoopCount + ".txt"))
                    {
                        Thread.Sleep(5);
                    }
                    string[] result = new string[classes.Count];
                    double[] resultParse = new double[classes.Count];
                    result = File.ReadAllText(DataSetRootPath + "/TestTemp/" + (iLoopCount) + ".txt").Replace("[", "").Replace("]", "").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var maximum = result.Max(x => double.Parse(x));

                    for (int jLoopCount = 0; jLoopCount < result.Count(); jLoopCount++)
                    {
                        resultParse[jLoopCount] = double.Parse(result[jLoopCount]);
                    }

                    var clsNum = Array.IndexOf(resultParse, maximum);

                    int ActualCount = Array.IndexOf(dirs, di.GetFiles("*", SearchOption.AllDirectories)[iLoopCount].Directory.FullName);
                    File.Delete(DataSetRootPath + "/TestTemp/" + iLoopCount + ".txt");
                    _testViewModel.SendLog("", di.GetFiles("*", SearchOption.AllDirectories)[iLoopCount].Name +
                        " : Predict : " + di.GetDirectories()[clsNum] + "\t Score : " + maximum + "\t Actual : " + di.GetDirectories()[ActualCount]);
                    resultList.Add((ActualCount, clsNum, 0));//actual/predict/0
                }
            }

            if (UsingTestSet)
            {
                pythoncode = "testData = testDataGen.flow_from_directory(directory='" + DataSetRootPath + "\\Test" + "', target_size=(224, 224), color_mode='rgb', batch_size=1, shuffle=False)";
                PythonEngine.RunSimpleString(pythoncode);
                //using (Py.GIL())
                //{
                    
                //}
                di = new DirectoryInfo(DataSetRootPath + "\\Test");
                var dirs = Directory.GetDirectories(DataSetRootPath + "\\Test");
                if (classes.Count == 0)
                {
                    for (int iLoopCount = 0; iLoopCount < di.GetDirectories().Count(); iLoopCount++)
                    {
                        classes.Add(di.GetDirectories()[iLoopCount].Name);
                    }
                }
                _testViewModel.ProgressMax = di.GetFiles("*", SearchOption.AllDirectories).Count();
                for (int iLoopCount = 0; iLoopCount < di.GetFiles("*", SearchOption.AllDirectories).Count(); iLoopCount++)
                {
                    _testViewModel.ProgressValue = iLoopCount+1;
                    _testViewModel.ProgressPercent = (float)_testViewModel.ProgressValue / (float)_testViewModel.ProgressMax;
                    pythoncode = "hist = model.predict(testData[" + iLoopCount + "][0])";
                    PythonEngine.RunSimpleString(pythoncode);
                    //using (Py.GIL())
                    //{
                        
                    //}
                    pythoncode = "f = open(TempTestSavePath + str(" + iLoopCount + ") + '.txt', 'w')" + Environment.NewLine +
                        "data = str(hist)" + Environment.NewLine +
                        "f.write(data)" + Environment.NewLine +
                        "f.close()";
                    PythonEngine.RunSimpleString(pythoncode);
                    //using (Py.GIL())
                    //{
                        
                    //}
                    
                    while (!CanFileAccess(DataSetRootPath + "/TestTemp/" + iLoopCount + ".txt"))
                    {
                        Thread.Sleep(5);
                    }
                    string[] result = new string[classes.Count];
                    double[] resultParse = new double[classes.Count];
                    result = File.ReadAllText(DataSetRootPath + "/TestTemp/" + (iLoopCount) + ".txt").Replace("[", "").Replace("]", "").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var maximum = result.Max(x => double.Parse(x));

                    for (int jLoopCount = 0; jLoopCount < result.Count(); jLoopCount++)
                    {
                        resultParse[jLoopCount] = double.Parse(result[jLoopCount]);
                    }

                    var clsNum = Array.IndexOf(resultParse, maximum);

                    int ActualCount = Array.IndexOf(dirs, di.GetFiles("*", SearchOption.AllDirectories)[iLoopCount].Directory.FullName);
                    File.Delete(DataSetRootPath + "/TestTemp/" + iLoopCount + ".txt");
                    _testViewModel.SendLog("", di.GetFiles("*", SearchOption.AllDirectories)[iLoopCount].Name +
                        " : Predict : " + di.GetDirectories()[clsNum] + "\t Score : " + maximum + "\t Actual : " + di.GetDirectories()[ActualCount]);
                    resultList.Add((ActualCount, clsNum, 0));//actual/predict/0
                }
            }
            _testViewModel.MakeConfusionMatrix(new List<string>(classes),new List<(int, int, int)>(resultList));
            _testViewModel.DataListRootPathViewModel.IsTraining = false;
            _testViewModel.IsTraining = false;

            pythoncode = "del hist" + Environment.NewLine +
                "del model" + Environment.NewLine +
                "del testData" + Environment.NewLine +
                "keras.backend.tensorflow_backend.clear_session()" + Environment.NewLine +
                "gc.collect()";
            PythonEngine.RunSimpleString(pythoncode);
            //using (Py.GIL())
            //{
                
            //}
            this.Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        
    }
}
