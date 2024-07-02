using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Uge_27_project.ViewModels;

namespace Uge_27_project.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        this.InitializeComponent();
        this.AddHandler(KeyDownEvent, OnKeyDown, RoutingStrategies.Tunnel);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            string key = e.Key.ToString();

            // Handle numeric keys
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                viewModel.InputText += (e.Key - Key.D0).ToString();
            }
            // Handle numpad keys
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                viewModel.InputText += (e.Key - Key.NumPad0).ToString();
            }
            // Handle operator keys
            else if (key == "Add" || key == "OemPlus")
            {
                viewModel.InputText += "+";
            }
            else if (key == "Subtract" || key == "OemMinus")
            {
                viewModel.InputText += "-";
            }
            else if (key == "Multiply" || key == "OemAsterisk")
            {
                viewModel.InputText += "*";
            }
            else if (key == "Divide" || key == "OemQuestion")
            {
                viewModel.InputText += "/";
            }
            else if (key == "OemOpenBrackets" || key == "D9" && (e.KeyModifiers & KeyModifiers.Shift) != 0)
            {
                viewModel.InputText += "(";
            }
            else if (key == "OemCloseBrackets" || key == "D0" && (e.KeyModifiers & KeyModifiers.Shift) != 0)
            {
                viewModel.InputText += ")";
            }
            // Handle Enter key for calculation
            else if (e.Key == Key.Enter)
            {
                viewModel.CalculateCommand.Execute(null);
            }
            // Handle Escape key for clearing input
            else if (e.Key == Key.Escape)
            {
                viewModel.ClearCommand.Execute(null);
            }
            else
            {
                // Prevent other keys from affecting the TextBox
                e.Handled = true;
            }
        }
    }

    private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
    {
        OnKeyDown(sender, e);
    }
}

