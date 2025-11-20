using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AgencyApplication : BaseEntity
    {
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string TaxNumber { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }

        public string TaxDocumentPath { get; set; }
        public string SignatureCircularPath { get; set; }
        public string? TradeRegistryPath { get; set; }

        public bool IsApproved { get; set; } = false;
        public DateTime AppliedDate { get; set; } = DateTime.Now;
        public virtual Customer User { get; set; }
    }
}
