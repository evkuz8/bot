using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model.RequestParams;
using System.IO;
using VkNet.Model;
using System.Threading;

namespace AddBadWordBot
{
    class Program
    {
        static VkApi vkApi = new VkApi(); 
        static long userID = 0;   


        static void Main(string[] args)
        {
            ChatBot chatBot = new ChatBot
                (
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"/badwords.txt");
            chatBot.GetStr += ChatBot_GetStr;

            Auth();
            vkApi.Status.Set("Mode \"матершинник\" is ON");

            while (true)
            {
                chatBot.GenAnswer(GetMessage());
                //ConsoleCtrlCheck(CtrlTypes.CTRL_CLOSE_EVENT);
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
            apiAuthParams.AccessToken = (File.ReadAllText(path)).ToString();

            vkApi.Authorize(apiAuthParams);

        }

        static void SendMessage(string newMessage)
        {
            MessagesSendParams messagesSendParams = new MessagesSendParams();
            messagesSendParams.UserId = userID;
            Console.Write("Бот говорит: {0}", newMessage);
            messagesSendParams.Message = newMessage;
            vkApi.Messages.Send(messagesSendParams);
        }

        static string GetMessage()
        {
            Message currentMessage = null;
            long myID = 489971207;

            while (true)
            {
                Message message = vkApi.Messages.Get(new MessagesGetParams
                {
                    Count = 1
                }).Messages[0];
                userID = message.UserId.Value;
                if (userID != myID && message.ReadState != VkNet.Enums.MessageReadState.Readed)
                {
                    currentMessage = message;
                    break;
                }


                Thread.Sleep(350);
            }

            return currentMessage.Body;
        }

        //public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        //// A delegate type to be used as the handler routine 
        //// for SetConsoleCtrlHandler.
        //public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        //// An enumerated type for the control messages
        //// sent to the handler routine.
        //public enum CtrlTypes
        //{
        //    CTRL_C_EVENT = 0,
        //    CTRL_BREAK_EVENT,
        //    CTRL_CLOSE_EVENT,
        //    CTRL_LOGOFF_EVENT = 5,
        //    CTRL_SHUTDOWN_EVENT
        //}

        //private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        //{
        //    vkApi.Status.Set("Отдыхаю, не тревожить");
        //    return true;
        //}



    }
}
