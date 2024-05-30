using AutoMapper;
using Common.AspNetCore.Extensions;
using TicketModule.Entities;
using TicketModule.Services.DTOs.Command.Ticket;
using TicketModule.Services.DTOs.Command.TicketMessage;
using TicketModule.Services.DTOs.Query;
using TicketModule.ViewModel;

namespace TicketModule.AutoMapperProfiles
{
    internal class TicketProfile : Profile
    {
        public TicketProfile()
        {

            CreateMap<Ticket, CreateTicketCommandDto>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => x.OwnerFullName))
                .ForMember(x => x.UserCreatedId, opt => opt.MapFrom(x => x.UserId))
                .ReverseMap();
            CreateMap<Ticket, UpdateTicketCommandDto>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => x.OwnerFullName))
                .ForMember(x => x.UserCreatedId, opt => opt.MapFrom(x => x.UserId))
                .ReverseMap();

            CreateMap<TicketMessage, CreateClientTicketMessageCommandDto>()
                .ForMember(x => x.TicketId, opt => opt.MapFrom(x => x.TicketId))
                .ForMember(x => x.FileAttachment, opt => opt.MapFrom(x => x.FileAttachment))
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => x.UserFullName))
                .ForMember(x => x.Message, opt => opt.MapFrom(x => x.Message))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId))
                .ReverseMap();

            CreateMap<TicketMessage, CreateOperatorTicketMessageCommandDto>()
                .ForMember(x => x.TicketId, opt => opt.MapFrom(x => x.TicketId))
                .ForMember(x => x.FileAttachment, opt => opt.MapFrom(x => x.FileAttachment))
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => x.UserFullName))
                .ForMember(x => x.Message, opt => opt.MapFrom(x => x.Message))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId))
                .ReverseMap();


            CreateMap<Ticket, GetDetaileTicketQueryDto>()
                .ForMember(x => x.Messages, opt => opt.MapFrom(x => x.Messages))
                .ForMember(x => x.TicketId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.TicketTitle, opt => opt.MapFrom(x => x.Title)).ReverseMap();

            CreateMap<GetTicketMessageQueryDto, TicketMessage>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(x => x.FileAttachment, opt => opt.MapFrom(x => x.FileAttachment))
                .ForMember(x => x.Message, opt => opt.MapFrom(x => x.Message))
                .ForMember(x => x.CreationDate, opt => opt.MapFrom(x => x.SendDate))
                .ForMember(x => x.OperationSend, opt => opt.MapFrom(x => x.OperationSend)).ReverseMap();

            CreateMap<Ticket,TicketViewModel > ()
                .ForPath(x => x.State, opt => opt.MapFrom(x => x.State.ToDisplay(DisplayProperty.Name)))
                .ReverseMap();
         
        }
    }
}
