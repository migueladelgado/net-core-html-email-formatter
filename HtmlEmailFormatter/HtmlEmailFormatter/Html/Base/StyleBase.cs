using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlEmailFormatter.HtmlEmailFormatter.Html.Base
{
    public abstract class StyleBase
    {
        public StyleSheet styleSheet { get; set; }

        public StyleBase()
        {

        }

        public string getStyleSheet(string name)
        {
            var property = styleSheet.GetType().GetProperty(name);
            List<Style> list = null;
            if (!(property == null))
            {
                list = (List<Style>)property.GetValue(styleSheet);
                return parsedStyleSheet(list);
            }
            else
            {
                throw new Exception("No matching stylesheet!");
            }
        }

        public void readStyles()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\data\\stylesheets.json");
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                styleSheet = JsonConvert.DeserializeObject<StyleSheet>(json);
            }
        }

        private string parsedStyleSheet(List<Style> list)
        {
            return string.Join(
                    ' ',
                    list.Select(l => $"{l.key}: {l.value};")
                );
        }
    }

    public class StyleSheet
    {
        public List<Style> container { get; set; }
        public List<Style> header { get; set; }
        public List<Style> paragraph { get; set; }
    }

    public class Style
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}

