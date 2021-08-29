using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T :class
    {
        IEnumerable<T> GetAll();//إرجاع جميع العناصر
        T GetById(object id);//إرجاع عنصر معين
        void Insert(T entity);//إضافة عنصر
        void Update(T entity);//تعديل عنصر
        void Delete(object id);//حذف عنصر
    }
}
