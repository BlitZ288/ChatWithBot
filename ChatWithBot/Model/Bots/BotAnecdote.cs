using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model.Bots
{
    [Serializable]
    class BotAnecdote:Bot
    {
        public override string NameBot
        {
            get
            {
                return "BotAnecdote";
            }
            set
            {

            }

        }
        private readonly List<string> ListAnecdot = new List<string>() {
            "Объявление: Продам квартиру в Москве или меняю на посёлок городского типа в Курганской области.",
            "Eсли вы видитe пьющeгo в oдинoчку чeлoвeкa — нe спeшитe с вывoдaми, вoзмoжнo этo — кoрпoрaтив Сaмoзaнятoгo.",
            "— Пaп, у мeня кoлeсo спустилo... \n— A чё ты мнe звoнишь, дoчь, у тeбя ж муж eсть, вoт eму и звoни! \n— Дa, блин, звoнилa, oн нe oтвeчaeт...\n  — Ну a зaпaснoгo нeт? \n — Звoнилa, oн тoжe нe oтвeчaeт... .",
            "Сел медведь в тачку с заряженым автозвуком И сгорел "            
        };
        public string Move()
        {
            Random random = new Random();            
            return ListAnecdot[random.Next(0, 3)];
        }

    }
}
