using ChatWithBot.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model
{
    [Serializable]

    class Chat
    {
        public string Name { get; set; }

        public List<User> Users = new List<User>();
      
        public List<Message> ListMessage = new List<Message>();

        public Dictionary<string, LogsUser> ChatLogUsers = new Dictionary<string, LogsUser>();

        public List<IBot> ChatBot = new List<IBot>();
        public static void CreateChat(User user, List<Chat> chats)
        {
            Chat chat = new Chat();
            Console.WriteLine("Название чата:");
            chat.Name = Console.ReadLine().Trim();
            Console.WriteLine("Доступны боты:");
            int i = 1;
            var ListBots = BinaryHelper.GetAllBot();
            foreach (var b in ListBots)
            {
                Console.WriteLine($"{i} {b.NameBot}");
                i++;
            }
            chat.Users.Add(user);
            chat.ChatLogUsers.Add(user.Name, new LogsUser() { StartChat = DateTime.Now, StopChat = null });
            chat.ChatBot.Add(ListBots[Convert.ToInt32(Console.ReadLine().Trim()) - 1]);        
            chats.Add(chat);
            BinaryHelper.CreatChat(chats);
            Console.WriteLine("Чат успешно создан");
        }
        public static void StartChat(List<Chat> chats)
        {
                   
            int j = 1;
            Console.WriteLine("Доступные чаты:");
            foreach (var c in chats)
            {
                Console.Write($"\t {j} {c.Name}");
                Console.Write("\t Боты:");
                foreach (var b in c.ChatBot)
                {
                    Console.Write($"\t {b.NameBot} \n");
                }
                j++;
            }
            Console.WriteLine("В какой хотете войти:");       
        }
        public static bool GetHistoryChat(Chat chat, User user)
        {

            try
            {
                if (chat.ChatLogUsers[user.Name] != null)
                {
                    var mes = chat.ListMessage.Where(m => chat.ChatLogUsers[user.Name].StartChat <= m.dateTime);
                    if (chat.ChatLogUsers[user.Name].StopChat != null)
                    {
                        mes = chat.ListMessage.Where(m => m.dateTime < chat.ChatLogUsers[user.Name].StopChat);
                    }
                    if (mes.Any())
                    {
                        foreach (var m in mes)
                        {
                            Console.WriteLine($"id={m.IdMessage} {m.dateTime} {m.user.Name} ({m.OutUser}): {m.Content} ");
                        }
                       return false;
                    }
                }
            }
            catch
            {
                Console.WriteLine($"Сообщений пока нет");
                
            }
            return true;

        }
       
    }
}
