using System.Collections.Generic;

namespace TSM.Models
{
    public class MemberList : List<WorkUnit>
    {
        public string FullName { get; set; }

        public int MemberId { get; set; }

        public List<WorkUnit> WorkUnits => this;

        public MemberList()
        {
        }

        public MemberList(IEnumerable<WorkUnit> workUnits)
        {
            foreach (var unit in workUnits)
                WorkUnits.Add(unit);
        }
    }
}
