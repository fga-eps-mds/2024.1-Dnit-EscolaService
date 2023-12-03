using api.CustoLogistico;
using api.Escolas;
using app.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Stubs
{
    public static class CustoLogisticoStub
    {
        public static List<CustoLogistico> ObterCustoLogisticos()
        {
            return new List<CustoLogistico> {

                    new CustoLogistico
                    {
                        Custo = 1,
                        RaioMin = 0,
                        RaioMax = 200,
                        Valor = 20,
                    },
                    new CustoLogistico
                    {
                        Custo = 2,
                        RaioMin = 200,
                        RaioMax = 500,
                        Valor = 40,
                    },
                    new CustoLogistico
                    {
                        Custo = 3,
                        RaioMin = 500,
                        RaioMax = 1000,
                        Valor = 60,
                    },
                    new CustoLogistico
                    {
                        Custo = 4,
                        RaioMin = 1000,
                        Valor = 100,
                    }

            };
        }

        public static List<CustoLogisticoItem> CustoLogisticoAtualizado()
        {
            return new List<CustoLogisticoItem> {

                    new CustoLogisticoItem
                    {
                        Custo = 1,
                        RaioMin = 0,
                        RaioMax = 400,
                        Valor = 10,
                    },
                    new CustoLogisticoItem
                    {
                        Custo = 2,
                        RaioMin = 400,
                        RaioMax = 800,
                        Valor = 30,
                    },
                    new CustoLogisticoItem
                    {
                        Custo = 3,
                        RaioMin = 800,
                        RaioMax = 1200,
                        Valor = 40,
                    },
                    new CustoLogisticoItem
                    {
                        Custo = 4,
                        RaioMin = 1200,
                        Valor = 50,
                    }

            };
        }
    }
}
