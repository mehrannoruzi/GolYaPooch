using System.Collections.Generic;

namespace GolPooch.Domain.Dto
{
    public class SpendChanceResultDto
    {
        public int AllParticipantCount { get; set; }
        public int CurrentParticipantCount { get; set; }
        public int ChanceOnRound { get; set; }
        public List<string> DrawCodes { get; set; }
    }
}