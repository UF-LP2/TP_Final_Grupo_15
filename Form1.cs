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

        Console.WriteLine();
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {

        // Separo en dos sublistas de acuerdo a la necesidad del elevador(Primero Sólo Pedidos Express)

        Algoritmo.SepararListas(Pedidos, PedidosConElevador, PedidosSinElevador, ETipoDeEntrega.Express);

        // Genero arrays

        int[] Volumenes = new int[PedidosConElevador.Count + 1];
        int[] Pesos = new int[PedidosConElevador.Count + 1];

        Volumenes[0] = 0;
        Pesos[0] = 0;

        for (int i = 1; i <PedidosConElevador.Count + 1; i++)
        {
            Volumenes[i] = (int)PedidosConElevador[i-1].Volumen;
            Pesos[i] = (int)PedidosConElevador[i-1].Peso;
        }

        // Primero tengo en cuenta el peso maximo

        List<int>IndicePedidos = Algoritmo.CargaMochila(Volumenes, Pesos, ListaVehiculos[1].PesoMaximo, PedidosConElevador.Count + 1);

        //Genero lista auxilair de pedidos que cumplen el primer requisito

        List<ClassPedido> PedidosAux = new List<ClassPedido>();

        for (int i = 0; i < IndicePedidos.Count; i++)
        {
            PedidosAux.Add(PedidosConElevador[IndicePedidos[i] - 1]);
        }

        // Genero arrays

        Volumenes = new int[PedidosAux.Count + 1];
        Pesos = new int[PedidosAux.Count + 1];

        Volumenes[0] = 0;
        Pesos[0] = 0;

        for (int i = 1; i < PedidosAux.Count + 1; i++)
        {
            Volumenes[i] = (int)PedidosAux[i - 1].Volumen;
            Pesos[i] = (int)PedidosAux[i - 1].Peso;
        }

        // Seguno tengo en cuenta el volumen maximo

        IndicePedidos = Algoritmo.CargaMochila(Pesos, Volumenes, (int)ListaVehiculos[1].VolumenMaximo, PedidosAux.Count + 1);

        // Cargo esos pedidos en el vehiculo que cumplen con los 2 requisitos

        for (int i = 0; i < IndicePedidos.Count; i++)
        {
            ListaVehiculos[1].listapedidos.Add(PedidosAux[IndicePedidos[i] - 1]);
            PedidosConElevador.Remove(PedidosAux[IndicePedidos[i] - 1]);
        }

        //Todo: pasar todo a un metodo para poder ejecutarse en todos los vehiculos y todas las prioridades
        //Todo: pasar los valores del csv a enteros para eviar problemas

        Console.WriteLine();
    }
}
