using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegisterActivityWebApi.DTO
{
    public class ProjectDTO
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string OrganzationName { get; set; }
        public bool isDone { get; set; }

    }
}