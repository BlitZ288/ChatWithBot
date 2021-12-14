using ChatWithBot.Model.Bots;
using System;
using System.Collections.Generic;

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
