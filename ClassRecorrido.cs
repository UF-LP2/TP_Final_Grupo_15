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
        public string _key;
        public int _dia;
        public string _vehiculo;
        public int KmRecorrido;

        #endregion 

        #region Constructors

        public ClassRecorrido(string key,string vehiculo, int dia, List<ClassGrafoNodo> ListaNodos)
        {
            _key = key;
            _vehiculo= vehiculo;
            _dia= dia;
            _listaNodosRecorrido = ListaNodos;
            _pedidosRecorrido = new List<ClassPedido>();
            KmRecorrido = 0;//Sumar los km de la lista de nodos

        }
        #endregion 

        #region GetSets

        #endregion 

        #region Method

        #endregion 
    }
}
