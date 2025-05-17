namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;

public interface IDocumentCommand
{
    public void Execute();
    public void Unexecute();
    string GetDescription();
}