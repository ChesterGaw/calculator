using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using calculator.Constants;
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

        private DelegateCommand _copyCommand;
        private DelegateCommand _exportCommand;
        private DelegateCommand _importCommand;
        private DelegateCommand _pasteCommand;
        
        #endregion Commands

        private string _currentDisplay = "0";
        private string _equalsSign = string.Empty;
        private List<HistoryEntry> _history = new List<HistoryEntry>();
        private bool _justSelectedOperator;
        private ObservableCollection<string> _memory;
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
        
        public DelegateCommand MemoryClearCommand
        {
            get
            {
                if (_memoryClearCommand == null)
                {
                    _memoryClearCommand = new DelegateCommand(
                        () => onMemoryCommand(MemoryActions.MemoryClear),
                        () => Memory != null && Memory.Count > 0);
                }

                return _memoryClearCommand;
            }
        }
        
        public DelegateCommand MemoryMinusCommand
        {
            get
            {
                if (_memoryMinusCommand == null)
                {
                    _memoryMinusCommand = new DelegateCommand(
                        () => onMemoryCommand(MemoryActions.MemoryMinus));
                }

                return _memoryMinusCommand;
            }
        }
        
        public DelegateCommand MemoryPlusCommand
        {
            get
            {
                if (_memoryPlusCommand == null)
                {
                    _memoryPlusCommand = new DelegateCommand(
                        () => onMemoryCommand(MemoryActions.MemoryPlus));
                }

                return _memoryPlusCommand;
            }
        } 
        
        public DelegateCommand MemoryRecallCommand
        {
            get
            {
                if (_memoryRecallCommand == null)
                {
                    _memoryRecallCommand = new DelegateCommand(
                        () => onMemoryCommand(MemoryActions.MemoryRecall),
                        () => Memory != null && Memory.Count > 0);
                }

                return _memoryRecallCommand;
            }
        }
        
        public DelegateCommand MemorySaveCommand
        {
            get
            {
                if (_memorySaveCommand == null)
                {
                    _memorySaveCommand = new DelegateCommand(
                        () => onMemoryCommand(MemoryActions.MemorySave));
                }

                return _memorySaveCommand;
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

        public DelegateCommand CopyCommand
        {
            get
            {
                if (_copyCommand == null)
                {
                    _copyCommand = new DelegateCommand(() =>
                    {
                        Clipboard.SetText(CurrentDisplay);
                    });
                }

                return _copyCommand;
            }
        }
        
        public DelegateCommand ExportCommand
        {
            get
            {
                if (_exportCommand == null)
                {
                    _exportCommand = new DelegateCommand(() => onExport());
                }

                return _exportCommand;
            }
        }
        
        public DelegateCommand ImportCommand
        {
            get
            {
                if (_importCommand == null)
                {
                    _importCommand = new DelegateCommand(() => onImport());
                }

                return _importCommand;
            }
        }

        public DelegateCommand PasteCommand
        {
            get
            {
                if (_pasteCommand == null)
                {
                    _pasteCommand = new DelegateCommand(() =>
                    {
                        float pastedString = float.MinValue;
                        if (float.TryParse(Clipboard.GetData(DataFormats.Text) as string, out pastedString))
                        {
                            CurrentDisplay = pastedString.ToString();
                        }
                        
                    });
                }

                return _pasteCommand;
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

        public ObservableCollection<string> Memory
        {
            get { return _memory; }
            set
            {
                _memory = value;
                OnPropertyChanged();
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
            Memory = new ObservableCollection<string>();
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

        private float getFloat(string numbers)
        {
            float floatNumber;
            if (float.TryParse(numbers, out floatNumber))
            {
                return floatNumber;
            }

            return floatNumber;
        }

        private void logHistory(
            string historyAction,
            string value)
        {
            _history.Add(
                new HistoryEntry(
                    historyAction,
                    (_history.Count + 1).ToString(),
                    value));
        }

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
            logHistory(HistoryActions.Clear, CurrentDisplay);

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
                        CurrentDisplay = (getFloat(Operand1) + getFloat(CurrentDisplay)).ToString();
                        EqualsSign = "=";
                        logHistory(HistoryActions.Equal, CurrentDisplay);
                    }
                    catch (OverflowException)
                    {
                        CurrentDisplay = "Number is out of range!";
                        logHistory(HistoryActions.Error, CurrentDisplay);
                    }
                    break;

                case Operations.Divide:
                    try
                    {
                        Operand2 = CurrentDisplay;
                        CurrentDisplay = (getFloat(Operand1) / getFloat(CurrentDisplay)).ToString();
                        EqualsSign = "=";
                        logHistory(HistoryActions.Equal, CurrentDisplay);
                    }
                    catch (OverflowException)
                    {
                        CurrentDisplay = "Number is out of range!";
                        logHistory(HistoryActions.Error, CurrentDisplay);
                    }
                    break;

                case Operations.Multiply:
                    try
                    {
                        Operand2 = CurrentDisplay;
                        CurrentDisplay = (getFloat(Operand1) * getFloat(CurrentDisplay)).ToString();
                        EqualsSign = "=";
                        logHistory(HistoryActions.Equal, CurrentDisplay);
                    }
                    catch (OverflowException)
                    {
                        CurrentDisplay = "Number is out of range!";
                        logHistory(HistoryActions.Error, CurrentDisplay);
                    }
                    break;
                
                case Operations.Subtract:
                    try
                    {
                        Operand2 = CurrentDisplay;
                        CurrentDisplay = (getFloat(Operand1) - getFloat(CurrentDisplay)).ToString();
                        EqualsSign = "=";
                        logHistory(HistoryActions.Equal, CurrentDisplay);
                    }
                    catch (OverflowException)
                    {
                        CurrentDisplay = "Number is out of range!";
                        logHistory(HistoryActions.Error, CurrentDisplay);
                    }
                    break;
                default:
                    break;
            }
        }

        private void onExport()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(saveFileDialog.FileName))
                {
                    foreach (HistoryEntry historyEntry in _history)
                    {
                        sw.WriteLine(historyEntry.ID + "," + historyEntry.Action + "," + historyEntry.Value);
                    }
                }                
            }
        }

        private void onImport()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = File.OpenText(openFileDialog.FileName))
                {
                    string s = string.Empty;
                    _history.Clear();
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] stringArray = s.Split(',');
                        if (stringArray.Count() == 3)
                        {
                            _history.Add(
                                new HistoryEntry(
                                    stringArray[0],
                                    stringArray[1],
                                    stringArray[2]));
                        }
                        
                    }
                }
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
                switch (operation)
                {
                    case Operations.Add:
                        logHistory(HistoryActions.Add, CurrentDisplay);
                        break;

                    case Operations.Divide:
                        logHistory(HistoryActions.Divide, CurrentDisplay);
                        break;

                    case Operations.Multiply:
                        logHistory(HistoryActions.Multiply, CurrentDisplay);
                        break;

                    case Operations.Subtract:
                        logHistory(HistoryActions.Subtract, CurrentDisplay);
                        break;

                    default:
                        break;
                }
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
                                logHistory(HistoryActions.Add, CurrentDisplay);
                                CurrentDisplay = (getFloat(Operand1) + getFloat(CurrentDisplay)).ToString();
                                Operand1 = CurrentDisplay;
                            }
                            catch (OverflowException)
                            {
                                CurrentDisplay = "Number is out of range!";
                                logHistory(HistoryActions.Error, CurrentDisplay);
                            }                        
                            break;

                        case Operations.Divide:
                            try
                            {
                                logHistory(HistoryActions.Divide, CurrentDisplay);
                                CurrentDisplay = (getFloat(Operand1) / getFloat(CurrentDisplay)).ToString();
                                Operand1 = CurrentDisplay;
                            }
                            catch (OverflowException)
                            {
                                CurrentDisplay = "Number is out of range!";
                                logHistory(HistoryActions.Error, CurrentDisplay);
                            }
                            break;

                        case Operations.Multiply:
                            try
                            {
                                logHistory(HistoryActions.Multiply, CurrentDisplay);
                                CurrentDisplay = (getFloat(Operand1) * getFloat(CurrentDisplay)).ToString();
                                Operand1 = CurrentDisplay;
                            }
                            catch (OverflowException)
                            {
                                CurrentDisplay = "Number is out of range!";
                                logHistory(HistoryActions.Error, CurrentDisplay);
                            }
                            break;
                        
                        case Operations.Subtract:
                            try
                            {
                                logHistory(HistoryActions.Subtract, CurrentDisplay);
                                CurrentDisplay = (getFloat(Operand1) - getFloat(CurrentDisplay)).ToString();
                                Operand1 = CurrentDisplay;
                            }
                            catch (OverflowException)
                            {
                                CurrentDisplay = "Number is out of range!";
                                logHistory(HistoryActions.Error, CurrentDisplay);
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
                    logHistory(HistoryActions.Negate, CurrentDisplay);
                    break;
                case Operations.SquareRoot:
                    CurrentDisplay = Math.Sqrt(getFloat(CurrentDisplay)).ToString();
                    logHistory(HistoryActions.SquareRoot, CurrentDisplay);
                    break;
                case Operations.OneDivideByX:
                    CurrentDisplay = (1 / getFloat(CurrentDisplay)).ToString();
                    logHistory(HistoryActions.OneDivideByX, CurrentDisplay);
                    break;
                default:
                    break;
            }
        }

        private void onMemoryCommand(string memoryActions)
        {
            switch (memoryActions)
            {
                case MemoryActions.MemoryClear:
                    Memory.Clear();
                    MemoryClearCommand.RaiseCanExecuteChanged();
                    MemoryRecallCommand.RaiseCanExecuteChanged();
                    break;

                case MemoryActions.MemoryMinus:
                    if (Memory.Count > 0)
                    {
                        Memory.Add((getFloat(Memory.Last()) - getFloat(CurrentDisplay)).ToString());
                    }
                    else
                    {
                        Memory.Add("-" + CurrentDisplay);
                    }
                    MemoryClearCommand.RaiseCanExecuteChanged();
                    MemoryRecallCommand.RaiseCanExecuteChanged();
                    break;

                case MemoryActions.MemoryPlus:
                    if (Memory.Count > 0)
                    {
                        Memory.Add((getFloat(Memory.Last()) + getFloat(CurrentDisplay)).ToString());
                    }
                    else
                    {
                        Memory.Add(CurrentDisplay);
                    }
                    MemoryClearCommand.RaiseCanExecuteChanged();
                    MemoryRecallCommand.RaiseCanExecuteChanged();
                    break;

                case MemoryActions.MemoryRecall:
                    CurrentDisplay = getFloat(Memory.Last()).ToString();
                    break;

                case MemoryActions.MemorySave:
                    Memory.Add(CurrentDisplay);
                    MemoryClearCommand.RaiseCanExecuteChanged();
                    MemoryRecallCommand.RaiseCanExecuteChanged();
                    break;

                default:
                    break;
            }
        }

        #endregion Private Methods
    }
}
