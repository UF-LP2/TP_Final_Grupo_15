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

        public void AgregarUnion(string partida, string destino, float peso)//Le pasamos las uniones en el csv
        {
            try
            {
                ClassGrafoNodo? NodoPartida = BuscarNodo(partida);//Buscamos el supuesto nodo
                ClassGrafoNodo? NodoDestino = BuscarNodo(destino);//Buscamos el supuesto nodo
                if (NodoPartida == null || NodoDestino == null)
                {
                    return;
                }

                ClassGrafoUnion NuevaUnionPartida = new ClassGrafoUnion(NodoPartida, NodoDestino, peso);
                NodoPartida.listaunion.Add(NuevaUnionPartida);//Añadimos la union creada

                ClassGrafoUnion NuevaUnionDestino = new ClassGrafoUnion(NodoDestino, NodoPartida, peso);
                NodoDestino.listaunion.Add(NuevaUnionDestino);//Añadimos la union creada

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

        public List<ClassGrafoNodo>? Camino(string inicio, string destino, List<ClassGrafoNodo> Recorrido, List<ClassGrafoNodo> NodosVisitados)//Algoritmo voraz
        {
            ClassGrafoNodo? NodoInicio = BuscarNodo(inicio);//Buscamos el supuesto nodo
            ClassGrafoNodo? NodoDestino = BuscarNodo(destino);//Buscamos el supuesto nodo

            if (NodoInicio == null || NodoDestino == null)
            {
                return null;
            }

            List<ClassGrafoNodo> RecorridoActual = new List<ClassGrafoNodo>(Recorrido);
            List<ClassGrafoNodo>? RecorridoAux;

            if (NodoInicio == NodoDestino)//Si llegamos al destino
            {
                Recorrido.Add(NodoDestino);//Agregamos el ultimo nodo al recorrido
                return Recorrido;
            }
            else
            {
                foreach (ClassGrafoNodo Nodo in NodosVisitados)//Revisamos que no hayamos pasado por el nodo que vamos a analizar
                {
                    if (Nodo == NodoInicio)
                    {
                        return null;
                    }
                }
                NodosVisitados.Add(NodoInicio);
                RecorridoActual.Add(NodoInicio);
                List<ClassGrafoUnion> PosiblesCaminos = NodoInicio.listaunion;

                foreach(ClassGrafoUnion union in PosiblesCaminos) //Vamos generando el recorrido a partir de las uniones
                {
                    RecorridoAux = Camino(union.NodoDestino.NombreNodo, destino, RecorridoActual, NodosVisitados);

                    if (RecorridoAux != null)//Si por la union se logra por alguna razon llegar al objetivo
                    {
                        return RecorridoAux;
                    }
                }
                return null;//Si ninguna union es valida para llegar al objetivo se vuelve al nodo anterior
            }
        }

        #endregion
    }
}
