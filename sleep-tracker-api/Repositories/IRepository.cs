﻿namespace sleep_tracker_api.Repositories
{
    public interface IRepository<T>
    {
        T? Get(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
