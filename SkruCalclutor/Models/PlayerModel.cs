﻿using System.ComponentModel.DataAnnotations;

namespace SkruCalclutor.Models;

public class PlayerModel
{
    [Required(ErrorMessage = "Player name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
    public string Name { get; set; } = string.Empty;
}