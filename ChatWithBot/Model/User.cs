using ChatWithBot.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public void SignUser(Chat chat, User user)
        {
            if (!chat.Users.Contains(user))
            {
                if (!chat.ChatLogUsers.ContainsKey(user.Name))
                {
                    chat.ChatLogUsers.Add(user.Name, new LogsUser() { StartChat = DateTime.Now, StopChat = null });
                }
                else
                {
                    chat.ChatLogUsers[user.Name].StopChat = null;
                }
                chat.Users.Add(user);
            }
            else
            {
                throw new ArgumentException("Пользователь уже в чате ");
            }

        }
        /// <summary>
        /// Удалить из чата 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="user"></param>
        /// <param name="NameInvite"></param>
        public void LogutUser(Chat chat, User user, string nameDelete)
        {
            var CickUer = chat.Users.Where(u => u.Name == nameDelete).FirstOrDefault();
            if (CickUer != null)
            {
                chat.ChatLogUsers[CickUer.Name].StopChat = DateTime.Now;
                chat.Users.Remove(CickUer);
            }
            else
            {
                throw new ArgumentNullException("Пользователя с таким именем нет");
            }

        }
        /// <summary>
        /// Отправить сообщение 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="chat"></param>
        public void SendMessage(Message message, Chat chat)
        {
            chat.ListMessage.Add(message);
            Console.WriteLine($"id={message.IdMessage + 1} {message.User.Name} {message.dateTime} ({message.OutUser}): {message.Content}");
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
            if (ChoiceMessage.User.Equals(user) && DateTime.Now.Subtract(ChoiceMessage.dateTime) < new TimeSpan(60, 0, 0))
            {
                chat.ListMessage.Remove(ChoiceMessage);
            }
            else
            {
                throw new ArgumentException("Вы не можете удалить это сообщение. Вы не являетесь его владельцем либо прошло слишком много времени");

            }
        }
        /// <summary>
        /// Удалить чат 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="user"></param>
        public bool DelChat(Chat chat, User user, List<Chat> chats)
        {
            if (chat.Users.Contains(user))
            {
                chat.ChatLogUsers[user.Name].StopChat = DateTime.Now;
                chat.Users.Remove(user);
                chats.Remove(chat);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Пригласить бота 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="NameBot"></param>
        public void InviteBot(Chat chat, IContext context)
        {
            var ListBots = context.GetAllBot();
            Console.WriteLine("Доступны боты:");
            int i = 1;
            foreach (var b in ListBots)
            {
                Console.WriteLine($"{i} {b.NameBot}");
                i++;
            }
            int PickBot = Convert.ToInt32(Console.ReadLine().Trim());
            if (PickBot != 0)
            {
                IBot bot = ListBots[PickBot - 1];
                chat.ChatBot.Add(bot);
                Console.WriteLine("Бот успешно добавлен");
                Console.WriteLine("Команды для бота \n");
                Console.WriteLine(bot.GetAllCommand() + "\n");
            }
        }
        public override bool Equals(object obj)
        {
            return (obj == null) || (obj.GetType() != typeof(User))
            ? false
            : Name == ((User)obj).Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
