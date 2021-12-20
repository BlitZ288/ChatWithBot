using ChatWithBot.Interface;
using ChatWithBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot
{
    class ChatApp
    {
        public List<Chat> ListChats = new List<Chat>();
        private List<User> Users = new List<User>();
        IContext Context;

        public ChatApp(IContext context)
        {
            Context = context;
            ListChats = context.GetAllChats();
            Users = context.GetUsers();
        }
        public void CreateChat(Chat chat)
        {
            ListChats.Add(chat);
            Context.CreatChat(ListChats);
           
        }
        public string StartChat(int index)
        {
            
        }
        public string GetHistoryChat(Chat chat , User user)
        {
            string result = "";
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
                            result+=($"id={m.IdMessage + 1} {m.dateTime} {m.user.Name} ({m.OutUser}): {m.Content} \n");
                        }
                        return result;
                    }
                }
            }
            catch
            {
                result = "Сообщений пока нет";
            }
            return result;
        }
        
        public string Inviteuser(Chat chat,User user,string nameInvite)
        {
            bool resutInvite = user.SignUser(chat, nameInvite, Context);
            if (resutInvite)
            {
                Context.CreatChat(ListChats);
                return $"Пользователь {nameInvite} присоединился";
            }
            else
            {
                return "Пользователя с таким именем не найдент или уже в чате ";
            }
            
        }

    }
}
