using System;
using System.Linq;
using System.Data.Entity;

namespace EmployeeListConsoleCore
{
    class Program
    {

        static void Main(string[] args)
        {
            // Prints out all the persons
            PrintPersons();

            while (true)
            {
                Console.WriteLine("1 - Print persons.");
                Console.WriteLine("2 - Add person.");
                Console.WriteLine("3 - Remove person.");

                int selection = Int32.Parse(Console.ReadLine());
                if (selection == 1)
                    PrintPersons();
                if (selection == 2)
                    AddPersonSub();
                if (selection == 3)
                    RemovePersonSub();

            }
        }
        static void PrintPersons()
        {
            Console.WriteLine("Please wait...");
            using (var ctx = new Context())
            {
                var list = ctx.Persons.ToList();
                Console.WriteLine("Persons count: " + list.Count);
                for (int i = 0; i < list.Count; i++)
                {
                    Person p = (Person)list[i];
                    Console.WriteLine(p.PersonId + ": " + p.FirstName + " " + p.LastName);
                }

            }
        }
        static void AddPersonSub()
        {
            Console.WriteLine("First name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Last name: ");
            string lastName = Console.ReadLine();

            Person p = new Person() { FirstName = firstName, LastName = lastName };
            using (var ctx = new Context())
            {
                ctx.Persons.Add(p);
                ctx.SaveChanges();
            }
        }

        static void RemovePersonSub()
        {
            Console.WriteLine("Enter person id: ");
            int toRemove = Int32.Parse(Console.ReadLine());

            using (var ctx = new Context())
            {
                Person p = new Person() { PersonId = toRemove };
                ctx.Persons.Attach(p);
                ctx.Persons.Remove(p);
                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Index probably out of range.");
                }
            }
        }
    }

    public class Person
    {
        public int PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
    public class Context : DbContext
    {
        public Context()
        { }
        public DbSet<Person> Persons { get; set; }
    }
}
