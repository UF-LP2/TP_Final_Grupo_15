using csvfiles;
using System.Net;
using System.Windows.Forms;

namespace tp_final;

public partial class Form1 : Form
{
    #region Variables

    //Listas globales
    public List<ClassPedido> Pedidos;
    public List<ClassVehiculo> ListaVehiculos;

    //Variables de impresion 
    public int dia;
    public int key;

    public int contador;//contador de pedidos atrasados de prueba

    //Variables para la carga de los camiones
    public ClassMochila Algoritmo;
    public List<ClassPedido> PedidosConElevador;
    public List<ClassPedido> PedidosSinElevador;

    //Variables para el calculo del camiono entre nodos
    public ClassGrafo grafo;
    public List<ClassGrafoNodo> NodosVisitadosRecorrido;
    public List<ClassGrafoNodo> ListaDeNodosRecorrido;

    //Variables para ListView
    public List<ClassRecorrido> listarecorridos;

    #endregion

    public Form1()
    {
        InitializeComponent();

        contador = 0;
        dia = 0;
        key = 0;
        Algoritmo = new ClassMochila();
        PedidosConElevador = new List<ClassPedido>();
        PedidosSinElevador = new List<ClassPedido>();
        ListaVehiculos = new List<ClassVehiculo>();
        grafo = new ClassGrafo();
        ClassGrafoUnion nodoAux;
        ListaDeNodosRecorrido = new List<ClassGrafoNodo>();
        NodosVisitadosRecorrido = new List<ClassGrafoNodo>();
        listarecorridos = new List<ClassRecorrido>();

        var csv_ = new csvfiles._csv();
        Pedidos = csv_.read_csv();

        //Inicializacion de vehiculos
        ListaVehiculos.Add(new ClassVehiculo("furgon"));
        ListaVehiculos.Add(new ClassVehiculo("furgoneta"));
        ListaVehiculos.Add(new ClassVehiculo("camioneta"));
        ListaVehiculos.Add(new ClassVehiculo("camioneta"));
        ListaVehiculos.Add(new ClassVehiculo("camioneta"));
        ListaVehiculos.Add(new ClassVehiculo("camioneta"));

        //Inicializacion los barrios del grafo
        grafo.NodoList= csv_.read_csv_NodosGrafo();
        csv_.read_csv_NodosUniones(grafo);

        for (int i = 0; i < grafo.NodoList.Count; i++) //Odeno las uniones de menor a mayor
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
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
        if(Pedidos.Count != 0)//Siempre que haya pedidos 
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
                        if (PedidosConElevador.Count != 0 && (PosVehiculo == 0 || PosVehiculo == 1))//Solo para Furgon y Furgoneta
                        {
                            Algoritmo.ProcesoDeLlenado(Pedidos, PedidosConElevador, ListaVehiculos[PosVehiculo]);//Se llenan los vehiculos 
                        }

                        if (PedidosSinElevador.Count != 0)
                        {
                            Algoritmo.ProcesoDeLlenado(Pedidos, PedidosSinElevador, ListaVehiculos[PosVehiculo]);//Se llenan los vehiculos
                        }

                        PosVehiculo++;
                        PosVehiculoMaximo = PosVehiculo;//Se guardan cuantos vehiculos fueron usados
                    }
                }
                else
                {
                    if (PosVehiculoMaximo == 0)//Si no se uso ningun vehiculo se fuerza el uso de uno en un dia
                    {
                        PosVehiculoMaximo = 1;
                    }

                    for (int i = 0; i < PosVehiculoMaximo; i++)
                    {
                        if (PedidosConElevador.Count != 0 && (i == 0 || i == 1))
                        {
                            Algoritmo.ProcesoDeLlenado(Pedidos, PedidosConElevador, ListaVehiculos[i]);//Se llenan los vehiculos
                        }

                        if (PedidosSinElevador.Count != 0)
                        {
                            Algoritmo.ProcesoDeLlenado(Pedidos, PedidosSinElevador, ListaVehiculos[i]);//Se llenan los vehiculos
                        }
                    }
                }
                PosTipoDeEntra++;
            }

            bool nodoencontrado = false;
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

                        listanodosaux = grafo.Camino(grafo.NodoList[8].NombreNodo, ListaVehiculos[i].listapedidos[j].Barrio, ListaDeNodosRecorrido, NodosVisitadosRecorrido);//Fuerzo el primer recorrido

                        if (ListaVehiculos[i].listapedidos.Count > 1)
                        {
                            foreach (ClassGrafoNodo nodo in listanodosaux)//Reviso que el siguiente barrio que tengo que buscar no haya pasado
                            {
                                if (grafo.BuscarNodo(ListaVehiculos[i].listapedidos[j + 1].Barrio) == nodo)
                                {
                                    nodoencontrado = true;
                                }
                            }

                            if (!nodoencontrado)//Si el nodo no fue encontrado
                            {
                                NodosVisitadosRecorrido = new List<ClassGrafoNodo>();
                                nodoauxiliar = listanodosaux[listanodosaux.Count - 1];//Guardamos el ultimo lugar del recorrido de la lista para pasarselo como primero al siguiente
                                listanodosaux.Remove(nodoauxiliar);//Remuevo el ultimo lugar del recorrido de la lista

                                listanodosaux = grafo.Camino(nodoauxiliar.NombreNodo, ListaVehiculos[i].listapedidos[j + 1].Barrio, listanodosaux, NodosVisitadosRecorrido);//Genero el recorrido en la lista hasta el nododestino por el cual no habiamos pasado
                            }
                        }
                    }
                    else if (j == ListaVehiculos[i].listapedidos.Count - 1)//Si es la ultima posicion termina el for
                    {
                        break;
                    }
                    else
                    {
                        foreach (ClassGrafoNodo nodo in listanodosaux)//Reviso que el siguiente barrio que tengo que buscar no haya pasado
                        {
                            if (grafo.BuscarNodo(ListaVehiculos[i].listapedidos[j + 1].Barrio) == nodo)
                            {
                                nodoencontrado = true;
                            }
                        }
                        if (nodoencontrado == false)//Si el nodo no fue encontrado
                        {
                            NodosVisitadosRecorrido = new List<ClassGrafoNodo>();
                            nodoauxiliar = listanodosaux[listanodosaux.Count - 1];//Guardamos el ultimo lugar del recorrido de la lista para pasarselo como primero al siguiente
                            listanodosaux.Remove(nodoauxiliar);//Remuevo el ultimo lugar del recorrido de la lista

                            listanodosaux = grafo.Camino(nodoauxiliar.NombreNodo, ListaVehiculos[i].listapedidos[j + 1].Barrio, listanodosaux, NodosVisitadosRecorrido);//Genero el recorrido en la lista hasta el nododestino por el cual no habiamos pasado
                        }
                    }
                }
                listarecorridos.Add(new ClassRecorrido(key.ToString(), ListaVehiculos[i], dia, listanodosaux));//Genero el recorrido para el vehiculo
                key++;
            }

            listView1.Items.Clear();
            foreach (ClassRecorrido recorrido in listarecorridos)//Imprimo en la ListView los recorridos
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

            for (int i = 0; i < ListaVehiculos.Count; i++) // Remuevo de la lista de pedidos los pedidos ya cargados en los vehiculos (vaciamos los vehiculos)
            {
                for (int j = 0; j < ListaVehiculos[i].listapedidos.Count; j++)
                {
                    Pedidos.Remove(ListaVehiculos[i].listapedidos[j]);
                }
            }

            for (int i = 0; i < PosVehiculoMaximo; i++)//Reparto los pedidos
            {
                for (int j = 0; j < listarecorridos[i]._listaNodosRecorrido.Count; j++)
                {
                    ListaVehiculos[i].QuitarPedido(listarecorridos[i]._listaNodosRecorrido[j]);
                }
                ListaVehiculos[i].VolumenDisponible = ListaVehiculos[i].VolumenMaximo;//Ajusto nuevamente el volumen
                ListaVehiculos[i].PesoDisponible = ListaVehiculos[i].PesoMaximo;//Ajusto nuevamente el peso
            }

            for (int i = 0; i < Pedidos.Count; i++)// Ajusto la priroidad de los pedidos restantes para los proximos dias de entrega
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

    private void button2_Click(object sender, EventArgs e)//Abrimos el otro form
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