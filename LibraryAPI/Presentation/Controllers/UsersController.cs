// LibraryAPI.Presentation/Controllers/UsersController.cs
using AutoMapper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LibraryAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _svc;
        private readonly IMapper _mapper;

        public UsersController(IUserService svc, IMapper mapper)
        {
            _svc = svc;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all users whose name contains the given fragment.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserDto>>> GetAll([FromQuery] string name)
        {
            var list = await _svc.GetByNameAsync(name);
            return Ok(_mapper.Map<List<UserDto>>(list));
        }

        /// <summary>
        /// Retrieves a specific user by id.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var u = await _svc.GetByIdAsync(id);
            if (u is null) return NotFound();
            return Ok(_mapper.Map<UserDto>(u));
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            var result = await _svc.CreateAsync(entity);
            if (result.HasErrors)
                return BadRequest(result.Errors);

            var resultDto = _mapper.Map<UserDto>(result.Data);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.Id = id;
            var result = await _svc.UpdateAsync(user);
            if (result.HasErrors)
                return BadRequest(result.Errors);

            return NoContent();
        }

        /// <summary>
        /// Deletes (logical) a user by id.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _svc.DeleteByIdAsync(id);
            if (result.HasErrors)
                return NotFound(result.Errors);

            return NoContent();
        }
    }
}
