using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using MyWedapi;

namespace ToDoLists.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private ToDoListsServices service;

        public ToDoListsController(ToDoListsServices service)
        {
            this.service = service;
        }
        
        [HttpGet("collection/today")]
        public ActionResult<IEnumerable<TodayTaskDTO>> GetMyListsWithTasks()
        {
            

            return Ok(service.GetMyListsWithTasks());
        }


        [HttpGet("dashboard")]
        public ActionResult<DashboardDto> GetToday()
        {
            

            return Ok(service.GetTodayTasks());
        }

        [HttpGet("lists")]
        public ActionResult<IEnumerable<MyList>> GetMyLists()
        {
            

            return Ok(service.GetAllLists());
        }
        [HttpGet("list/{id}")]
        public ActionResult<MyList> GetMyList(int id)
        {
            
            if (service.IsContainsListId(id))
            {
                return Ok(service.GetList(id));
            }
            else 
            {
                return NotFound();
            }
        }

        [HttpGet("list/{idList}/task/{idTask}")]
        public ActionResult<MyTask> GetMyTask(int idList, int idTask)
        {
            if (service.IsContainsTaskId(idTask))
            {
                return Ok(service.GetTask(idList, idTask));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("lists/{id}/tasks")]
        public ActionResult<MyList> GetMyTasks(int id, bool isAll)
        {
            if (service.IsContainsListId(id))
            {
                return Ok(service.GetAllTaskFromList(id,isAll));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("list")]
        public ActionResult<MyList> PostMyList(MyList myList)
        {                        
            return Ok(service.AddList(myList));
        }
        [HttpPost("list/{id}/task")]
        public ActionResult<MyTask> PostMyTask(int id, MyTask model)
        {           
            if (service.IsContainsListId(id))
            {             
                return Ok(service.AddTask(id,model));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPatch("list/{idList}")]
        public ActionResult<MyList> PatchMyList(int idList, MyList model)
        {
            // TODO: Your code her
            if (service.IsContainsListId(idList))
            {  
                return Ok(service.UpdateList(idList, model));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPatch("list/{idList}/task/{idTask}")]
        public ActionResult<MyTask> PatchMyTask(int idList, int idTask, UpdateTaskDto model)
        {
            // TODO: Your code her
            if (service.IsContainsListId(idList) && service.IsContainsTaskId(idTask))
            {
                return Ok(service.UpdateTask(idList, idTask, model));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("list/{id}")]
        public ActionResult<MyList> DeleteMyListById(int id)
        {
                        
            
            if (service.IsContainsListId(id))
            {
                return service.DeleteList(id);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("list/{idList}/task/{idTask}")]
        public ActionResult<MyTask> DeleteMyListById(int idList, int idTask)
        {
                        
            if (service.IsContainsListId(idList) && service.IsContainsTaskId(idTask))
            {
                return Ok(service.DeleteTask(idList, idTask));
            }
            else
            {
                return NotFound();
            }
        }


    }
}