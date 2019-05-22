using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TodoListWebAPICore.Data;
using TodoListWebAPICore.Data.Entities;
using TodoListWebAPICore.Models;

namespace TodoListWebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListTasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public ListTasksController(ITaskRepository taskRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this._taskRepository = taskRepository;
            this._mapper = mapper;
            this._linkGenerator = linkGenerator;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTaskModel>>> Get()
        {
            try
            {
                var tasks = await _taskRepository.GetAllAsync();
                if (tasks == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<IEnumerable<TodoTaskModel>>(tasks));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Reposiry Failure");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TodoTaskModel>> Get(int id)
        {
            try
            {
                var task = await _taskRepository.GetTaskByIdAsync(id);
                return _mapper.Map<TodoTaskModel>(task);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Reposiry Failure");
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<TodoTaskModel> Post(TodoTaskModel todoTaskModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Inavlid data");
                }

                var task = _mapper.Map<TodoTask>(todoTaskModel);
                var newTask = _taskRepository.Add(task);

                if (newTask == null)
                {
                    return BadRequest("Could not create new task");
                }

                var location = _linkGenerator.GetPathByAction("Get", "ListTasks", new { id = newTask.Id });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current Id");
                }

                return Created(location, _mapper.Map<TodoTaskModel>(newTask));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Reposiry Failure");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TodoTaskModel>> Put(int id, TodoTaskModel todoTaskModel)
        {
            try
            {
                var oldTask = await _taskRepository.GetTaskByIdAsync(id);
                if (oldTask == null) NotFound($"Could not find task with id of {id}");

                _mapper.Map(todoTaskModel, oldTask);


                return _mapper.Map<TodoTaskModel>(oldTask);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Reposiry Failure");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldTask = await _taskRepository.GetTaskByIdAsync(id);
                if (oldTask == null) NotFound($"Could not find task with id of {id}");

                _taskRepository.Remove(id);

                if ((oldTask = await _taskRepository.GetTaskByIdAsync(id)) == null) return Ok();
                else return StatusCode(StatusCodes.Status500InternalServerError, "Reposiry Failure");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Reposiry Failure");
            }

        }
    }
}
