using application_fh.Dtos.External.Aredl;
using domain_fh.Common.Result;
using domain_fh.Common.Result.Errors;

namespace application_fh.Interfaces.Application.Aredl;

public interface IStartLinkAredl
{
    Task<Result<AredlProfileDto, ApiError>> FetchAsync(ulong discordId, CancellationToken ct = default);
}