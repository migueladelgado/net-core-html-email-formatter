using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlEmailFormatter.HtmlEmailFormatter.Requests
{
    public class EmailRequest
    {
        public Sections body { get; set; }
    }

    public class Sections
    {
        public List<Section> sections { get; set; }
    }

    public class Section
    {
        public bool container { get; set; }
        public List<SingleElement> elements { get; set; }
    }

    public class SingleElement
    {
        public bool style { get; set; }
        public string type { get; set; }
        public string text { get; set; }
    }
}
