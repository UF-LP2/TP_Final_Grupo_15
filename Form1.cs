using csvfiles;
using System.Net;
using System.Windows.Forms;

namespace tp_final;

public partial class Form1 : Form
{

    //Listas globales

    public List<ClassPedido> Pedidos;

    public List<ClassVehiculo> ListaVehiculos;

    public int dia;

    public int key;

    public int contador; // contador de pedidos atrasados

    //Listas globales

    //Variables para la carga de los camiones

    public ClassMochila Algoritmo;

    public List<ClassPedido> PedidosConElevador;

    public List<ClassPedido> PedidosSinElevador;

    //Variables para la carga de los camiones

    //Variables para el calculo del camiono entre nodos

    public ClassGrafo grafo;

    public List<ClassGrafoNodo> NodosVisitadosRecorrido;

    public List<ClassGrafoNodo> ListaDeNodosRecorrido;

    //Variables para el calculo del camiono entre nodos

    //Variables para ListView

    public List<ClassRecorrido> listarecorridos;

    //Variables para ListView

    public Form1()
    {
        InitializeComponent();

        //lista global de pedidos
        contador = 0;
        var csv_ = new csvfiles._csv();
        Pedidos = csv_.read_csv();

        dia = 0;

        key = 0;

        //lista global de pedidos

        //Variables para la carga de los camiones

        Algoritmo = new ClassMochila();

        PedidosConElevador = new List<ClassPedido>();

        PedidosSinElevador = new List<ClassPedido>();

        //Variables para la carga de los camiones

        //Inicializacion de vehiculos

        ListaVehiculos = new List<ClassVehiculo>();

        ListaVehiculos.Add(new ClassVehiculo("furgon"));
        ListaVehiculos.Add(new ClassVehiculo("furgoneta"));
        ListaVehiculos.Add(new ClassVehiculo("camioneta"));
        ListaVehiculos.Add(new ClassVehiculo("camioneta"));
        ListaVehiculos.Add(new ClassVehiculo("camioneta"));
        ListaVehiculos.Add(new ClassVehiculo("camioneta"));

        //Inicializacion de vehiculos

        //Grafo con los barrios

        grafo = new ClassGrafo();
        grafo.NodoList= csv_.read_csv_NodosGrafo();
        csv_.read_csv_NodosUniones(grafo);

        //Odeno las uniones de menor a mayor

        ClassGrafoUnion nodoAux;

        for (int i = 0; i < grafo.NodoList.Count; i++)
        {
            for (int j = 1; j < grafo.NodoList[i].listaunion.Count; j++)
            {
                for (int h = 0; h < grafo.NodoList[i].listaunion.Count - 1; h++)
                {
                    if (grafo.NodoList[i].listaunion[h].peso > grafo.NodoList[i].listaunion[h + 1].peso)
                    {
                        nodoAux = grafo.NodoList[i].listaunion[h];
                        grafo.NodoList[i].listaunion[h] = grafo.NodoList[i].listaunion[h + 1];
                        grafo.NodoList[i].listaunion[h + 1] = nodoAux;
                    }
                }
            }
        }

        //Grafo con los barrios

        ListaDeNodosRecorrido = new List<ClassGrafoNodo>();

        NodosVisitadosRecorrido = new List<ClassGrafoNodo>();

        //grafo.Camino("comuna 1", "la matanza", ListaDeNodosRecorrido, NodosVisitadosRecorrido); //test metodo camino

        //Variables para ListView

        listarecorridos = new List<ClassRecorrido>();

        Console.WriteLine();
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {

        if(Pedidos.Count != 0)
        {
            int PosTipoDeEntra = 0;

            int PosVehiculoMaximo = 0;

            while (PosTipoDeEntra != 4)
            {
                int PosVehiculo = 0;

                PedidosConElevador = new List<ClassPedido>();

                PedidosSinElevador = new List<ClassPedido>();

                Algoritmo.SepararListas(Pedidos, PedidosConElevador, PedidosSinElevador, (ETipoDeEntrega)PosTipoDeEntra);

                if (ETipoDeEntrega.Express == (ETipoDeEntrega)PosTipoDeEntra)
                {
                    while ((PedidosConElevador.Count != 0 || PedidosSinElevador.Count != 0) && PosVehiculo != 6)
                    {
                        if (PedidosConElevador.Count != 0 && (PosVehiculo == 0 || PosVehiculo == 1))
                        {
                            Algoritmo.ProcesoDeLlenado(Pedidos, PedidosConElevador, ListaVehiculos[PosVehiculo]);
                        }

                        if (PedidosSinElevador.Count != 0)
                        {
                            Algoritmo.ProcesoDeLlenado(Pedidos, PedidosSinElevador, ListaVehiculos[PosVehiculo]);
                        }

                        PosVehiculo++;
                        PosVehiculoMaximo = PosVehiculo;
                    }
                }
                else
                {
                    if (PosVehiculoMaximo == 0)
                    {
                        PosVehiculoMaximo = 1;
                    }

                    for (int i = 0; i < PosVehiculoMaximo; i++)
                    {
                        if (PedidosConElevador.Count != 0 && (i == 0 || i == 1))
                        {
                            Algoritmo.ProcesoDeLlenado(Pedidos, PedidosConElevador, ListaVehiculos[i]);
                        }

                        if (PedidosSinElevador.Count != 0)
                        {
                            Algoritmo.ProcesoDeLlenado(Pedidos, PedidosSinElevador, ListaVehiculos[i]);
                        }
                    }
                }
                PosTipoDeEntra++;
            }

            bool nodoencontrado = false;

            bool nodoencontrado2 = false;

            List<ClassGrafoNodo> listaNodosAuxilar;

            ClassGrafoNodo nodoauxiliar;

            for (int i = 0; i < PosVehiculoMaximo; i++)
            {
                List<ClassGrafoNodo> listanodosaux = new List<ClassGrafoNodo>();

                for (int j = 0; j < ListaVehiculos[i].listapedidos.Count; j++)
                {
                    nodoencontrado = false;

                    if (j == 0)
                    {
                        ListaDeNodosRecorrido = new List<ClassGrafoNodo>();

                        NodosVisitadosRecorrido = new List<ClassGrafoNodo>();

                        listanodosaux = grafo.Camino(grafo.NodoList[8].NombreNodo, ListaVehiculos[i].listapedidos[j].Barrio, ListaDeNodosRecorrido, NodosVisitadosRecorrido);

                        if (ListaVehiculos[i].listapedidos.Count > 1)
                        {
                            foreach (ClassGrafoNodo nodo in listanodosaux)
                            {
                                if (grafo.BuscarNodo(ListaVehiculos[i].listapedidos[j + 1].Barrio) == nodo)
                                {
                                    nodoencontrado = true;
                                }
                            }

                            if (!nodoencontrado)
                            {
                                NodosVisitadosRecorrido = new List<ClassGrafoNodo>();

                                nodoauxiliar = listanodosaux[listanodosaux.Count - 1];

                                listanodosaux.Remove(nodoauxiliar);

                                listanodosaux = grafo.Camino(nodoauxiliar.NombreNodo, ListaVehiculos[i].listapedidos[j + 1].Barrio, listanodosaux, NodosVisitadosRecorrido);
                            }
                        }
                    }

                    else if (j == ListaVehiculos[i].listapedidos.Count - 1)
                    {
                        break;
                    }

                    else
                    {
                        foreach (ClassGrafoNodo nodo in listanodosaux)
                        {

                            if (grafo.BuscarNodo(ListaVehiculos[i].listapedidos[j + 1].Barrio) == nodo)
                            {
                                nodoencontrado = true;
                            }
                        }
                        if (nodoencontrado == false)
                        {
                            NodosVisitadosRecorrido = new List<ClassGrafoNodo>();

                            nodoauxiliar = listanodosaux[listanodosaux.Count - 1];

                            listanodosaux.Remove(nodoauxiliar);

                            listanodosaux = grafo.Camino(nodoauxiliar.NombreNodo, ListaVehiculos[i].listapedidos[j + 1].Barrio, listanodosaux, NodosVisitadosRecorrido);
                        }
                    }
                }

                listarecorridos.Add(new ClassRecorrido(key.ToString(), ListaVehiculos[i], dia, listanodosaux));
                key++;
            }

            // Imprimo en la ListView los recorridos

            listView1.Items.Clear();

            foreach (ClassRecorrido recorrido in listarecorridos)
            {
                ListViewItem lista = new ListViewItem(recorrido._key);
                lista.SubItems.Add(recorrido._dia.ToString());
                lista.SubItems.Add(recorrido._vehiculo);
                lista.SubItems.Add(string.Format("{0:N2}", recorrido.KmRecorrido));
                lista.SubItems.Add(string.Format("{0:N2}", recorrido.PesoMaximo));
                lista.SubItems.Add(string.Format("{0:N2}", recorrido.PesoMaximo - recorrido.PesoDisponible));
                lista.SubItems.Add(string.Format("{0:N2}", recorrido.VolumenMaximo));
                lista.SubItems.Add(string.Format("{0:N2}", recorrido.VolumenMaximo - recorrido.VolumenDisponible));
                lista.SubItems.Add(string.Format("{0:N2}", recorrido.ObjetoVehiculo.GastoDeGasolina(recorrido.KmRecorrido)));
                listView1.Items.Add(lista);
            }

            // Remuevo de la lista de pedidos los pedidos ya cargados en los vehiculos

            for (int i = 0; i < ListaVehiculos.Count; i++)
            {
                for (int j = 0; j < ListaVehiculos[i].listapedidos.Count; j++)
                {
                    Pedidos.Remove(ListaVehiculos[i].listapedidos[j]);
                }
            }

            //Reparto los pedidos

            for (int i = 0; i < PosVehiculoMaximo; i++)
            {
                for (int j = 0; j < listarecorridos[i]._listaNodosRecorrido.Count; j++)
                {
                    ListaVehiculos[i].QuitarPedido(listarecorridos[i]._listaNodosRecorrido[j]);
                }
                ListaVehiculos[i].VolumenDisponible = ListaVehiculos[i].VolumenMaximo;
                ListaVehiculos[i].PesoDisponible = ListaVehiculos[i].PesoMaximo;
            }

            // Ajusto la ptiroidad de los pedidos restantes para los proximos dias de entrega

            for (int i = 0; i < Pedidos.Count; i++)
            {
                if(Pedidos[i].TipoDeEntrega != ETipoDeEntrega.Express)
                {
                    Pedidos[i].TipoDeEntrega = (ETipoDeEntrega)(Pedidos[i].TipoDeEntrega - 1);
                }
                else
                {
                    contador++;
                }
            }
        }
        dia++;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        FormDetallesPedidos detalles = null;
        foreach (ListViewItem lista in listView1.SelectedItems)
        {
            detalles = new FormDetallesPedidos(listarecorridos[Int32.Parse(lista.Text)]);
        }
        if (detalles != null)
        {
            detalles.Show();
        }
    }
}
