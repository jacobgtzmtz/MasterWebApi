using Application.Core;

namespace Application.Cursos.GetCursos
{
    public class GetCursosRequest: PagingParams
    {
       public string? Titulo { get; set; }
       public string? Descripcion { get; set; }
    }
}