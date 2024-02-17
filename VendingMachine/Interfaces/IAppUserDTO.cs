namespace VendingMachine.Interfaces
{
    public interface IAppUserDTO
    {
        string Id { get; set; }
        string Role { get; set; }
        string UserName { get; set; }
    }
}