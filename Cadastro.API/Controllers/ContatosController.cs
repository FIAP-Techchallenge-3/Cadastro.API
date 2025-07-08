using Cadastro.API.DTO;
using Cadastro.API.Mensageria;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContatosController : ControllerBase
{
	private readonly PublicadorRabbit _publicador;

	public ContatosController(PublicadorRabbit publicador)
	{
		_publicador = publicador;
	}

	[HttpPost]
	public IActionResult Cadastre(Contato contato)
	{
		contato.Id = Guid.NewGuid();
		Task task = _publicador.Publique(contato);
		return Accepted();
	}
}
