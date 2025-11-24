using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CustomerDTOs
{
    public record AgencyApplicationDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public string CompanyName { get; init; }
        public string ContactPerson { get; init; }
        public string Phone { get; init; }
        public bool IsApproved { get; init; }

        public string TaxDocumentPath { get; init; }
        public string SignatureCircularPath { get; init; }
        public string? TradeRegistryPath { get; init; }

        public DateTime AppliedDate { get; init; }
    }
}
