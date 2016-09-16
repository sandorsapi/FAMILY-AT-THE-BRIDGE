using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;

namespace Bridge_App.ViewModel
{
    public class OperatorViewModel : ViewModelBase
    {
        private long peopleNumber;
        private int randomTime;
        private long progressValue = 0;
        private bool randomSelect = true;
        private bool manualSelect = false;
        private string addButtonHidden = "Hidden";
        private string randomButtonHidden;
        private string randomTextBoxVisibility;
        private bool peopleNumberTextFocus = true;
        private RandomNumberCreate rnd;
        private FileReadOrWrite fileReadOrWrite = new FileReadOrWrite();
        private Thread progressBarThread;

        private string peopleListFileName = ConfigurationManager.AppSettings["peopleListFileName"];        

        public ObservableCollection<People> People { get; private set; } = new ObservableCollection<People>();
        public List<long> peopleMovedTimeList = new List<long>();

        public OperatorViewModel()
        {
            this.rnd = new RandomNumberCreate();
            this.ManualAddCommand = new RelayCommand(ManualAdd);
            this.RandomAddCommand = new RelayCommand(RandomAdd);

            this.PeopleListReadFromFile();           
        }

        public long ProgressValue
        {
            get { return progressValue; }
            set { Set(nameof(ProgressValue), ref progressValue, value); }
        }

        public long PeopleNumber
        {
            get { return peopleNumber; }
            set { Set(nameof(PeopleNumber), ref peopleNumber, value); }
        }

        public int RandomTime
        {
            get { return randomTime; }
            set { Set(nameof(RandomTime), ref randomTime, value); }
        }

        public Thread ProgressBarThread
        {
            get { return progressBarThread; }
            set { progressBarThread = value; }
        }

        public bool RandomSelect
        {
            get { return randomSelect; }
            set
            {
                Set(nameof(RandomSelect), ref randomSelect, value);
                if (randomSelect)
                {
                    this.AddButtonVisibility = "Hidden";
                    this.RandomButtonVisibility = "Visible";
                    this.RandomTextBoxVisibility = "Visible";
                    this.PeopleNumberTextFocus = true;
                    this.RandomTime = 0;
                    this.PeopleNumber = 0;
                    this.peopleMovedTimeList.Clear();
                    this.People.Clear();
                }
                else
                {
                    this.RandomButtonVisibility = "Hidden";
                    this.AddButtonVisibility = "Visible";
                    this.RandomTextBoxVisibility = "Hidden";
                    this.PeopleNumberTextFocus = true;
                    this.RandomTime = 0;
                    this.PeopleNumber = 0;
                    this.peopleMovedTimeList.Clear();
                    this.People.Clear();
                }
            }
        }

        public bool ManualSelect
        {
            get { return manualSelect; }
            set
            {
                Set(nameof(ManualSelect), ref manualSelect, value);
            }
        }

        public string AddButtonVisibility
        {
            get { return addButtonHidden; }
            set { Set(nameof(AddButtonVisibility), ref addButtonHidden, value); }
        }

        public string RandomButtonVisibility
        {
            get { return randomButtonHidden; }
            set { Set(nameof(RandomButtonVisibility), ref randomButtonHidden, value); }
        }

        public string RandomTextBoxVisibility
        {
            get { return randomTextBoxVisibility; }
            set { Set(nameof(RandomTextBoxVisibility), ref randomTextBoxVisibility, value); }
        }

        public bool PeopleNumberTextFocus
        {
            get { return peopleNumberTextFocus; }
            set { Set(nameof(PeopleNumberTextFocus), ref peopleNumberTextFocus, value); }
        }

        public RelayCommand ManualAddCommand { get; private set; }

        //Fill in list manual number
        private void ManualAdd()
        {
            this.peopleMovedTimeList.Clear();
            if (this.PeopleNumber != 0)
            {
                if (!SameValue(this.PeopleNumber))
                {
                    this.peopleMovedTimeList.Add(this.PeopleNumber);
                    FillPepoleNameAndValue(this.PeopleNumber);
                }
                else
                {
                    this.PeopleNumber = 0;
                    MessageBox.Show("The same value!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            this.PeopleNumber = 0;
            this.PeopleNumberTextFocus = true;
        }

        public RelayCommand RandomAddCommand { get; private set; }

        //Fill in list random number
        private void RandomAdd()
        {
            this.peopleMovedTimeList.Clear();
            this.People.Clear();

            RandomTask();
            this.FillPeopleNameAndValueRandomData(this.peopleMovedTimeList);
        }

        private void RandomTask()
        {
            if (this.RandomTime < this.PeopleNumber * 3)
            {
                MessageBox.Show("The random time value is at least three times the number of people!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (this.PeopleNumber != 0 && this.RandomTime != 0)
            {
                foreach (var randomItem in this.rnd.FillListRandomNumber(this.peopleNumber, this.randomTime))
                {
                    this.peopleMovedTimeList.Add(randomItem);
                }
            }
            else
            {
                MessageBox.Show("The input text for field no value!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Checked same value
        private bool SameValue(long number)
        {
            return this.peopleMovedTimeList.Contains(number);
        }

        //Random name create
        private string RandomChar(long number)
        {
            StringBuilder sb = new StringBuilder();
            string[] letter = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            string convertNumber = number.ToString();
            int length = convertNumber.Length;
            for (int i = 0; i < length; i++)
            {
                var a = convertNumber.Substring(i, 1);
                int b = Convert.ToInt32(a);
                if (b == 0)
                {
                    sb.Append(letter[9]);
                }
                else
                {
                    sb.Append(letter[b - 1]);
                }
            }
            return sb.ToString();
        }

        //Manual fill to People list
        private void FillPepoleNameAndValue(long peopleNumber)
        {
            this.People.Add(new People
            {
                peopleName = RandomChar(peopleNumber),
                movedTime = peopleNumber
            });
        }

        //Random fill to People list
        private void FillPeopleNameAndValueRandomData(List<long> list)
        {
            this.People.Clear();

            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    this.People.Add(new People
                    {
                        peopleName = RandomChar(item),
                        movedTime = item
                    });
                }
            }
        }

        //People list read from file
        private void PeopleListReadFromFile()
        {
            FileInfo fi = new FileInfo(this.peopleListFileName);
            if (fi.Exists)
            {
                if (this.fileReadOrWrite.ReadFile().Count != 0)
                {
                    foreach (var item in this.fileReadOrWrite.ReadFile())
                    {
                        this.People.Add(new People
                        {
                            peopleName = item.peopleName,
                            movedTime = Convert.ToInt64(item.movedTime)
                        });
                    }
                }
            }
        }      
    }
}