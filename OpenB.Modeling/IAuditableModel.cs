using OpenB.Core;

namespace OpenB.Modeling
{
    public interface IAuditableModel : IModel
    {
        AuditRegistration Created { get; set; }
        AuditRegistration Modified { get; set; }
        AuditRegistration Accessed { get; set; }
    }
}