using SharedKernel.common.errs;

namespace SayPostAuthService.Domain.common.interfaces;
public interface IEmailService
{
    Task<ErrOrNothing> SendRegistrationConfirmationLink(Email email, string link);
}
