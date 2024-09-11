using APICSharpToDoList.Context;
using APICSharpToDoList.Interfaces;
using APICSharpToDoList.Models;
using APICSharpToDoList.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APICSharpToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository toDoRepository;
        public ToDoController(IToDoRepository toDoRepository)
        {
            this.toDoRepository = toDoRepository;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(ToDoDTO toDoDTO)
        {
            try
            {
                if (toDoDTO == null) return BadRequest();

                await toDoRepository.Create(toDoDTO);

                return Created();
            }
            catch (Exception ex) 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var tdToDelete = await toDoRepository.GetById(id);

                if (tdToDelete == null) return NoContent();

                await toDoRepository.Delete(id);

                return Ok(new {message = $"Task '{id}' removed."});
            }
            catch (Exception ex) 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetAll()
        {
            try
            {
                return Ok(await toDoRepository.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update(int id, ToDo toDo)
        {
            try
            {
                if(id != toDo.Id) return BadRequest("Task ID mismatch");

                var td = await toDoRepository.GetById(id);

                if (td == null) return NoContent();

                await toDoRepository.Update(id, toDo);

                return Ok(new { message = $"Task '{toDo.Title}' changed."} );
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
