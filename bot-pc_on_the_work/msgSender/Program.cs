using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model.RequestParams;
using System.IO;
using VkNet.Model;
using System.Threading;

namespace msgSender
{
    class Program
    {
        static VkApi vkApi = new VkApi(); // сережа - 171971262
        static long userID = 135311173;   // женя - 135311173


        static void Main(string[] args)
        {
            ChatBot chatBot = new ChatBot
                (
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"/data.txt",
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"/badwords.txt");
            chatBot.GetStr += ChatBot_GetStr;

            Auth();
            SendMessage("Привет! это точка запуска.");
            
            while (true)
            {
                chatBot.GenAnswer(GetMessage());
            }
        }
        static void ChatBot_GetStr(string obj)
        {
            SendMessage(obj);
        }

        static void Auth()
        {
            ApiAuthParams apiAuthParams = new ApiAuthParams();
            apiAuthParams.ApplicationId = 6493465; 
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"/tss.txt";
            //Console.WriteLine((File.ReadAllText(path)).ToString());
            //Console.ReadLine();
            apiAuthParams.AccessToken = (File.ReadAllText(path)).ToString();

            vkApi.Authorize(apiAuthParams);
           
        }
        
        static void SendMessage(string newMessage)
        {
            MessagesSendParams messagesSendParams = new MessagesSendParams();
            messagesSendParams.UserId = userID;
            Console.Write("Бот говорит: {0}",newMessage);
            messagesSendParams.Message = newMessage;
            vkApi.Messages.Send(messagesSendParams);
        }

        static string GetMessage()
        {
            Message currentMessage = null, lastMessage = null;
            bool isFirst = true;
            long myID = 489971207;

            while (true)
            {
                Message message = vkApi.Messages.Get(new MessagesGetParams
                {
                    Count = 1
                }).Messages[0];
                if (message.OwnerId!= myID && message.ReadState != VkNet.Enums.MessageReadState.Readed)
                {
                    /*if (isFirst)
                    {
                        lastMessage = vkApi.Messages.Get(new MessagesGetParams
                        {
                            Count = 1
                        }).Messages[0];
                        isFirst = false;
                    }
                    else
                    {
                        currentMessage = vkApi.Messages.Get(new MessagesGetParams
                        {
                            Count = 1
                        }).Messages[0];

                        if (currentMessage.OwnerId != myID && currentMessage.Date != lastMessage.Date)
                        {
                            break;
                        }
                        isFirst = true;
                    }*/
                    currentMessage = message;
                    break;
                }
                
                Thread.Sleep(350);
            }

            return currentMessage.Body;
        }

    }
}
