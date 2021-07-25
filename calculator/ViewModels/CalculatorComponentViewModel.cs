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

        #region Commands declarations
        
        private DelegateCommand _backCommand;
        private DelegateCommand _clearCommand;
        private DelegateCommand _clearEntryCommand;
        
        private DelegateCommand _memoryClearCommand;
        private DelegateCommand _memoryMinusCommand;
        private DelegateCommand _memoryPlusCommand;
        private DelegateCommand _memoryRecallCommand;
        private DelegateCommand _memorySaveCommand;

        private DelegateCommand _addCommand;
        private DelegateCommand _divideCommand;
        private DelegateCommand _equalsCommand;
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

        #endregion Commands declarations

        private string _currentDisplay = "0";
        private string _equalsSign = string.Empty;
        private List<HistoryEntry> _history = new List<HistoryEntry>();
        private bool _justSelectedOperator;
        private ObservableCollection<string> _memory;
        private string _operand1 = string.Empty;
        private string _operand2 = string.Empty;
        private string _operator = string.Empty;

        #endregion Declarations

        #region Properties

        #region Commands

        /// <summary>
        /// Backspace command
        /// </summary>
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

        /// <summary>
        /// Clear command
        /// </summary>
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

        /// <summary>
        /// Clear entry command
        /// </summary>
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

        #region Memory Commands

        /// <summary>
        /// Memory clear command
        /// </summary>
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
        
        /// <summary>
        /// Memory minus command
        /// </summary>
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
        
        /// <summary>
        /// Memory plus command
        /// </summary>
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
        
        /// <summary>
        /// Memory recall command
        /// </summary>
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
        
        /// <summary>
        /// Memory save command
        /// </summary>
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

        #endregion Memory Commands

        #region Basic Operation Commands

        /// <summary>
        /// Add command
        /// </summary>
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

        /// <summary>
        /// Divide command
        /// </summary>
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

        /// <summary>
        /// Equals Command
        /// </summary>
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

        /// <summary>
        /// Multiply command
        /// </summary>
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
        
        /// <summary>
        /// Negate command
        /// </summary>
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

        /// <summary>
        /// One divide by x command
        /// </summary>
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

        /// <summary>
        /// Subtract command
        /// </summary>
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

        /// <summary>
        /// Square root command
        /// </summary>
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

        #endregion Basic Operation Commands

        #region Number Commands

        /// <summary>
        /// One command
        /// </summary>
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

        /// <summary>
        /// Two command
        /// </summary>
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

        /// <summary>
        /// Three command
        /// </summary>
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

        /// <summary>
        /// Four command
        /// </summary>
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

        /// <summary>
        /// Five command
        /// </summary>
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

        /// <summary>
        /// Six command
        /// </summary>
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

        /// <summary>
        /// Seven command
        /// </summary>
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

        /// <summary>
        /// Eight command
        /// </summary>
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

        /// <summary>
        /// Nine command
        /// </summary>
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

        /// <summary>
        /// Zero command
        /// </summary>
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

        /// <summary>
        /// Dot command
        /// </summary>
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

        #endregion Number Commands

        #region Menu Bar Commands

        /// <summary>
        /// Copy command
        /// </summary>
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

        /// <summary>
        /// Export command
        /// </summary>
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

        /// <summary>
        /// Import command
        /// </summary>
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

        /// <summary>
        /// Paste command
        /// </summary>
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

        #endregion Menu Bar Commands

        #endregion Commands

        /// <summary>
        /// Current display on the calculator
        /// </summary>
        public string CurrentDisplay
        {
            get { return _currentDisplay; }
            set
            {
                _currentDisplay = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Equal sign on the calculator
        /// </summary>
        public string EqualsSign
        {
            get { return _equalsSign; }
            set
            {
                _equalsSign = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Bool that contains whether user recently selected an operator
        /// </summary>
        public bool JustSelectedOperator
        {
            get { return _justSelectedOperator; }
            set
            {
                _justSelectedOperator = value;
            }
        }

        /// <summary>
        /// Collection of memory queue
        /// </summary>
        public ObservableCollection<string> Memory
        {
            get { return _memory; }
            set
            {
                _memory = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// First operand
        /// </summary>
        public string Operand1
        {
            get { return _operand1; }
            set
            {
                _operand1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Second operand
        /// </summary>
        public string Operand2
        {
            get { return _operand2; }
            set
            {
                _operand2 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Operator
        /// </summary>
        public string Operator
        {
            get { return _operator; }
            set
            {
                _operator = value;
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

        #region Private Methods

        /// <summary>
        /// Check whether we need to clear current display
        /// </summary>
        /// <param name="isDot"></param>
        private void CheckClearCurrentDisplay(bool isDot)
        {
            // If user recently selected an operator (ex. +,-,/,*)
            if (JustSelectedOperator)
            {
                // Reset to 0 if dot requested to clear
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

        /// <summary>
        /// Safely translate string to float
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private float getFloat(string numbers)
        {
            float floatNumber;
            if (float.TryParse(numbers, out floatNumber))
            {
                return floatNumber;
            }

            return floatNumber;
        }

        /// <summary>
        /// Log history
        /// </summary>
        /// <param name="historyAction"></param>
        /// <param name="value"></param>
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

        /// <summary>
        /// Simulate backspace on CurrentDisplay
        /// </summary>
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

        /// <summary>
        /// Clear
        /// </summary>
        /// <param name="clearEntry"></param>
        private void onClear(bool clearEntry)
        {
            CurrentDisplay = "0";
            logHistory(HistoryActions.Clear, CurrentDisplay);

            // If clear entry executed this
            if (!clearEntry ||
                !string.IsNullOrEmpty(EqualsSign))
            { 
                // Clear all display
                EqualsSign = string.Empty;
                Operand1 = string.Empty;
                Operand2 = string.Empty;
                Operator = string.Empty;
                JustSelectedOperator = false;
            }
        }

        /// <summary>
        /// Equals is pressed
        /// </summary>
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

        /// <summary>
        /// Export history to text file
        /// </summary>
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
                using (StreamWriter streamWriter = File.CreateText(saveFileDialog.FileName))
                {
                    // Write each history into text file
                    foreach (HistoryEntry historyEntry in _history)
                    {
                        streamWriter.WriteLine(historyEntry.Id + "," + historyEntry.Action + "," + historyEntry.Value);
                    }
                }                
            }
        }

        /// <summary>
        /// Import history form text file
        /// </summary>
        private void onImport()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader streamReader = File.OpenText(openFileDialog.FileName))
                {
                    string stringLine = string.Empty;
                    _history.Clear();

                    // Fill up _history with history from text file)
                    while ((stringLine = streamReader.ReadLine()) != null)
                    {
                        string[] stringArray = stringLine.Split(',');

                        // Check whether file is properly formatted
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

        /// <summary>
        /// Operate where two operands are required
        /// </summary>
        /// <param name="operation"></param>
        private void onOperate(
            string operation)
        {
            JustSelectedOperator = true;
            
            // If there are no operands yet
            if (string.IsNullOrWhiteSpace(Operand1) ||
                !string.IsNullOrWhiteSpace(EqualsSign))
            {
                Operand1 = CurrentDisplay;
                Operand2 = string.Empty;
                EqualsSign = string.Empty;

                // Log action depending on operation
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

        /// <summary>
        /// Operate where only one operand is required
        /// </summary>
        /// <param name="operation"></param>
        private void onSimpleOperate(string operation)
        {
            // Execute operation
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

        /// <summary>
        /// Manipulate memory
        /// </summary>
        /// <param name="memoryActions"></param>
        private void onMemoryCommand(string memoryActions)
        {
            // Manipulate memory depending on memoryActions
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
