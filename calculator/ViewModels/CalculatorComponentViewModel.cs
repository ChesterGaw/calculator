using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using calculator.Constants;
using calculator.ViewModels.ArithmeticOperations;
using Prism.Commands;

namespace calculator.ViewModels
{
    public class CalculatorComponentViewModel : PropertyChangedBase
    {
        #region Declarations

        #region Commands
        
        private DelegateCommand _addCommand;
        private DelegateCommand _backCommand;
        private DelegateCommand _clearCommand;
        private DelegateCommand _clearEntryCommand;
        private DelegateCommand _divideCommand;
        private DelegateCommand _equalsCommand;
        private DelegateCommand _memoryClearCommand;
        private DelegateCommand _memoryMinusCommand;
        private DelegateCommand _memoryPlusCommand;
        private DelegateCommand _memoryRecallCommand;
        private DelegateCommand _memorySaveCommand;
        private DelegateCommand _multiplyCommand;
        private DelegateCommand _negateCommand;
        private DelegateCommand _oneDivideByXCommand;
        private DelegateCommand _subtractCommand;
        private DelegateCommand _squareRootCommand;

        private DelegateCommand _oneCommand;
        private DelegateCommand _twoCommand;
        private DelegateCommand _threeCommand;
        private DelegateCommand _fourCommand;
        private DelegateCommand _fiveCommand;
        private DelegateCommand _sixCommand;
        private DelegateCommand _sevenCommand;
        private DelegateCommand _eightCommand;
        private DelegateCommand _nineCommand;
        private DelegateCommand _zeroCommand;
        private DelegateCommand _dotCommand;

        #endregion Commands

        private string _currentDisplay = "0";
        private string _equalsSign = string.Empty;
        private bool _justSelectedOperator;
        private string _operand1 = string.Empty;
        private string _operand2 = string.Empty;
        private string _operator = string.Empty;
        private string _result = string.Empty;

        #endregion Declarations

        #region Properties

        #region Commands

        public DelegateCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new DelegateCommand(
                        () => onOperate(Operations.Add));
                }

