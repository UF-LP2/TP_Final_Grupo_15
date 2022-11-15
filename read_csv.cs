using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Collections.Generic;
using tp_final.Properties;
using tp_final;
using Microsoft.VisualBasic.ApplicationServices;

namespace csvfiles
{
    public class _csv 
    {

        public List<ClassPedido> read_csv()
        {
            //string path = "..\\..\\..\\data.csv";

            using (var reader = new StreamReader(Resources.archivo))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            { 
                List<ClassPedido> records = new List<ClassPedido>();

                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    ClassPedido record = new ClassPedido
                    (
                        csv.GetField<string>("producto"),
                        csv.GetField<float>("precio"),
                        csv.GetField<float>("largo"),
                        csv.GetField<float>("ancho"),
                        csv.GetField<float>("alto"),
                        csv.GetField<float>("largo"),
                        csv.GetField<string>("prioridad"),
                        csv.GetField<string>("barrio")
                    );
                    records.Add(record);
                }
                return records;
            }
        }

        public List<ClassGrafoNodo> read_csv_NodosGrafo()
        {
            string path = "..\\..\\..\\Barrios_BA.csv";

            using (var reader = new StreamReader(path))// Barrios_BA.csv
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                List<ClassGrafoNodo> records = new List<ClassGrafoNodo>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    ClassGrafoNodo record = new ClassGrafoNodo(csv.GetField<string>("Barrios"));
                    records.Add(record);
                }
                return records;
            }
        }

        public void read_csv_NodosUniones(ClassGrafo grafo)
        {
            string path = "..\\..\\..\\UnionesNodos.csv";

            using (var reader = new StreamReader(path)) // UnionesNodos.csv
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    grafo.AgregarUnion(
                        csv.GetField<string>("partida"),
                        csv.GetField<string>("destino"),
                        csv.GetField<float>("peso")
                        );
                }
                return;
            }
        }
    }
};
