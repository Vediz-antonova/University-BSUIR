using ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;
namespace ConsoleDoc.ConsoleDocumentEditor.Services;

public class UndoRedoManager
{
    private readonly Stack<IDocumentCommand> _undoStack = new();
    private readonly Stack<IDocumentCommand> _redoStack = new();

    public void ExecuteCommand(IDocumentCommand command)
    {
        command.Execute();
        _undoStack.Push(command);
        _redoStack.Clear(); 
        Console.WriteLine("Выполнено: " + command.GetDescription());
    }

    public void Undo()
    {
        if (_undoStack.Count > 0)
        {
            var command = _undoStack.Pop();
            command.Unexecute();
            _redoStack.Push(command);
            Console.WriteLine("Отмена: " + command.GetDescription());
        }
    }
    public void Redo()
    {
        if (_redoStack.Count > 0)
        {
            var command = _redoStack.Pop();
            command.Execute();
            _undoStack.Push(command);
            Console.WriteLine("Повтор: " + command.GetDescription());
        }
    }
}