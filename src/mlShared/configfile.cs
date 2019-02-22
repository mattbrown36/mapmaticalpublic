using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mlShared
{
    public class ConfigFile
    {
        //TODO: Consider encrypting/decrypting the information on access.
        //This means that if a server is compromised and the attacker does
        //a memory dump they won't be able to read our secrets directly out of memory.

        private Dictionary<string, string> info = new Dictionary<string, string>();

        public ConfigFile(string path, bool throwIfNotExists) {
            if (!File.Exists(path)) {
                if (throwIfNotExists) {
                    throw new FileNotFoundException("Could not find " + path);
                }
                var fi = new FileInfo(path);
                fi.Directory.Create();
                var exampleText = "#Example Setting:" + Environment.NewLine
                    + "SettingName=SettingValue" + Environment.NewLine
                    +"#Settings and comments may not be on the same line.";
                File.WriteAllText(path, exampleText);
            }
            var lines = File.ReadAllLines(path);
            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim();
                if (line.StartsWith("#") || line.StartsWith("//"))
                {
                    continue;
                }
                var parts = line.Split('=');
                if (parts.Length < 2)
                {
                    continue;
                }
                var key = parts[0].Trim();
                var value = "";
                for (int i = 1; i < parts.Length; i++)
                {
                    if (i == parts.Length - 1)
                    {
                        value += parts[i].TrimEnd();
                    }
                    else if (i == 1)
                    {
                        value += parts[i].TrimStart() + "=";
                    }
                    else
                    {
                        value += parts[i] + "=";
                    }
                }
                info[key] = value;
            }
        }

        public string GetValue(string key) {
            string ret;
            return info.TryGetValue(key, out ret)? ret : "";
        }
    }
}
