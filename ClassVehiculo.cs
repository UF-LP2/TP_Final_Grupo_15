using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_final
{
    public enum ETipoDeVehiculo
    {
        Furgon= 0,
        Furgoneta=1,
        Camioneta=2
    }
    internal class ClassVehiculo
    {
        #region Variables

        public List<ClassPedido> listapedidos;
        public List<ClassGrafoNodo>? Recorrido;
        public int PesoMaximo;
        public float VolumenMaximo;//se puede hacer con int
        public ETipoDeVehiculo vehiculo;

        #endregion 

        #region Constructors

        public ClassVehiculo(float volumenMaximo,int pesomaximo)
        {
            VolumenMaximo = volumenMaximo;
            PesoMaximo = pesomaximo;
            listapedidos = new List<ClassPedido>();
            Recorrido = null;
        }
        public ClassVehiculo(string Vehiculito)//POSIBLE CONSTRUCTOR
        {
            switch (Vehiculito)
            {
                case "furgon":
                    vehiculo = ETipoDeVehiculo.Furgon;
                    VolumenMaximo = 10.8F;
                    PesoMaximo = 7000;
                    break;
                case "furgoneta,":
                    vehiculo = ETipoDeVehiculo.Furgoneta;
                    VolumenMaximo = 17.0F;
                    PesoMaximo = 3500;
                    break;
                case "camioneta,":
                    vehiculo = ETipoDeVehiculo.Camioneta;
                    VolumenMaximo = 5.49F;
                    PesoMaximo = 750;
                    break;
                default:
                    vehiculo = ETipoDeVehiculo.Camioneta;
                    VolumenMaximo = 5.49F;
                    PesoMaximo = 750;
                    break;
            }
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
        public float GastoDeGasolina()//Hay q revisarla con un caso
        {
            float suma = 0;
            for(int i=0; i < Recorrido.Count; i++)
            {
                for(int j=0;j< Recorrido[i].listaunion.Count;j++)
                {
                    if(Recorrido[i].listaunion[j].NodoDestino== Recorrido[i+1])
                    {
                        suma += Recorrido[i].listaunion[j].peso;
                        break;
                    }
                }
            }
            switch (vehiculo)
            {
                case ETipoDeVehiculo.Furgon:
                    suma = (suma * 8.904F) / 100;
                    break;
                case ETipoDeVehiculo.Furgoneta:
                    suma = (suma * 6.94F) / 100;
                    break;
                case ETipoDeVehiculo.Camioneta:
                    suma = (suma * 7.5F) / 100;//NI IDEAAAA ES NAFTERO
                    break;
                default:
                    break;
            }

            return 0;
        }
        public void AgregarRecorrido(List<ClassGrafoNodo> Recorrido)
        {
            this.Recorrido = Recorrido;
        }

        #endregion
    }
}
