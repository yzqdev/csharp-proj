using System.Xml.Serialization;

namespace TopMostForm.Languages {
    public class LanguageString {
        [XmlAttribute(@"name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }

        public string Format(params object[] args) {
            return string.Format(Value, args);
        }

        public override string ToString() {
            return $@"{Name}: {Value}";
        }
    }
}
