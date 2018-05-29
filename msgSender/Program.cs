using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model.RequestParams;

namespace msgSender
{
    class Program
    {
        static VkApi vkApi = new VkApi();
        static void Main(string[] args)
        {
            Auth();
            while (true)
            {
                Console.ReadLine();
                MessagesSendParams messagesSendParams = new MessagesSendParams();
                messagesSendParams.UserId = 135311173;
                messagesSendParams.Message = Console.ReadLine();
                vkApi.Messages.Send(messagesSendParams);
            }
        }

        static void Auth()
        {
            ulong appID = 6493465;
            string login = @"evgn.worker@gmail.com";
            string password = "3W0k8E4r";
            Settings settings = Settings.All;

            ApiAuthParams apiAuthParams = new ApiAuthParams();
            apiAuthParams.ApplicationId = appID;
            apiAuthParams.Login = login;
            apiAuthParams.Password = password;
            apiAuthParams.Settings = settings;
            apiAuthParams.AccessToken = 

            try
            {
                vkApi.Authorize(apiAuthParams);

            }
            catch (CaptchaNeededException cEx)
            {
                Uri captchUri = cEx.Img.;
                //MediaPlayer
                Console.WriteLine();
                Console.WriteLine(new string('-',50));
                Console.WriteLine();
                Console.WriteLine(cEx);
                //System.Diagnostics.Process.Start(captchUri);
                string captchKey = Console.ReadLine();
                apiAuthParams.CaptchaKey = captchKey;
                apiAuthParams.CaptchaSid = cEx.Sid;
                vkApi.Authorize(apiAuthParams);
            }
        }
        
    }
}
