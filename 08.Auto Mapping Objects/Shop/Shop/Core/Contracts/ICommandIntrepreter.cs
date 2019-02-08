namespace Shop.App.Core.Contracts
{
    public interface ICommandIntrepreter
    {
        string Read(string[] input);
    }
}