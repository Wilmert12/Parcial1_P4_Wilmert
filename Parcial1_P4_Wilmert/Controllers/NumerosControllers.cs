using Microsoft.AspNetCore.Mvc;
using Parcial1_P4_Wilmert.Models;
using Parcial1_P4_Wilmert.Services;

namespace Parcial1_P4_Wilmert.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NumerosController : ControllerBase
{
    private readonly NumbersService _numbersService;

    public NumerosController(NumbersService numbersService)
    {
        _numbersService = numbersService;
    }

   
    [HttpGet("{numero:int}")]
    [ProducesResponseType(typeof(NumberRecord), StatusCodes.Status200OK)]
    public async Task<IActionResult> SumarConsigoMismo(int numero)
    {
        
        var record = new NumberRecord(0, DateTime.Now, numero, (long)numero + numero);

        var guardado = await _numbersService.SaveAsync(record);
        return Ok(guardado);
    }

    [HttpPut("{id:int}/{numero:int}")]
    public async Task<IActionResult> Actualizar(int id, int numero)
    {
        var existente = await _numbersService.GetByIdAsync(id);
        if (existente is null)
            return NotFound($"No existe el registro con Id {id}");

       
        var actualizado = existente with
        {
            Fecha = DateTime.Now,
            Numero = numero,
            Resultado = (long)numero + numero
        };

        await _numbersService.UpdateAsync(actualizado);
        return Ok(actualizado);
    }

    [HttpGet("historial/{id:int}")]
    [ProducesResponseType(typeof(NumberRecord), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var record = await _numbersService.GetByIdAsync(id);
        return record is null ? NotFound() : Ok(record);
    }

    
    [HttpGet("historial")]
    [ProducesResponseType(typeof(List<NumberRecord>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList()
    {
        var lista = await _numbersService.GetListAsync();
        return Ok(lista);
    }
}