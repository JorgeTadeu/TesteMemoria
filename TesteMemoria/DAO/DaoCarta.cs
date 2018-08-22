using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using Jogo_da_Memoria.Model;


namespace Jogo_da_Memoria.DAO
{
    public class DaoCarta
    {
        OleDbConnection conexao;

        public DaoCarta()
        {
            //conexao = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0;Data Source = F:/Cartas.xlsx;Extended Properties =’Excel 12.0 Xml; HDR = YES;'");
           conexao = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = F:/Cartas.xls; Extended Properties = 'Excel 8.0;HDR=YES;'");
           // conexao = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source =" + banco + "; Extended Properties = 'Excel 8.0;HDR=YES;ReadOnly=False'");
        }

        public List<Cartas> carrega_carta()
        {
            string comandoSql = "select* from[Cartas$]";

            OleDbCommand comando = new OleDbCommand(comandoSql, conexao);

            List<Cartas> ListaCarta = new List<Cartas>();

            try
            {
                conexao.Open();
                OleDbDataReader rd = comando.ExecuteReader();

                while (rd.Read())
                {
                    ListaCarta.Add(new Cartas()
                    {
                        simbolo = Convert.ToString(rd["SIMBOLO"]),
                    });

                }
                if (ListaCarta.Count() > 0)
                {
                    return ListaCarta;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
            finally
            {
                conexao.Close();
            }
        }

        public Cartas read(Cartas carta)
        {
            string comandoSql = "select* from[Cartas$] Where SIMBOLO =" + carta.simbolo;

            OleDbCommand comando = new OleDbCommand(comandoSql, conexao);

            try
            {
                conexao.Open();
                OleDbDataReader rd = comando.ExecuteReader();

                while (rd.Read())
                {
                    carta.simbolo = Convert.ToString(rd["SIMBOLO"]);
                }

                return carta;

            }
            catch
            {
                return null;
            }
            finally
            {
                conexao.Close();

            }
        }
    }
}
