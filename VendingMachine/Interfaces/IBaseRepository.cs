namespace VendingMachine.Interfaces
{
	public interface IBaseRepository<T> where T : class
	{
		T GetAll();
		T GetById(int id);

	}
}
