using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tmoji
{
    public class Settings
    {
        public readonly List<ButtonGroup> ButtonGroups = new List<ButtonGroup>();
        private readonly string SaveFilePath;

        public Settings(string saveFilePath = null)
        {
            if (saveFilePath is null)
            {
                string saveFileName = "settings.txt";
                string saveFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                saveFilePath = Path.Combine(saveFolder, saveFileName);
            }
            SaveFilePath = saveFilePath;
        }

        public void Add(string group, string[] labels) => ButtonGroups.Add(new ButtonGroup(group, labels));

        public void Add(ButtonGroup buttonGroup) => ButtonGroups.Add(buttonGroup);

        public void Clear() => ButtonGroups.Clear();

        public void Save() => System.IO.File.WriteAllLines(SaveFilePath, ButtonGroups.Select(x => x.ToString()));

        public void Launch() => Process.Start("explorer.exe", SaveFilePath);

        public void Load()
        {
            if (!System.IO.File.Exists(SaveFilePath))
            {
                Debug.WriteLine("Loading default emoji");
                LoadDefaultSettings();
                return;
            }

            Debug.WriteLine($"Loading emoji from: {SaveFilePath}");
            Clear();
            string[] lines = System.IO.File.ReadAllLines(SaveFilePath);
            foreach (string line in lines.Where(x => x.Contains(":")))
            {
                string name = line.Split(':')[0];
                string[] labels = line.Split(':')[1].Split(',');
                Debug.WriteLine($"Adding group {name} with {labels.Length} buttons");
                Add(name, labels);
            }
        }

        public void LoadDefaultSettings()
        {
            string defaultEmojiString =
                "😜,😝,😎,🤓,😂,🤣," +
                "🤔,💁,😅,💀,☠️,😈," +
                "⚠️,💡,✔️,❌,❓,🎉," +
                "👍,👈,👉,☝️,🤞,💪";

            Clear();
            Add("Emoji", defaultEmojiString.Split(','));
            Add("Symbols", "±,Δ,µ,Ω,σ,τ,λ,↑,↓,←,→,⤙".Split(','));
        }
    }
}
