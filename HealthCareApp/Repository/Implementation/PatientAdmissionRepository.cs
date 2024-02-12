using HealthcareApp.Models.DataModels;
using HealthcareApp.Models.Shared;
using HealthcareApp.Repository.Interface;
using HealthcareApp.Utils;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Repository.Implementation
{
    public class PatientAdmissionRepository : CrudRepository<PatientAdmission>, IPatientAdmissionRepository
    {
        public PatientAdmissionRepository(HealthcareDbContext context) : base(context)
        {
        }

        private IQueryable<PatientAdmission> GetDetailedQuery()
        {
            return _context.PatientAdmissions.Include(d => d.Doctor).Include(p => p.Patient);
        }

        private async Task CheckDoctorAndPatient(Guid? specialistId, Guid? patientId)
        {
            if (specialistId is not null)
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == specialistId && d.Title == DoctorTitle.Specialist && !d.IsDeleted);
                if (doctor is null)
                {
                    throw new DbObjectNotFound($"Selected specialist is not found! Please select specialist from refreshed list.");
                }
            }

            if (patientId is not null)
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == patientId && !d.IsDeleted);
                if (patient is null)
                {
                    throw new DbObjectNotFound($"Patient is not found! Please select patient from refreshed list.");
                }
            }
        }

        public async Task<List<PatientAdmission>> GetAllDetailedPatientAdmissions(DateTime? startDate, DateTime? endDate, Guid? patientId = null)
        {
            var query = GetDetailedQuery();
            if (startDate is not null)
            {
                query = query.Where(p => p.AdmissionDateTime > startDate);
            }
            if (endDate is not null)
            {
                query = query.Where(p => p.AdmissionDateTime < endDate);
            }
            if (patientId is not null)
            {
                query = query.Where(p => p.PatientId == patientId);
            }
            return await query.ToListAsync();
        }

        public async Task<PatientAdmission?> GetDetailedPatientAdmission(Guid id)
        {
            var query = GetDetailedQuery();
            return await query.FirstOrDefaultAsync(pa => pa.Id == id);
        }

        public override async Task Add(PatientAdmission entity)
        {
            await CheckDoctorAndPatient(entity.DoctorId, entity.PatientId);
            await base.Add(entity);
        }

        public override async Task Update(PatientAdmission entity)
        {
            if (!entity.IsCancelled)
            {
                await CheckDoctorAndPatient(entity.DoctorId, entity.PatientId);
            }
            await base.Update(entity);
        }
    }
}
