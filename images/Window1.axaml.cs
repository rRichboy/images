using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace images;

public partial class Window1 : Window
{
    private TextBox passwordTextBox;
    private Button showPasswordButton;

    public Window1()
    {
        InitializeComponent();
    }

    private void Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        RegistrationTabControl.IsSelected = true;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        passwordTextBox = this.Find<TextBox>("Pass");
        showPasswordButton = this.Find<Button>("ShowPasswordButton");

        if (showPasswordButton != null)
        {
            showPasswordButton.Click += ShowPassword_Click;
        }
    }

    private void ShowPassword_Click(object sender, RoutedEventArgs e)
    {
        if (passwordTextBox != null)
        {
            if (passwordTextBox.PasswordChar == '\0')
            {
                passwordTextBox.PasswordChar = '*';
            }
            else
            {
                passwordTextBox.PasswordChar = '\0';
            }
        }
    }

    public void Exit1(object sender, RoutedEventArgs args)
    {
        Environment.Exit(0);
    }
}