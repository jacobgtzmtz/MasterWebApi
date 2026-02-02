using Domain;
using MediatR;
using Persistence;

namespace Application.Cursos.CursoCreate;

public class CursoCreateCommand
{
    public record CursoCreateCommandRequest(CursoCreateRequest Request) : IRequest<Guid>;

    internal class CursoCreateCommandHandler : IRequestHandler<CursoCreateCommandRequest, Guid>
    {
        private readonly MasterNetDBContext _dbContext;

        public CursoCreateCommandHandler(MasterNetDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CursoCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var nuevoCurso = new Curso
            {
                Id = Guid.NewGuid(),
                Titulo = request.Request.Titulo,
                Descripcion = request.Request.Descripcion
            };

            _dbContext.Add(nuevoCurso);
            await _dbContext.SaveChangesAsync();

            return nuevoCurso.Id;
        }
    }
}
