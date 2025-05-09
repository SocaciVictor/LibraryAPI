using AutoMapper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml;

namespace LibraryAPI.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing books.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IMapper _mapper;

        public BooksController(IBookService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves books matching provided filters.
        /// </summary>
        /// <param name="filterBook">Filter parameters (e.g., title, author).</param>
        /// <returns>List of books.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<BookDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BookDto>>> GetByFilter([FromQuery] FilterBook filterBook)
        {
            var books = await _service.SearchAsync(filterBook);
            return Ok(_mapper.Map<List<BookDto>>(books));
        }

        /// <summary>
        /// Retrieves a specific book by unique id.
        /// </summary>
        /// <param name="id">Book identifier.</param>
        /// <returns>Book details.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var book = await _service.GetByIdAsync(id);
            if (book is null) return NotFound();
            return Ok(_mapper.Map<BookDto>(book));
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="dto">Data for the new book.</param>
        /// <returns>The newly created book.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookDto dto)
        {
            var bookEntity = _mapper.Map<Book>(dto);
            var result = await _service.CreateAsync(bookEntity);
            if (result.HasErrors)
                return BadRequest(result.Errors);

            var resultDto = _mapper.Map<BookDto>(result.Data);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <param name="id">Book identifier.</param>
        /// <param name="dto">Updated book data.</param>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
        {
            var toUpdate = _mapper.Map<Book>(dto);
            toUpdate.Id = id;
            var result = await _service.UpdateAsync(toUpdate, dto.Author);
            if (result.HasErrors)
                return BadRequest(result.Errors);

            return NoContent();
        }

        /// <summary>
        /// Deletes a book by id.
        /// </summary>
        /// <param name="id">Book identifier.</param>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteByIdAsync(id);
            if (result.HasErrors)
                return NotFound(result.Errors);

            return NoContent();
        }
    }
}
