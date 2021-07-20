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

        private string _currentDisplay = string.Empty;
        private string _result = string.Empty;
        private string _operand1 = string.Empty;
        private string _operand2 = string.Empty;


        #endregion Declarations

        #region Properties

        #region Commands

        public DelegateCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new DelegateCommand(() => onOperate(Operations.Add));
                }

                return _addCommand;
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
                        CurrentDisplay += "1";
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
                        CurrentDisplay += "2";
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
                        CurrentDisplay += "3";
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
                        CurrentDisplay += "4";
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
                        CurrentDisplay += "5";
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
                        CurrentDisplay += "6";
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
                        CurrentDisplay += "7";
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
                        CurrentDisplay += "8";
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
                        CurrentDisplay += "9";
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
                        CurrentDisplay += "0";
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



        #endregion Public Methods

        #region Private Methods

        private void onOperate(
            string operation)
        {
            if (string.IsNullOrWhiteSpace(_operand1))
            {
                _operand1 = CurrentDisplay;
            }
            else
            {
                switch (operation)
                {
                    case Operations.Add:
                        CurrentDisplay = (float.Parse(_operand1) + float.Parse(CurrentDisplay)).ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion Private Methods

        
    }
}
