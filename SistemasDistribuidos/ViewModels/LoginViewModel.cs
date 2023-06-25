using System;
using SistemasDistribuidos.Helpers;

namespace SistemasDistribuidos.ViewModels
{
	public class LoginViewModel
	{
		public LoginViewModel()
		{
		}

        public bool ValidateCredentials(string user, string password)
        {
            bool valid = false;

            new Microsoft.Maui.Controls.Command(() =>
            {
                string url = $"http://lb-reader-param-377588952.us-east-1.elb.amazonaws.com/login?username={user}&password={password}";
                var service = new HttpHelper<bool>();
                var data = service.GetRestServiceLoginAsync(url);
                valid = data;
            }).Execute(null);
            return valid;
        }        
    }
}

