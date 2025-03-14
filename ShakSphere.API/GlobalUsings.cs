﻿global using Asp.Versioning;
global using AutoMapper;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using ShakSphere.Application.Contracts.Posts.Request;
global using ShakSphere.Application.Contracts.Posts.Response;
global using ShakSphere.Application.UseCases.Posts.Command;
global using ShakSphere.Application.UseCases.Posts.Query;
global using ShakSphere.Application.Contracts.UserProfile.Request;
global using ShakSphere.Application.UseCases.AppUserProfile.Commands;
global using ShakSphere.Application.Contracts.UserProfile.Response;
global using ShakSphere.Application.UseCases.AppUserProfile.Queries;
global using ShakSphere.API.Filters;
global using ShakSphere.Application.Behaviors;
global using Asp.Versioning.ApiExplorer;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using Microsoft.AspNetCore.Diagnostics;
global using ShakSphere.Domain.CustomExceptions;
global using System.Net;
global using Microsoft.AspNetCore.Mvc.Filters;
global using ShakSphere.API.Configuration.DependencyInjection.Abstractions;
global using ShakSphere.API.Options;
global using FluentValidation;
global using System.Reflection;
global using Microsoft.EntityFrameworkCore;
global using ShakSphere.Application.DataInterface;
global using ShakSphere.Infrastructure.Data;
global using ShakSphere.Application.Models;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using Microsoft.AspNetCore.Identity;


global using ShakSphere.API.Configuration.DependencyInjection.Implementations;
global using ShakSphere.API.Configuration.Middleware.Implementations;
