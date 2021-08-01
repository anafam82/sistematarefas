using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoAspNetMVC03.Data.Interfaces;
using ProjetoAspNetMVC03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Controllers
{
    [Authorize] //Somente usuarios autenticados podem acessar o controlador
    public class UsuarioController : Controller
    {
        //atributo
        private readonly IUsuarioRepository _usuarioRepository;

        //construtor para o AspNet possa inicializar o atributo
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult MeusDados()
        {
            try
            {
                //obter o email do usuario que está autenticado no sistema
                var email = User.Identity.Name;
                //acessar o banco de dados para obter os dados do usuario
                var usuario = _usuarioRepository.Obter(email);

                //exibir os dados na página
                TempData["IdUsuario"] = usuario.IdUsuario;
                TempData["Nome"] = usuario.Nome;
                TempData["Email"] = usuario.Email;
                TempData["DataCadastro"] = usuario.DataCadastro.ToString("dd/MM/yyyy");
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View();
        }

        public IActionResult EditarSenha()
        {
            return View();
        }

        [HttpPost] //método que recebe o SUBMIT POST do formulário
        public IActionResult EditarSenha(UsuarioEditarSenhaModel model)
        {
            //verificar se todos os campos da model
            //foram validados com sucesso
            if (ModelState.IsValid)
            {
                try
                {
                    //obter o email do usuario autenticado
                    var email = User.Identity.Name;
                    //obter os dados do usuario autenticado
                    var usuario = _usuarioRepository.Obter(email);

                    //verificar se a senha atual informada esta correta
                    if (_usuarioRepository.Obter(usuario.Email, model.SenhaAtual) != null)
                    {
                        //atualizar a senha
                        _usuarioRepository.Alterar(usuario.IdUsuario, model.NovaSenha);
                        TempData["Mensagem"] = "Nova senha atualizada com sucesso. Saia e entre novamente no sistema para testar sua nova senha.";
                    }
                    else
                    {
                        TempData["Mensagem"] = "Sua senha atual está incorreta, por favor tente novamente.";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View();
        }
    }
}





