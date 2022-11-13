using csvfiles;
using System.Net;

namespace tp_final;

public partial class Form1 : Form
{

    //Listas globales

    public List<ClassPedido> Pedidos;

    public List<ClassVehiculo> ListaVehiculos;

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

        var csv_ = new csvfiles._csv();
        Pedidos = csv_.read_csv();

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

        // Separo en dos sublistas de acuerdo a la necesidad del elevador(Primero Sólo Pedidos Express)

        //Algoritmo.SepararListas(Pedidos, PedidosConElevador, PedidosSinElevador, ETipoDeEntrega.Express);

        //Algoritmo.ProcesoDeLlenado(PedidosConElevador, ListaVehiculos[0]);

        //if (PedidosConElevador.Count != 0)
        //{
        //    Algoritmo.ProcesoDeLlenado(PedidosConElevador, ListaVehiculos[1]);
        //}

        //for (int i = 2; i < ListaVehiculos.Count; i++)
        //{
        //    if (PedidosSinElevador.Count != 0)
        //    {
        //        Algoritmo.ProcesoDeLlenado(PedidosSinElevador, ListaVehiculos[i]);
        //    }
        //}

        //int PosVehiculo = 0;

        //while (PedidosConElevador.Count != 0 || PedidosSinElevador.Count != 0)
        //{
        //    if (PedidosConElevador.Count != 0 && (PosVehiculo == 0 || PosVehiculo == 1))
        //    {
        //        Algoritmo.ProcesoDeLlenado(PedidosConElevador, ListaVehiculos[PosVehiculo]);
        //    }

        //    if (PedidosSinElevador.Count != 0)
        //    {
        //        Algoritmo.ProcesoDeLlenado(PedidosSinElevador, ListaVehiculos[PosVehiculo]);
        //    }

        //    PosVehiculo++;
        //}

        int PosTipoDeEntra = 0;

        int PosVehiculoMaximo = 0;

        while (PosTipoDeEntra != 4)
        {
            int PosVehiculo = 0;

            Algoritmo.SepararListas(Pedidos, PedidosConElevador, PedidosSinElevador, (ETipoDeEntrega)PosTipoDeEntra);

            if(ETipoDeEntrega.Express == (ETipoDeEntrega)PosTipoDeEntra)
            {
                while ((PedidosConElevador.Count != 0 || PedidosSinElevador.Count != 0 ) && PosVehiculo != 6)
                {
                    if (PedidosConElevador.Count != 0 && (PosVehiculo == 0 || PosVehiculo == 1))
                    {
                        Algoritmo.ProcesoDeLlenado(PedidosConElevador, ListaVehiculos[PosVehiculo]);
                    }

                    if (PedidosSinElevador.Count != 0)
                    {
                        Algoritmo.ProcesoDeLlenado(PedidosSinElevador, ListaVehiculos[PosVehiculo]);
                    }

                    PosVehiculo++;
                    PosVehiculoMaximo = PosVehiculo;
                }
            }
            else
            {
                for(int i = 0; i < PosVehiculoMaximo; i++)
                {
                    if (PedidosConElevador.Count != 0 && (i == 0 || i == 1))
                    {
                        Algoritmo.ProcesoDeLlenado(PedidosConElevador, ListaVehiculos[i]);
                    }

                    if (PedidosSinElevador.Count != 0)
                    {
                        Algoritmo.ProcesoDeLlenado(PedidosSinElevador, ListaVehiculos[i]);
                    }
                }
            }
            PosTipoDeEntra++;
        }

        bool nodoencontrado = false;

        for (int i = 0; i < PosVehiculoMaximo; i++)
        {
            List<ClassGrafoNodo> listanodosaux = new List<ClassGrafoNodo>();

            for (int j = 0; j < ListaVehiculos[i].listapedidos.Count; j++)
            {
                nodoencontrado = false;

                if(j == 0)
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
                            listanodosaux = grafo.Camino(ListaVehiculos[i].listapedidos[j].Barrio, ListaVehiculos[i].listapedidos[j + 1].Barrio, listanodosaux, NodosVisitadosRecorrido);
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
                    if (!nodoencontrado)
                    {
                        NodosVisitadosRecorrido = new List<ClassGrafoNodo>();
                        listanodosaux = grafo.Camino(ListaVehiculos[i].listapedidos[j].Barrio, ListaVehiculos[i].listapedidos[j + 1].Barrio, listanodosaux, NodosVisitadosRecorrido);
                    }  
                } 
            }

            listarecorridos.Add(new ClassRecorrido("a", ListaVehiculos[i]._vehiculo,1, listanodosaux));
        }

        foreach (ClassRecorrido recorrido in listarecorridos)
        {
            ListViewItem lista = new ListViewItem(recorrido._key);
            lista.SubItems.Add(recorrido._dia.ToString());
            lista.SubItems.Add(recorrido._vehiculo);
            listView1.Items.Add(lista);
        }

        //

        for(int i = 0; i < listarecorridos[0]._listaNodosRecorrido.Count; i++)
        {
            ListaVehiculos[0].QuitarPedido(listarecorridos[0]._listaNodosRecorrido[i]);
        }


        // Remuevo de la lista de pedidos los pedidos ya cargados en los vehiculos

        for (int i = 0; i < ListaVehiculos.Count; i++)
        {
            for (int j = 0; j < ListaVehiculos[i].listapedidos.Count; j++)
            {
                Pedidos.Remove(ListaVehiculos[i].listapedidos[j]);
            }
        }

        // Ajusto la ptiroidad de los pedidos restantes para los proximos dias de entrega

        for (int i = 0; i < Pedidos.Count; i++)
        {
            Pedidos[i].TipoDeEntrega = (ETipoDeEntrega)(Pedidos[i].TipoDeEntrega - 1);
        }

        Console.WriteLine();
    }
}
