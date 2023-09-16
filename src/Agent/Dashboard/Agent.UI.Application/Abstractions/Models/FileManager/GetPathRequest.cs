namespace Agent.UI.Application.Abstractions.Models.FileManager;

public sealed record GetPathRequest(string Root, string? Path = null);