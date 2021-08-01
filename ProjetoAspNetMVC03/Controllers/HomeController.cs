using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoAspNetMVC03.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Controllers
{
    [Authorize] //Permitir somente usuarios autenticados!
    public class HomeController : Controller
    {
        //atributos
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITarefaRepository _tarefaRepository;

        //construtor para que o AspNet possa inicializar os atributos (injeção de dependencia)
        public HomeController(IUsuarioRepository usuarioRepository, ITarefaRepository tarefaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _tarefaRepository = tarefaRepository;
        }

        //método para abrir a página /Home/Index
        public IActionResult Index()
        {
            try
            {
                //obter o primeiro dia do mes atual
                var primeiroDiaDoMesAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                //obter o ultimo dia do mes atual
                var ultimoDiaDoMesAtual = primeiroDiaDoMesAtual.AddMonths(1).AddDays(-1);

                TempData["PrimeiroDiaDoMesAtual"] = primeiroDiaDoMesAtual.ToString("dd/MM/yyyy");
                TempData["UltimoDiaDoMesAtual"] = ultimoDiaDoMesAtual.ToString("dd/MM/yyyy");

                //obter o email do usuario autenticado..
                var email = User.Identity.Name;
                //obter os dados do usuario no banco de dados..
                var usuario = _usuarioRepository.Obter(email);

                //consultar as tarefas no banco de dados do usuario dentro do periodo de datas
                var tarefas = _tarefaRepository.ConsultarPorUsuarioEPeriodo
                    (usuario.IdUsuario, primeiroDiaDoMesAtual, ultimoDiaDoMesAtual);

                //calcular o total de tarefas por cada prioridade
                TempData["TotalPrioridadeBaixa"] = tarefas.Count(t => t.Prioridade.Equals("BAIXA"));
                TempData["TotalPrioridadeMedia"] = tarefas.Count(t => t.Prioridade.Equals("MEDIA"));
                TempData["TotalPrioridadeAlta"] = tarefas.Count(t => t.Prioridade.Equals("ALTA"));

                //enviando a lista de tarefas para a página
                return View(tarefas);
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View();
        }
    }
}
