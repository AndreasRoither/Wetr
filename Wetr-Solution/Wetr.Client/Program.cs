using Common.Dal.Ado;
using System;
using System.Linq;

namespace Wetr.Client
{
    internal class Program
    {
        private class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public Person(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }
        }

        private static void Main(string[] args)
        {
            AdoTemplate template = new AdoTemplate(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));
            Console.WriteLine("Printing persons:");
            template.Query("select * from persons", record => new Person((int)record["id"], (string)record["name"])).ToList().ForEach(person =>
            {
                Console.WriteLine($"{person.Id}: {person.Name}");
            });

            Console.ReadLine();
        }
    }
}