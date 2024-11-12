using System.Reflection;

var employees = new List<Employee>
{
	new Employee { Name = "John", Age = 30, Experience = 5, IsManager = true },
	new Employee { Name = "Jane", Age = 25, Experience = 3, IsManager = false },
	new Employee { Name = "Bob", Age = 40, Experience = 10, IsManager = true },
	new Employee { Name = "Alice", Age = 22, Experience = 1, IsManager = false },
	new Employee { Name = "Tom", Age = 35, Experience = 8, IsManager = true }
};

// Динамическая загрузка библиотеки
string path = @"D:\University\C#_3 sem\Lab6\353504_Antonava\353504_Antonava\bin\Debug\net8.0\FileService.dll";
var assembly = Assembly.LoadFrom(path);

Type? type = assembly.GetTypes().FirstOrDefault(t => t.Name.StartsWith("FileService`1"));

if (type is not null) { 
	Type closedType = type.MakeGenericType(typeof(Employee));

	MethodInfo? saveDataMethod = closedType.GetMethod("SaveData");
	object? fileServiceInstance = Activator.CreateInstance(closedType);

	saveDataMethod?.Invoke(fileServiceInstance, new object[] { employees, "employees.json" });

	MethodInfo? readDataMethod = closedType.GetMethod("ReadFile");
	IEnumerable<Employee>? employee = readDataMethod?.Invoke(fileServiceInstance, new object[] { "employees.json" }) as IEnumerable<Employee>;

	// Вывод данных на консоль
	foreach (var employe in employee)
		{
			Console.WriteLine($"Name: {employe.Name}, Age: {employe.Age}, Experience: {employe.Experience}, IsManager: {employe.IsManager}");
		}
	}
else
{
	Console.WriteLine("Ошибка: тип FileService`1 не найден в библиотеке.");
}