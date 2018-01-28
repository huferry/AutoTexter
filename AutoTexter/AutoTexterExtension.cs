using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoTexter
{
    public static class AutoTexterExtension
    {
        public static string[] AutoText(this string[] text, params AutoTextValues[] autoTextValuesList)
        {
            var result = new List<string>();
            foreach (var line in text)
                result.AddRange(
                    line.Texts(
                        autoTextValuesList
                            .FirstOrDefault(v => line.IsMatchVariables(v.Variables))));

            return result.ToArray();
        }

        public static void AutoTextFile(this string sourceFileName, string targetFileName, params AutoTextValues[] autoTextValuesList)
        {
            using (var writer = new StreamWriter(targetFileName))
            {
                sourceFileName
                    .AutoTextFromFile(autoTextValuesList)
                    .ToList()
                    // ReSharper disable once AccessToDisposedClosure
                    .ForEach(t => writer.WriteLine(t));

                writer.Close();
            }
        }

        public static string[] AutoTextFromFile(this string fileName, params AutoTextValues[] autoTextValuesList)
        {
            using (var reader = new StreamReader(fileName))
            {
                var content = new List<string>();
                while (!reader.EndOfStream)
                {
                    content.Add(reader.ReadLine());
                }

                return content.ToArray().AutoText(autoTextValuesList);
            }
        }

    }
}