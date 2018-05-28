using System;

namespace developingChatBot
{
    class Program
    {
        static void ChatBot_GetStr(string obj)
        {
            Console.WriteLine(obj);
        }

        static void Main(string[] args)
        {
            ChatBot chatBot = new ChatBot("data.txt","badwords.txt");

            chatBot.GetStr += ChatBot_GetStr; // подписка на событие

            Console.Write("Пользователь говорит: ");

            while (true)
            {
                
                string q = Console.ReadLine();

                chatBot.GenAnswer(q);
            }
        }
    }
}
