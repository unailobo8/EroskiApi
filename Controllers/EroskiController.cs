using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EroskiApi.Models;

namespace EroskiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EroskiController : ControllerBase
    {
        private readonly EroskiContext _context;

        public EroskiController(EroskiContext context)
        {
            _context = context;
        }

        // GET: api/Eroski
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EroskiSeccion>>> GetEroskiSeccions()
        {
            return await _context.EroskiSeccions.ToListAsync();
        }

        // GET: api/Eroski/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EroskiSeccion>> GetEroskiSeccion(int id)
        {
            var eroskiSeccion = await _context.EroskiSeccions.FindAsync(id);

            if (eroskiSeccion == null)
            {
                return NotFound();
            }

            return eroskiSeccion;
        }

        //PUT: api/Eroski/5
        //turno en la respectiva seccion del eroski que actualiza el trabajador
        [HttpPut("{id}")]
        public async Task<IActionResult> TurnoEroskiSeccion(int id){

             var eroskiSeccion = await _context.EroskiSeccions.FindAsync(id);

            if (eroskiSeccion == null)
            {
                return NotFound();
            }

            eroskiSeccion.Ticket++;

           _context.SaveChanges();

            return NoContent();
        }
        //PUT: api/Eroski
        //resetea la variable de tiquet
        [HttpPut("reset")]
        public async Task<IActionResult> ResetEroskiSeccion(int id){

             var eroskiSeccion = await _context.EroskiSeccions.FindAsync(id);

            if (eroskiSeccion == null)
            {
                return NotFound();
            }

            eroskiSeccion.Ticket = 0;

           _context.SaveChanges();

            return NoContent();
        }

        

        // POST: api/Eroski
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EroskiSeccion>> PostEroskiSeccion(EroskiSeccion eroskiSeccion)
        {
            _context.EroskiSeccions.Add(eroskiSeccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEroskiSeccion", new { id = eroskiSeccion.Id }, eroskiSeccion);
        }

        // DELETE: api/Eroski/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEroskiSeccion(int id)
        {
            var eroskiSeccion = await _context.EroskiSeccions.FindAsync(id);
            if (eroskiSeccion == null)
            {
                return NotFound();
            }

            _context.EroskiSeccions.Remove(eroskiSeccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EroskiSeccionExists(int id)
        {
            return _context.EroskiSeccions.Any(e => e.Id == id);
        }
    }
}
