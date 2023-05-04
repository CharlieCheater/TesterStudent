﻿namespace TesterStudent.Client.Models;

public class Parameter
{
    public Parameter(string name, string value)
    {
        Name = name;
        Value = value;
    }
    public string Name { get; set; }
    public string Value { get; set; }
    public string GetFullValue() => Name + "=" + Value;
}
