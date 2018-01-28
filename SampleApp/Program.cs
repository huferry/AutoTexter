using AutoTexter;

namespace SampleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // create script1.sql from the template, with 1 insert
            "template.txt"
                .AutoTextFile(
                    "script1.sql",
                    new AutoTextValues("description", "Phase 1 addition"),
                    new AutoTextValues("author", "Clark Kent"),
                    new AutoTextValues(
                        "id|name|desc|allowcreditcard",
                        "1|unknown|Customer that is not yet registered.|false"));

            // create script2.sql from the template, with 2 inserts
            "template.txt"
                .AutoTextFile(
                    "script2.sql",
                    new AutoTextValues("description", "Phase 2 addition"),
                    new AutoTextValues("author", "Lex Luthor"),
                    new AutoTextValues(
                        "id|name|desc|allowcreditcard",
                        "2|registered|Customer that is registered.|true",
                        "3|frequent|Customer that orders 2 times a week or more.|true"));
        }
    }
}