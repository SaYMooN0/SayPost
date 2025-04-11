using SayPostAuthService.Domain.unconfirmed_app_user_aggregate;

namespace SayPostAuthService.Application.configs;

public class FrontendConfig
{
    public string Url { get; init; }
    public string ConfirmRegistrationUrl { get; init; }

    public string GenerateConfirmationLink(UnconfirmedAppUser userToConfirm) =>
        $"{ConfirmRegistrationUrl}/{userToConfirm.Id}/{userToConfirm.ConfirmationCode}";

    public FrontendConfig(string url, string confirmRegistrationUrl) {
        Url = url;
        ConfirmRegistrationUrl = confirmRegistrationUrl;
    }
}