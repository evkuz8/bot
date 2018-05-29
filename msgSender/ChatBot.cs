using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace developingChatBot
{
    class ChatBot
    {
        List<string> data = new List<string>();
        bool isWork = true;
        string dataPath, badWordsPath, userAnswer;
        static string question;

        public event Action<string> GetStr; // событие

        public ChatBot(string dpath, string bwpath)
        {
            dataPath = dpath;
            badWordsPath = bwpath;
            try
            {
                data.AddRange(File.ReadAllLines(dpath));
            }
            catch (Exception)
            {

            }

            GetStr += ChatBot_GetStr;
            GetStr("\nПользователь говорит: ");
            
        }

        public void GenAnswer (string q)
        {
            if (isWork)
            {
                question = q;
                string answer = Answer(question);
                if (answer == string.Empty)
                {
                    isWork = false;

                    GetStr("Я еще не умею отвечать на такой вопрос, пожалуйста введите свой ответ:");
                }
                else
                {
                    GetStr(answer); //возвращаем ответ
                }

            }
            else
            {
                isWork = true;
                userAnswer = q;
                if (AntiMat(userAnswer) || AntiMat(question))
                {
                    GetStr("Не ругайся!");
                }
                else
                {
                    Teach();
                    GetStr("Запомнил!");
                }
                GetStr += ChatBot_GetStr;

            }
        }

        void Teach()
        {
            data.Add(question); // add a question 
            data.Add(userAnswer); // add an answer
            File.WriteAllLines(dataPath, data.ToArray()); // saving
        }

        bool AntiMat (string inputString)
        {
            string[] loadingBadWords = { };
            try
            {
                loadingBadWords = File.ReadAllLines(badWordsPath);

            }
            catch (Exception)
            {
            }
            //string allbadwords = "";
            //foreach (string item in loadingBadWords)
            //{
            //    allbadwords += item;
            //}
            //string[] badWords = allbadwords.Split(',');
            string[] words = Exclude(inputString.ToLower(), "`~!@#$%^&*(){}[]_-+=|\\|/?.,<>'\"№".ToCharArray()).Split();

            foreach (string badWord in loadingBadWords)
            {
                foreach (string word in words)
                {
                    if (badWord == word)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string Answer(string q) 

        {
            string answer = string.Empty,
                symbols = "`~!@#$%^&*(){}[]_-+=|\\|/?.,<>'\"№";
            question=Exclude(q.ToLower(),symbols.ToCharArray());

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == question)
                {
                    answer = data[i + 1];
                    break;
                }
            }
           
            return answer;
        }

        static string Exclude(string q, char[] symbols)
        {
            foreach (char symbol in symbols)
            {
                q = q.Replace(symbol.ToString(), string.Empty);
            }
            
            return q;
        }

        void ChatBot_GetStr(string obj)
        {
            // заглушка
        }
    }
}
