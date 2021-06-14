using Dm1.Models;
using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Dm1.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Dm1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {

        private Service _service = new Service();
        private IUserService _tUserService;
        public Register()
        {
            this.InitializeComponent();
            this._tUserService = _service.UserService();
        }

        private async void SignUp_Button_Click(object sender, RoutedEventArgs e)
        {
            var username = this.User_Username.Text;
            var password = this.User_Password.Text;
            
            var user = new User()
            {
                UserName = username,
                Password = password,
            };

            var errors = user.Validate();

            if (errors.Count > 0)
            {
                HandleError(errors);
            }
            else
            {
                var responseUser = this._tUserService.Create(user);
                if (responseUser)
                {
                    this.Frame.Navigate(typeof(Login));
                }
            }
        }

        private void HandleError(Dictionary<string, string> errors)
        {
            if (errors.ContainsKey("Username"))
            {
                var error = errors["Username"];
                DisplayError(SignUp_Error_Username, error);
            }
            else
            {
                DisplayError(SignUp_Error_Username, null);
            }

            if (errors.ContainsKey("Password"))
            {
                var error = errors["Password"];
                DisplayError(SignUp_Error_Password, error);
            }
            else
            {
                DisplayError(SignUp_Error_Password, null);
            }


        }

        private void DisplayError(TextBlock textBlock, string error)
        {
            if (error != null)
            {
                textBlock.Visibility = Visibility.Visible;
                textBlock.Text = error;
            }
            else
            {
                textBlock.Visibility = Visibility.Collapsed;
                textBlock.Text = "";
            }
        }




        private void SignIn_HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Login));
        }
    }
}
