using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmoji
{
    /// <summary>
    /// A group of symbols which will be displayed using text
    /// </summary>
    public class SymbolGroup : ButtonGroup
    {
        public SymbolGroup(string name)
        {
            Name = name;
        }

        public SymbolGroup(string name, string emojis)
        {
            Name = name;
            var textParts = System.Globalization.StringInfo.GetTextElementEnumerator(emojis);
            while (textParts.MoveNext())
                Labels.Add((string)textParts.Current);
        }
    }
}
