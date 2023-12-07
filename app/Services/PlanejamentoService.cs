using System.Numerics;
using api.Planejamento;
using app.Entidades;
using app.Repositorios.Interfaces;
using service.Interfaces;

namespace app.Services
{
    public class PlanejamentoService : IPlanejamentoService
    {
        private readonly IPlanejamentoRepositorio planejamentoRepositorio;
        private readonly ModelConverter modelConverter;
        private readonly AppDbContext dbContext;

        public PlanejamentoService
        (
            IPlanejamentoRepositorio planejamentoRepositorio,
            ModelConverter modelConverter,
            AppDbContext dbContext
        )
        {
            this.planejamentoRepositorio = planejamentoRepositorio;
            this.modelConverter = modelConverter;
            this.dbContext = dbContext;
        }

        // Implementar os metodos da inteface
        public async Task<PlanejamentoMacroDetalhadoModel> ObterPlanejamentoMacroDetalhado(Guid id)
        {
            return await planejamentoRepositorio.ObterPlanejamentoMacroDetalhado(id);
        }

        public async Task ExcluirPlanejamentoMacro(Guid id)
        {
            var planejamento = await planejamentoRepositorio.ObterPlanejamentoMacroDetalhado(id);
            dbContext.Remove(planejamento);
            await dbContext.SaveChangesAsync();
        }
        
        /* [1]
        rascunho de função-custo:
        
            X -> vetor q-dimensional
            custo = 0;
            for(int i = 0, i<q, i++)
            {
                custo += -V[X[i]].Ups + V[X[i]].custologistico
            }

        */

        public Task<PlanejamentoMacro> GerarRecomendacaoDePlanejamento(PlanejamentoMacroDTO planejamento)
        {
            var q = planejamento.QuantidadeAcoes;
            var n = q + q * 0.35;

            //pegar um vetor V com as n primeiras escolas do ranking
            //normalizar V num objeto com {Id, Ups, CustoLogistico}
            //implemetar a função-custo como: [1]
            //rodar uma otimização por enxame de partículas ou um algoritmo genético
            //usar q dimensões num S = [0, n]
            //captar o vetor R com o resultado da otimização
            //R é composto pelos indices das escolhas escolhidas em V
            //dividir q igualmente pela quantidade de meses
            //tentar agrupar por UF as escolas selecionadas no mesmo Mês
            //Montar um objeto PlanejamentoMacro com o resultado

            throw new NotImplementedException();
        }

        public PlanejamentoMacro CriarPlanejamentoMacro(PlanejamentoMacroDetalhadoDTO planejamento)
        {
            
            throw new NotImplementedException();
        }
    }
}