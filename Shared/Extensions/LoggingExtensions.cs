using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class LoggingExtensions
    {
        public static string HttpInfo(this HttpRequest request, string message) => $"RequestId: {request.HttpContext.TraceIdentifier}, Info: {message} [ {DateTime.UtcNow.ToString("yyyy/MMM/dd - HH:mm:ss.fff")}]";
        public static string HttpException(this HttpRequest request ,Exception ex) => $"RequestId: {request.HttpContext.TraceIdentifier}, Reason: {ex.GetStackTraceRoot().Message} {DateTime.UtcNow.ToString("yyyy/MMM/dd - HH:mm:ss.fff")}";

    }
}
