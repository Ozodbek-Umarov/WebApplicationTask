﻿namespace WebApplicationTask.Application.Common.Exceptions.entity;

public class ErrorMessage
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}
