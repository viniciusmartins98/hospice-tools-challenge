using HospiceToolsChallenge.Domain.Entities.Statistics;
using MediatR;

namespace HospiceToolsChallenge.Application.Queries.Patients
{
    public class GetPatientStatisticsQuery : IRequest<PatientStatistics>
    {
    }
}
