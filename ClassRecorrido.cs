using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_final
{
    public class ClassRecorrido
    {
        #region Variables

        public List<ClassGrafoNodo> _listaNodosRecorrido;
        public List<ClassPedido> _pedidosRecorrido;
        public ClassVehiculo ObjetoVehiculo;
        public string _key;
        public int _dia;
        public string _vehiculo;
        public float KmRecorrido;
        public int PesoMaximo;
        public int VolumenMaximo;
        public int PesoDisponible;
        public int VolumenDisponible;

        #endregion 

        #region Constructors

        public ClassRecorrido(string key, ClassVehiculo vehiculo, int dia, List<ClassGrafoNodo> ListaNodos)
        {
            _key = key;
            _vehiculo= vehiculo._vehiculo;
            _dia= dia;
            _listaNodosRecorrido = ListaNodos;
            _pedidosRecorrido = new List<ClassPedido>(vehiculo.listapedidos);
            ObjetoVehiculo = vehiculo;
            PesoMaximo = vehiculo.PesoMaximo;
            VolumenMaximo = vehiculo.VolumenMaximo;
            PesoDisponible=vehiculo.PesoDisponible;
            VolumenDisponible = vehiculo.VolumenDisponible;

            KmRecorrido = 0;

            for(int i = 0; i < ListaNodos.Count - 1; i++)//Sumar los km de la lista de nodos
            {
                for(int j = 0; j < ListaNodos[i].listaunion.Count;j++)
                {
                    if (ListaNodos[i].listaunion[j].NodoDestino == ListaNodos[i + 1])
                    {
                        KmRecorrido += ListaNodos[i].listaunion[j].peso;
                    }
                }
            }
        }

        #endregion 

        #region GetSets

        #endregion 

        #region Method

        #endregion 
    }
}
