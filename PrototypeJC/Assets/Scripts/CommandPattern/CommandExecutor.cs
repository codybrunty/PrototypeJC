public class CommandExecutor {
    public void ExecuteCommand(ICommand command) {
        command.Execute();
    }
}