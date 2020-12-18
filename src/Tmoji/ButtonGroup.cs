using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tmoji
{
    public class ButtonGroup
    {
        public readonly List<string> Labels = new List<string>();
        public string Name;
        public override string ToString() => Name + ":" + string.Join(",", Labels);

        public ButtonGroup(string name, string[] labels)
        {
            Name = name;
            Labels.AddRange(labels);
        }
    }
}
