using Application.Calificaciones.GetCalificaciones;
using Application.Core;
using Application.Fotos.GetFotos;
using Application.Instructores.GetInstructores;
using Application.Precios.GetPrecios;
using Domain;
using MediatR;

namespace Application.Cursos.GetCurso;

public class GetCursoQuery
{
    public record GetCursoQueryRequest: IRequest<Result<CursoResponse>>;

}

public record CursoResponse(
    Guid Id,
    string Title,
    string Description,
    DateTime FechaPublicacion,
    List<InstructorResponse> Instructores,
    List<CalificacionResponse> Calificaciones,
    List<PrecioResponse> Precios,
    List<FotoResponse> Fotos
);