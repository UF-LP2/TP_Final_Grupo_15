using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tp_final
{
    public partial class FormDetallesPedidos : Form
    {
        #region Variables

        public ClassRecorrido recorrido;
        public int id;

        #endregion

        public FormDetallesPedidos(ClassRecorrido recorrido)
        {
            InitializeComponent();
            this.recorrido = recorrido;
            id = 0;
        }

        private void FormDetallesPedidos_Load(object sender, EventArgs e)
        {
            foreach (ClassPedido pedido in recorrido._pedidosRecorrido)//Imprimo en la ListView los recorridos
            {
                ListViewItem lista = new ListViewItem(Convert.ToString(id));
                lista.SubItems.Add(pedido._producto);

                string TipoDeProductoAux;

                switch (pedido.TipoDePrducto)
                {
                    case ETipoDePrducto.Electronico:
                        TipoDeProductoAux = "Electronico";
                        break;
                    case ETipoDePrducto.LineaBlanca:
                        TipoDeProductoAux = "Linea Blanca";
                        break;
                    case ETipoDePrducto.PequenoElectrodomestico:
                        TipoDeProductoAux = "Pequeno Electrodomestico";
                        break;
                    case ETipoDePrducto.Televisor:
                        TipoDeProductoAux = "Televisor";
                        break;
                    default:
                        TipoDeProductoAux = "Pequeno Electrodomestico";
                        break;
                }

                lista.SubItems.Add(TipoDeProductoAux);
                lista.SubItems.Add(string.Format("{0:N2}", pedido.Peso));
                lista.SubItems.Add(string.Format("{0:N2}", pedido.Volumen));
                lista.SubItems.Add(pedido.Barrio);

                string TipoDeEntrega;

                switch (pedido.TipoDeEntrega)
                {
                    case ETipoDeEntrega.Express:
                        TipoDeEntrega = "Express";
                        break;
                    case ETipoDeEntrega.Semiexpress:
                        TipoDeEntrega = "Semiexpress";
                        break;
                    case ETipoDeEntrega.Normal:
                        TipoDeEntrega = "Normal";
                        break;
                    case ETipoDeEntrega.Diferido:
                        TipoDeEntrega = "Diferido";
                        break;
                    default:
                        TipoDeEntrega = "Normal";
                        break;
                }

                lista.SubItems.Add(pedido._prioridad);
                listView1.Items.Add(lista);
                id++;
            }

            foreach(ClassGrafoNodo Nodo in recorrido._listaNodosRecorrido)//Imprimo en la ListView los recorridos
            {
                ListViewItem lista = new ListViewItem(Nodo.NombreNodo);
                listView2.Items.Add(lista);
            }
        }
    }
}
