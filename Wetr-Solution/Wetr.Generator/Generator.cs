using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Generator
{
    class Generator
    {

        private string[] names;

        public Generator()
        {
            this.Generate();
        }

        private void Generate()
        {
            /* Loading datapools */
            this.LoadUsernames();

            using (StreamWriter file = new StreamWriter("insert.sql"))
            {
                GenerateUsers(file);
            }
        }

        static void Main(string[] args)
        {
            var generator = new Generator();
            generator.Generate();
        }

        private void LoadUsernames()
        {
            this.names = File.ReadLines("users.txt").ToArray(); ;
            this.names.ToList().ForEach(line => this.names[Array.IndexOf(this.names, line)] = line.Trim());
        }

        private void GenerateUsers(StreamWriter file)
        {

            string password = "$1$YOXunEUT$4X9aCMw9B63FkjCntsoGG0";


            file.WriteLine("INSERT INTO user (userId, firstName, lastName, password) VALUES");

            file.Write($"(0, \"{names[0].Split(' ')[0]}\", \"{names[0].Split(' ')[1]}\", \"{password}\")");

            for (int id = 1; id < names.Count(); id++)
            {
                file.Write($",\n({id}, \"{names[id].Split(' ')[0]}\", \"{names[id].Split(' ')[1]}\", \"{password}\")");
            }

            file.Write(";");

        }
    }
}
