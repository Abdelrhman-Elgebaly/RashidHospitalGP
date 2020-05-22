namespace Hospital.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<CallBoard> CallBoards { get; set; }
        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<Diagons> Diagonses { get; set; }
        public virtual DbSet<IHC> IHCs { get; set; }
        public virtual DbSet<LabResualt> LabResualts { get; set; }
        public virtual DbSet<MedicalCondition> MedicalConditions { get; set; }
        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }
        public virtual DbSet<MenstrualHistory> MenstrualHistories { get; set; }
        public virtual DbSet<Pathology> Pathologies { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientUnit> PatientUnits { get; set; }
        public virtual DbSet<RadiologyRequest> RadiologyRequests { get; set; }
        public virtual DbSet<RadiologyResult> RadiologyResults { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<SurgicalCondition> SurgicalConditions { get; set; }
        public virtual DbSet<LabOrder> LabOrders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasMany(e => e.CallBoards)
                .WithRequired(e => e.Appointment)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.MedicalRecords)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.DoctorID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.RadiologyRequests)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
               .HasMany(e => e.Pathologies)
               .WithRequired(e => e.Doctors)
               .HasForeignKey(e => e.DoctorId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
              .HasMany(e => e.LabOrders)
               .WithRequired(e => e.AspNetUser)
              .HasForeignKey(e => e.DoctorId)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
               .HasMany(e => e.LabResualts)
               .WithRequired(e => e.Doctor)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
              .HasMany(e => e.RadiologyResults)
              .WithRequired(e => e.AspNetUser)
              .HasForeignKey(e => e.CreatedBy)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clinic>()
                .HasMany(e => e.Appointments)
                .WithRequired(e => e.Clinic)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clinic>()
                .HasMany(e => e.MedicalRecords)
                .WithRequired(e => e.Clinic)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Diagons>()
                .HasMany(e => e.Patients)
                .WithRequired(e => e.Diagons)
                .HasForeignKey(e => e.DiagnoseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Appointments)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.CallBoards)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.IHCs)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.LabResualts)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
               .HasMany(e => e.LabOrders)
               .WithRequired(e => e.Patient)
               .HasForeignKey(e=>e.PatinentId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.MedicalConditions)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.MedicalRecords)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.MenstrualHistories)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.RadiologyRequests)
                .WithRequired(e => e.Patient)
                .HasForeignKey(e => e.PateintID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.RadiologyResults)
                .WithRequired(e => e.Patient)
                .HasForeignKey(e => e.PateintID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Requests)
                .WithRequired(e => e.Patient)
                .HasForeignKey(e => e.PateintID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.SurgicalConditions)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PatientUnit>()
                .HasMany(e => e.Patients)
                .WithRequired(e => e.PatientUnit)
                .HasForeignKey(e => e.UnitID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RadiologyResult>()
                .Property(e => e.M)
                .IsFixedLength();

            modelBuilder.Entity<LabOrder>()
             .HasMany(e => e.OrderDetails)
             .WithRequired(e => e.Orders)
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pathology>()
                .HasMany(e => e.IHCS)
                .WithRequired(e => e.Pathologies)
                .HasForeignKey(e => e.PathologyId)
                .WillCascadeOnDelete(false);

        }


    }
}
