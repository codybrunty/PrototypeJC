using System.Collections.Generic;

public class CommandExecutor {
    private readonly Stack<ICommand> commandHistory = new();
    public void ExecuteCommand(ICommand command) {
        command.Execute();
        commandHistory.Push(command);
    }

    public void UndoLastCommand() {
        if (commandHistory.Count > 0) {
            commandHistory.Pop().Undo();
        }
    }

    public void UndoAllCommands() {
        while (commandHistory.Count > 0) {
            commandHistory.Pop().Undo();
        }
    }

    public void ClearHistory() {
        commandHistory.Clear();
    }
}