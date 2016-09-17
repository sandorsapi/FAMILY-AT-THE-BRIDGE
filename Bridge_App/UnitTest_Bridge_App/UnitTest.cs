using Bridge_App;
using Bridge_App.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using static People.People;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void moved_rightlist_returnmoved_leftlist()
        {
            //Arrange
            var moqMinimumValue = new Mock<IEvaluationAndSolution>();
            var moqpeopleValueRepository = new Mock<IPeopleRepository>();

            List<Peoples> peoplesValue = new List<Peoples>();
            List<Peoples> left = new List<Peoples>();

            peoplesValue.Add(
                new Peoples
                {
                    peopleName = "Flinn",
                    movedTime = 2
                });

            peoplesValue.Add(
                new Peoples
                {
                    peopleName = "Susan",
                    movedTime = 12
                });

            peoplesValue.Add(
               new Peoples
               {
                   peopleName = "Alex",
                   movedTime = 6
               });

            moqMinimumValue.Setup(x => x.PeopleValue(peoplesValue))
           .Returns(() => peoplesValue);

            var evaluationAndSolution = new EvaluationAndSolution(moqMinimumValue.Object, moqpeopleValueRepository.Object);

            //Act
            evaluationAndSolution.CreateTestValue(peoplesValue);

            //Assert
            moqMinimumValue.Verify(v => v.ReturnMoved(left, peoplesValue), Times.Never);
        }

        [TestMethod]
        public void moved_rightlist_minimum_between_maximum_value_moved_leftlist()
        {
            var moqBetweenValue = new Mock<IEvaluationAndSolution>();
            var moqpeopleValueRepository = new Mock<IPeopleRepository>();

            List<Peoples> peoplesValue = new List<Peoples>();
            List<Peoples> left = new List<Peoples>();

            peoplesValue.Add(
                new Peoples
                {
                    peopleName = "Flinn",
                    movedTime = 2
                });

            peoplesValue.Add(
                new Peoples
                {
                    peopleName = "Susan",
                    movedTime = 12
                });

            peoplesValue.Add(
               new Peoples
               {
                   peopleName = "Alex",
                   movedTime = 6
               });

            moqBetweenValue.Setup(x => x.PeopleValue(peoplesValue))
           .Returns(() => peoplesValue);

            var evaluationAndSolution = new EvaluationAndSolution(moqBetweenValue.Object, moqpeopleValueRepository.Object);

            //Act
            evaluationAndSolution.CreateTestValue(peoplesValue);

            //Assert
            moqBetweenValue.Verify(v => v.MovedTwoMemberMin(left, peoplesValue), Times.Never);
        }

        [TestMethod]
        public void moved_rightlist_minimum_value_moved_leftlist()
        {
            var moqMinimumValue = new Mock<IEvaluationAndSolution>();
            var moqpeopleValueRepository = new Mock<IPeopleRepository>();

            List<Peoples> peoplesValue = new List<Peoples>();
            List<Peoples> left = new List<Peoples>();

            peoplesValue.Add(
                new Peoples
                {
                    peopleName = "Flinn",
                    movedTime = 2
                });

            peoplesValue.Add(
                new Peoples
                {
                    peopleName = "Susan",
                    movedTime = 12
                });

            peoplesValue.Add(
               new Peoples
               {
                   peopleName = "Alex",
                   movedTime = 6
               });

            moqMinimumValue.Setup(x => x.PeopleValue(peoplesValue))
           .Returns(() => peoplesValue);

            var evaluationAndSolution = new EvaluationAndSolution(moqMinimumValue.Object, moqpeopleValueRepository.Object);

            //Act
            evaluationAndSolution.CreateTestValue(peoplesValue);

            //Assert
            moqMinimumValue.Verify(v => v.MovedTwoMemberMin(left, peoplesValue), Times.Never);
        }

        [TestMethod]
        public void moved_rightlist_maximum_value_moved_leftlist()
        {
            var moqMaximumValue = new Mock<IEvaluationAndSolution>();
            var moqpeopleValueRepository = new Mock<IPeopleRepository>();

            List<Peoples> peoplesValue = new List<Peoples>();
            List<Peoples> left = new List<Peoples>();

            peoplesValue.Add(
                new Peoples
                {
                    peopleName = "Flinn",
                    movedTime = 2
                });

            peoplesValue.Add(
                new Peoples
                {
                    peopleName = "Susan",
                    movedTime = 12
                });

            peoplesValue.Add(
               new Peoples
               {
                   peopleName = "Alex",
                   movedTime = 6
               });

            moqMaximumValue.Setup(x => x.PeopleValue(peoplesValue))
           .Returns(() => peoplesValue);

            var evaluationAndSolution = new EvaluationAndSolution(moqMaximumValue.Object, moqpeopleValueRepository.Object);

            //Act
            evaluationAndSolution.CreateTestValue(peoplesValue);

            //Assert
            moqMaximumValue.Verify(v => v.MovedTwoMemberMax(left, peoplesValue), Times.Never);
        }
    }
}