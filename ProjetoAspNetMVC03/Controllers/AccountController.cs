using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProjetoAspNetMVC03.Data.Entities;
using ProjetoAspNetMVC03.Data.Interfaces;
using ProjetoAspNetMVC03.Messages;
using ProjetoAspNetMVC03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Controllers
{
    public class AccountController : Controller
    {
        //atributo para acessar os métodos do repositorio (IUsuarioRepository)
        private readonly IUsuarioRepository _usuarioRepository;

        //método construtor atraves do qual do AspNet MVC irá inicializar
        //a interface IUsuarioRepository (injeção de dependencia)
        public AccountController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        //método utilizado para abrir a página de Login
        public IActionResult Login()
        {
            return View();
        }

        //método executado pelo formulário da página
        //chamado pelo SUBMIT (HTTP POST)
        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            //verificar se todos os campos passaram
            //nas regras de validação com sucesso!
            if (ModelState.IsValid)
            {
                try
                {
                    //consultar o usuario no banco de dados atraves do email e senha
                    var usuario = _usuarioRepository.Obter(model.Email, model.Senha);

                    //verificar se o usuario foi encontrado
                    if (usuario != null)
                    {
                        //criando a autenticação do usuario
                        var autenticacao = new ClaimsIdentity(
                            new[] { new Claim(ClaimTypes.Name, usuario.Email) },
                            CookieAuthenticationDefaults.AuthenticationScheme
                            );

                        //gravar em cookie a autenticação
                        var claim = new ClaimsPrincipal(autenticacao);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claim);

                        //redirecionar para a página /Home/Index
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Acesso negado.";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = "Erro: " + e.Message;
                }
            }

            return View();
        }

        //método para abrir a página /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost] //receber os campos enviados pelo SUBMIT do formulário
        public IActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario();

                    usuario.Nome = model.Nome;
                    usuario.Email = model.Email;
                    usuario.Senha = model.Senha;

                    //verificar se o email informado já existe no banco de dados
                    if (_usuarioRepository.Obter(usuario.Email) != null)
                    {
                        TempData["Mensagem"] = "O email informado já encontra-se cadastrado.";
                    }
                    else
                    {
                        _usuarioRepository.Inserir(usuario);

                        TempData["Mensagem"] = $"Usuário {usuario.Nome}, cadastrado com sucesso!";
                        ModelState.Clear(); //limpar os campos do formulário
                    }
                }
                catch (Exception e)
                {
                    //gerar uma mensagem de erro..
                    TempData["Mensagem"] = "Erro: " + e.Message;
                }
            }

            return View();
        }

        public IActionResult Logout()
        {
            //destruir o cookie de autenticação do usuario
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //redirecionar para a página de login
            return RedirectToAction("Login", "Account");
        }

        //abrir a página de recuperação de senha
        public IActionResult PasswordRecovery()
        {
            return View();
        }

        [HttpPost] //receber o SUBMIT POST do formulário
        public IActionResult PasswordRecovery(AccountPasswordRecoveryModel model)
        {
            //verificar se não há erros de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar o usuario no banco de dados atraves do email informado..
                    var usuario = _usuarioRepository.Obter(model.Email);

                    //verificar se o usuario foi encontrado
                    if (usuario != null)
                    {
                        //gerar uma nova senha composta apenas de numeros aleatorios..
                        var novaSenha = new Random().Next(99999999, 999999999).ToString();
                        //atualizar a senha do usuario no banco de dados
                        _usuarioRepository.Alterar(usuario.IdUsuario, novaSenha);

                        //enviando a nova senha por email para o usuario
                        var to = usuario.Email;
                        var subject = "Nova senha gerada com sucesso - Sistema de controle de tarefas";
                        var body = $@"
                            <div style='text-align: center; margin: 40px; padding: 60px; border: 2px solid #ccc; font-size: 16pt;'>
                            <img src='https://www.cotiinformatica.com.br/imagens/logo-coti-informatica.png' />
                            <br/><br/>
                            Olá <strong>{usuario.Nome}</strong>,
                            <br/><br/>    
                            O sistema gerou uma nova senha para que você possa acessar sua conta.<br/>
                            Por favor utilize a senha: <strong>{novaSenha}</strong>
                            <br/><br/>
                            Não esqueça de, ao acessar o sistema, atualizar esta senha para outra
                            de sua preferência.
                            <br/><br/>              
                            Att<br/>   
                            Equipe COTI Informatica
                            </div>
                        ";

                        //enviando o email
                        var message = new EmailServiceMessage();
                        message.EnviarMensagem(to, subject, body);

                        TempData["Mensagem"] = $"Uma nova senha foi gerada com sucesso e enviada para o email {usuario.Email}.";
                        ModelState.Clear(); //limpar o formulário
                    }
                    else
                    {
                        TempData["Mensagem"] = "O email informado não está cadastrado no sistema.";
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


