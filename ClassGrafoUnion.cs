using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_final
{
    public class ClassGrafoUnion
    {
        #region Variables

        public ClassGrafoNodo NodoPartida;
        public ClassGrafoNodo NodoDestino;
        public float peso;

        #endregion

        #region Constructors

        public ClassGrafoUnion(ClassGrafoNodo NodoPartida, ClassGrafoNodo NodoDestino, float peso)
        {
            this.NodoPartida = NodoPartida;
            this.NodoDestino = NodoDestino;
            this.peso = peso;
        }

        #endregion

        #region GetSets

        #endregion

        #region Method

        #endregion
    }
}
