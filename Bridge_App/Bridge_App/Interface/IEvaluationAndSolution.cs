using System.Collections.Generic;
using static People.People;

namespace Bridge_App.Interface
{
    public interface IEvaluationAndSolution
    {
        void ReturnMoved(List<Peoples> leftMembers, List<Peoples> rightMembers);

        void MovedTwoMemberMin(List<Peoples> leftMembers, List<Peoples> rightMembers);

        void MovedTwoMemberMax(List<Peoples> leftMembers, List<Peoples> rightMembers);

        List<Peoples> PeopleValue(List<Peoples> people);
    }
}