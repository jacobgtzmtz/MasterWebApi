using System;
using System.Linq;
using Application.Calificaciones.GetCalificaciones;
using Application.Core;
using Application.Cursos.GetCurso;
using Application.Fotos.GetFotos;
using Application.Instructores.GetInstructores;
using Application.Precios.GetPrecios;
using Domain;
using MediatR;
using Persistence;

namespace Application.Cursos.GetCursos;

public class GetCursosQuery
{
    public record GetCursosQueryRequest: IRequest<Result<PagedList<CursoResponse>>>
    {
        public GetCursosRequest CursosRequest { get; set; } = new GetCursosRequest();
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
            var predicate = ExpressionBuilder.New<Curso>();

            if (!string.IsNullOrEmpty(request.CursosRequest.Titulo))
            {
                //query = query.Where(x => x.Titulo!.Contains(request.CursosRequest.Titulo));
                predicate = predicate.And(x => x.Titulo!.ToLower().Contains(request.CursosRequest.Titulo.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.CursosRequest.Descripcion))
            {
                //query = query.Where(x => x.Descripcion!.Contains(request.CursosRequest.Descripcion));
                predicate = predicate.And(x => x.Descripcion!.ToLower().Contains(request.CursosRequest.Descripcion.ToLower()));
            }

            query = query.Where(predicate);

            if(request.CursosRequest.OrderBy != null)
            {
                switch (request.CursosRequest.OrderBy.ToLower())
                {
                    case "titulo":
                        query = request.CursosRequest.Ascending
                            ? query.OrderBy(x => x.Titulo)
                            : query.OrderByDescending(x => x.Titulo);
                        break;
                    case "descripcion":
                        query = request.CursosRequest.Ascending
                            ? query.OrderBy(x => x.Descripcion)
                            : query.OrderByDescending(x => x.Descripcion);
                        break;
                    default:
                        query = query.OrderBy(x => x.Titulo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(x => x.Titulo);
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

            var pagedList = await PagedList<CursoResponse>.CreateAsync(projection, request.CursosRequest.PageNumber, request.CursosRequest.PageSize);

            return Result<PagedList<CursoResponse>>.Success(pagedList);
        }
    }
}
