﻿using System.ComponentModel.DataAnnotations;

namespace Services.Users.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "Solo se permite un maximo de 50 caracteres")]
        public string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El cargo es requerido")]
        public int positionId { get; set; }
    }
}
