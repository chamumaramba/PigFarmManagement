namespace PigFarmManagement.Application.Interfaces.Services
{
    public interface ICurrentUserServices
    {
        Guid UserId {get;}
        Guid FarmId { get; }
    }
}