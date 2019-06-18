using System.Reflection;

namespace CommLiby
{
    public class SheetColumn
    {
        public string Header { get; set; }
        public string DataProperty { get; set; }
        public PropertyInfo Pi { get; set; }
        public int ColIndex { get; set; }
    }
}
