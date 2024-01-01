using Microsoft.AspNetCore.Mvc;
using sleep_tracker_api.Models;
using sleep_tracker_api.Repositories;

namespace sleep_tracker_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : Controller
    {
        IRepository<Record> db;

        public RecordController(IRepository<Record> _db)
        {
            db = _db;
        }

        [HttpGet("{id}")]
        public ActionResult<Record> Get(int id)
        {
            var record = db.Get(id);

            if (record is null)
                return NotFound();

            return record;
        }

        [HttpGet]
        public ActionResult<List<Record>> GetAll() => db.GetAll().ToList();


        [HttpGet("search")]
        public ActionResult<List<Record>> Search([FromQuery] string term)
        {
            if (String.IsNullOrEmpty(term))
                return BadRequest("Search Term is missing.");

            var records = db.GetAll().Where(r => new[] { r.Name, r.Description, r.Status, r.SleepQuality, r.Duration.ToString(), r.StartTime.ToString(), r.EndTime.ToString() }.Any(f => f.Contains(term))).ToList();

            if (records.Count == 0)
                return NotFound($"No records found matching '{term}'");

            return records;
        }


        [HttpPost]
        public IActionResult Create(Record record)
        {
            db.Add(record);
            return CreatedAtAction(nameof(Get), new { id = record.Id }, record);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Record record)
        {
            if (record is null || record.Id != id)
                return BadRequest();

            var existingRecord = db.Get(id);
            if (existingRecord is null)
                return NotFound();

            db.Update(record);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var record = db.Get(id);
            if (record is null) return NotFound();

            db.Delete(id);
            return NoContent();
        }





    }
}
