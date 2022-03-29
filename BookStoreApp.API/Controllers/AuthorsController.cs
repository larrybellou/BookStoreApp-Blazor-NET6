#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using AutoMapper;
using BookStoreApp.API.Static;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<AuthorsController> logger;

        public AuthorsController(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDTO>>> GetAuthors()
        {
            logger.LogInformation($"Request to {nameof(GetAuthors)}");
            try
            {
                var authors = await _context.Authors.ToListAsync();
                var authorDTOs = mapper.Map<IEnumerable<AuthorReadOnlyDTO>>(authors);
                return Ok(authorDTOs);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error performing {nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadOnlyDTO>> GetAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                var authorDTO = mapper.Map<AuthorReadOnlyDTO>(author);

                if (author == null)
                {
                    logger.LogWarning(Messages.WarningAuthorNotFound);
                    return NotFound();
                }

                return authorDTO;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error performing {nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDTO authorDTO)
        {
            try
            {
                if (id != authorDTO.Id)
                {
                    logger.LogWarning(Messages.WarningIDsDoNotMatch);
                    return BadRequest();
                }

                var author = mapper.Map<Author>(authorDTO);

                _context.Entry(author).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(id))
                    {
                        logger.LogWarning(Messages.WarningAuthorNotFound);
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error performing {nameof(PutAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDTO>> PostAuthor(AuthorCreateDTO authorDTO)
        {
            try
            {
                var author = mapper.Map<Author>(authorDTO);
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error performing {nameof(PostAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try { 
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    logger.LogWarning(Messages.WarningAuthorNotFound);
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error performing {nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
