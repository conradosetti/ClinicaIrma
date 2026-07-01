using ClinicaIrma.Application.Interfaces;

namespace ClinicaIrma.Application.Auth.Commands.Login;

// Retorna uma string que será o nosso Token JWT
public class LoginCommandHandler : IHandler<string, LoginCommand>
{
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public Task<string> HandleAsync(LoginCommand item, CancellationToken cancellationToken)
    {
        // Validação estática para o MVP da clínica
        if (item.Email == "admin@clinicairma.com" && item.Senha == "senha123")
        {
            var token = _tokenService.GerarToken(item.Email);
            return Task.FromResult(token);
        }

        throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
    }
}