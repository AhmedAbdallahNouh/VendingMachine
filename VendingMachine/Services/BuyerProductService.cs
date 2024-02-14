using VendingMachine.Interfaces;

namespace VendingMachine.Services
{
	public class BuyerProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		public BuyerProductService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
	}
}
