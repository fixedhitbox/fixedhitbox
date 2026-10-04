using application_fh.Dtos.External.Aredl;
using application_fh.Interfaces.Application.Aredl;
using application_fh.Interfaces.Infrastructure.Aredl;
using domain_fh.Common.Result;
using domain_fh.Common.Result.Errors;

namespace application_fh.UseCases.Aredl;

internal sealed class StartLinkAredl(IAredlApiService aredlApi) : IStartLinkAredl
{
    public async Task<Result<AredlProfileDto, ApiError>> FetchAsync(ulong discordId, CancellationToken ct = default)
    { 
        return await aredlApi.FetchProfileAsync(discordId, ct);
    }
}