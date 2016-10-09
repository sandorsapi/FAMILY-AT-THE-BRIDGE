using Bridge_App.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Bridge_App
{
    public class EvaluationAndSolution : INotifyPropertyChanged, IAlgoritm
    {
        private readonly IAlgoritm _IAlgoritm;

        private long runTime = 0;
        private long step = 1;
        private int progressStep = 0;
        private string methodName;
        private StringBuilder solutionText = new StringBuilder();
        private List<People> basedMembers = new List<People>();
        private List<People> rightMembers = new List<People>();
        private List<People> leftMembers = new List<People>();

        private List<Step> stepping = new List<Step>();

        public event PropertyChangedEventHandler PropertyChanged;

        public EvaluationAndSolution(IAlgoritm iAlgoritm)
        {
            this._IAlgoritm = iAlgoritm;
        }

        public EvaluationAndSolution()
        {
        }

        public void ProgressChange(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public long RunTime
        {
            get { return runTime; }
            set { runTime = value; }
        }

        public List<People> BasedMembers
        {
            get { return basedMembers; }
            set { basedMembers = value; }
        }

        public List<People> RightMembers
        {
            get { return rightMembers; }
            set { rightMembers = value; }
        }

        public List<People> LeftMembers
        {
            get { return leftMembers; }
            set { leftMembers = value; }
        }

        public StringBuilder SolutionText
        {
            get { return solutionText; }
            set { solutionText = value; }
        }

        public int ProgressStep
        {
            get { return progressStep; }
            set
            {
                progressStep = value;
                this.ProgressChange("ProgressStep");
            }
        }

        public string MethodName
        {
            get { return methodName; }
            set { methodName = value; }
        }

        public List<Step> Stepping
        {
            get { return stepping; }
            set { stepping = value; }
        }

        //Optimalization
        public void Solution()
        {
            this.solutionText.Clear();

            this.Solv(this.basedMembers);
            foreach (var stepItem in this.Stepping)
            {
                if (stepItem.way.Equals("There"))
                {
                    this.SolutionText.AppendLine(string.Format("{0}: {1}({2}) and {3}({4}) - Moved time: {5} - Run time {6}", stepItem.way, stepItem.moved_2, stepItem.time_2, stepItem.moved_1, stepItem.time_1, stepItem.time_2, stepItem.runTime));
                }
                else
                {
                    this.SolutionText.AppendLine(string.Format("{0}: {1}({2}) - Moved time: {3} - Run time {4}", stepItem.way, stepItem.moved_1, stepItem.time_1, stepItem.time_1, stepItem.runTime));
                }
            }
            this.solutionText.AppendLine(string.Format("Run time: {0}s", this.runTime));

            this.Stepping.Clear();
            this.runTime = 0;
            this.step = 1;
            this.ProgressStep = 0;
            this.leftMembers.Clear();
            this.rightMembers.Clear();
        }

        //The best speed people return moved result
        private void ReturnMoved(List<People> leftMembers, List<People> rightMembers)
        {
            long time = 0;
            string name = null;
            rightMembers.Add(new People
            {
                movedTime = leftMembers.Min(m => m.movedTime),
                peopleName = leftMembers.Find(f => f.movedTime == leftMembers.Min(m => m.movedTime)).peopleName
            });
            time = leftMembers.Min(m => m.movedTime);
            name = leftMembers.Find(f => f.movedTime == time).peopleName;
            this.runTime = this.runTime + leftMembers.Min(m => m.movedTime);
            Stepping.Add(new Step
            {
                way = "Back",
                time_1 = time,
                time_2 = 0,
                moved_1 = name,
                moved_2 = null,
                runTime = this.runTime
            });
            var a = leftMembers.FindIndex(f => f.movedTime == time);
            leftMembers.RemoveAt(a);
        }

        //Find the lowest values
        private void MovedTwoMemberMin(List<People> leftMembers, List<People> rightMembers)
        {
            try
            {
                long time1 = 0;
                long time2 = 0;

                string name1 = null;
                string name2 = null;

                for (int k = 0; k < 2; k++)
                {
                    if (k == 0)
                    {
                        leftMembers.Add(new People
                        {
                            movedTime = rightMembers.Min(m => m.movedTime),
                            peopleName = rightMembers.Find(f => f.movedTime == rightMembers.Min(m => m.movedTime)).peopleName
                        });
                        time1 = rightMembers.Min(m => m.movedTime);
                        name1 = rightMembers.Find(f => f.movedTime == time1).peopleName;
                        var a = rightMembers.FindIndex(f => f.movedTime == time1);
                        rightMembers.RemoveAt(a);
                    }
                    else
                    {
                        leftMembers.Add(new People
                        {
                            movedTime = rightMembers.Min(m => m.movedTime),
                            peopleName = rightMembers.Find(f => f.movedTime == rightMembers.Min(m => m.movedTime)).peopleName
                        });
                        time2 = rightMembers.Min(m => m.movedTime);
                        name2 = rightMembers.Find(f => f.movedTime == time2).peopleName;
                        this.runTime = this.runTime + rightMembers.Min(m => m.movedTime);
                        var a = rightMembers.FindIndex(f => f.movedTime == time2);
                        rightMembers.RemoveAt(a);
                    }
                }
                Stepping.Add(new Step
                {
                    way = "There",
                    time_1 = time1,
                    time_2 = time2,
                    moved_1 = name1,
                    moved_2 = name2,
                    runTime = this.runTime
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Run time error!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Find highest value
        private void MovedTwoMemberMax(List<People> leftMembers, List<People> rightMembers)
        {
            try
            {
                long time1 = 0;
                long time2 = 0;

                string name1 = null;
                string name2 = null;

                for (int k = 0; k < 2; k++)
                {
                    if (k == 0)
                    {
                        leftMembers.Add(new People
                        {
                            movedTime = rightMembers.Max(m => m.movedTime),
                            peopleName = rightMembers.Find(f => f.movedTime == rightMembers.Max(m => m.movedTime)).peopleName
                        });
                        time1 = rightMembers.Max(m => m.movedTime);
                        name1 = rightMembers.Find(f => f.movedTime == time1).peopleName;
                        this.runTime = this.runTime + rightMembers.Max(m => m.movedTime);
                        var a = rightMembers.FindIndex(f => f.movedTime == time1);
                        rightMembers.RemoveAt(a);
                    }
                    else
                    {
                        leftMembers.Add(new People
                        {
                            movedTime = rightMembers.Max(m => m.movedTime),
                            peopleName = rightMembers.Find(f => f.movedTime == rightMembers.Max(m => m.movedTime)).peopleName
                        });
                        time2 = rightMembers.Max(m => m.movedTime);
                        name2 = rightMembers.Find(f => f.movedTime == time2).peopleName;
                        var a = rightMembers.FindIndex(f => f.movedTime == time2);
                        rightMembers.RemoveAt(a);
                    }
                }
                Stepping.Add(new Step
                {
                    way = "There",
                    time_1 = time2,
                    time_2 = time1,
                    moved_1 = name2,
                    moved_2 = name1,
                    runTime = this.runTime
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Run time error!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Find external value
        private void MovedTwoMemberBetween(List<People> leftMembers, List<People> rightMembers)
        {
            try
            {
                long time1 = 0;
                long time2 = 0;

                string name1 = null;
                string name2 = null;

                for (int k = 0; k < 2; k++)
                {
                    if (k == 0)
                    {
                        leftMembers.Add(new People
                        {
                            movedTime = rightMembers.Max(m => m.movedTime),
                            peopleName = rightMembers.Find(f => f.movedTime == rightMembers.Max(m => m.movedTime)).peopleName
                        });
                        time1 = rightMembers.Max(m => m.movedTime);
                        name1 = rightMembers.Find(f => f.movedTime == time1).peopleName;
                        this.runTime = this.runTime + rightMembers.Max(m => m.movedTime);
                        var a = rightMembers.FindIndex(f => f.movedTime == time1);
                        rightMembers.RemoveAt(a);
                    }
                    else
                    {
                        leftMembers.Add(new People
                        {
                            movedTime = rightMembers.Min(m => m.movedTime),
                            peopleName = rightMembers.Find(f => f.movedTime == rightMembers.Min(m => m.movedTime)).peopleName
                        });
                        time2 = rightMembers.Min(m => m.movedTime);
                        name2 = rightMembers.Find(f => f.movedTime == time2).peopleName;
                        var a = rightMembers.FindIndex(f => f.movedTime == time2);
                        rightMembers.RemoveAt(a);
                    }
                }
                Stepping.Add(new Step
                {
                    way = "There",
                    time_1 = time2,
                    time_2 = time1,
                    moved_1 = name2,
                    moved_2 = name1,
                    runTime = this.runTime
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Run time error!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Checked list Content
        private bool ValueContentCheck()
        {
            bool compare = false;
            int contentIndex = 0;

            foreach (var rightItem in this.rightMembers)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (rightItem.movedTime == this.basedMembers[i].movedTime)
                    {
                        contentIndex++;
                    }
                    if (contentIndex == 2)
                    {
                        compare = true;
                        break;
                    }
                    else
                    {
                        compare = false;
                    }
                }
            }
            return compare;
        }

        //Solution body
        public void Solv(List<People> peoples)
        {
            this.RightMembers.AddRange(peoples);

            while (this.rightMembers.Count != 0)
            {
                //First step
                if (step == 1)
                {
                    this.MethodName = "MinValue";
                    ProcessorSelect(this.MethodName);
                    this.step++;
                    this.ProgressStep++;
                }

                //Return moved
                if (step % 2 == 0)
                {
                    this.MethodName = "ReturnValue";
                    ProcessorSelect(this.MethodName);
                    this.step++;
                    this.ProgressStep++;
                }

                //More moved
                if (this.step % 2 >= 1)
                {
                    if (ValueContentCheck())
                    {
                        if (this.rightMembers.Count == 3)
                        {
                            this.MethodName = "MaxValue";
                            ProcessorSelect(this.MethodName);
                            this.step++;
                            this.ProgressStep++;
                        }
                        else
                        {
                            this.MethodName = "MinValue";
                            ProcessorSelect(this.MethodName);
                            this.step++;
                            this.ProgressStep++;
                        }
                    }
                    else
                    {
                        this.MethodEvaluation();
                        this.ProcessorSelect(this.MethodEvaluation());
                        this.step++;
                        this.ProgressStep++;
                    }
                }
            }
        }

        //Processor select to Solv method
        public void ProcessorSelect(string methodName)
        {
            switch (methodName)
            {
                case "MinValue" :
                    MovedTwoMemberMin(this.leftMembers, this.rightMembers);
                    break;
                case "MaxValue":
                    MovedTwoMemberMax(this.leftMembers, this.rightMembers);
                    break;
                case "BetweenValue":
                    MovedTwoMemberBetween(this.leftMembers, this.rightMembers);
                    break;
                case "ReturnValue":
                    ReturnMoved(this.leftMembers, this.rightMembers);
                    break;
                default :
                    MessageBox.Show("Incorrect method name!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        //Decision
        private string MethodEvaluation()
        {
            long difference = Math.Abs(this.BasedMembers[0].movedTime - this.BasedMembers[1].movedTime);

            if (difference < this.BasedMembers.Count)
            {
                return this.MethodName = "MaxValue";
            }
            else
            {
                return this.MethodName = "BetweenValue";
            }
        }
    }
}