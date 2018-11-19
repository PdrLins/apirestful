using System.Collections.Generic;
using DesafioPitang.Business;
using DesafioPitang.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPitang.Controllers
{
    //[Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IUserService _userService;
        public ValuesController(IUserService service)
        {
            _userService = service;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public IActionResult GetById(int id)
        {
            _userService.Handle(new SaveUser { Email = "Abc", Password = "123" });
            var a = _userService.Handle(new FindUser { Id = 1 });

            return  Ok($"valor: {id} + abc");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return null;
        }
    }
}
