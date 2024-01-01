using sleep_tracker_api.Models;

namespace sleep_tracker_api.Repositories
{
    public class RecordRepository : IRepository<Record>
    {
        ApplicationDbContext context;

        public RecordRepository()
        {
            context = new();
        }
        public void Add(Record entity)
        {
            context.Records.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var record = Get(id);
            if (record is null)
                return;

            context.Records.Remove(record);
            context.SaveChanges();
        }

        public Record? Get(int id) => context.Records.FirstOrDefault<Record>(r => r.Id == id);

        public IEnumerable<Record> GetAll() => context.Records.ToList();


        public void Update(Record entity)
        {
            var record = Get(entity.Id);
            if (record is null)
                return;

            ApplicationDbContext db = new();
            db.Records.Update(entity);
            db.SaveChanges();
        }
    }
}
