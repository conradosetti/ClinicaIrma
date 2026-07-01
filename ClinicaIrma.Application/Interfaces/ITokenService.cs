namespace ClinicaIrma.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(string email);
}