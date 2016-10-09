using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;

namespace Bridge_App.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private OperatorViewModel operatorViewModel;
        private InformationInterfaceViewModel InformationInterfaceViewModel;
        private EvaluationAndSolution evaluationAndSolution;
        private FileReadOrWrite fileReadOrWrite;
        private int progressValue;
        private int progressBarMax;
        private bool runEnabled;
        private bool stopEnabled;

        private Thread solutionThread;
        private Thread progressBarThread;

        private string solutionTextFileName = ConfigurationManager.AppSettings["solutionFileName"];

        public MenuViewModel(OperatorViewModel operatorViewModel, InformationInterfaceViewModel informationInterfaceViewModel)
        {
            this.operatorViewModel = operatorViewModel;
            this.InformationInterfaceViewModel = informationInterfaceViewModel;

            this.StopCommand = new RelayCommand(this.Stop);
            this.RunCommand = new RelayCommand(this.Run);
            this.evaluationAndSolution = new EvaluationAndSolution();

            this.SolutionTextReadFromFile();

            this.progressBarMax = 100;

            this.evaluationAndSolution.PropertyChanged += (s, e) => this.ProgressValue = this.evaluationAndSolution.ProgressStep;
            this.operatorViewModel.PropertyChanged += (s, e) => this.RunEnabled = this.operatorViewModel.RunButtonEnabled;

            if (this.operatorViewModel.People.Count < 3)
            {
                this.RunEnabled = false;
            }
            else
            {
                this.RunEnabled = true;
            }

            this.StopEnabled = false;
        }

        public RelayCommand StopCommand { get; private set; }

        //Threads stop
        private void Stop()
        {
            this.solutionThread.Abort();
            this.progressBarThread.Abort();
            MessageBox.Show("Run abort", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            this.StopEnabled = false;
            this.ProgressValue = 0;
            this.evaluationAndSolution.ProgressStep = 0;
            this.RunEnabled = true;
        }

        public RelayCommand RunCommand { get; private set; }

        //Solution run
        private void Run()
        {
            if (this.operatorViewModel.People.Count >= 3)
            {
                this.StopEnabled = true;

                this.InformationInterfaceViewModel.SolutionText = null;

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

                this.solutionThread = new Thread(new ThreadStart(this.SolutionRun));
                this.progressBarThread = new Thread(new ThreadStart(this.ProgressBarRun));
                this.solutionThread.Start();
                this.progressBarThread.Start();
                this.RunEnabled = false;
                this.operatorViewModel.RunButtonEnabled = false;
            }
            else
            {
                MessageBox.Show("The process can not run!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public int ProgressValue
        {
            get { return progressValue; }
            set
            {
                Set(nameof(ProgressValue), ref progressValue, value);
                Thread.Sleep(50);
            }
        }

        public int ProgressBarMax
        {
            get { return progressBarMax; }
            set { Set(nameof(ProgressBarMax), ref progressBarMax, value); }
        }

        public bool RunEnabled
        {
            get { return runEnabled; }
            set { Set(nameof(RunEnabled), ref runEnabled, value); }
        }

        public bool StopEnabled
        {
            get { return stopEnabled; }
            set { Set(nameof(StopEnabled), ref stopEnabled, value); }
        }

        //Solution run to task
        private void SolutionRun()
        {
            this.evaluationAndSolution.Solution();
            this.InformationInterfaceViewModel.SolutionText = this.evaluationAndSolution.SolutionText.ToString();
            this.fileReadOrWrite.SolutionWriteInFile(this.evaluationAndSolution.SolutionText);
            this.evaluationAndSolution.SolutionText.Clear();
            this.operatorViewModel.ProgressValue = 0;
            this.StopEnabled = false;
            this.RunEnabled = true;
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
            this.ProgressBarMax = (this.evaluationAndSolution.BasedMembers.Count * 2) - 4;
        }

        //Solution text read from file
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