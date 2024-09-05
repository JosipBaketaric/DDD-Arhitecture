namespace Logistics.Application.Command.Shipping
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
