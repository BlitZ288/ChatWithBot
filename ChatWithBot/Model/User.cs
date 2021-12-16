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
    class User
    {
        public string Name { get; set; }

         public User(string Name)
        {
            this.Name = Name;
        }
        public User() { }

        /// <summary>
        /// Добавление в чат 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="NameInvite"></param>
        public void SignUser(Chat chat, string NameInvite)
        {  
            var Inviteuser = BinaryHelper.GetUsers().Where(u => u.Name == NameInvite).FirstOrDefault();    
            if (Inviteuser != null && !chat.Users.Contains(Inviteuser))
            {
                if (!chat.ChatLogUsers.ContainsKey(NameInvite))
                {
                    chat.ChatLogUsers.Add(NameInvite, new LogsUser() { StartChat = DateTime.Now, StopChat = null });
                }
                else
                {
                    chat.ChatLogUsers[NameInvite].StopChat = null;
                }
                chat.Users.Add(Inviteuser);
                Console.WriteLine($"Пользователь {NameInvite} приглашен");
            }
            else
            {
                Console.WriteLine("Пользователя с таким именем не найдент или уже в чате ");
            }
        }
        /// <summary>
        /// Удалить из чата 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="user"></param>
        /// <param name="NameInvite"></param>
        public void LogutUser(Chat chat,User user, string NameInvite)
        {
            if (chat.Users.Contains(user))
            {
                //chat.LogActions.Add(new LogAction(DateTime.Now, choice[0], user.Name));
                var CickUer = chat.Users.Where(u => u.Name == NameInvite).FirstOrDefault();
                if (CickUer != null)
                {
                    chat.ChatLogUsers[CickUer.Name].StopChat = DateTime.Now;
                    chat.Users.Remove(CickUer);
                    Console.WriteLine($"Пользователь {NameInvite} удален из чата");
                }
                else
                {
                    Console.WriteLine("Пользователя с таким именем нет");
                }
            }
            else
            {
                Console.WriteLine("Вы не являись участником");
                Console.WriteLine("Чтобы присоединиться используйте команду sign @username");
            }

        }
        /// <summary>
        /// Отправить сообщение 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="chat"></param>
        public void SendMessage(Message message,Chat chat)
        {
            chat.ListMessage.Add(message);
            Console.WriteLine($"id={message.IdMessage} {message.user.Name} {message.dateTime} ({message.OutUser}): {message.Content}");
        }
        /// <summary>
        /// Удалить сообщение 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="index"></param>
        /// <param name="user"></param>
        public void DelMessage(Chat chat, int index, User user)
        {
            Message ChoiceMessage = chat.ListMessage[index];
            //chat.LogActions.Add(new LogAction(DateTime.Now, choice[0], user.Name));
            if (ChoiceMessage.user.Equals(user) && DateTime.Now.Subtract(ChoiceMessage.dateTime) < new TimeSpan(60, 0, 0))
            {
                chat.ListMessage.Remove(ChoiceMessage);
                Console.WriteLine("Сообщение успешно удаленно ");
            }
            else
            {
                Console.WriteLine("Вы не можете удалить это сообщение. Вы не являетесь его владельцем либо прошло слишком много времени");
            }

        }
        /// <summary>
        /// Удалить чат 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="user"></param>
        public void DelChat(Chat chat,User user,List<Chat> chats)
        {
            if (chat.Users.Contains(user))
            {
                chat.ChatLogUsers[user.Name].StopChat = DateTime.Now;
                chat.Users.Remove(user);
                chats.Remove(chat);
                Console.WriteLine("Чат упешео удален");
            }
        }
        /// <summary>
        /// Пригласить бота 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="NameBot"></param>
        public void InviteBot(Chat chat, string NameBot)
        {
            var ListBot = BinaryHelper.GetAllBot().Where(b => b.NameBot == NameBot).FirstOrDefault();
            if (ListBot != null && !chat.ChatBot.Contains(ListBot))
            {
                chat.ChatBot.Add(ListBot);
                Console.WriteLine("Бот успешно добавлен");
            }
            else
            {
                Console.WriteLine("Такого бота нет ((");
            }
        }
        public override bool Equals(object obj)
        {
            return (obj==null) || (obj.GetType() != typeof(User))
            ? false 
            : Name==((User)obj).Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
