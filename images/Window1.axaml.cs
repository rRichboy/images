using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Avalonia.Skia.Helpers;
using System;
using System.Drawing;
using Npgsql;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Avalonia.Controls.Shapes;

namespace images
{
    public partial class Window1 : Window
    {
        private readonly NpgsqlConnection conn;

        public Window1()
        {
            //получение пути к файлу settings.json
            string path = Directory.GetCurrentDirectory();
            string projectPath = Directory.GetParent(path).Parent.Parent.FullName;
            string settingsFile = projectPath + @"\settings.json";
            string? connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=postgres";

            if (File.Exists(settingsFile))
            {
                //чтение файла и его разбор 
                JObject? jobj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(settingsFile));

                //получение строки подключения
                connectionString = jobj["ConnectionString"]?["DefaultConnection"]?.ToString();

            } 
            
            conn = new NpgsqlConnection(connectionString);
            
            InitializeComponent();
        }

        //на вкладке "Войти" нажимаем регистриция и перекидывает на "Регистриция"
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            RegistrationTabControl.IsSelected = true;
        }

        //показ и скрытие пароля при регистрации по нажатию "*"
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Pass != null)
            {
                if (Pass.PasswordChar == '\0')
                {
                    Pass.PasswordChar = '*';
                }
                else
                {
                    Pass.PasswordChar = '\0';
                }
            }
        }

        //кнопка "выйти" на вкладке "Выйти"
        public void Exit1(object sender, RoutedEventArgs args)
        {
            Environment.Exit(0);
        }

        //вкладка "Регистрация" кнопка зарегистрироваться
        private async void Button_Click_100(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                await conn.OpenAsync();

                //инсертим данные при регистрации
                await using (var cmd = new NpgsqlCommand("INSERT INTO users (login, pwd, date) VALUES (@login, @pwd, @date)", conn))
                {
                    var loginTextBox = this.FindControl<TextBox>("Login");
                    var passwordTextBox = this.FindControl<TextBox>("Pass");

                    cmd.Parameters.AddWithValue("login", loginTextBox.Text);
                    cmd.Parameters.AddWithValue("pwd", passwordTextBox.Text);
                    cmd.Parameters.AddWithValue("date", datePick.SelectedDate.Value.DateTime);

                    await cmd.ExecuteNonQueryAsync();

                    //если все красиво, то сообщение:
                    ErEx.Text = "Регистрация успешна!";
                }
            }
            catch (Exception ex)
            {
                //если все не очень красиво, то сообщение:
                ErEx.Text = $"Ошибка регистрации: {ex.Message}";

            }
            finally
            {
                //закрываем соединение с бд-шкой
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        //вкладка "Войти" кнопка войти
        private async void Button_Click5(object sender, RoutedEventArgs e)
        {
            var usernameTextBox = this.FindControl<TextBox>("UsernameTextBox");
            var passwordTextBox = this.FindControl<TextBox>("PasswordTextBox");

            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            try
            {
                await conn.OpenAsync();

                //чекаем валидность данных в бд
                await using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE login = @login AND pwd = @pwd", conn))
                {
                    cmd.Parameters.AddWithValue("login", username);
                    cmd.Parameters.AddWithValue("pwd", password);

                    int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    if (count > 0)
                    {
                        //если все красиво, то сообщение + ивенты:
                        LoginTabControl.IsVisible = false;
                        RegistrationTabControl.IsVisible = false;
                        MeowForAuthBlock.IsVisible = false;
                        Private.IsSelected = true;
                        myPic4.IsVisible = true;

                        ErEx0.Text = "Вход выполнен успешно!";
                    }
                    else
                    {
                        //мискликнул
                        ErEx0.Text = "Неверный логин или пароль.";
                    }
                }
            }
            catch (Exception ex)
            {
                //если все не очень красиво, то сообщение:
                ErEx0.Text = $"Ошибка входа: {ex.Message}";
            }
            finally
            {
                //закрываем соединение с бд-шкой
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        //на вкладке "Войти" нажимаем регистриция и перекидывает на "Регистриция"
        private void Button_Click_200(object sender, RoutedEventArgs e)
        {
            ErEx2.Text = "В разработке.";
        }
    }
}
