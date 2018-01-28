using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoTexter
{
    public static class StringTexterExtension
    {
        public static bool IsMatchVariables(this string template, string variables)
        {
            var templateVars = Regex
                .Matches(template, @"{(\w+)}").Cast<Match>()
                .Where(m => m.Success)
                .Select(m => m.Groups[1].Value)
                .ToList();

            var givenVars = variables.Split('|').ToList();

            templateVars.Sort();
            givenVars.Sort();

            return string.Join("$", templateVars) == string.Join("$", givenVars);
        }

        public static string Text(this string template, string variables, params string[] values)
        {
            return string.Join("\n", template.Texts(variables, values));
        }

        public static string[] Texts(this string template, AutoTextValues autoTextValues)
        {
            if (autoTextValues == null) return new[] {template};

            return Texts(template, autoTextValues.Variables, autoTextValues.Values);
        }

        public static string[] Texts(this string template, string variables, params string[] values)
        {
            if (!template.IsMatchVariables(variables)) return new[] {template};

            var lines = new List<string>();

            foreach (var value in values)
            {
                var pairs = CreatePair(variables, value);

                var result = template;
                pairs.Keys.ToList().ForEach(v =>
                    result = result.ReplaceVariable(v, pairs[v]));

                lines.Add(result);
            }

            return lines.ToArray();
        }

        private static string ReplaceVariable(this string template, string variable, string value)
        {
            return template.Replace("{" + variable + "}", value);
        }

        private static IDictionary<string, string> CreatePair(string variables, string values)
        {
            var result = new Dictionary<string, string>();
            var variableList = variables.Split('|');
            var valueList = values.Split('|');

            var idx = 0;
            foreach (var variable in variableList)
            {
                result.Add(variable, valueList.Length >= idx + 1 ? valueList[idx] : string.Empty);
                idx++;
            }

            return result;
        }
    }
}