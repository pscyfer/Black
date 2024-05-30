using AutoMapper;
using Common.Application;
using Common.Application.DataTableConfig;
using Common.Application.Exceptions;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using TicketModule.Entities;
using TicketModule.Services.DTOs.Command.Ticket;
using TicketModule.Services.DTOs.Command.TicketMessage;
using TicketModule.Services.DTOs.Query;
using TicketModule.ViewModel;

namespace TicketModule.Services
{
    public class TicketService : ITicketService
    {
        private readonly TicketModuleContext _context;
        private readonly IMapper _mapper;

        public TicketService(TicketModuleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OperationResult> CreateTicketAsync(CreateTicketCommandDto command, CancellationToken cancellationToken)
        {
            try
            {
                var createTicket = _mapper.Map<Ticket>(command);
                await _context.Tickets.AddAsync(createTicket, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return OperationResult.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<OperationResult> UpdateTicketAsync(UpdateTicketCommandDto command, CancellationToken cancellationToken)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(command.Id);
                if (ticket == null) OperationResult.Error();
                ticket.Title = command.Title;
                ticket.OwnerFullName = command.FullName;
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync(cancellationToken);
                return OperationResult.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<OperationResult> CreateTicketMessageAsync(CreateClientTicketMessageCommandDto command, CancellationToken cancellationToken)
        {
            try
            {
                var createTicketMessage = _mapper.Map<TicketMessage>(command);
                var ticket = await _context.Tickets.FindAsync(command.TicketId);
                if (ticket is not null)
                {
                    ticket.State = TicketState.Pending;
                    createTicketMessage.OperationSend = OperationSend.Client;

                    await _context.TicketMessages.AddAsync(createTicketMessage, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return OperationResult.Success();
                }
                return OperationResult.NotFound("تیکت پیدا نشد.");
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<OperationResult> CreateTicketMessageAsync(CreateOperatorTicketMessageCommandDto command, CancellationToken cancellationToken)
        {
            try
            {
                var createTicketMessage = _mapper.Map<TicketMessage>(command);
                var ticket = await _context.Tickets.FindAsync(command.TicketId);
                if (ticket is not null)
                {
                    createTicketMessage.OperationSend = OperationSend.Operator;
                    ticket.State = TicketState.Answered;
                    await _context.TicketMessages.AddAsync(createTicketMessage, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return OperationResult.Success();
                }
                return OperationResult.NotFound("تیکت پیدا نشد.");
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<OperationResult> DeleteTicketAsync(RequestQueryById request)
        {
            try
            {
                var findTicket = await _context.Tickets.FindAsync(request.Identifier);
                if (findTicket == null) return OperationResult.NotFound();
                _context.Tickets.Remove(findTicket);
                await _context.SaveChangesAsync();
                return OperationResult.Success();
            }
            catch (BaseApplicationExceptions)
            {

                throw;
            }
        }

        public async Task<OperationResult<TicketViewModel>> GetTicketViewModel(RequestQueryById requestQueryById)
        {
            try
            {
                var query = await _context.Tickets.AsNoTracking().FirstOrDefaultAsync(x => x.Id == requestQueryById.Identifier);
                if (query is null) return OperationResult<TicketViewModel>.NotFound();
                var mapped = _mapper.Map<TicketViewModel>(query);

                return OperationResult<TicketViewModel>.Success(mapped);
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult<TicketViewModel>> GetForRemoveAsync(RequestQueryById requestQueryById)
        {
            try
            {
                var ticket = await _context.Tickets.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == requestQueryById.Identifier);

                return OperationResult<TicketViewModel>.Success(new TicketViewModel()
                {
                    Id = ticket.Id,
                    Title = ticket.Title
                });
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult> AddMessageToTicket(CreateOperatorTicketMessageCommandDto command)
        {
            try
            {
                if (!await _context.Tickets.AsNoTracking().AnyAsync(x => x.Id == command.TicketId))
                    return OperationResult.NotFound();
                var ticket = await _context.Tickets.FindAsync(command.TicketId);
                var addedMessageToTicket = new TicketMessage()
                {
                    CreationDate = DateTime.Now,
                    FileAttachment = command.FileAttachment,
                    Message = command.Message,
                    OperationSend = OperationSend.Operator,
                    UserId = command.UserId,
                    TicketId = command.TicketId,
                    UserFullName = command.FullName
                };
                ticket.State = TicketState.Answered;
                await _context.AddAsync(addedMessageToTicket);
                _context.Tickets.Update(ticket);

                await _context.SaveChangesAsync();

                return OperationResult.NotFound();
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult> AddMessageToTicket(CreateClientTicketMessageCommandDto command)
        {
            try
            {
                if (!await _context.Tickets.AsNoTracking().AnyAsync(x => x.Id == command.TicketId))
                    return OperationResult.NotFound();
                var ticket = await _context.Tickets.FindAsync(command.TicketId);
                var addedMessageToTicket = new TicketMessage()
                {
                    CreationDate = DateTime.Now,
                    FileAttachment = command.FileAttachment,
                    Message = command.Message.SanitizeText(),
                    OperationSend = OperationSend.Client,
                    UserId = command.UserId,
                    TicketId = command.TicketId,
                    UserFullName = command.FullName

                };
                if (ticket != null)
                {
                    ticket.State = TicketState.Pending;
                    await _context.AddAsync(addedMessageToTicket);
                    _context.Tickets.Update(ticket);
                }

                await _context.SaveChangesAsync();

                return OperationResult.NotFound();
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult> CloseTicket(RequestQueryById request)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(request.Identifier);
                if (ticket is null) return OperationResult.NotFound();
                ticket.State = TicketState.Closed;
                await _context.SaveChangesAsync();
                return OperationResult.Success();
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<PaginationDataTableResult<TicketViewModel>> GetTicketPaggingAsync(FiltersFromRequestDataTableBase request, CancellationToken cancellationToken)
        {
            try
            {
                OperationResult<List<GetDetaileTicketQueryDto>> operationResult;
                var query = _context.Tickets.AsNoTracking().AsQueryable();
                if (!(string.IsNullOrEmpty(request.sortColumn) && string.IsNullOrEmpty(request.sortColumnDirection)))
                {
                    query = query.OrderBy(request.sortColumn + " " + request.sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(request.searchValue))
                {
                    query = query.Where(x =>
                        x.Title.Contains(request.searchValue));
                }

                var recordsFiltered = await query.CountAsync(cancellationToken: cancellationToken);
                var queryResult = await query.Skip(request.skip).Take(request.pageSize)
                    .ToListAsync(cancellationToken: cancellationToken);


                var userDtoList = _mapper.Map<List<TicketViewModel>>(queryResult);
                int recordsTotal = await _context.Tickets.CountAsync(cancellationToken: cancellationToken);
                PaggingDataTableExtention.ConfigPaging(ref request, recordsTotal);
                var result = new PaginationDataTableResult<TicketViewModel>()
                {
                    draw = request.draw,
                    recordsFiltered = recordsFiltered,
                    recordsTotal = recordsTotal,
                    data = userDtoList
                };
                return result;
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<OperationResult<GetDetaileTicketQueryDto>> GetTicketWithMessagesQueryAsync(Guid ticketId, CancellationToken cancellationToken)
        {
            try
            {

                var getTicketWithMessages = await _context.Tickets.AsNoTracking()
                    .Include(c => c.Messages.OrderBy(x => x.CreationDate))
                      .FirstOrDefaultAsync(x => x.Id == ticketId, cancellationToken: cancellationToken);

                var mapped = _mapper.Map<GetDetaileTicketQueryDto>(getTicketWithMessages);
                mapped.Messages.ForEach(x => x.FileAttachment = x.FileAttachment.GenerateStaticUrl());
                if (mapped is not null)
                    return OperationResult<GetDetaileTicketQueryDto>.Success(mapped);

                return OperationResult<GetDetaileTicketQueryDto>.NotFound();
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }
    }
}
