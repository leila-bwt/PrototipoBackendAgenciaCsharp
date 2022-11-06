using Microsoft.AspNetCore.Mvc;
using BackendAgenciaCsharp.Model;
using BackendAgenciaCsharp.Repository;


namespace BackendAgenciaCsharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _repository;
        public UsuarioController (IUsuarioRepository repository){
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var usuarios = await _repository.GetUsuarios();
            return usuarios.Any() ? Ok(usuarios) : NoContent();
        }

          [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id){
            var usuario = await _repository.GetUsuarioById(id);
            return usuario != null
            ? Ok(usuario) : NotFound("Usuário não encontrado.");
        }

            [HttpPost]
        public async Task<IActionResult> Post(Usuario usuario){
            _repository.AddUsuario(usuario);
            return await _repository.SaveChangesAsync()?
            Ok("Usuario adicionado") : BadRequest("Algo deu errado");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Usuario usuario){
            var usuarioExiste = await _repository.GetUsuarioById(id);

            if(usuarioExiste == null) return NotFound("Usuário não encontrado");

            usuarioExiste.nome = usuario.nome ?? usuarioExiste.nome;
            usuarioExiste.sobrenome = usuario.sobrenome ?? usuarioExiste.sobrenome;
            usuarioExiste.endereço = usuario.endereço ?? usuarioExiste.endereço;
            usuarioExiste.cidade = usuario.cidade ?? usuarioExiste.cidade;
            usuarioExiste.estado = usuario.estado ?? usuarioExiste.estado;
            usuarioExiste.cep = usuario.cep ?? usuarioExiste.cep;
            usuarioExiste.email = usuario.email ?? usuarioExiste.email;
            usuarioExiste.senha = usuario.senha ?? usuarioExiste.senha;

            _repository.AtualizarUsuario(usuarioExiste);

            return await _repository.SaveChangesAsync()
            ? Ok("Usuário atualizado") : BadRequest ("Algo deu errado.");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var usuarioExiste = await _repository.GetUsuarioById(id);
            if(usuarioExiste == null)
            return NotFound("Usuário não encontrado");

            _repository.DeletarUsuario(usuarioExiste);

            return await _repository.SaveChangesAsync()
            ? Ok("Usuário atualizado.") : BadRequest("Algo deu errado.");
        }
    }
    }