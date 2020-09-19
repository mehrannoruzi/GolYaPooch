using GolPooch.Domain.Enum;

namespace GolPooch.Domain.Dto
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }

        public int? UserId { get; set; }

        public NotificationType Type { get; set; }

        public NotificationAction Action { get; set; }

        public bool IsRead { get; set; }

        public string ActionText { get; set; }

        public string Subject { get; set; }

        public string ImageUrl { get; set; }

        public string IconUrl { get; set; }

        public string ActionUrl { get; set; }

        public string Text { get; set; }
    }
}