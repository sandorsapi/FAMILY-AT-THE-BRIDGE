using System.Collections.Generic;

namespace Bridge_App.Interface
{
    public interface IEvaluationAndSolution
    {
        void ReturnMoved(List<People> leftMembers, List<People> rightMembers);

        void MovedTwoMemberMin(List<People> leftMembers, List<People> rightMembers);

        void MovedTwoMemberMax(List<People> leftMembers, List<People> rightMembers);

        void MovedTwoMemberBetween(List<People> leftMembers, List<People> rightMembers);

        List<People> PeopleValue(List<People> people);
    }
}