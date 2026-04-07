using OnlineLearningAngular.DataAccess.Settings;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface IEmailService
    {
        // Gửi email cơ bản
        Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default);

        // Gửi email kèm theo file đính kèm (ví dụ: Chứng chỉ khóa học)
        Task SendEmailWithAttachmentAsync(string to, string subject, string body, IEnumerable<AttachmentDto> attachments, CancellationToken ct = default);

        // Gửi email dựa trên Template (ví dụ: Welcome, Reset Password)
        Task SendTemplateEmailAsync(string to, string subject, string templateName, object model, CancellationToken ct = default);
    }
}
