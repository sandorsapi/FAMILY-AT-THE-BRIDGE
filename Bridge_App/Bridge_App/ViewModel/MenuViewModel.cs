using Bridge_App.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System;
using System.Collections.Generic;

namespace Bridge_App.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private OperatorViewModel operatorViewModel;
        private InformationInterfaceViewModel InformationInterfaceViewModel;
        private EvaluationAndSolution evaluationAndSolution;
        private FileReadOrWrite fileReadOrWrite;
        private int progressValue;
        private bool runEnabled;
        Thread th;

        private string solutionTextFileName = ConfigurationManager.AppSettings["solutionFileName"];

        public MenuViewModel(OperatorViewModel operatorViewModel, InformationInterfaceViewModel informationInterfaceViewModel)
        {
            this.operatorViewModel = operatorViewModel;
            this.InformationInterfaceViewModel = informationInterfaceViewModel;

            this.StopCommand = new RelayCommand(this.Stop);
            this.RunCommand = new RelayCommand(this.Run);
            this.evaluationAndSolution = new EvaluationAndSolution();

            this.SolutionTextReadFromFile();

            this.RunEnabled = true;
        }

        public RelayCommand StopCommand { get; private set; }

        //Threads stop
        private void Stop()
        {
            if (th != null)
            {
                th.Abort();
                this.operatorViewModel.ProgressBarThread.Abort();
                MessageBox.Show("Run abort", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                this.ProgressValue = 0;
            }          
        }

        public RelayCommand RunCommand { get; private set; }

        //Solution run
        private void Run()
        {
            fileReadOrWrite = new FileReadOrWrite(WriteToStringBuilder(this.operatorViewModel.People));

            this.evaluationAndSolution.BasedMembers.Clear();

            if (this.operatorViewModel.People.Count != 0)
            {
                foreach (var peopleItem in this.operatorViewModel.People)
                {
                    this.evaluationAndSolution.BasedMembers.Add(new People
                    {
                       movedTime = peopleItem.movedTime,
                       peopleName = peopleItem.peopleName
                    });
                }
            }

            th = new Thread(new ThreadStart(this.SolutionRun));
            th.Start();

            this.operatorViewModel.ProgressBarThread = new Thread(new ThreadStart(this.ProgressBarRun));
            this.operatorViewModel.ProgressBarThread.Start();
        }

        public int ProgressValue
        {
            get { return progressValue; }
            set { Set(nameof(ProgressValue), ref progressValue, value); }
        }

        public bool RunEnabled
        {
            get { return runEnabled; }
            set { Set(nameof(RunEnabled), ref runEnabled, value); }
        }

        //Solution run to thread
        private void SolutionRun()
        {
            this.evaluationAndSolution.Solution();            
            this.InformationInterfaceViewModel.SolutionText = this.evaluationAndSolution.SolutionText.ToString();
            if (this.evaluationAndSolution.SolutionText != null)
            {
                this.fileReadOrWrite.SolutionWriteInFile(this.evaluationAndSolution.SolutionText);
            }
            this.evaluationAndSolution.SolutionText.Clear();
            this.operatorViewModel.ProgressValue = 0;          
        }

        /// <summary>
        /// Write to file people list
        /// </summary>
        /// <param name="list"></param>
        /// <returns>StringBuilder text</returns>
        private StringBuilder WriteToStringBuilder(ObservableCollection<People> list)
        {
            StringBuilder sb = new StringBuilder();
            if (list != null)
            {
                sb.AppendLine("PEOPLE");
                foreach (var item in list)
                {
                    sb.AppendLine(item.peopleName + "," + item.movedTime);
                }
            }
            return sb;
        }

        public void ProgressBarRun()
        {
            this.ProgressValue = 0;
            for (int i = 0; i < 100; i++)
            {
                this.ProgressValue = i;
                Thread.Sleep(50);
            }
            this.ProgressValue = 0;
        }

        //Solution text write to file
        private void SolutionTextReadFromFile()
        {
            StringBuilder sb = new StringBuilder();

            FileInfo fi = new FileInfo(this.solutionTextFileName);

            this.fileReadOrWrite = new FileReadOrWrite();

            if (fi.Exists)
            {
                if (this.fileReadOrWrite.SolutionTextReadFromFile() != null)
                {
                    this.InformationInterfaceViewModel.SolutionText = this.fileReadOrWrite.SolutionTextReadFromFile().ToString();
                }
            }
        }
    }
}