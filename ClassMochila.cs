using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_final
{
    public class ClassMochila
    {
        #region Variables


        #endregion 

        #region Constructors

        public ClassMochila()
        {

        }
        #endregion 

        #region GetSets

        #endregion 

        #region Method

        public void SepararListas(List<ClassPedido> Pedidos, List<ClassPedido> PedidosConElevador, List<ClassPedido> PedidosSinElevador, ETipoDeEntrega tipoDeEntrega)
        {
            for (int i = 0; i < Pedidos.Count; i++)
            {
                if (Pedidos[i].TipoDeEntrega == tipoDeEntrega)
                {
                    if (Pedidos[i].Elevador == true)
                    {
                        PedidosConElevador.Add(Pedidos[i]);
                    }
                    else
                    {
                        PedidosSinElevador.Add(Pedidos[i]);
                    }
                }
            }
        }

        public List<int> CargaMochila(int[] Valor, int[] Peso, int CapacidadMochila, int NroElementos)
        {
            int[,] Soluciones = new int[NroElementos + 1, CapacidadMochila + 1];

            for (int i = 0; i <= NroElementos; i++)
            {
                for (int j = 0; j <= CapacidadMochila; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        Soluciones[i, j] = 0;
                    }

                    else if (Peso[i - 1] <= j)
                    {
                        Soluciones[i, j] = Math.Max(Valor[i - 1] + Soluciones[i - 1, j - Peso[i - 1]], Soluciones[i - 1, j]);
                    }
                    else
                    {
                        Soluciones[i, j] = Soluciones[i - 1, j];
                    }
                }
            }
            int[,] Soluciones_2 = new int[NroElementos, CapacidadMochila + 1];

            for (int i = 0; i < NroElementos; i++)
            {
                for (int j = 0; j <= CapacidadMochila; j++)
                {
                    Soluciones_2[i, j] = Soluciones[i + 1, j];
                }
            }
            List<int> listadepedidos = new List<int>();

            int capacidadaux = CapacidadMochila;

            for (int i = NroElementos - 1; i > 0; i--)
            {
                if (Soluciones_2[i, capacidadaux] != Soluciones_2[i - 1, capacidadaux] && Soluciones_2[i, capacidadaux] == Soluciones_2[i - 1, capacidadaux - Peso[i]] + Valor[i])
                {
                    listadepedidos.Add(i);
                    capacidadaux -= Peso[i];
                }
            }
            return listadepedidos;
        }
        #endregion 
    }
}
