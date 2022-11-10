using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_final
{
    public enum ETipoDePrducto
    {
        PequenoElectrodomestico,
        LineaBlanca,
        Electronico,
        Televisor
    }

    public enum ETipoDeEntrega
    {
        Express,
        Normal,
        Diferido
    }

    public class ClassPedido
    {
        #region Variables

        private ETipoDePrducto _tipodeprducto;
        private float _precio;
        private float _largo;
        private float _ancho;
        private float _alto;
        private float _peso;
        private ETipoDeEntrega _tipodeentrega;
        private string? _barrio;

        #endregion 

        #region Constructors

        public ClassPedido(string producto, float precio, float largo, float ancho, float alto, float peso, string prioridad, string barrio)
        {
            switch (producto)
            {
                case "Televisor":
                    TipoDePrducto = ETipoDePrducto.Televisor;
                    break;
                default:
                    break;
            }
            Precio = precio;
            Largo = largo;
            Ancho = ancho;
            Alto = alto;
            Peso = peso;

            switch (prioridad)
            {
                case "express":
                    TipoDeEntrega = ETipoDeEntrega.Express;
                    break;
                case "normal":
                    TipoDeEntrega = ETipoDeEntrega.Normal;
                    break;
                case "diferido":
                    TipoDeEntrega = ETipoDeEntrega.Diferido;
                    break;
                default:
                    TipoDeEntrega = ETipoDeEntrega.Normal;
                    break;
            }

            Barrio = barrio;
        }

        ~ClassPedido()
        {
            Console.WriteLine("");
        }

        #endregion 

        #region GetSets
        public float Precio { get { return _precio; } set { _precio = value; } }
        public float Largo { get { return _largo; } set { _largo = value; } }
        public float Ancho { get { return _ancho; } set { _ancho = value; } }
        public float Alto { get { return _alto; } set { _alto = value; } }
        public float Peso { get { return _peso; } set { _peso = value; } }
        public ETipoDePrducto TipoDePrducto { get { return _tipodeprducto; } set { _tipodeprducto = value; } }
        public ETipoDeEntrega TipoDeEntrega { get { return _tipodeentrega; } set { _tipodeentrega = value; } }
        public string? Barrio { get { return _barrio; } set { _barrio = value; } }

        #endregion 
    }
}
