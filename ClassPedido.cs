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
        Semiexpress,
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
        private float _volumen;
        private ETipoDeEntrega _tipodeentrega;
        private string? _barrio;
        private bool _elevador;

        #endregion 

        #region Constructors

        public ClassPedido(string? producto, float precio, float largo, float ancho, float alto, float peso, string? prioridad, string? barrio)
        {
            _producto = producto;

            switch (_producto)
            {
                case "impresoras":
                    TipoDePrducto = ETipoDePrducto.Electronico;
                    break;
                case "disco solido":
                    TipoDePrducto = ETipoDePrducto.Electronico;
                    break;
                case "disco duro":
                    TipoDePrducto = ETipoDePrducto.Electronico;
                    break;
                case "computadoras personales":
                    TipoDePrducto = ETipoDePrducto.Electronico;
                    break;
                case "webcam":
                    TipoDePrducto = ETipoDePrducto.Electronico;
                    break;
                case "teclado":
                    TipoDePrducto = ETipoDePrducto.Electronico;
                    break;
                case "parlante":
                    TipoDePrducto = ETipoDePrducto.Electronico;
                    break;
                case "mouse":
                    TipoDePrducto = ETipoDePrducto.Electronico;
                    break;
                case "monitor":
                    TipoDePrducto = ETipoDePrducto.Electronico;
                    break;
                case "termotanques":
                    TipoDePrducto = ETipoDePrducto.LineaBlanca;
                    break;
                case "cocinas":
                    TipoDePrducto = ETipoDePrducto.LineaBlanca;
                    break;
                case "calefones":
                    TipoDePrducto = ETipoDePrducto.LineaBlanca;
                    break;
                case "secarropas":
                    TipoDePrducto = ETipoDePrducto.LineaBlanca;
                    break;
                case "microondas":
                    TipoDePrducto = ETipoDePrducto.LineaBlanca;
                    break;
                case "lavarropas":
                    TipoDePrducto = ETipoDePrducto.LineaBlanca;
                    break;
                case "heladeras":
                    TipoDePrducto = ETipoDePrducto.LineaBlanca;
                    break;
                case "freezers":
                    TipoDePrducto = ETipoDePrducto.LineaBlanca;
                    break;
                case "molinillos de granos de café":
                    TipoDePrducto = ETipoDePrducto.PequenoElectrodomestico;
                    break;
                case "tostadoras":
                    TipoDePrducto = ETipoDePrducto.PequenoElectrodomestico;
                    break;
                case "cafeteras":
                    TipoDePrducto = ETipoDePrducto.PequenoElectrodomestico;
                    break;
                case "ralladores":
                    TipoDePrducto = ETipoDePrducto.PequenoElectrodomestico;
                    break;
                case "licuadoras":
                    TipoDePrducto = ETipoDePrducto.PequenoElectrodomestico;
                    break;
                case "exprimidores":
                    TipoDePrducto = ETipoDePrducto.PequenoElectrodomestico;
                    break;
                case "televisor 52":
                    TipoDePrducto = ETipoDePrducto.Televisor;
                    break;
                case "televisor 55":
                    TipoDePrducto = ETipoDePrducto.Televisor;
                    break;
                case "televisor 62":
                    TipoDePrducto = ETipoDePrducto.Televisor;
                    break;
                case "televisor 68":
                    TipoDePrducto = ETipoDePrducto.Televisor;
                    break;
                case "televisor 75":
                    TipoDePrducto = ETipoDePrducto.Televisor;
                    break;
                case "televisor 82":
                    TipoDePrducto = ETipoDePrducto.Televisor;
                    break;
                case "televisor 90":
                    TipoDePrducto = ETipoDePrducto.Televisor;
                    break;
                default:
                    _producto = "molinillos de granos de cafe";
                    TipoDePrducto = ETipoDePrducto.PequenoElectrodomestico;
                    break;
            }

            Precio = precio;
            Largo = largo;
            Ancho = ancho;
            Alto = alto;
            Peso = peso*3;
            Volumen = ((ancho) * (largo) * (alto) / 1000);
            _prioridad = prioridad;
            Barrio = barrio;

            switch (_prioridad)
            {
                case "express":
                    TipoDeEntrega = ETipoDeEntrega.Express;
                    break;
                case "semiexpress,":
                    TipoDeEntrega = ETipoDeEntrega.Semiexpress;
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

            if (TipoDePrducto == ETipoDePrducto.LineaBlanca)
            {
                Elevador = true;
            }
            else
            {
                Elevador = false;
            }

        }

        ~ClassPedido()
        {

        }

        #endregion 

        #region GetSets
        public float Precio { get { return _precio; } set { _precio = value; } }
        public float Volumen { get { return _volumen; } set { _volumen = value; } }
        public float Largo { get { return _largo; } set { _largo = value; } }
        public float Ancho { get { return _ancho; } set { _ancho = value; } }
        public float Alto { get { return _alto; } set { _alto = value; } }
        public float Peso { get { return _peso; } set { _peso = value; } }
        public ETipoDePrducto TipoDePrducto { get { return _tipodeprducto; } set { _tipodeprducto = value; } }
        public ETipoDeEntrega TipoDeEntrega { get { return _tipodeentrega; } set { _tipodeentrega = value; } }
        public string? Barrio { get { return _barrio; } set { _barrio = value; } }
        public string? _producto { get; set; }
        public string? _prioridad { get; set; }
        public bool Elevador  { get { return _elevador; } set { _elevador = value; } }

        #endregion 
    }
}
