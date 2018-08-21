using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{

    // [Route("api/[controller]")]属性: コントローラやアクション単位でルーティングを記述する
    // 
    // [controller]は、"Controller"サフィックスを除いたクラス名に置き換えられる
    // ASP.NET Core のルーティングでは、大文字と小文字が区別されない
    // 本クラスの場合、"todo"となる
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
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            // MVCは、自動的にオブジェクトをJSONにシリアライズして、レスポンスメッセージに書き込む
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
    }
}