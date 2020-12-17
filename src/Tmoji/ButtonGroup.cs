using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmoji
{
    public abstract class ButtonGroup
    {
        public readonly List<string> Labels = new List<string>();
        public string Name;
    }
}
