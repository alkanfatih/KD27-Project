using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CustomerDTOs
{
    public record MyAgencyApplicationDto
    {
        public string CompanyName { get; init; }
        public string TaxNumber { get; init; }
        public string Phone { get; init; }
        public string ContactPerson { get; init; }

        public string TaxDocumentPath { get; init; }
        public string SignatureCircularPath { get; init; }
        public string? TradeRegistryPath { get; init; }

        public bool IsApproved { get; init; }
        public DateTime AppliedDate { get; init; }
    }
}
