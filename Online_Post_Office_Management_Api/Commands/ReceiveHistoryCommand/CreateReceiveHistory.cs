using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Commands.ReceiveHistoryCommand
{
    public class CreateReceiveHistory : IRequest<bool>
    {
        public ReceiveHistory ReceiveHistory { get; set; }

        public CreateReceiveHistory(ReceiveHistory history)
        {
            ReceiveHistory = history;
        }
    }
}
