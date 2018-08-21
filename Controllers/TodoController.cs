using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{

    // [Route("api/[controller]")]属性: コントローラやアクション単位でルーティングを記述する
    [Route("api/[controller]")]
    // [ApiController]属性: WebAPIコントローラークラスを表す
    // ControllerBaseと組み合わせて使用する
    // 
    // [ApiController]属性の機能
    // - 検証エラーが発生すると、HTTP 400 応答が自動的にトリガーされる
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                // TodoItemsが空の場合は新しいTodoItemを作成する
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }
    }
}