using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Uge_27_project.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string Greeting => "Lets do some math!";

        private string _inputText;

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged();
            }
        }

        public ICommand ButtonCommand { get; }
        public ICommand CalculateCommand { get; }
        public ICommand ClearCommand { get; }

        public MainViewModel()
        {
            ButtonCommand = new RelayCommand(param => AddInput(param));
            CalculateCommand = new RelayCommand(param => Calculate());
            ClearCommand = new RelayCommand(param => Clear());
        }

        private void AddInput(object param)
        {
            if (param != null)
            {
                InputText += param.ToString();
            }
        }

        private void Calculate()
        {
            try
            {
                // A simple parser for the input string
                var result = EvaluateExpression(InputText);
                InputText = result.ToString();
            }
            catch (Exception ex)
            {
                InputText = "Error";
            }
        }

        private void Clear()
        {
            InputText = string.Empty;
        }
        private double EvaluateExpression(string expression)
        {
            var dataTable = new System.Data.DataTable();
            return Convert.ToDouble(dataTable.Compute(expression, ""));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}
