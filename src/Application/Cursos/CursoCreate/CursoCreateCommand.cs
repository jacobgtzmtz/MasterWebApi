using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Cursos.CursoCreate;

public class CursoCreateCommand
{
    public record CursoCreateCommandRequest(CursoCreateRequest Request) : IRequest<Result<Guid>>;

    public class CursoCreateCommandRequestValidator : AbstractValidator<CursoCreateCommandRequest>
    {
        public CursoCreateCommandRequestValidator()
        {
            RuleFor(x => x.Request).SetValidator(new CursoCreateValidator());
        }
    }

    internal class CursoCreateCommandHandler : IRequestHandler<CursoCreateCommandRequest, Result<Guid>>
    {
        private readonly MasterNetDBContext _dbContext;

        public CursoCreateCommandHandler(MasterNetDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Handle(CursoCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var nuevoCurso = new Curso
            {
                Id = Guid.NewGuid(),
                Titulo = request.Request.Titulo,
                Descripcion = request.Request.Descripcion
            };

            _dbContext.Add(nuevoCurso);
            var resultado = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
            
            return resultado ? Result<Guid>.Success(nuevoCurso.Id) : Result<Guid>.Failure("No se pudo crear el curso");
        }
    }
}
