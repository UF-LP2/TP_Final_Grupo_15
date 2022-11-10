using csvfiles;
using System.Net;

namespace tp_final;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        var csv_ = new csvfiles._csv();
        List<ClassPedido> Pedidos = csv_.read_csv();
        ClassGrafo grafo= new ClassGrafo();
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }
}
