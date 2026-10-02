using application_fh.Dtos.External.Aredl;
using domain_fh.Common.Result;
using domain_fh.Common.Result.Errors;

namespace application_fh.Interfaces.Infrastructure.Aredl;

public interface IAredlApiService
{
    Task<Result<AredlProfileDto, ApiError>> FetchProfileAsync(ulong discordId, CancellationToken ct = default);
}