using AutoMapper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LibraryAPI.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing authors.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _svc;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService svc, IMapper mapper)
        {
            _svc = svc;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        /// <returns>List of authors.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AuthorDto>>> GetAll()
        {
            var list = await _svc.GetAllAsync();
            return Ok(_mapper.Map<List<AuthorDto>>(list));
        }

        /// <summary>
        /// Retrieves a specific author by unique id.
        /// </summary>
        /// <param name="id">Author identifier.</param>
        /// <returns>Author details.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var a = await _svc.GetAsync(id);
            if (a is null) return NotFound();
            return Ok(_mapper.Map<AuthorDto>(a));
        }

        /// <summary>
        /// Creates a new author.
        /// </summary>
        /// <param name="dto">Data for the new author.</param>
        /// <returns>The newly created author.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthorDto>> Create([FromBody] CreateAuthorDto dto)
        {
            var entity = _mapper.Map<Author>(dto);
            var result = await _svc.CreateAsync(entity);
            if (result.HasErrors)
                return BadRequest(result.Errors);

            var resultDto = _mapper.Map<AuthorDto>(result.Data);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }

        /// <summary>
        /// Updates an existing author.
        /// </summary>
        /// <param name="id">Author identifier.</param>
        /// <param name="dto">Updated author data.</param>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorDto dto)
        {
            var author = _mapper.Map<Author>(dto);
            author.Id = id;
            var result = await _svc.UpdateAsync(author);
            if (result.HasErrors)
                return BadRequest(result.Errors);

            return NoContent();
        }

        /// <summary>
        /// Deletes an author by id.
        /// </summary>
        /// <param name="id">Author identifier.</param>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _svc.DeleteAsync(id);
            if (result.HasErrors)
                return NotFound(result.Errors);

            return NoContent();
        }
    }
}
