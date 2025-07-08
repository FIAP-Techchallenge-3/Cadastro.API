using Cadastro.API.DTO;
using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Cadastro.API.Mensageria;

public class PublicadorRabbit
{
	private readonly IConfiguration _configuracao;
	private readonly string _fila;
	private readonly string _host;

	public PublicadorRabbit(IConfiguration configuracao)
	{
		_configuracao = configuracao;
		_host = _configuracao["RabbitMQ:Host"];
		_fila = _configuracao["RabbitMQ:Fila"];
	}

	public async Task Publique(Contato contato)
	{
		var factory = new ConnectionFactory()
		{
			HostName = "localhost",
			UserName = "guest",
			Password = "guest"
		};
		using var conexao = await factory.CreateConnectionAsync();
		using var canal = await conexao.CreateChannelAsync();

		canal.QueueDeclareAsync(queue: _fila, durable: true, exclusive: false, autoDelete: false);

		var corpo = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(contato));

		canal.BasicPublishAsync(exchange: "", routingKey: _fila, body: corpo);
	}
}
