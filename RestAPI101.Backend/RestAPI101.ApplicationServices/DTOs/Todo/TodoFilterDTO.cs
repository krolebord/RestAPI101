using System.Collections.Generic;
using RestAPI101.Domain.Enums;

namespace RestAPI101.ApplicationServices.DTOs.Todo
{
    public class TodoFilterDTO
    {
        public TodoIncludeMode IncludeMode { get; set; }

        public TodoFilterLabelsMode LabelsMode { get; set; }

        public HashSet<int> LabelIds { get; set; }

        public TodoFilterDTO()
        {
            IncludeMode = TodoIncludeMode.All;
            LabelsMode = TodoFilterLabelsMode.Union;
            LabelIds = new HashSet<int>();
        }
    }
}
