using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yes_Chef.Models.Interfaces;

public class RecipeTag
{
    public int RecipeID { get; set; }
    public Recipe Recipe { get; set; }

    public int TagID { get; set; }
    public Tag Tag { get; set; }
}
