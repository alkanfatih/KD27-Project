using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commons
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } = false;

        public void Restore()
        {
            if (!IsDeleted) 
            { 
                IsDeleted = true;
                UpdateDate = DateTime.Now;
                DeletedDate = null;
            }
        }

        public void SoftDelete()
        {
            if (IsDeleted) 
            {
                IsDeleted = false;
                DeletedDate = DateTime.Now;
                UpdateDate = DateTime.Now;
            }
        }
    }
}
