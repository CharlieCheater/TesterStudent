﻿namespace TesterStudent.Infrastructure.Models;

public class Test
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Variant> Variants { get; set; }
}