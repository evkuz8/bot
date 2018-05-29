using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model.RequestParams;
using System.IO;

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
                MessagesSendParams messagesSendParams = new MessagesSendParams();
                messagesSendParams.UserId = 135311173;
                Console.Write("Бот говорит: ");
                string msg = Console.ReadLine();
                messagesSendParams.Message = msg;
                vkApi.Messages.Send(messagesSendParams);

                Console.ReadLine();


                MessagesGetHistoryParams messagesGetHistoryParams = new MessagesGetHistoryParams();
                messagesGetHistoryParams.UserId = 135311173;
                messagesGetHistoryParams.Count = 1;
                Console.WriteLine("user: ");

                vkApi.Messages.GetHistory(messagesGetHistoryParams);
            }
        }

        static void Auth()
        {
            ApiAuthParams apiAuthParams = new ApiAuthParams();
            apiAuthParams.ApplicationId = 6493465; 
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "tss.txt";
            apiAuthParams.AccessToken = (File.ReadAllLines(path)).ToString();

            vkApi.Authorize(apiAuthParams);
           
        }
        
    }
}
