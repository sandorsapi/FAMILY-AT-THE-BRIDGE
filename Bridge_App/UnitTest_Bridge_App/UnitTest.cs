using Bridge_App;
using Bridge_App.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void solv_method_running()
        {
            //Arrange
            var moqMinimumValue = new Mock<IAlgoritm>();

            List<People> peopleList = new List<People>();

            peopleList.Add(
                new People
                {
                    peopleName = "Flinn",
                    movedTime = 2
                });

            peopleList.Add(
                new People
                {
                    peopleName = "Susan",
                    movedTime = 12
                });

            peopleList.Add(
               new People
               {
                   peopleName = "Alex",
                   movedTime = 6
               });

            peopleList.Add(
              new People
              {
                  peopleName = "Rock",
                  movedTime = 8
              });

            moqMinimumValue.Setup(x => x.Solv(peopleList));

            var evaluationAndSolution = new EvaluationAndSolution(moqMinimumValue.Object);

            evaluationAndSolution.BasedMembers.Add(new People
            {
                movedTime = peopleList[0].movedTime
            });
            evaluationAndSolution.BasedMembers.Add(new People
            {
                movedTime = peopleList[2].movedTime
            });

            //Act
            evaluationAndSolution.Solv(peopleList);

            // //Assert
            moqMinimumValue.Verify(v => v.Solv(peopleList), Times.AtMostOnce);
        }
    }
}