using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Room.Commands.CreateRoom
{
    public class CreateRoomCommand : IRequest<CreateRoomResponse>
    {
        public RoomDto CreateRoom { get; set; }
    }
}
