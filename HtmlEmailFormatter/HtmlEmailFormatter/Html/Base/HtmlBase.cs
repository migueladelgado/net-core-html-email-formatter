using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlEmailFormatter.HtmlEmailFormatter.Html.Base
{
    public abstract class HtmlBase : StyleBase
    {
        public string div { get; set; } = "<div class style>{}";
        private List<string> classes { get; set; } = new List<string>();
        private List<string> styles { get; set; } = new List<string>();
        private List<string> words { get; set; } = new List<string>();

        public HtmlBase()
        {

        }

        /// <summary>
        /// Can add multiple styles by passing a list 
        /// Ex. new List<string> { "my-class123", "bold" }
        /// or, chaining
        /// .addClass("myclass").addClass("toocool")
        /// </summary>
        /// <param name="_classes"></param>
        /// <returns></returns>

        //styles only suported for now
        //public HtmlBase addClass(dynamic _classes)
        //{
        //    if (_classes is List<string>)
        //        classes.AddRange(_classes);
        //    else
        //        classes.Add(_classes);
        //    return this;
        //}

        public HtmlBase addStyle(dynamic _styles)
        {
            if (_styles is List<string>)
                styles.AddRange(_styles);
            else
                styles.Add(_styles);
            return this;
        }

        public HtmlBase addWords(dynamic _words)
        {
            if (_words is List<string>)
                words.AddRange(_words);
            else
                words.Add(_words);
            return this;
        }

        public HtmlBase addStyleSheet(string name)
        {
            if (styleSheet == null)
                readStyles();
            addStyle(getStyleSheet(name));
            return this;
        }


        /// <summary>
        /// Must be called at the end of addings classes / styles to element 
        /// to build element
        /// </summary>
        /// <returns></returns>
        public string build()
        {
            div = div.Replace("class", $"class=\"{string.Join(' ', classes)}\"")
                        .Replace("style", $"style=\"{string.Join(' ', styles)}\"")
                        .Replace("{}", string.Join(' ', words));
            return div;
        }
    }
}
