﻿using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model.RequestParams;
using System.IO;
using VkNet.Model;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace AddBadWordBot
{
    class Program
    {
        static VkApi vkApi = new VkApi(); 
        static long userID = 0;
        static long myID;
         
        



        static void Main(string[] args)
        {
            ChatBot chatBot = new ChatBot();
            chatBot.GetStr += ChatBot_GetStr;

            Auth();
            vkApi.Status.Set("Mode \"матершинник\" is ON");

            myID = vkApi.UserId.Value;
            Console.WriteLine(new string('-',100));
            Console.WriteLine(vkApi.Account.GetInfo());
            Console.WriteLine(new string('-', 100));

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
            string currentMessageText = string.Empty;
            long myID = 489971207;

            while (true)
            {

                List<Message> messages = new List<Message>();

                foreach (var msg in vkApi.Messages.Get(new MessagesGetParams { Count = 50  }).Messages)
                {
                    if (msg.ReadState == VkNet.Enums.MessageReadState.Unreaded /* && msg.OwnerId != myID*/)
                    {
                        messages.Add(msg);
                    }
                }
               
                Console.Clear();
                Message message;

                if (messages.Count >= 1)
                {
                    currentMessageText = LoadMessage(message = messages[messages.Count - 1]); // самое старое
                    userID = message.UserId.Value;
                    break;
                }
                
                Thread.Sleep(350);
            }

            return currentMessageText;
        }

        static string  LoadMessage(Message message)
        {
            string currentMessageText = string.Empty;
            if (message.ForwardedMessages.Count > 0) //проверка на пересылаемые сообщения
            {
                if (message.ForwardedMessages.Count == 1)
                {
                    currentMessageText = message.ForwardedMessages[0].Body;
                }
                else
                {
                    SendMessage("Пожалуйста, пересылай мне не больше 1 сообщения!");
                }
            }
            else
            {
                if (message.Attachments.Count > 0 && message.Body.Length == 0) //проврека на вложения без текста
                {
                    SendMessage("Пожалуйста, отправляй мне только текст!");
                }
                else
                {
                    currentMessageText = message.Body; //верни текст сообщения
                }
            }
            return currentMessageText;
        }

        //static string GenerateAnswer()
        //{
        //    string currentMessageText = string.Empty;

        //    var dialogs = vkApi.Messages.GetDialogs(new MessagesDialogsGetParams { Count = })




        //    return currentMessageText;
        //}




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
