using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using WebGallery.Common.Enums;
using WebGallery.Common.Helpers.Interfaces;
using WebGallery.Common.Services.Interfaces;

namespace WebGallery.Common.Services
{
    public class SeriLogLoggingService : ILoggingService
    {
        private readonly IFileHelper fileHelper;
        private ILogger logger;

        public SeriLogLoggingService(IFileHelper fileHelper)
        {
            this.fileHelper = fileHelper;
            this.ConfigureLogging();
        }

        public void ConfigureLogging()
        {
            Log.Logger = this.logger = new LoggerConfiguration()
      .Enrich.FromLogContext()
      .WriteTo.Debug()
      .WriteTo.File(this.fileHelper.GetFilePath(Enums.DirectoryType.Logging, "log.txt"), rollingInterval: RollingInterval.Day)
      .CreateLogger();
        }

        public void Trace(string message, Exception exception = null)
        {
            this.logger.Verbose(exception, message);
        }

        public void Debug(string message, Exception exception = null)
        {
            this.logger.Debug(exception, message);
        }

        public void Info(string message, Exception exception = null)
        {
            this.logger.Information(exception, message);
        }

        public void Warn(string message, Exception exception = null)
        {
            this.logger.Warning(exception, message);
        }

        public void Error(string message, Exception exception = null)
        {
            this.logger.Error(exception, message);
        }

        public void Fatal(string message, Exception exception = null)
        {
            this.logger.Fatal(exception, message);
        }
    }
}
