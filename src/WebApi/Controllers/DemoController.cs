using Microsoft.AspNetCore.Mvc;
using Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;


//Para las versiones de SDK 9 no viene configurado los controles en el program así que hay que agregarlos manualmente

//El acceso es urlbase/nombreRuta/nombreMetodo/ http//localhost:5001/demo/getString

namespace Controllers
{
    [ApiController]
    [Route("demo")]
    public class Democontroller: ControllerBase
    {
        private readonly MasterNetDBContext _context;

        public Democontroller(MasterNetDBContext context)
        {
            _context = context;
        }

        [HttpGet("get-cursos")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            return await _context.Cursos!.ToListAsync();
        }
    }

}