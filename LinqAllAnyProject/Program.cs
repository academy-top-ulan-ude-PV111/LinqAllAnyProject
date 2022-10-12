using System.Diagnostics.CodeAnalysis;

namespace LinqAllAnyProject
{
    class PersonEqualsComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            bool result = false;
            result = x.Name?.ToLower() == y.Name?.ToLower();
            result = result && x.Age == y.Age;
            return result;
        }

        public int GetHashCode([DisallowNull] Person obj)
        {
            throw new NotImplementedException();
        }
    }

    class Person
    {
        public string? Name { set; get; }
        public int Age { set; get; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new()
            {
                new Person{ Name = "Mike", Age = 23 },
                new Person{ Name = "Tom", Age = 35 },
                new Person{ Name = "Bill", Age = 41 },
                new Person{ Name = "Sam", Age = 29 },
                new Person{ Name = "Joe", Age = 34 },
                new Person{ Name = "Susan", Age = 23 },
                new Person{ Name = "Leo", Age = 19 },
                new Person{ Name = "Peet", Age = 32 },
                new Person{ Name = "Jim", Age = 45 },
                new Person{ Name = "Tim", Age = 27 },
            };

            Person pers = new Person { Name = "Tim", Age = 27 };

            var personsAll = persons.All(p => p.Age >= 16);
            Console.WriteLine($"{personsAll}");

            var personsAny = persons.Any(p => p.Age < 16);
            Console.WriteLine($"{personsAny}");

            var personContain = persons.Contains(
                                        new Person { Name = "Tim", Age = 27 },
                                        new PersonEqualsComparer());
            Console.WriteLine($"{personContain}");

            var personContainUpper = persons.Contains(
                                new Person { Name = "TIM", Age = 27 },
                                new PersonEqualsComparer());
            Console.WriteLine($"{personContainUpper}");

            try
            {
                var first = persons.First(p => p.Name.Contains('N'));
                Console.WriteLine($"first: {first.Name} {first.Age}");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //var firstDefault = persons.FirstOrDefault(p => p.Name.Contains('N'));
            //Console.WriteLine($"first: {firstDefault.Name} {firstDefault.Age}");
            Console.WriteLine(new String('-', 10));

            int age = 25;
            var names = from p in persons
                        where p.Age >= age
                        select new { Name = p.Name };


            age = 35;
            foreach (var name in names)
                Console.WriteLine(name.Name);
            Console.WriteLine(new String('-', 10));
            age = 25;
            foreach (var name in names)
                Console.WriteLine(name.Name);
            Console.WriteLine(new String('-', 10));
        }
    }
}