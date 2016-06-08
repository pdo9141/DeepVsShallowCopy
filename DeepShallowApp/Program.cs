using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeepShallowApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Shallow copy retains reference pointers
            Person p1 = new Person(15, "Bob", "Henry");
            Person p2 = (Person)p1.ShallowCopy();
            p2.Desc.FirstName = "Jack";
            Console.WriteLine(p2.Desc.FirstName);
            Console.WriteLine(p1.Desc.FirstName);
            Console.WriteLine("HashCode: " + p2.GetHashCode());
            Console.WriteLine("HashCode: " + p1.GetHashCode());
            Console.WriteLine("");

            // You can also do deep copies by using serialization (binary (BinaryFormatter, class must be marked [Serializable], slow), xml, json, etc.)
            Person p3 = new Person(18, "Cindy", "Crawford");
            Person p4 = p3.DeepCopy();
            p3.Desc.FirstName = "Jack";
            Console.WriteLine(p3.Desc.FirstName);
            Console.WriteLine(p4.Desc.FirstName);
            Console.WriteLine("HashCode: " + p2.GetHashCode());
            Console.WriteLine("HashCode: " + p1.GetHashCode());
            Console.WriteLine("");

            Person p5 = new Person(27, "Jessica", "Alba");
            Person p6 = p5;
            Console.WriteLine("HashCode: " + p5.GetHashCode());
            Console.WriteLine("HashCode: " + p6.GetHashCode());
            
            Console.ReadLine();
        }
    }

    class Person
    {
        public int Age;
        public PersonDescription Desc;

        public Person(int age, string firstName, string lastName)
        {
            this.Age = age;
            this.Desc = new PersonDescription(firstName, lastName);
        }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        public Person DeepCopy()
        {
            Person deepCopyPerson = new Person(this.Age, this.Desc.FirstName, this.Desc.LastName);
            return deepCopyPerson;
        }
    }

    class PersonDescription
    {
        public string FirstName;
        public string LastName;

        public PersonDescription(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }

    public static class ExtensionMethods
    {
        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