                return _addCommand;
            }
        }

        public DelegateCommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new DelegateCommand(
                        () => onBack(),
                        () => !string.IsNullOrWhiteSpace(CurrentDisplay));
                }

                return _backCommand;
            }
        }

        public DelegateCommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new DelegateCommand(
                        () =>
                        {
                            onClear(false);
                        });
                }

                return _clearCommand;
            }
        }

        public DelegateCommand ClearEntryCommand
        {
            get
            {
                if (_clearEntryCommand == null)
                {
                    _clearEntryCommand = new DelegateCommand(
                        () => onClear(true));
                }

                return _clearEntryCommand;
            }
        }

        public DelegateCommand DivideCommand
        {
            get
            {
                if (_divideCommand == null)
                {
                    _divideCommand = new DelegateCommand(
                        () => onOperate(Operations.Divide));
                }

                return _divideCommand;
            }
        }

        public DelegateCommand EqualsCommand
        {
            get
            {
                if (_equalsCommand == null)
                {
                    _equalsCommand = new DelegateCommand(
                        () => onEquals());
                }

                return _equalsCommand;
            }
        }

        public DelegateCommand MultiplyCommand
        {
            get
            {
                if (_multiplyCommand == null)
                {
                    _multiplyCommand = new DelegateCommand(
                        () => onOperate(Operations.Multiply));
                }

                return _multiplyCommand;
            }
        }

        public DelegateCommand NegateCommand
        {
            get
            {
                if (_negateCommand == null)
                {
                    _negateCommand = new DelegateCommand(
                        () => onSimpleOperate(Operations.Negate));
                }

                return _negateCommand;
            }
        }

        public DelegateCommand OneDivideByXCommand
        {
            get
            {
                if (_oneDivideByXCommand == null)
                {
                    _oneDivideByXCommand = new DelegateCommand(
                        () => onSimpleOperate(Operations.OneDivideByX));
                }

                return _oneDivideByXCommand;
            }
        }

        public DelegateCommand SubtractCommand
        {
            get
            {
                if (_subtractCommand == null)
                {
                    _subtractCommand = new DelegateCommand(
                        () => onOperate(Operations.Subtract));
                }

                return _subtractCommand;
            }
        }

        public DelegateCommand SquareRootCommand
        {
            get
            {
                if (_squareRootCommand == null)
                {
                    _squareRootCommand = new DelegateCommand(
                        () => onSimpleOperate(Operations.SquareRoot));
                }

                return _squareRootCommand;
            }
        }

        public DelegateCommand OneCommand
        {
            get
            {
                if (_oneCommand == null)
                {
                    _oneCommand = new DelegateCommand(() =>
                    {
                        CheckClearCurrentDisplay(false);
                        if (CurrentDisplay != "0")
                        {
                            CurrentDisplay += "1";
                        }
                        else
                        {
                            CurrentDisplay = "1";
                        }
                        
                    });
                }

                return _oneCommand;
            }
        }

        public DelegateCommand TwoCommand
        {
            get
            {
                if (_twoCommand == null)
                {
                    _twoCommand = new DelegateCommand(() =>
                    {
                        CheckClearCurrentDisplay(false);
                        if (CurrentDisplay != "0")
                        {
                            CurrentDisplay += "2";
                        }
                        else
                        {
                            CurrentDisplay = "2";
                        }
                    });
                }

                return _twoCommand;
            }
        }

        public DelegateCommand ThreeCommand
        {
            get
            {
                if (_threeCommand == null)
                {
                    _threeCommand = new DelegateCommand(() =>
                    {
                        CheckClearCurrentDisplay(false);
                        if (CurrentDisplay != "0")
                        {
                            CurrentDisplay += "3";
                        }
                        else
                        {
                            CurrentDisplay = "3";
                        }
                    });
                }

                return _threeCommand;
            }
        }

        public DelegateCommand FourCommand
        {
            get
            {
                if (_fourCommand == null)
                {
                    _fourCommand = new DelegateCommand(() =>
                    {
                        CheckClearCurrentDisplay(false);
                        if (CurrentDisplay != "0")
                        {
                            CurrentDisplay += "4";
                        }
                        else
                        {
                            CurrentDisplay = "4";
                        }
                    });
                }

                return _fourCommand;
            }
        }

        public DelegateCommand FiveCommand
        {
            get
            {
                if (_fiveCommand == null)
                {
                    _fiveCommand = new DelegateCommand(() =>
                    {
                        CheckClearCurrentDisplay(false);
                        if (CurrentDisplay != "0")
                        {
                            CurrentDisplay += "5";
                        }
                        else
                        {
                            CurrentDisplay = "5";
                        }
                    });
                }

                return _fiveCommand;
            }
        }

        public DelegateCommand SixCommand
        {
            get
            {
                if (_sixCommand == null)
                {
                    _sixCommand = new DelegateCommand(() =>
                    {
                        CheckClearCurrentDisplay(false);
                        if (CurrentDisplay != "0")
                        {
                            CurrentDisplay += "6";
                        }
                        else
                        {
                            CurrentDisplay = "6";
                        }
                    });
                }

                return _sixCommand;
            }
        }

        public DelegateCommand SevenCommand
        {
            get
            {
                if (_sevenCommand == null)
                {
                    _sevenCommand = new DelegateCommand(() =>
                    {
                        CheckClearCurrentDisplay(false);
                        if (CurrentDisplay != "0")
                        {
                            CurrentDisplay += "7";
                        }
                        else
                        {
                            CurrentDisplay = "7";
                        }
                    });
                }

                return _sevenCommand;
            }
        }

        public DelegateCommand EightCommand
        {
            get
            {
                if (_eightCommand == null)
                {
                    _eightCommand = new DelegateCommand(() =>
                    {
                        CheckClearCurrentDisplay(false);
                        if (CurrentDisplay != "0")
                        {
                            CurrentDisplay += "8";
                        }
                        else
                        {
                            CurrentDisplay = "8";
                        }
                    });
                }

                return _eightCommand;
            }
        }

        public DelegateCommand NineCommand
        {
            get
            {
                if (_nineCommand == null)
                {
                    _nineCommand = new DelegateCommand(() =>
                    {
                        CheckClearCurrentDisplay(false);
                        if (CurrentDisplay != "0")
                        {
                            CurrentDisplay += "9";
                        }
                        else
                        {
                            CurrentDisplay = "9";
                        }
                    });
                }

                return _nineCommand;
            }
        }

        public DelegateCommand ZeroCommand
        {
            get
            {
                if (_zeroCommand == null)
                {
                    _zeroCommand = new DelegateCommand(() =>
                    {
                        CheckClearCurrentDisplay(false);
                        if (CurrentDisplay != "0")
                        {
                            CurrentDisplay += "0";
                        }
                    });
                }

                return _zeroCommand;
            }
        }

        public DelegateCommand DotCommand
        {
            get
            {
                if (_dotCommand == null)
                {
                    _dotCommand = new DelegateCommand(
                    () =>
                    {
                        CheckClearCurrentDisplay(true);
                        CurrentDisplay += ".";
                    },
                    () =>
                    {
                        return
                            CurrentDisplay != null &&
                            !CurrentDisplay.Contains(".");
                    });
                }

                return _dotCommand;
            }
        }

        #endregion Commands

        public string CurrentDisplay
        {
            get { return _currentDisplay; }
            set
            {
                _currentDisplay = value;
                OnPropertyChanged();
            }
        }

        public string EqualsSign
        {
            get { return _equalsSign; }
            set
            {
                _equalsSign = value;
                OnPropertyChanged();
            }
        }

        public bool JustSelectedOperator
        {
            get { return _justSelectedOperator; }
            set
            {
                _justSelectedOperator = value;
            }
        }

        public string Operand1
        {
            get { return _operand1; }
            set
            {
                _operand1 = value;
                OnPropertyChanged();
            }
        }

        public string Operand2
        {
            get { return _operand2; }
            set
            {
                _operand2 = value;
                OnPropertyChanged();
            }
        }

        public string Operator
        {
            get { return _operator; }
            set
            {
                _operator = value;
                OnPropertyChanged();
            }
        }

        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Constructor

        public CalculatorComponentViewModel()
        {
        }

        #endregion Constructor

        #region Public Methods

        public void CheckClearCurrentDisplay(bool isDot)
        {
            if (JustSelectedOperator)
            {
                if (isDot)
                {
                    CurrentDisplay = "0";
                }
                else
                {
                    CurrentDisplay = string.Empty;
                }
                
                JustSelectedOperator = false;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void onBack()
        {
            if (CurrentDisplay.Length == 1)
            {
                CurrentDisplay = "0";
            }
            else
            {
                CurrentDisplay = CurrentDisplay.Remove(CurrentDisplay.Count() - 1);
            }            
        }

        private void onClear(bool clearEntry)
        {
            CurrentDisplay = "0";
            if (!clearEntry ||
                !string.IsNullOrEmpty(EqualsSign))
            { 
                EqualsSign = string.Empty;
                Operand1 = string.Empty;
                Operand2 = string.Empty;
                Operator = string.Empty;
                JustSelectedOperator = false;
            }
        }

        private void onEquals()
        {           
            switch (Operator)
            {
                case Operations.Add:
                    try
                    {
                        Operand2 = CurrentDisplay;
                        CurrentDisplay = (float.Parse(Operand1) + float.Parse(CurrentDisplay)).ToString();
                        EqualsSign = "=";
                    }
                    catch (OverflowException)
                    {
                        CurrentDisplay = "Number is out of range!";
                    }
                    break;
                case Operations.Subtract:
                    try
                    {
                        Operand2 = CurrentDisplay;
                        CurrentDisplay = (float.Parse(Operand1) - float.Parse(CurrentDisplay)).ToString();
                        EqualsSign = "=";
                    }
                    catch (OverflowException)
                    {
                        CurrentDisplay = "Number is out of range!";
                    }
                    break;
                case Operations.Multiply:
                    try
                    {
                        Operand2 = CurrentDisplay;
                        CurrentDisplay = (float.Parse(Operand1) * float.Parse(CurrentDisplay)).ToString();
                        EqualsSign = "=";
                    }
                    catch (OverflowException)
                    {
                        CurrentDisplay = "Number is out of range!";
                    }
                    break;
                case Operations.Divide:
                    try
                    {
                        Operand2 = CurrentDisplay;
                        CurrentDisplay = (float.Parse(Operand1) / float.Parse(CurrentDisplay)).ToString();
                        EqualsSign = "=";
                    }
                    catch (OverflowException)
                    {
                        CurrentDisplay = "Number is out of range!";
                    }
                    break;
                default:
                    break;
            }
        }

        private void onOperate(
            string operation)
        {
            JustSelectedOperator = true;
            
            if (string.IsNullOrWhiteSpace(Operand1) ||
                !string.IsNullOrWhiteSpace(EqualsSign))
            {
                Operand1 = CurrentDisplay;
                Operand2 = string.Empty;
                EqualsSign = string.Empty;
            }
            else
            {   
                if (Operator == operation)
                {
                    switch (operation)
                    {
                        case Operations.Add:
                            try
                            {
                                CurrentDisplay = (float.Parse(Operand1) + float.Parse(CurrentDisplay)).ToString();
                                Operand1 = CurrentDisplay;
                            }
                            catch (OverflowException)
                            {
                                CurrentDisplay = "Number is out of range!";
                            }                        
                            break;
                        case Operations.Subtract:
                            try
                            {
                                CurrentDisplay = (float.Parse(Operand1) - float.Parse(CurrentDisplay)).ToString();
                                Operand1 = CurrentDisplay;
                            }
                            catch (OverflowException)
                            {
                                CurrentDisplay = "Number is out of range!";
                            }
                            break;
                        case Operations.Multiply:
                            try
                            {
                                CurrentDisplay = (float.Parse(Operand1) * float.Parse(CurrentDisplay)).ToString();
                                Operand1 = CurrentDisplay;
                            }
                            catch (OverflowException)
                            {
                                CurrentDisplay = "Number is out of range!";
                            }
                            break;
                        case Operations.Divide:
                            try
                            {
                                CurrentDisplay = (float.Parse(Operand1) / float.Parse(CurrentDisplay)).ToString();
                                Operand1 = CurrentDisplay;
                            }
                            catch (OverflowException)
                            {
                                CurrentDisplay = "Number is out of range!";
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            Operator = operation;
        }

        private void onSimpleOperate(string operation)
        {
            switch (operation)
            {
                case Operations.Negate:
                    if (CurrentDisplay.StartsWith("-"))
                    {
                        CurrentDisplay = CurrentDisplay.Remove(0, 1);
                    }
                    else
                    {
                        CurrentDisplay = CurrentDisplay.Insert(0, "-");
                    }                    
                    break;
                case Operations.SquareRoot:
                    CurrentDisplay = Math.Sqrt(float.Parse(CurrentDisplay)).ToString();
                    break;
                case Operations.OneDivideByX:
                    CurrentDisplay = (1 / float.Parse(CurrentDisplay)).ToString();
                    break;
                default:
                    break;
            }
        }

        #endregion Private Methods


    }
}
