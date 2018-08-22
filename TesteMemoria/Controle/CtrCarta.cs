using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jogo_da_Memoria.DAO;
using Jogo_da_Memoria.Model;


namespace Jogo_da_Memoria.Controle
{
    class CtrCarta
    {
        DaoCarta daocarta;
        public List<Cartas> lista_carta;
        Random random = new Random();

        public CtrCarta()
        {
            daocarta = new DaoCarta();
            lista_carta = new List<Cartas>();
        }

        public void carrega_carta()
        {
            lista_carta = daocarta.carrega_carta();
        }

        public int embaralhar()
        {
            int randomNumber = random.Next(lista_carta.Count);
            return randomNumber;
        }
    }
}
