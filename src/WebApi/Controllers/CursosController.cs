using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Application.Cursos.CursoCreate;


//Para las versiones de SDK 9 no viene configurado los controles en el program así que hay que agregarlos manualmente
//El acceso es urlbase/nombreRuta/nombreMetodo/ http//localhost:5001/demo/getString

namespace Controllers
{
    [ApiController]
    [Route("api/cursos")]
    public class Cursoscontroller: ControllerBase
    {
        private readonly ISender _sender;
        public Cursoscontroller(ISender sender)
        {
            _sender = sender;
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<Guid>>CursoCreate([FromForm] CursoCreateRequest request, CancellationToken cancellationToken)
        {
            var command = new CursoCreateCommand.CursoCreateCommandRequest(request);
            var resultado = await _sender.Send(command);
            return Ok(resultado);
        }

    }

}