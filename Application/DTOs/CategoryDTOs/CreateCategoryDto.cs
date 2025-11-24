using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CategoryDTOs
{
    public record CreateCategoryDto
    {
        [Required(ErrorMessage = "Katagori ad alanı boş geçilemez!")]
        public string Name { get; init; }
    }
}
