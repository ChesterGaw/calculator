namespace calculator.ViewModels.ArithmeticOperations
{
    public class AddViewModel : IBasicArithmeticOperationViewModel
    {
        public float PerformArithmeticOperation(float val1, float val2)
        {
            return 
                val1 + val2 < float.MaxValue &&
                val1 + val2 > float.MinValue ?
                val1 + val2 : float.MinValue;
        }
    }
}
