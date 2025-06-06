﻿// LibraryAPI.Presentation.Controllers/LoansController.cs
using System.Threading.Tasks;
using AutoMapper;
using LibraryAPI.Core.Helper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Presentation.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing book loans.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _service;
        private readonly IMapper _mapper;

        public LoansController(ILoanService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // *** Noul endpoint GET all ***
        /// <summary>
        /// Retrieves all loans.
        /// </summary>
        /// <returns>200 OK with list of <see cref="LoanDto"/></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LoanDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetAll()
        {
            var loans = await _service.GetAllAsync();
            // mapăm lista de Loan (model) la LoanDto
            var dtos = _mapper.Map<IEnumerable<LoanDto>>(loans);
            return Ok(dtos);
        }


        /// <summary>
        /// Borrows a copy of the specified book.
        /// </summary>
        /// <param name="request">Contains the <see cref="LoanRequestDto.BookId"/> to borrow.</param>
        /// <returns>
        /// 201 Created with <see cref="LoanDto"/> if successful, or
        /// 400 BadRequest with error messages if borrowing fails.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(LoanDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Borrow([FromBody] LoanRequestDto request ,
                                               [FromHeader(Name = "X-Notify-Email")] string notifyEmail)
        {
            var loan = _mapper.Map<Loan>(request);
            var result = await _service.BorrowAsync(loan, notifyEmail);
            if (result.HasErrors)
                return BadRequest(result.Errors);

            var dto = _mapper.Map<LoanDto>(result.Data!);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Returns a previously borrowed book.
        /// </summary>
        /// <param name="loanId">The ID of the loan to return.</param>
        /// <returns>
        /// 204 NoContent if successful, or
        /// 400 BadRequest with error messages if return fails.
        /// </returns>
        [HttpPost("{loanId:int}/return")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Return(int loanId, 
                                                [FromHeader(Name = "X-Notify-Email")] string notifyEmail)
        {
            var result = await _service.ReturnAsync(loanId, notifyEmail);
            if (result.HasErrors)
                return BadRequest(result.Errors);

            return NoContent();
        }

        /// <summary>
        /// Retrieves the details of a specific loan.
        /// </summary>
        /// <param name="id">The ID of the loan.</param>
        /// <returns>
        /// 200 OK with <see cref="LoanDto"/> if found, or
        /// 404 NotFound if no such loan exists.
        /// </returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(LoanDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LoanDto>> GetById(int id)
        {
            var loan = await _service.GetByIdAsync(id);
            if (loan is null)
                return NotFound();

            return Ok(_mapper.Map<LoanDto>(loan));
        }

        /// <summary>
        /// Deletes (logical) a user by id.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (result.HasErrors)
                return NotFound(result.Errors);

            return NoContent();
        }

        /// <summary>
        /// Updates an existing loan.
        /// </summary>
        /// <param name="id">Loan identifier.</param>
        /// <param name="dto">Updated loan data.</param>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LoanDto dto)
        {
            if(dto.ReturnedAt == null)
                return BadRequest("ReturnedAt cannot be null");
            var toUpdate = _mapper.Map<Loan>(dto);
            toUpdate.Id = id;
            var result = await _service.UpdateAsync(toUpdate);
            if (result.HasErrors)
                return BadRequest(result.Errors);

            return NoContent();
        }

    }
}
