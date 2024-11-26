using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("people2Service")] IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet("all")]
        public List<People> GetPeoples() => Repository.People;

        [HttpGet("{id}")]
        public ActionResult<People> Get(int Id)
        {
            var people = Repository.People.FirstOrDefault(p => p.Id == Id);
            if(people == null)
            {
                return NotFound();
            }

            return Ok(people);
        }

        [HttpGet("search/{search}")]
        public List<People> Get(string search) =>
            Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();

        [HttpPost]
        public IActionResult Add(People people)
        {
            if(!_peopleService.Validate(people))
            {
                return BadRequest();
            }

            Repository.People.Add(people);

            return NoContent();
        }
        
    }

    public class Repository
    {
        public static List<People> People = new List<People>
        {
            new People()
            {
                Id = 1,
                Name = "Juan",
                BirthDate = new DateTime(1990, 12, 03),

            },
            new People()
            {
                Id = 2,
                Name = "Jose",
                BirthDate = new DateTime(1992, 11, 03),

            },
            new People()
            {
                Id = 3,
                Name = "Emilio",
                BirthDate = new DateTime(1985, 01, 08),

            },
            new People()
            {
                Id = 4,
                Name = "Jorge",
                BirthDate = new DateTime(1993, 04, 25),

            },
        };
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }


}
