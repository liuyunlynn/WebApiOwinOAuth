using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using OwinOauthClient.Interfaces;
using OwinOauthClient.Services;

namespace OwinOauthClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _userService = new UserService(_baseUrl);

        }

        private async void BtnValidateUser_Click(object sender, EventArgs e)
        {
            var response = await _userService.ValidateUser(txtUserName.Text, txtPassword.Text);
            var result = response.ModelObject;
            if (response.IsFalied)
            {
                lblResult.Text = response.ErrorMessage;
            }
            else
            {
                lblResult.Text = result;
                _userService.AccessToken= JObject.Parse(response.ModelObject)["access_token"].Value<string>();
            }
        }

        private async void BtnGetUser_Click(object sender, EventArgs e)
        {
            var response = await _userService.GetUserProfileById("user123");
            var result = response.ModelObject;
            if (response.IsFalied)
            {
                lblResult.Text = response.ErrorMessage;
            }
            else
            {
                lblResult.Text = result.Name;
            }
        }

        private IUserService _userService;
        private readonly string _baseUrl = "http://localhost:44367/api/";

    }
}
