using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;

namespace ConsoleDoc.Tests
{
    public class CommandTests
    {
        [Fact]
        public void EditTextCommand_Should_UpdateContent_And_BeUndoable()
        {
            var document = new PlainTextDocument("TestDoc");
            document.Edit("Original");
            var command = new EditTextCommand(document, "Modified");
            command.Execute();
            Assert.Equal("Modified", document.Content);
            command.Unexecute();
            Assert.Equal("Original", document.Content);
        }

        [Fact]
        public void InsertTextCommand_Should_InsertTextAtCorrectPosition_AndUndoWorks()
        {
            var document = new PlainTextDocument("TestDoc");
            document.Edit("Hello World");
            var command = new InsertTextCommand(document, "Beautiful ", 6);
            command.Execute();
            Assert.Equal("Hello Beautiful World", document.Content);
            command.Unexecute();
            Assert.Equal("Hello World", document.Content);
        }

        [Fact]
        public void DeleteTextCommand_Should_RemoveSubstring_AndUndoWorks()
        {
            var document = new PlainTextDocument("TestDoc");
            document.Edit("Hello Beautiful World");
            var command = new DeleteTextCommand(document, 6, 10);
            command.Execute();
            Assert.Equal("Hello World", document.Content);
            command.Unexecute();
            Assert.Equal("Hello Beautiful World", document.Content);
        }
    }
}