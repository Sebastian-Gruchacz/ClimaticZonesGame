﻿namespace GameGenerator
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    internal static class Helpers
    {
        public static string LoadFile(string path)
        {
            string baseFile = Assembly.GetEntryAssembly().Location;
            string asmFolder = new FileInfo(baseFile).DirectoryName;

            string targetFilePath = Path.Combine(asmFolder, path);
            if (!File.Exists(targetFilePath))
            {
                throw new FileNotFoundException(targetFilePath);
            }

            using (var file = File.OpenText(targetFilePath))
            {
                return file.ReadToEnd();
            }
        }

        public static IEnumerable<Card> BuildCardSet(CardInfo[] cardInfo)
        {
            foreach (var info in cardInfo)
            {
                for (int i = 0; i < info.CardCount; i++)
                {
                    yield return info.ToCard();
                }
            }
        }

        public static void WriteFileFromTemplate(StreamWriter writer, string template, IDictionary<string, string> replacements)
        {
            string output = template;
            foreach (var replacement in replacements)
            {
                output = output.Replace($"{{#{replacement.Key}#}}", replacement.Value);
            }

            writer.Write(output);
        }
    }
}