using System.Collections.Generic;
using TSM.Models;

namespace TSM.Dto
{
    public class WorkUnitListDto
    {
        public int ScheduleId { get; set; }

        public int MemberId { get; set; }

        public List<WorkUnit> WorkUnits { get; set; }
    }
}
