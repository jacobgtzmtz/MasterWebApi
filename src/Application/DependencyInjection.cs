using Application.Cursos.CursoCreate;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
namespace Application
{
    public static class DependencyInjection
    {
        //Aquí podemos agregar servicios de aplicación que necesitemos inyectar en los controladores
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Agregar MediatR
            services.AddMediatR(cfg => 
             {cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);}
             );

              services.AddFluentValidationAutoValidation();
             services.AddValidatorsFromAssemblyContaining<CursoCreateCommand>();

            return services;
        }
    }
}