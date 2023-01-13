using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProyectoBanco.Core.Excepciones;
using System.Net;

namespace ProyectoBanco.Infrastructure.Filters
{
    public class FiltroDeExcepcionesGlobal : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(ExcepcionesDeNegocio))
            {
                var excepcion = (ExcepcionesDeNegocio)context.Exception;
                var validacion = new
                {
                    Status = 400,
                    Title = "Bad Request",
                    Detail = excepcion.Message
                };
                var json = new
                {
                    errors = new[] { validacion }
                };
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }

        }
    }
}
