using ConsoleStudentRecordManagement.DTO;
using ConsoleStudentRecordManagement.DataAccess;
using ConsoleStudentRecordManagement.Integration;
using ConsoleStudentRecordManagement.Application.Commands;
namespace ConsoleStudentRecordManagement.Presentation;

public class ConsoleUI
    {
        private readonly IDataRepository _repository;
        private readonly IQuoteApiAdapter _quoteAdapter;

        public ConsoleUI(IDataRepository repository, IQuoteApiAdapter quoteAdapter)
        {
            _repository = repository;
            _quoteAdapter = quoteAdapter;
        }

        public async Task RunAsync()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. Добавить студента");
                Console.WriteLine("2. Редактировать студента");
                Console.WriteLine("3. Просмотреть студентов");
                Console.WriteLine("4. Удалить студента");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите опцию: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await AddStudentAsync();
                        break;
                    case "2":
                        await EditStudentAsync();
                        break;
                    case "3":
                        ViewStudents();
                        break;
                    case "4":
                        await DeleteStudentAsync();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }
            }
        }

        private async Task AddStudentAsync()
        {
            Console.Write("Введите имя студента: ");
            var name = Console.ReadLine();
            Console.Write("Введите оценку студента: ");
            if (!double.TryParse(Console.ReadLine(), out double grade))
            {
                Console.WriteLine("Некорректный ввод оценки.");
                return;
            }

            var studentDto = DTOFactory.CreateStudentDTO(name, grade);

            try
            {
                var command = new AddStudentCommand(studentDto, _repository, _quoteAdapter);
                await command.ExecuteAsync();
                if (command.Quote != null)
                {
                    Console.WriteLine("Мотивирующая цитата:");
                    Console.WriteLine($"\"{command.Quote.Content}\" – {command.Quote.Author}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        private async Task EditStudentAsync()
        {
            var students = _repository.GetAllStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("Нет студентов для редактирования.");
                return;
            }
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i}: {students[i].Name} - {students[i].Grade}");
            }
            Console.Write("Выберите индекс студента для редактирования: ");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Некорректный индекс.");
                return;
            }
            Console.Write("Введите новое имя студента: ");
            var newName = Console.ReadLine();
            Console.Write("Введите новую оценку студента: ");
            if (!double.TryParse(Console.ReadLine(), out double newGrade))
            {
                Console.WriteLine("Некорректный ввод оценки.");
                return;
            }
            var studentDto = DTOFactory.CreateStudentDTO(newName, newGrade);

            try
            {
                var command = new EditStudentCommand(index, studentDto, _repository);
                await command.ExecuteAsync();
                Console.WriteLine("Данные студента обновлены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        private void ViewStudents()
        {
            var command = new ViewStudentCommand(_repository);
            command.ExecuteAsync().Wait();
            var students = command.Students;
            if (students.Count == 0)
            {
                Console.WriteLine("Нет студентов для отображения.");
                return;
            }
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i}: {students[i].Name} - {students[i].Grade}");
            }
        }

        private async Task DeleteStudentAsync()
        {
            var students = _repository.GetAllStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("Нет студентов для удаления.");
                return;
            }
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i}: {students[i].Name} - {students[i].Grade}");
            }
            Console.Write("Выберите индекс студента для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Некорректный индекс.");
                return;
            }

            try
            {
                var command = new DeleteStudentCommand(index, _repository);
                await command.ExecuteAsync();
                Console.WriteLine("Студент удалён.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }