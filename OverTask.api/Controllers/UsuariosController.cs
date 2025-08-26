using Microsoft.AspNetCore.Mvc;
using OverTask.api.Repositories.Interfaces;
using OverTask.Shared.Models.Dtos.Usuarios;

namespace OverTask.api.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuariosRepository _usuariosRepository;

    public UsuariosController(IUsuariosRepository usuariosRepository)
    {
        _usuariosRepository = usuariosRepository;
    }

    [HttpGet]
    public ActionResult<List<UsuarioReadDto>> GetUsuario()
    {
        var usuarios = _usuariosRepository.GetUsuario();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public ActionResult<UsuarioReadDto> GetUsuario(int id)
    {
        var usuario = _usuariosRepository.GetUsuario(id);

        if (usuario == null)
        {
            return NotFound();
        }

        return Ok(usuario);
    }

    [HttpPost]
    public ActionResult<UsuarioReadDto> PostUsuario(UsuarioCreateDto usuarioDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
    
        try
        {
            var usuarioCriado = _usuariosRepository.PostUsuario(usuarioDto);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioCriado.Id }, usuarioCriado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult PutUsuario(int id, UsuarioUpdateDto usuarioDto)
    {
        try
        {
            var result = _usuariosRepository.PutUsuario(id, usuarioDto);
            
            if (result == null)
                return NotFound("Usuário não encontrado.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteUsuario(int id)
    {
        try
        {
            var result = _usuariosRepository.DeletarUsuario(id);
            
            if (!result)
                return NotFound("Usuário não encontrado.");

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }
}