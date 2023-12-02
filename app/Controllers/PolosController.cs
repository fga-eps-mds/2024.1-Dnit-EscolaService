using api;
using api.Polos;
using app.Entidades;
using app.Services;
using app.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PolosController : AppController
{
    private readonly IPoloService _poloService;
    private readonly AuthService authService;

    public PolosController(
        IPoloService poloService,
        AuthService authService)
    {
        this._poloService = poloService;
        this.authService = authService;
    }

    [HttpGet("{id}")]
    public async Task<Polo> Obter(int id)
    {
        authService.Require(Usuario, Permissao.EscolaVisualizar);
        return await _poloService.ObterPorIdAsync(id);
    }

    [HttpGet("paginado")]
    public async Task<ListaPoloPaginada<PoloModel>> ObterPolosAsync([FromQuery] PesquisaPoloFiltro filtro)
    {
        return await _poloService.ListarPaginadaAsync(filtro);
    }
    
    [HttpPost]
    public async Task CriarPolo(CadastroPoloDTO poloDto)
    {
        await _poloService.CadastrarAsync(poloDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarPolo(int id, CadastroPoloDTO poloDto)
    {
        authService.Require(Usuario, Permissao.EscolaEditar);

        var poloExistente = await _poloService.ObterPorIdAsync(id);

        if (poloExistente == null)
        {
            return NotFound();
        }

        await _poloService.AtualizarAsync(poloExistente, poloDto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirPolo(int id)
    {
        authService.Require(Usuario, Permissao.EscolaExcluir);

        var poloExistente = await _poloService.ObterPorIdAsync(id);

        if (poloExistente == null)
        {
            return NotFound();
        }

        await _poloService.ExcluirAsync(poloExistente);

        return NoContent();
    }


    
}
