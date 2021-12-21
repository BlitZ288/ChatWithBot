using ChatWithBot.Interface;
using ChatWithBot.Model;
using System;
using System.Linq;

namespace ChatWithBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать");
            IContext context = new BinaryHelper();
            ChatApp chatApp = new ChatApp(context);

            Console.WriteLine("Ваше имя:");
            string UserName = Console.ReadLine().Trim();
            User user=chatApp.GetUser(UserName);
           
            Console.WriteLine("Cписок доступных команд\n create-chat\n start-chat \n");
            bool showMenu = true;
            while (showMenu)
            {
                string choice = Console.ReadLine().Trim();
                switch (choice)
                {
                    case "create-chat":
                        Console.WriteLine("Название чата:");
                        string NameChat = Console.ReadLine().Trim();
                        Chat Newchat = new Chat(user, NameChat);
                        chatApp.CreateChat(Newchat);
                        Console.WriteLine("Чат успешно создан");
                        /*Пока под вопросом
                        Newchat.AddLogChat(Newchat, choice, user);
                        */
                        DialogChat(user, Newchat, chatApp);
                        break;
                    case "start-chat":
                        if (chatApp.ListChats.Count == 0)
                        {
                            Console.WriteLine("Доступных чатов нет");
                            Console.WriteLine("Для создания чата введите:\n create-chat");
                            break;
                        }
                        int j = 1;
                        Console.WriteLine("Доступные чаты:");
                        foreach (var c in chatApp.ListChats)
                        {
                            Console.Write($"\t {j} {c.Name}");
                            Console.Write("\t Боты:");
                            foreach (var b in c.ChatBot)
                            {
                                Console.Write($"\t {b.NameBot} \n");
                            }
                            j++;
                        }
                        Console.WriteLine("В какой хотите войти:");
                        try
                        {
                            int chociChat = Convert.ToInt32(Console.ReadLine().Trim()) - 1;
                            Chat chat = chatApp.ListChats[chociChat];
                            //chat.AddLogChat(chat, choice, user);
                            DialogChat(user, chat, chatApp);
                            showMenu = false;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный формат ввода");
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Неверный формат ввода");
                        }
                        break;
                    default:
                        Console.WriteLine("Введинте одну из команд:\n create-chat \n start-chat");
                        break;
                }
            }
        }
        static void DialogChat(User user, Chat chat,  ChatApp chatApp)
        {
            Console.WriteLine("Список участников данного чата : ");
            foreach (var u in chat.Users)
            {
                Console.WriteLine($"{u.Name}");
            }
            Console.WriteLine(chatApp.GetHistoryChat(chat, user));
            string listCommand = " sign @username \n logut @username\n add-mes @username ``message``\n" +
              " del-mes messageId \n bot @botname @username /bot-command\n stop-сhat \n signb \n 0-Сохранить и выйти\n";
            Console.WriteLine("Доступные команды ");
            Console.WriteLine(listCommand);
            if (chat.ChatBot.Count > 0)
            {
                Console.WriteLine("Команды для ботов \n");
                foreach (var b in chat.ChatBot)
                {
                    Console.WriteLine($"Название бота {b.NameBot}");
                    Console.WriteLine(b.GetAllCommand() + "\n");
                }
            }
            bool flagStop = true;
            while (flagStop)
            {
                string[] CommandParts = Console.ReadLine().Trim().Split(" ");
                switch (CommandParts[0])
                {
                    case "sign":
                        string NameInvite = CommandParts[1].Replace("@", "");
                        try
                        {
                            chatApp.InviteUser(chat, user, NameInvite);
                            Console.WriteLine($"Пользователь {NameInvite} успешно присоединился");
                        }
                        catch (ArgumentNullException e)
                        {
                            Console.WriteLine(e.ParamName);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "logut":
                        NameInvite = CommandParts[1].Replace("@", "");
                        try
                        {
                            chatApp.DeleteUser(chat, user, NameInvite);
                            Console.WriteLine($"Пользователь {NameInvite} успешно удален");
                        }
                        catch (ArgumentNullException e)
                        {
                            Console.WriteLine(e.ParamName);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "add-mes":
                        string NameSend = CommandParts[1].Replace("@", "");
                        Console.WriteLine("Сообщение:");
                        string Content = Console.ReadLine().Trim();
                        try
                        {
                            var message = chatApp.SendMessege(chat, user, Content, NameSend);
                            Console.WriteLine($"id={message.IdMessage + 1} {message.User.Name} {message.dateTime} ({message.OutUser}): {message.Content}");
                            chat.AddLogChat(chat, CommandParts[0], user);
                        }
                        catch (ArgumentNullException e)
                        {
                            Console.WriteLine(e.ParamName);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "del-mes":
                        try
                        {
                            int IndexMessege = Convert.ToInt32(CommandParts[1]) - 1;
                            chatApp.DeleteMessge(chat, IndexMessege, user);
                            Console.WriteLine("Сообщение успешно удаленно ");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный формат ввода");
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Сообщение с таким номер нет");

                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "bot":
                        try
                        {
                            string botName = CommandParts[1].Replace("@", "");
                            string commadBot = CommandParts[^1];
                            var message=chatApp.BotMove(chat, user, botName, commadBot);
                            Console.WriteLine($"id={message.IdMessage + 1} {message.User.Name} {message.dateTime} ({message.OutUser}): {message.Content}");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный формат ввода");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Неверный формат ввода");
                        }
                        catch(ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "signb":
                        var ListBots = chatApp.Bots;
                        Console.WriteLine("Доступны боты:");
                        int i = 1;
                        foreach (var b in ListBots)
                        {
                            Console.WriteLine($"{i} {b.NameBot}");
                            i++;
                        }
                        try
                        {
                            int PickBot = Convert.ToInt32(Console.ReadLine().Trim())-1;
                            IBot bot = chatApp.InviteBot(PickBot, chat, user);
                            Console.WriteLine("Бот успешно добавлен");
                            Console.WriteLine("Команды для бота \n");
                            Console.WriteLine(bot.GetAllCommand() + "\n");
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Неверный формат ввода");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "stop-chat":
                        try
                        {
                            chatApp.DeleteChat(chat, user);
                            Console.WriteLine("Чат упешео удален");
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "0":
                        flagStop = false;
                        break;
                    default:
                        Console.WriteLine("Неверная команда , пожалуйста выберите из списка доступных команд :");
                        Console.WriteLine(listCommand);
                        break;
                }
            }
        }
    }

}
