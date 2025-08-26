using OverTask.Shared.Models.Dtos.Usuarios;

namespace OverTask.api.Repositories.Interfaces;

public interface IUsuariosRepository
{
    public List<UsuarioReadDto> GetUsuario();
    public UsuarioReadDto GetUsuario(int id);
    public UsuarioReadDto PostUsuario(UsuarioCreateDto usuario);    
    public UsuarioUpdateDto PutUsuario(int id, UsuarioUpdateDto usuarioDto);
    public bool DeletarUsuario(int id);
}