using Microsoft.AspNetCore.Mvc;

//Para las versiones de SDK 9 no viene configurado los controles en el program así que hay que agregarlos manualmente

//El acceso es urlbase/nombreRuta/nombreMetodo/ http//localhost:5001/demo/getString

namespace Controllers
{
    [ApiController]
    [Route("demo")]
    public class Democontroller: ControllerBase
    {
        [HttpGet("getString")]
        public string GetNombre()
        {
            return "Jacob Gutiérrez";
        }
    }

}