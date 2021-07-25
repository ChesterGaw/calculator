using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using calculator.ViewModels;

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new CalculatorComponentViewModel();
        }

        /// <summary>
        /// Detects keypresses from main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _onKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CalculatorComponentViewModel calculatorComponentViewModel = (CalculatorComponentViewModel)DataContext;

            switch (e.Key)
            {
                #region Numbers

                case Key.D1:
                case Key.NumPad1:
                    if (calculatorComponentViewModel.OneCommand.CanExecute())
                    {
                        calculatorComponentViewModel.OneCommand.Execute();
                    }
                    break;
                case Key.D2:
                case Key.NumPad2:
                    if (calculatorComponentViewModel.TwoCommand.CanExecute())
                    {
                        calculatorComponentViewModel.TwoCommand.Execute();
                    }
                    break;
                case Key.D3:
                case Key.NumPad3:
                    if (calculatorComponentViewModel.ThreeCommand.CanExecute())
                    {
                        calculatorComponentViewModel.ThreeCommand.Execute();
                    }
                    break;
                case Key.D4:
                case Key.NumPad4:
                    if (calculatorComponentViewModel.FourCommand.CanExecute())
                    {
                        calculatorComponentViewModel.FourCommand.Execute();
                    }
                    break;
                case Key.D5:
                case Key.NumPad5:
                    if (calculatorComponentViewModel.FiveCommand.CanExecute())
                    {
                        calculatorComponentViewModel.FiveCommand.Execute();
                    }
                    break;
                case Key.D6:
                case Key.NumPad6:
                    if (calculatorComponentViewModel.SixCommand.CanExecute())
                    {
                        calculatorComponentViewModel.SixCommand.Execute();
                    }
                    break;
                case Key.D7:
                case Key.NumPad7:
                    if (calculatorComponentViewModel.SevenCommand.CanExecute())
                    {
                        calculatorComponentViewModel.SevenCommand.Execute();
                    }
                    break;
                case Key.D8:
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        if (calculatorComponentViewModel.MultiplyCommand.CanExecute())
                        {
                            calculatorComponentViewModel.MultiplyCommand.Execute();
                        }
                    }
                    else
                    {
                        if (calculatorComponentViewModel.EightCommand.CanExecute())
                        {
                            calculatorComponentViewModel.EightCommand.Execute();
                        }
                    }
                    break;
                case Key.NumPad8:
                    if (calculatorComponentViewModel.EightCommand.CanExecute())
                    {
                        calculatorComponentViewModel.EightCommand.Execute();
                    }
                    break;
                case Key.D9:
                case Key.NumPad9:
                    if (calculatorComponentViewModel.NineCommand.CanExecute())
                    {
                        calculatorComponentViewModel.NineCommand.Execute();
                    }
                    break;
                case Key.D0:
                case Key.NumPad0:
                    if (calculatorComponentViewModel.ZeroCommand.CanExecute())
                    {
                        calculatorComponentViewModel.ZeroCommand.Execute();
                    }
                    break;
                case Key.Decimal:
                case Key.OemPeriod:
                    if (calculatorComponentViewModel.DotCommand.CanExecute())
                    {
                        calculatorComponentViewModel.DotCommand.Execute();
                    }
                    break;

                #endregion Numbers

                #region Operations

                case Key.Add:
                    if (calculatorComponentViewModel.AddCommand.CanExecute())
                    {
                        calculatorComponentViewModel.AddCommand.Execute();
                    }
                    break;
                case Key.OemMinus:
                case Key.Subtract:
                    if (calculatorComponentViewModel.SubtractCommand.CanExecute())
                    {
                        calculatorComponentViewModel.SubtractCommand.Execute();
                    }
                    break;
                case Key.Multiply:
                    if (calculatorComponentViewModel.MultiplyCommand.CanExecute())
                    {
                        calculatorComponentViewModel.MultiplyCommand.Execute();
                    }
                    break;
                case Key.Oem2:
                case Key.Divide:
                    if (calculatorComponentViewModel.DivideCommand.CanExecute())
                    {
                        calculatorComponentViewModel.DivideCommand.Execute();
                    }
                    break;
                case Key.Back:
                    if (calculatorComponentViewModel.BackCommand.CanExecute())
                    {
                        calculatorComponentViewModel.BackCommand.Execute();
                    }
                    break;
                case Key.OemPlus:
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        if (calculatorComponentViewModel.AddCommand.CanExecute())
                        {
                            calculatorComponentViewModel.AddCommand.Execute();
                        }
                    }
                    else
                    {
                        if (calculatorComponentViewModel.EqualsCommand.CanExecute())
                        {
                            calculatorComponentViewModel.EqualsCommand.Execute();
                        }
                    }
                    break;

                #endregion Operations

                #region Menu Bar 

                case Key.C:
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        if (calculatorComponentViewModel.CopyCommand.CanExecute())
                        {
                            calculatorComponentViewModel.CopyCommand.Execute();
                        }
                    }
                    break;
                case Key.V:
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        if (calculatorComponentViewModel.PasteCommand.CanExecute())
                        {
                            calculatorComponentViewModel.PasteCommand.Execute();
                        }
                    }
                    break;

                #endregion Menu Bar

                default:
                    break;

            }
        }
    }
}
