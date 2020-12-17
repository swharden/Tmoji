using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmoji
{
    public class ButtonGroup
    {
        public readonly List<string> Labels = new List<string>();
        public string Name;

        public ButtonGroup(string name, string[] labels)
        {
            Name = name;
            Labels.AddRange(labels);
        }
    }
}
