using ChatWithBot.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model.Bots
{
    [Serializable]
    class BotAnecdote : IBot
    {
        public  string NameBot
        {
            get
            {
                return "BotAnecdote";
            }
            set
            {

            }

        }
        private readonly Dictionary<string, string> ListAnecdot = new Dictionary<string, string>() {
            {"Грустно","Объявление: Продам квартиру в Москве или меняю на посёлок городского типа в Курганской области."},
            {"Скучно", "Eсли вы видитe пьющeгo в oдинoчку чeлoвeкa — нe спeшитe с вывoдaми, вoзмoжнo этo — кoрпoрaтив Сaмoзaнятoгo." },
            {"Хочу анекдот","— Пaп, у мeня кoлeсo спустилo... \n— A чё ты мнe звoнишь, дoчь, у тeбя ж муж eсть, вoт eму и звoни! \n— Дa, блин, звoнилa, oн нe oтвeчaeт...\n  — Ну a зaпaснoгo нeт? \n — Звoнилa, oн тoжe нe oтвeчaeт... ." },
            {"Лучший ", "Сел медведь в тачку с заряженым автозвуком и сгорел " }          
        };
        public string Move(string command)
        {
            command = command.Trim().Replace("/", "");
            if (ListAnecdot.ContainsKey(command))
            {
                return ListAnecdot[command];
            }
            else
            {
                return "Такого анекдота нет((((";
            }
        }
    }
}
