using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Cursos.CursoCreate;
using static Application.Cursos.CursoReporteExcel.CursoReporteExcelQuery;
using Application.Cursos.GetCurso;
using static Application.Cursos.GetCurso.GetCursoQuery;
using Application.Cursos.GetCursos;
using static Application.Cursos.GetCursos.GetCursosQuery;


//Para las versiones de SDK 9 no viene configurado los controles en el program así que hay que agregarlos manualmente
//El acceso es urlbase/nombreRuta/nombreMetodo/ http//localhost:5001/demo/getString

namespace Controllers
{
    [ApiController]
    [Route("api/cursos")]
    public class CursosController : ControllerBase
    {
        private readonly ISender _sender;
        public CursosController(ISender sender)
        {
            _sender = sender;
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<Guid>>CursoCreate([FromForm] CursoCreateRequest request, CancellationToken cancellationToken)
        {
            var command = new CursoCreateCommand.CursoCreateCommandRequest(request);
            var resultado = await _sender.Send(command, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet("reporte")]
        public async Task<ActionResult>CursoReporteExcel(CancellationToken cancellationToken)
        {
            var query = new CursoReporteExcelQueryRequest();
            var resultado = await _sender.Send(query, cancellationToken);
            return File(resultado.ToArray(), "text/csv", "cursos.csv");
        }

        [HttpGet("PaginationCursos")]
        public async Task<ActionResult>PaginationCursos(
            [FromBody] GetCursosRequest request,
            CancellationToken cancellationToken)
        {
         var query = new GetCursosQueryRequest { CursosRequest = request};  
         var resultado = await _sender.Send(query, cancellationToken);
         return resultado.IsSuccess ? Ok(resultado.Value) : NotFound(resultado.Error);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult>CursoGet(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCursoQueryRequest { Id = id };
            var resultado = await _sender.Send(query, cancellationToken);
            return resultado.IsSuccess ? Ok(resultado.Value) : NotFound(resultado.Error);
        }

    }

}