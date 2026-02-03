using Application.Calificaciones.GetCalificaciones;
using Application.Core;
using Application.Fotos.GetFotos;
using Application.Instructores.GetInstructores;
using Application.Precios.GetPrecios;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Cursos.GetCurso;

public class GetCursoQuery
{
    public record GetCursoQueryRequest: IRequest<Result<CursoResponse>>
    {
        public Guid Id { get; init; }
    };

    internal class GetCursoQueryHandler : IRequestHandler<GetCursoQueryRequest, Result<CursoResponse>>
    {
        private readonly MasterNetDBContext _dbContext;

        public GetCursoQueryHandler(MasterNetDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<CursoResponse>> Handle(GetCursoQueryRequest request, CancellationToken cancellationToken)
        {
            var curso = await _dbContext.Cursos!.Where(c => c.Id == request.Id)
            .FirstOrDefaultAsync();

            if (curso == null)
            {
                return Result<CursoResponse>.Failure("Curso no encontrado");
            }

            var instructores = curso.Instructores?
                .Select(i => new InstructorResponse(i.Id, i.Nombre!, i.Apellidos!, i.Grado!))
                .ToList() ?? new List<InstructorResponse>();

            var calificaciones = curso.Calificaciones?
                .Select(c => new CalificacionResponse(c.Alumno!, c.Puntaje, c.Comentario!))
                .ToList() ?? new List<CalificacionResponse>();

            var precios = curso.Precios?
                .Select(p => new PrecioResponse(p.Id, p.Nombre!, p.PrecioActual, p.PrecioPromocion))
                .ToList() ?? new List<PrecioResponse>();

            var fotos = curso.Fotos?
                .Select(f => new FotoResponse(f.Url!, f.CursoId ?? Guid.Empty))
                .ToList() ?? new List<FotoResponse>();

            var cursoResponse = new CursoResponse(
                curso.Id,
                curso.Titulo!,
                curso.Descripcion!,
                instructores,
                calificaciones,
                precios,
                fotos
            );

            return Result<CursoResponse>.Success(cursoResponse);
        }
    }

}

public record CursoResponse(
    Guid Id,
    string Titulo,
    string Descripcion,
    List<InstructorResponse> Instructores,
    List<CalificacionResponse> Calificaciones,
    List<PrecioResponse> Precios,
    List<FotoResponse> Fotos
);