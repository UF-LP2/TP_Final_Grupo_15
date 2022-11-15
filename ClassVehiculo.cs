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
    public class ClassVehiculo
    {
        #region Variables

        public List<ClassPedido> listapedidos;
        public List<ClassGrafoNodo>? Recorrido;
        public int PesoMaximo;
        public int VolumenMaximo;
        public int PesoDisponible;
        public int VolumenDisponible;
        public string? _vehiculo;
        public ETipoDeVehiculo vehiculo;

        #endregion 

        #region Constructors

        public ClassVehiculo(string Vehiculito)
        {
            _vehiculo = Vehiculito;

            switch (Vehiculito)
            {
                case "furgon":
                    vehiculo = ETipoDeVehiculo.Furgon;
                    VolumenMaximo = 10800000 / 1000;
                    PesoMaximo = 7000;
                    break;
                case "furgoneta":
                    vehiculo = ETipoDeVehiculo.Furgoneta;
                    VolumenMaximo = 17000000 / 1000;
                    PesoMaximo = 3500;
                    break;
                case "camioneta":
                    vehiculo = ETipoDeVehiculo.Camioneta;
                    VolumenMaximo = 5490000 / 1000;
                    PesoMaximo = 750;
                    break;
                default:
                    vehiculo = ETipoDeVehiculo.Camioneta;
                    VolumenMaximo = 5490000;
                    PesoMaximo = 750;
                    break;
            }
            listapedidos = new List<ClassPedido>();
            Recorrido = null;
            VolumenDisponible = VolumenMaximo;
            PesoDisponible = PesoMaximo;
        }

        #endregion 

        #region GetSets

        #endregion 

        #region Method

        public void AgregarPedido(ClassPedido pedido)
        {
            listapedidos.Add(pedido);
            VolumenDisponible -= (int)pedido.Volumen;
            PesoDisponible -= (int)pedido.Peso;
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

        public float GastoDeGasolina(float kilometros)//Calculo de gasto de gasolina
        {
            float suma = 0;

            switch (vehiculo)
            {
                case ETipoDeVehiculo.Furgon:
                    suma = (kilometros * 8.904F) / 100;
                    break;
                case ETipoDeVehiculo.Furgoneta:
                    suma = (kilometros * 6.94F) / 100;
                    break;
                case ETipoDeVehiculo.Camioneta:
                    suma = (kilometros * 7.5F) / 100;
                    break;
                default:
                    break;
            }
            return suma;
        }

        public void AgregarRecorrido(List<ClassGrafoNodo> Recorrido)
        {
            this.Recorrido = Recorrido;
        }

        #endregion
    }
}
