using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmoji
{
    /// <summary>
    /// A group of emoji which will be displayed graphically as images
    /// </summary>
    public class EmojiGroup : ButtonGroup
    {
        public EmojiGroup(string name)
        {
            Name = name;
        }

        public EmojiGroup(string name, string emojis)
        {
            Name = name;
            var textParts = System.Globalization.StringInfo.GetTextElementEnumerator(emojis);
            while (textParts.MoveNext())
                Labels.Add((string)textParts.Current);
        }
    }
}
