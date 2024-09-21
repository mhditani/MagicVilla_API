﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.DTO
{
    public class VillaNumberCreateDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]

        public int VillaId { get; set; }

        public string SpecialDetails { get; set; } = "";
    }
}
