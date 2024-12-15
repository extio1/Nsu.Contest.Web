namespace Nsu.Contest.Web.Common.Util;

using Nsu.Contest.Web.Common.Entity;

public class EmployeeReader
{
    public IEnumerable<T> ReadEmployees<T>(string path) where T : Employee
    {
        var employees = new List<T>();

        using (var input = new StreamReader(File.OpenRead(path)))
        {
            while (!input.EndOfStream)
            {
                var line = input.ReadLine()?.Split(';', 2);
                if (line == null || line.Length < 2) continue;

                var id = int.Parse(line[0]);
                var name = line[1];

                var employee = (T)Activator.CreateInstance(typeof(T), id, name);
                employees.Add(employee);
            }
        }

        return employees;
    }

    public T GetEmployeeById<T>(string path, int id) where T : Employee
    {
        var employees = ReadEmployees<T>(path);
        return employees.First(e => e.Id == id);
    }
}

