﻿using LTMSistem.Enuns;
using LTMSistem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LTMSistem.Filters
{
    public class PaginaRestritaSomenteSecretaria : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "Action", "Index" } });
            }
            else
            {
                UsuarioModel usuario= JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
                if(usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "Action", "Index" } });

                }
                if (usuario.Perfil != PerfilEnum.Administrador && usuario.Perfil != PerfilEnum.Secretaria)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "Action", "Index" } });
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
