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

        public string Name { get; set; }

        public string Text
        {
            get
            {
                return string.Join(",", Labels);
            }
            set
            {
                Labels.Clear();
                Labels.AddRange(value.Split(','));
            }
        }

        public override string ToString() => Name + ":" + string.Join(",", Labels);

        public ButtonGroup(string name, int randomCount)
        {
            Name = name;
            AddRandom(randomCount);
        }

        public ButtonGroup(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public ButtonGroup(string name, string[] labels)
        {
            Name = name;
            Labels.AddRange(labels);
        }

        private void AddRandom(int count, bool emoji = true, bool symbols = true)
        {
            List<string> source = new List<string>();
            if (emoji)
                source.AddRange("😜,😝,😎,🤓,😂,🤣,🤔,💁,💡,💀,😈".Split(','));
            if (symbols)
                source.AddRange("⚠️,❓,☠️,☝️±,Δ,µ,Ω,σ,τ,λ,↑,↓,←,→,⤙".Split(','));

            var rand = new Random();
            for (int i = 0; i < count; i++)
                Labels.Add(source[rand.Next(source.Count)]);
        }
    }
}
