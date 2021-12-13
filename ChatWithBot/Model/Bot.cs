using ChatWithBot.Model.Bots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model
{
    [Serializable]
    public class Bot
    {
       
        public virtual string NameBot { get; set; }
        public static List<Bot> GetAllBott()
        {
            List<Bot> Bots = new List<Bot>()
            {
                new BotLoader(),
                new BotAnecdote(),
                new BotTime()

            };
            return Bots;
        } 
    }
}
