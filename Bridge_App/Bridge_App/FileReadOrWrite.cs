using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using static People.People;

namespace Bridge_App
{
    public class FileReadOrWrite
    {
        private StringBuilder peopleListText;
        private string peopleListFileName = ConfigurationManager.AppSettings["peopleListFileName"];
        private string solutionTextFileName = ConfigurationManager.AppSettings["solutionFileName"];

        public FileReadOrWrite()
        {
        }

        public FileReadOrWrite(StringBuilder peopleListText)
        {
            this.peopleListText = peopleListText;
            ListWriteToFile(this.peopleListText);
        }

        //People list to file
        private void ListWriteToFile(StringBuilder peopleListText)
        {
            FileInfo fi = new FileInfo(this.peopleListFileName);
            if (fi.Exists)
            {
                fi.Delete();
            }
            using (StreamWriter write = fi.AppendText())
            {
                write.Write(peopleListText);
            }
        }

        /// <summary>
        /// File people list from file
        /// </summary>
        /// <returns>ObservableCollection<People></returns>
        public ObservableCollection<Peoples> ReadFile()
        {
            ObservableCollection<Peoples> people = new ObservableCollection<Peoples>();
            string[] data = null;
            string[] dataRow = null;
            string[] separator = { "," };
            int i = 1;
            FileInfo fi = new FileInfo(this.peopleListFileName);
            if (fi.Exists)
            {
                dataRow = System.IO.File.ReadAllLines(this.peopleListFileName);
                if (dataRow[0].Equals("PEOPLE"))
                {
                    while (i != dataRow.Length)
                    {
                        data = dataRow[i].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        people.Add(new Peoples
                        {
                            peopleName = data[0],
                            movedTime = Convert.ToInt64(data[1])
                        });
                        i++;
                    }
                }
                else
                {
                    MessageBox.Show("File error in data!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return people;
        }

        //Solution text write to file
        public void SolutionWriteInFile(StringBuilder solutionText)
        {
            FileInfo fi = new FileInfo(this.solutionTextFileName);
            if (fi.Exists)
            {
                fi.Delete();
            }
            using (StreamWriter write = fi.AppendText())
            {
                write.Write(solutionText);
            }
        }

        /// <summary>
        /// Solution text read from  file
        /// </summary>
        /// <returns>StringBuilder text</returns>
        public StringBuilder SolutionTextReadFromFile()
        {
            StringBuilder sb = new StringBuilder();

            string[] dataRow = null;

            FileInfo fi = new FileInfo(this.peopleListFileName);
            if (fi.Exists)
            {
                dataRow = System.IO.File.ReadAllLines(this.solutionTextFileName);
                if (dataRow.Length != 0)
                {
                    for (int i = 0; i < dataRow.Length; i++)
                    {
                        sb.AppendLine(dataRow[i]);
                    }
                }                
            }
            else
            {
                MessageBox.Show("File error in data!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return sb;
        }
    }
}