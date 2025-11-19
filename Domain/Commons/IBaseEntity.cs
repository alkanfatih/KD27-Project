using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commons
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? UpdateDate { get; set; }
        DateTime? DeletedDate { get; set; }
        bool IsDeleted { get; set; }

        void SoftDelete();
        void Restore();
    }
}
