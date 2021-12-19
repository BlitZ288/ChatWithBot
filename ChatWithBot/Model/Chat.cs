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
        public Chat CreateChat(User user, List<Chat> chats,IContext context)
        {
            Chat chat = new Chat();
            Console.WriteLine("Название чата:");
            chat.Name = Console.ReadLine().Trim();         
            chat.Users.Add(user);
            chat.ChatLogUsers.Add(user.Name, new LogsUser() { StartChat = DateTime.Now, StopChat = null });
            chats.Add(chat);
            context.CreatChat(chats);
            Console.WriteLine("Чат успешно создан");
            return chat;
        }
        public  bool StartChat(List<Chat> chats)
        {
            if (chats.Count == 0)
            {
                Console.WriteLine("Доступных чатов нет");
                return false;
            }
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
                Console.WriteLine("");
            }
            Console.WriteLine("В какой хотите войти:");
            return true;
        }
        public  bool GetHistoryChat(Chat chat, User user)
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
