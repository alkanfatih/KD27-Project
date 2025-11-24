using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CategoryDTOs
{
    public record UpdateCategoryDto : BaseDto
    {
        public string Name { get; init; }
    }
}
