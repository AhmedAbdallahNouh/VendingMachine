using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Validations
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class DepositAcceptedValue : ValidationAttribute
	{
		private readonly int[] _allowedValues = {5,10,50,50,10};

		public override bool IsValid(object? value)
		{
			if (value == null)
			{
				return false;
			}
			int depositValue;
			if (int.TryParse(value.ToString(), out depositValue))
			{
				return Array.Exists(_allowedValues, element => element == depositValue);
			}
			return base.IsValid(value);
		}
	}
}
