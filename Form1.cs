using csvfiles;
using System.Net;

namespace tp_final;

public partial class Form1 : Form
{

    private ClassGrafo grafo;

    private List<ClassPedido> Pedidos;

    private ClassDataGrafoTest dataGrafo;

    public List<ClassGrafoNodo> recorrido;

    public List<ClassGrafoNodo> NodosVisitados;

    public Form1()
    {
        InitializeComponent();

        var csv_ = new csvfiles._csv();
        Pedidos = csv_.read_csv();

        grafo= new ClassGrafo();

        //grafo.NodoList= csv_.read_csv_NodosGrafo();
        //csv_.read_csv_NodosUniones(grafo);

        //test carga data grafo cambiar por csv

        dataGrafo= new ClassDataGrafoTest();

        foreach(string NombreNodo in dataGrafo.listaNodos)
        {
            grafo.AgregarNodo(NombreNodo);
        }


        for(int i = 0; i < dataGrafo.listaNodosPartida.Count - 1; i++)
        {
            grafo.AgregarUnion(dataGrafo.listaNodosPartida[i], dataGrafo.listaNodosDestino[i], dataGrafo.listaNodosPeso[i]);
        }

        //test carga data grafo cambiar por csv

        recorrido = new List<ClassGrafoNodo>();

        NodosVisitados = new List<ClassGrafoNodo>();

        recorrido = grafo.Camino("comuna 1", "comuna 2", recorrido, NodosVisitados); //recorrido lista de nodos entre partida y destino

        Console.WriteLine();

    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }
}
