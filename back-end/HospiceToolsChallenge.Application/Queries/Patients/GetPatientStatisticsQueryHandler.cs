using HospiceToolsChallenge.Application.Repositories;
using HospiceToolsChallenge.Domain.Entities.Statistics;
using MediatR;

namespace HospiceToolsChallenge.Application.Queries.Patients
{
    public class GetPatientStatisticsQueryHandler(IPatientRepository repository) : IRequestHandler<GetPatientStatisticsQuery, PatientStatistics>
    {
        public async Task<PatientStatistics> Handle(GetPatientStatisticsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetPatientStatisticsAsync(cancellationToken);
        }
    }
}
