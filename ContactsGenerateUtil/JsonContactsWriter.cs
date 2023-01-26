using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Phonebook.Models;

namespace ContactsGenerateUtil
{
    class JsonContactsWriter
    {
        public IEnumerable<Contact> Contacts { get; set; }
        public StreamWriter StreamWriter { get; set; }

        public void Write()
        {
            if (Contacts == null)
            {
                return;
            }
            if (StreamWriter == null)
            {
                return;
            }

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,
                    UnicodeRanges.Cyrillic)
            };

            StreamWriter.WriteLine("[");
            int counter = 0;
            foreach (var contact in Contacts)
            {
                string itemJson = JsonSerializer.Serialize(
                    value: contact,
                    inputType: typeof(Contact),
                    options: jsonOptions);
                StreamWriter.WriteLine(counter > 0 ? "," : "");
                StreamWriter.Write(itemJson);
                counter++;
            }
            StreamWriter.WriteLine();
            StreamWriter.WriteLine("]");

            //string str = JsonSerializer.Serialize(
            //    value: Contacts,
            //    inputType: typeof(IEnumerable<Contact>),
            //    options: jsonOptions);
            //using (StringReader sr = new StringReader(str))
            //{
            //    string line = "";
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        StreamWriter.WriteLine(line);
            //    }
            //}

        }
    }
}
