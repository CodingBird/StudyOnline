using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class DocCategory
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }

        public ICollection<DocData> DocDatas { set; get; }
    }
}
