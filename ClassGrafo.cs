using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tp_final
{
    public class ClassGrafo
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

        public void AgregarUnion(string partida, string destino, float peso)
        {
            try
            {
                ClassGrafoNodo? NodoPartida = BuscarNodo(partida);
                ClassGrafoNodo? NodoDestino = BuscarNodo(destino);
                if (NodoPartida == null || NodoDestino == null)
                {
                    return;
                }

                ClassGrafoUnion NuevaUnionPartida = new ClassGrafoUnion(NodoPartida, NodoDestino, peso);
                NodoPartida.listaunion.Add(NuevaUnionPartida);

                ClassGrafoUnion NuevaUnionDestino = new ClassGrafoUnion(NodoDestino, NodoPartida, peso);
                NodoDestino.listaunion.Add(NuevaUnionDestino);

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
                if (Nodo.NombreNodo == zona) { return Nodo; }
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

            
            //List<ClassGrafoNodo> RecorridoActual = Recorrido; // todo: clonar lista

            List<ClassGrafoNodo> RecorridoActual = new List<ClassGrafoNodo>(Recorrido); // Revisar

            List<ClassGrafoNodo>? RecorridoAux;
            if (NodoInicio == NodoDestino)
            {
                Recorrido.Add(NodoDestino); //modificado para incluir en ultimo nodo en el recorrido
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
                    RecorridoAux = Camino(union.NodoDestino.NombreNodo, destino, RecorridoActual, NodosVisitados);

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
