using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.ViewModels.ArithmeticOperations
{
    public interface IArithmeticOperationViewModel
    {
        float PerformArithmeticOperation(
            float val1, 
            float val2);
    }
}
