using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bot
{
    class Program
    {
        static string Exclude(string question, char [] symbols)
        {
            foreach (char symbol in symbols)
            {
                question = question.Replace(symbol.ToString(), string.Empty);
            }
            return question;
        }
        static string Answer(string q)
        {
            string symbols = "`~!@#$%^&*(){}[]_-+=|\\|/?.,<>'\"№",
                question = Exclude(q.ToLower(),symbols.ToCharArray()) ,
               
                answer = string.Empty;

            string[] data = File.ReadAllLines(Properties.Resources.data);
            for (int i = 0; i < data.Length; i++)
            {
                if (question == data[i])
                {
                    answer= data[i+1];
                    break;
                }
            }
            return answer;
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Pls ask here: ");
                string question = Console.ReadLine();
                Console.WriteLine("Chat bot says: " + Answer(question) );
            }

        }
    }
}
