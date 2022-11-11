using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_final
{
    internal class ClassVehiculo
    {
        #region Variables

        public List<ClassPedido> listapedidos;
        public List<ClassGrafoNodo>? Recorrido;
        public int PesoMaximo;
        public int VolumenMaximo;

        #endregion 

        #region Constructors

        public ClassVehiculo(int volumenMaximo,int pesomaximo)
        {
            VolumenMaximo = volumenMaximo;
            PesoMaximo = pesomaximo;
            listapedidos = new List<ClassPedido>();
            Recorrido = null;
        }
        #endregion 

        #region GetSets

        #endregion 

        #region Method

        public void AgregarPedido(ClassPedido pedido)
        {
            listapedidos.Add(pedido);
        }

        public void QuitarPedido(ClassGrafoNodo nodoactual)
        {
            for (int i = 0; i < listapedidos.Count; i++)
            {
                if (listapedidos[i].Barrio == nodoactual.NombreNodo)
                {
                    listapedidos.Remove(listapedidos[i]);
                    i--;
                }
            }
        }

        public void AgregarPedido(List<ClassGrafoNodo> Recorrido)
        {
            this.Recorrido = Recorrido;
        }

        #endregion
    }
}
