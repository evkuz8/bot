using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace AddBadWordBot
{
    class ChatBot
    {
        SqlConnection sqlConnection = new SqlConnection();
        List<string> data = new List<string>();
        string dataPath;
        static string question;

        public event Action<string> GetStr; // событие

        public ChatBot(string dpath)
        {
            dataPath = dpath;
            try
            {
                data.AddRange(File.ReadAllLines(dpath));
            }
            catch (Exception)
            {

            }

            GetStr += ChatBot_GetStr;
            //GetStr("\nПользователь говорит: ");
            
        }

        public void GenAnswer (string q)
        {
            
            question = q;
            string answer = Answer(question);
            if (answer == string.Empty)
            {
                Teach();
                GetStr("Запомнил! Давай еще!");
                GetStr += ChatBot_GetStr;
            }
            else
            {
                GetStr("Такое я уже знаю! Давай что-нибудь пооригинальнее "); //возвращаем ответ
            }

        }

        void Teach()
        {
            data.Add(question); // add a question 
            File.WriteAllLines(dataPath, data.ToArray()); // saving
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
                    answer = data[i];
                    break;
                }
            }
           
            return answer; // при проверке стоит поставить точку останова
        }

        static string Exclude(string q, char[] symbols)
        {
            foreach (char symbol in symbols)
            {
                q = q.Replace(symbol.ToString(), string.Empty);
            }
            
            return q; // при проверке стоит поставить точку останова
        }

        void ChatBot_GetStr(string obj)
        {
            // заглушка
        }
    }
}
