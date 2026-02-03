using System;
using Application.Calificaciones.GetCalificaciones;
using Application.Core;
using Application.Cursos.GetCurso;
using Application.Fotos.GetFotos;
using Application.Instructores.GetInstructores;
using Application.Precios.GetPrecios;
using MediatR;
using Persistence;

namespace Application.Cursos.GetCursos;

public class GetCursosQuery
{
    public record GetCursosQueryRequest: IRequest<Result<PagedList<CursoResponse>>>
    {
        public GetCursosRequest Params { get; set; } = new GetCursosRequest();
    }
        
    internal class GetCursosQueryHandler : IRequestHandler<GetCursosQueryRequest, Result<PagedList<CursoResponse>>>
    {
        private readonly MasterNetDBContext _dbContext;

        public GetCursosQueryHandler(MasterNetDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagedList<CursoResponse>>> Handle(GetCursosQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Cursos!.AsQueryable();

            if (!string.IsNullOrEmpty(request.Params.Titulo))
            {
                query = query.Where(x => x.Titulo!.Contains(request.Params.Titulo));
            }

            if (!string.IsNullOrEmpty(request.Params.Descripcion))
            {
                query = query.Where(x => x.Descripcion!.Contains(request.Params.Descripcion));
            }

            var projection = query.Select(x => new CursoResponse(
                x.Id,
                x.Titulo!,
                x.Descripcion!,
                x.Instructores!.Select(i => new InstructorResponse(i.Id, i.Nombre!, i.Apellidos!, i.Grado!)).ToList(),
                x.Calificaciones!.Select(c => new CalificacionResponse(c.Alumno!, c.Puntaje, c.Comentario!)).ToList(),
                x.Precios!.Select(p => new PrecioResponse(p.Id, p.Nombre!, p.PrecioActual, p.PrecioPromocion)).ToList(),
                x.Fotos!.Select(f => new FotoResponse(f.Url!, f.CursoId ?? Guid.Empty)).ToList()
            ));

            var pagedList = await PagedList<CursoResponse>.CreateAsync(projection, request.Params.PageNumber, request.Params.PageSize);

            return Result<PagedList<CursoResponse>>.Success(pagedList);
        }
    }
}
