using System;

namespace WebGallery.Common.Services.Interfaces
{
    public interface ILoggingService
    {
        void ConfigureLogging();
        void Debug(string message, Exception exception = null);
        void Error(string message, Exception exception = null);
        void Fatal(string message, Exception exception = null);
        void Info(string message, Exception exception = null);
        void Trace(string message, Exception exception = null);
        void Warn(string message, Exception exception = null);
    }
}