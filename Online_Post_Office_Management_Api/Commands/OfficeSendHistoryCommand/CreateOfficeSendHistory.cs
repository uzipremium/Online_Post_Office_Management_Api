using MediatR;
using MongoDB.Bson;

namespace Online_Post_Office_Management_Api.Commands.OfficeSendHistoryCommand
{
    public class CreateOfficeSendHistory : IRequest<bool>
    {
        public string ReceiveId { get; set; }
        public string OfficeId { get; set; }
        public DateTime SendDate { get; set; }

        public CreateOfficeSendHistory(string receiveId, string officeId, DateTime sendDate)
        {
            ReceiveId = receiveId;
            OfficeId = officeId;
            SendDate = sendDate;
        }
    }
}
