using ChatWithBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Interface
{
    interface IContext
    {
        public void CreatChat(List<Chat> chat);
        public List<Chat> GetAllChats();
        public List<User> GetUsers();
        public void CreatUser(List<User> users);
        public List<IBot> GetAllBot();
    }
}
