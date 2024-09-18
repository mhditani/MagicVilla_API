﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_villaAPI.Models
{
    public class Villa
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; } = "";

        public DateTime CreatedDate { get; set; }
    }
}