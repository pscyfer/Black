using Common.Application;
using Common.Application.Exceptions;
using Microsoft.Extensions.Logging;
using Monitoring.Abstractions.DTOs.ResponseTime;
using Monitoring.Abstractions.Interfaces;
using Monitoring.Core;
using Monitoring.Core.Entities;

namespace Monitoring.EF_Services
{
    internal class ResponsTimeService : IResponsTimeService
    {
        private readonly MonitorDbContext _context;
        private readonly ILogger<ResponsTimeService> _logger;
        public ResponsTimeService(MonitorDbContext context, ILogger<ResponsTimeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OperationResult> CreateResponsTime(CreateResponsTimeDto command)
        {
            try
            {
                _logger.LogInformation("Insert responsTime for monitor Id={0}  responsTimeValue={1}", command.MonitorId, command.ResponsTime);
                if (command == null) return OperationResult.Error("null values ....");
                var createResponsTime = new ResponsTimeMonitor(command.MonitorId, command.ResponsTime,command.CreateAt);

                await _context.ResponsOfRequests.AddAsync(createResponsTime);

                await _context.SaveChangesAsync();

                return OperationResult.Success();
            }
            catch (BaseApplicationExceptions e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
