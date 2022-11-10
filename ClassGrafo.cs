using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tp_final
{
    internal class ClassGrafo
    {
        #region Variables

        public List<ClassGrafoNodo> NodoList;

        #endregion

        #region Constructors

        public ClassGrafo()
        {
            NodoList = new List<ClassGrafoNodo>();
        }

        #endregion

        #region GetSets

        #endregion

        #region Method

        public void AgregarNodo(string zona)
        {
            ClassGrafoNodo NuevoNodo = new ClassGrafoNodo(zona);
            NodoList.Add(NuevoNodo);
        }

        public void AgregarUnion(string partida, string destino, int peso)
        {
            try
            {
                ClassGrafoNodo NodoPartida = BuscarNodo(partida);
                ClassGrafoNodo NodoDestino = BuscarNodo(destino);
                ClassGrafoUnion NuevaUnion = new ClassGrafoUnion(NodoPartida, NodoDestino, peso);
                NodoPartida.listaunion.Add(NuevaUnion);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ClassGrafoNodo? BuscarNodo(string zona)
        {
            foreach (ClassGrafoNodo Nodo in NodoList)
            {
                if (Nodo.zona == zona) { return Nodo; }
            }
            return null;
        }

        public List<ClassGrafoNodo>? Camino(string inicio, string destino, List<ClassGrafoNodo> Recorrido, List<ClassGrafoNodo> NodosVisitados)
        {
            ClassGrafoNodo? NodoInicio = BuscarNodo(inicio);
            ClassGrafoNodo? NodoDestino = BuscarNodo(destino);

            if(NodoInicio == null || NodoDestino == null)
            {
                return null;
            }

            List<ClassGrafoNodo> RecorridoActual = Recorrido; // todo: clonar lista
            List<ClassGrafoNodo>? RecorridoAux;
            if (NodoInicio == NodoDestino)
            {
                return Recorrido;
            }
            else
            {
                foreach (ClassGrafoNodo Nodo in NodosVisitados)
                {
                    if (Nodo == NodoInicio)
                    {
                        return null;
                    }
                }
                NodosVisitados.Add(NodoInicio);
                RecorridoActual.Add(NodoInicio);
                List<ClassGrafoUnion> PosiblesCaminos = NodoInicio.listaunion;
                foreach(ClassGrafoUnion union in PosiblesCaminos) 
                {
                    RecorridoAux = Camino(union.NodoDestino.zona, destino, RecorridoActual, NodosVisitados);

                    if (RecorridoAux != null)
                    {
                        return RecorridoAux;
                    }
                }
                return null;
            }
        }
        #endregion
    }
}
