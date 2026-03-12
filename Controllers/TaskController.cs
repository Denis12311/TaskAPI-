using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;


namespace TaskApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {

        public static List<TaskItem> tasks = new List<TaskItem>();
        private static int nextId = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = tasks.FirstOrDefault(x => x.Id == id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public IActionResult Post(TaskItem task)
        {
            task.Id=nextId++;
            task.IsCompleted = false;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult PutById(int id,TaskItem task)
        {
            var taskUpdated=tasks.FirstOrDefault(x=>x.Id==id);
            if (taskUpdated == null) return NotFound();

            taskUpdated.Title =task.Title;
            taskUpdated.IsCompleted=task.IsCompleted;

           
            return Ok(taskUpdated);
        }
        [HttpDelete("{id}")]

        public IActionResult DeleteById(int id)
        {
            var task = tasks.FirstOrDefault(x => x.Id == id);
            if (task == null) return NotFound();
            tasks.Remove(task);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchById(int id)
        {
            var task = tasks.FirstOrDefault(x => x.Id == id);
            if (task == null) return NotFound();
            task.IsCompleted = !task.IsCompleted;
            

            return Ok(task);

        }




    }
}
