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

        public void SepararListas(List<ClassPedido> Pedidos, List<ClassPedido> PedidosConElevador, List<ClassPedido> PedidosSinElevador, ETipoDeEntrega tipoDeEntrega)//Separamos listas en las que tienen elevador y quienes no de tal prioridad
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

       public void ProcesoDeLlenado(List<ClassPedido> PedidosGlobales, List<ClassPedido> Pedidos, ClassVehiculo Vehiculo)//Se le pasa la lista ya filtradas de elevador o no
        {

            // Genero arrays

            int[] Volumenes = new int[Pedidos.Count + 1];
            int[] Pesos = new int[Pedidos.Count + 1];

            Volumenes[0] = 0;
            Pesos[0] = 0;

            for (int i = 1; i < Pedidos.Count + 1; i++)//Guardamos en el array los datos de los pedidos
            {
                Volumenes[i] = (int)Pedidos[i - 1].Volumen;
                Pesos[i] = (int)Pedidos[i - 1].Peso;
            }

            // Primero tengo en cuenta el peso maximo

            List<int> IndicePedidos = CargaMochila(Volumenes, Pesos, Vehiculo.PesoDisponible, Pedidos.Count + 1);//Devolvemos las posiciones de la lista de pedidos que vamos a usar para la mochila

            //Genero lista auxiliar de pedidos que cumplen el primer requisito

            List<ClassPedido> PedidosAux = new List<ClassPedido>();

            for (int i = 0; i < IndicePedidos.Count; i++)//Guardamos los pedidos que vamos a usar teniendo en cuenta el peso 
            {
                PedidosAux.Add(Pedidos[IndicePedidos[i] - 1]);
            }

            // Genero arrays

            Volumenes = new int[PedidosAux.Count + 1];
            Pesos = new int[PedidosAux.Count + 1];

            Volumenes[0] = 0;
            Pesos[0] = 0;

            for (int i = 1; i < PedidosAux.Count + 1; i++)//Guardamos en el array los datos de los pedidos
            {
                Volumenes[i] = (int)PedidosAux[i - 1].Volumen;
                Pesos[i] = (int)PedidosAux[i - 1].Peso;
            }

            // Segundo tengo en cuenta el volumen maximo sobre la lista que nos devolvio el peso maximo

            IndicePedidos = CargaMochila(Pesos, Volumenes, (int)Vehiculo.VolumenDisponible, PedidosAux.Count + 1);//Devolvemos las posiciones de la lista de pedidos que vamos a usar para la mochila

            for (int i = 0; i < IndicePedidos.Count; i++)// Cargo esos pedidos en el vehiculo que cumplen con los 2 requisitos
            {
                Vehiculo.AgregarPedido(PedidosAux[IndicePedidos[i] - 1]);
                Pedidos.Remove(PedidosAux[IndicePedidos[i] - 1]);//Lo sacamos de la lista de pedidos con elevador o sin elevador
                PedidosGlobales.Remove(PedidosAux[IndicePedidos[i] - 1]);
            }

            Console.WriteLine();
        }

        public List<int> CargaMochila(int[] Valor, int[] Peso, int CapacidadMochila, int NroElementos)//Algoritmo mochila
        {
            int[,] Soluciones = new int[NroElementos + 1, CapacidadMochila + 1];//Genero matriz

            for (int i = 0; i <= NroElementos; i++)
            {
                for (int j = 0; j <= CapacidadMochila; j++)
                {
                    if (i == 0 || j == 0)//Generamos primera linea y primera columna en 0
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

            List<int> listadepedidos = new List<int>();//Genero la lista de indices que voy a devolver
            int capacidadaux = CapacidadMochila;

            for (int i = NroElementos - 1; i > 0; i--)
            {
                if (Soluciones_2[i, capacidadaux] != Soluciones_2[i - 1, capacidadaux] && Soluciones_2[i, capacidadaux] == Soluciones_2[i - 1, capacidadaux - Peso[i]] + Valor[i])
                {
                    listadepedidos.Add(i);//Agrego el pedido a la lista
                    capacidadaux -= Peso[i];
                }
            }
            return listadepedidos;
        }
        #endregion 
    }
}
