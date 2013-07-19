using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RecordData
    {
        public int Id { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }
        public bool IsSimulationTest { set; get; }

        public string UserName { set; get; }

        public string ResourceName { set; get; }    //所学资源的名称
        public int ResourceId { set; get; }         //所学资源的id
        public ResourceType ResourceType { set; get; }   //所学资源的类型
    }
    public enum ResourceType
    {
        VideoResource,
        DocResource
    }
}
