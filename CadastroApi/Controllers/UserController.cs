using CadastroApi.Context;
using CadastroApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]

    public class UserController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid || user == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    try
                    {
                        if (UserExists(user) == false)
                        {
                            _context.Add(user);
                            await _context.SaveChangesAsync();
                            return Ok();
                        }
                        else
                        {
                            return NotFound("Contato já existe.");
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(User user)
        {
            try
            {

                if (!ModelState.IsValid || user == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else if (user.Id == 0)
                {
                    return NotFound("Contato não encontrado.");
                }
                else
                {
                    try
                    {
                        if (UserExists(user.Id) == true)
                        {
                            _context.Update(user);
                            await _context.SaveChangesAsync();
                            return Ok();
                        }
                        else
                        {
                            return NotFound("Contato já existe.");
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound("Contato não informado.");
                }
                else
                {
                    User user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

                    if (user == null)
                    {
                        return NotFound("Contato não encontrado.");
                    }
                    else
                    {
                        return Ok(user);
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _context.Users.ToListAsync());
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound("Contato não informado.");
                }
                else
                {
                    User user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
                    if (user == null)
                    {
                        return NotFound("Contato não encontrado.");
                    }
                    else
                    {
                        try
                        {
                            _context.Remove(user);
                            await _context.SaveChangesAsync();
                            return Ok();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!UserExists(user.Id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        private bool UserExists(int? id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        private bool UserExists(User user)
        {
            return _context.Users.Any(e => e.Name == user.Name && e.LastName == user.LastName);
        }
    }
}