using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
using Phonebook.Models;

namespace ContactsGenerateUtil
{
    class Program
    {
        static void PrintContacts(IEnumerable<Contact> contacts)
        {
            Console.WriteLine("***Print***");
            int count = 0;
            foreach (var contact in contacts)
            {
                Console.Write($"id={contact.ContactId}");
                Console.Write($"\tname='{contact.Lastname}' '{contact.Firstname}' '{contact.Patronymic}'");
                Console.Write($"\tphone='{contact.Phonenumber}'");
                Console.WriteLine();
                count++;
            }
            Console.WriteLine("---");
            Console.WriteLine($"count={count}");
        }

        static string GeneratePhonenumber()
        {
            string code = "7";
            string[] prefixArr = new[]
            {
                "902",
                "904",
                "908",
                "912",
                "922",
                "906",
                "951",
                "925",
                "926"
            };
            int bodyLength = 7;

            Random rnd = new Random();
            int rndNum = rnd.Next(prefixArr.Length);
            string prefix = prefixArr[rndNum];

            StringBuilder sb = new StringBuilder();
            sb.Append(code);
            sb.Append(prefix);
            for (int i = 0; i < bodyLength; i++)
            {
                int num = rnd.Next(minValue: 0, maxValue: 9);
                sb.Append(num);
            }
            return sb.ToString();
        }

        private static IEnumerable<Contact> ReadContacts(string inputFilename)
        {
            IEnumerable<Contact> contacts;
            ContactParser parser = new ContactParser();
            using (FileStream fs = new FileStream(
                path: inputFilename,
                mode: FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    parser.StreamReader = sr;
                    parser.Parse();
                }
            }
            contacts = parser.Contacts;
            return contacts;
        }

        private static void WriteContactsToJson(string outputFilename, IEnumerable<Contact> contacts2)
        {
            JsonContactsWriter writer = new JsonContactsWriter();
            writer.Contacts = contacts2;
            using (FileStream fs = new FileStream(
                path: outputFilename,
                mode: FileMode.OpenOrCreate | FileMode.Truncate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    writer.StreamWriter = sw;
                    writer.Write();
                }
            }
        }
        private static IEnumerable<Contact> ReadContactsFromJson(string filename)
        {
            string json = File.ReadAllText(path: filename);
            return JsonSerializer.Deserialize(
                json: json,
                returnType: typeof(IEnumerable<Contact>)) as IEnumerable<Contact>;
        }

        static void Main(string[] args)
        {
            string inputFilename = @"C:\nazarov\doc\person-name-list-test--1.txt";
            string outputFilename = @"C:\nazarov\doc\contacts-list-out--1.json";

            //IEnumerable<Contact> contacts = ReadContacts(inputFilename);

            //int id = 1;
            //contacts.ToList()
            //    .ForEach(elem =>
            //    {
            //        elem.ContactId = id;
            //        elem.Phonenumber = GeneratePhonenumber();
            //        id++;
            //    });

            //PrintContacts(contacts);

            //IEnumerable<Contact> contacts2 = contacts;
            //WriteContactsToJson(outputFilename, contacts2);

            //IEnumerable<Contact> contacts3 = ReadContactsFromJson(outputFilename);
            //PrintContacts(contacts3);

            //string filename1 = @"C:\Users\nazar\source\repos\Phonebook\Phonebook\Data\contacts-items.json";
            string filename1 = @"contacts-items.json";
            IEnumerable<Contact> contacts3 = ReadContactsFromJson(filename: filename1);
            //PrintContacts(contacts3);

            SqlContactsWriter sqlWriter = new SqlContactsWriter();
            sqlWriter.Contacts = contacts3;
            int num = sqlWriter.Write();
            Console.WriteLine($"writed: {num}");
            return;
        }

    }
}
