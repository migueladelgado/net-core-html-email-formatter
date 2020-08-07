using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlEmailFormatter.HtmlEmailFormatter.Html.Components
{
    public class Element
    {
        public List<HtmlComponent> elements { get; set; } = new List<HtmlComponent>();

        public Element()
        {

        }

        public HtmlComponent addElement()
        {
            var comp = new HtmlComponent();
            elements.Add(comp);
            return comp;
        }

        public HtmlComponent createComponent() => new HtmlComponent();

        //build Html
        public string build()
        {
            return string.Join(
                            string.Empty,
                            elements.Select(x => x.div)
                        ) + string.Join(
                            string.Empty,
                            Enumerable.Range(1, elements.Count)
                            .Select(x => "</div>")
                        );
        }
    }
}
