using Dm1.Models;
using Dm1.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Dm1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {

        private Service _service = new Service();
        private IUserService _tUserService;

        public Login()
        {
            this.InitializeComponent();
            this._tUserService = _service.UserService();
        }

        private async void SignIn_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadingProgressRing.IsActive = true;
                User_Error.Visibility = Visibility.Collapsed;

                var username = this.User_Username.Text;
                var password = this.User_Password.Password;

                var tUser = new User()
                {
                    UserName = username,
                    Password = password
                };
                /*                var errors = tUser.ValidateSignIn();
                                if (errors.Count > 0)
                                {
                                    var error = "";
                                    if (errors.ContainsKey("Error"))
                                    {
                                        error = errors["Error"];
                                    }
                                    User_Error.Visibility = Visibility.Visible;
                                    User_Error.Text = error;
                                }
                                else
                                {*/
                var rs = this._tUserService.CheckLogin(tUser);
                if (rs)
                {
                    Result.Text = "Success";
                } 
                else 
                {
                    Result.Text = "Fail";
                } 
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>> SignInView - SignIn_Button_Click: [Error] ", ex);
                User_Error.Visibility = Visibility.Visible;
                User_Error.Text = "An error occurred please try again";
            }
            finally
            {
                LoadingProgressRing.IsActive = false;
            }
        }

        private void SignUp_HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Register));
        }
    }
}
