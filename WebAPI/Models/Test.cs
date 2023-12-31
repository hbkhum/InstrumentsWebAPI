﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class Test : AbstractTest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid TestId { get; set; }
    public Guid GroupTestId { get; set; }
    public GroupTest? GroupTest { get; set; }
}