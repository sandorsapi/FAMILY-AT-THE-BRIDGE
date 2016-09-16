using Bridge_App;
using Bridge_App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Bridge_App
{
    public class EvaluationAndSolution
    {
        private readonly IEvaluationAndSolution _evaluationAndSolution;
        private readonly IPeopleRepository _peopleRepository;

        private long runTime = 0;
        private long step = 1;
        private int progressStep = 0;
        private StringBuilder solutionText = new StringBuilder();
        private List<People> basedMembers = new List<People>();
        private List<People> rightMembers = new List<People>();
        private List<People> leftMembers = new List<People>();

        public EvaluationAndSolution(IEvaluationAndSolution evaluationAndSolution, IPeopleRepository peopleRepository)
        {
            this._evaluationAndSolution = evaluationAndSolution;
            this._peopleRepository = peopleRepository;
        }

        public EvaluationAndSolution() { }

        public List<People> BasedMembers
        {
            get { return basedMembers; }
            set { basedMembers = value; }
        }

        public StringBuilder SolutionText
        {
            get { return solutionText; }
            set { solutionText = value; }
        }

        public int ProgressStep
        {
            get { return progressStep; }
        }

        
        //Optimalization
        public void Solution()
        {
            this.rightMembers.AddRange(this.basedMembers);

            this.solutionText.Clear();

            this.runTime = this.basedMembers.Sum(s => s.movedTime);

            while (this.rightMembers.Count != 0)
            {
                //First step
                if (step == 1)
                {
                    MovedTwoMemberMin(this.leftMembers, this.rightMembers);
                    this.step++;
                    this.progressStep++;
                }

                //Return moved
                if (step % 2 == 0)
                {
                    ReturnMoved(this.leftMembers, this.rightMembers);
                    this.step++;
                    this.progressStep++;
                }

                if (step == 3)
                {
                    MovedTwoMemberMax(this.leftMembers, this.rightMembers);
                    this.step++;
                    this.progressStep++;
                }

                //More moved
                if (this.step % 2 >= 1)
                {
                    if (ValueContentCheck())
                    {
                        if (this.rightMembers.Count == 3)
                        {
                            MovedTwoMemberMax(this.leftMembers, this.rightMembers);
                            this.step++;
                            this.progressStep++;
                        }
                        else
                        {
                            MovedTwoMemberMin(this.leftMembers, this.rightMembers);
                            this.step++;
                            this.progressStep++;
                        }
                    }
                    else
                    {
                        if (this.rightMembers.Count == 3)
                        {
                            MovedTwoMemberBetween(this.leftMembers, this.rightMembers);
                            this.step++;
                            this.progressStep++;
                        }
                        else
                        {
                            MovedTwoMemberMax(this.leftMembers, this.rightMembers);
                            this.step++;
                            this.progressStep++;
                        }
                    }
                }
            }
            this.solutionText.AppendLine(string.Format("Result: Full time {0} - Run time {1}s - The rest time {2}s ", this.leftMembers.Sum(s => s.movedTime), (this.leftMembers.Sum(s => s.movedTime) - this.runTime), (this.leftMembers.Sum(s => s.movedTime) - (this.leftMembers.Sum(s => s.movedTime) - this.runTime))));

            this.runTime = 0;
            this.step = 1;
            this.leftMembers.Clear();
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
            this.runTime = this.runTime - leftMembers.Min(m => m.movedTime);
            this.solutionText.AppendLine(string.Format("Return: {0}({1}) - Moved time: {2} - Run time {3}", name, time, time, this.runTime));
            var a = leftMembers.FindIndex(f => f.movedTime == time);
            leftMembers.RemoveAt(a);         
        }

       
        //Extremes members
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
                        this.runTime = this.runTime - rightMembers.Max(m => m.movedTime);
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
                this.solutionText.AppendLine(string.Format("Moved: {0}({1}), {2}({3}) - Moved time: {4} - Run time {5}", name1, time1, name2, time2, time1, this.runTime));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Run time error!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                        name2 = rightMembers.Find(f =>f.movedTime == time2).peopleName;
                        this.runTime = this.runTime - rightMembers.Min(m => m.movedTime);
                        var a = rightMembers.FindIndex(f => f.movedTime == time2);
                        rightMembers.RemoveAt(a);
                    }
                }
                this.solutionText.AppendLine(string.Format("Moved: {0}({1}), {2}({3}) - Moved time: {4} - Run time {5}", name2, time2, name1, time1, time2, this.runTime));
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
                        this.runTime = this.runTime - rightMembers.Max(m => m.movedTime);
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
                this.solutionText.AppendLine(string.Format("Moved: {0}({1}), {2}({3}) - Moved time: {4} - Run time {5}", name1, time1, name2, time2, time1, this.runTime));                
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
            foreach (var rightItem in this.rightMembers)
            {
                if (rightItem.movedTime.Equals(this.basedMembers[0].movedTime) || (rightItem.movedTime.Equals(this.basedMembers[1].movedTime)))
                {
                    compare = true;
                    break;
                }
                else
                {
                    compare = false;
                }
            }
            return compare;
        }

        //Create test data
        public void CreateTestValue(List<People> people)
        {
            List<People> peopels = new List<People>();

            peopels.AddRange(_evaluationAndSolution.PeopleValue(people));
     
            if (peopels.Count == 0)
            {
                throw new InvalidPeopleValueException();
            }

            _peopleRepository.Save(peopels);
        }
    }
}