
using Course.Entities;
using System.Globalization;

namespace Course
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            Console.WriteLine();

            List<Employee> list = new List<Employee>();

            using (StreamReader sr = File.OpenText(path))
            {
                while(!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    string name = fields[0];
                    string email = fields[1];
                    double price = double.Parse(fields[2], CultureInfo.InvariantCulture);
                    list.Add(new Employee(name, email, price));
                }
            }

            
            Console.Write("Enter salary: ");
            double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine();

            Console.WriteLine($"Email of people whose salary is more than {salary}: ");
            // usando notação similar SQL
            var getEmails = from e in list
                            where e.Salary > salary
                            orderby e.Email
                            select e.Email;
                           
            foreach (string email in getEmails)
            {
                Console.WriteLine(email);
            }

            Console.WriteLine();
            // forma 1
            var sum = list.Where(e => e.Name[0] == 'M').Select(e => e.Salary).Sum();
            // forma 2
            var sum1 = list.Where(e => e.Name[0] == 'M')
                .Select(e => e.Salary).Aggregate(0.0, (x, y) => x+y);

            // usando notação similar SQL
            var sum2 = (from e in list where e.Name[0] == 'M' select e.Salary)
                .Aggregate(0.0, (x,y) => x + y);

            Console.WriteLine("Sum of salary of people whose name starts with 'M': "
                + sum2.ToString("f2", CultureInfo.InvariantCulture));


        }
    }
}
