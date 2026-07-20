using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = string.Empty;
            string user = UserNameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(user))
            {
                ErrorTextBlock.Text = "Tài khoản không được để trống.";
                UserNameTextBox.Focus();
                return;
            }
            if (password.Length < 4)
            {
                ErrorTextBlock.Text = "Mật khẩu phải có ít nhất 4 ký tự.";
                PasswordBox.Focus();
                return;
            }
            MessageBox.Show($"Đăng nhập thành công: {user}");

        }
    }
}