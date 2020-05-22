namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Request
    {
        public int Id { get; set; }

        [Required]
        public string Note { get; set; }

        public int AppointmentId { get; set; }

        public bool FastingBloodSugar { get; set; }

        public bool PPBloodSugar { get; set; }

        public bool RandomBloodSugar { get; set; }

        public bool SerumUrea { get; set; }

        public bool SerumCeratinine { get; set; }

        public bool SerumUricAcid { get; set; }

        public bool SerumTotalCalcium { get; set; }

        public bool SerumPhosphorus { get; set; }

        public bool SerumNA { get; set; }

        public bool SerumK { get; set; }

        public bool SerumChloride { get; set; }

        public bool DirectBilirubin { get; set; }

        public bool TotalBilirubin { get; set; }

        public bool SGOTAST { get; set; }

        public bool SGOTALT { get; set; }

        public bool SerumTotalProtiens { get; set; }

        public bool SerumAlbumin { get; set; }

        public bool AlkalinePhosphatase { get; set; }

        public bool GGT { get; set; }

        public bool SerumCholesterol { get; set; }

        public bool SerumTriglycerides { get; set; }

        public bool HDLCholesterol { get; set; }

        public bool LDLCholesterol { get; set; }

        public bool CBK { get; set; }

        public bool CKMB { get; set; }

        public bool TropininTest { get; set; }

        public bool LDH { get; set; }

        public bool ProtinElectrophoresis { get; set; }

        public bool CommpleteBloodPicture { get; set; }

        public bool TotalWBCs { get; set; }

        public bool PlateletCount { get; set; }

        public bool HemopglibinConcPCV { get; set; }

        public bool HbElectrophoresis { get; set; }

        public bool SerumIron { get; set; }

        public bool TIBC { get; set; }

        public bool Ferritin { get; set; }

        public bool DirectCoombasTest { get; set; }

        public bool InDirectCoombasTest { get; set; }

        public bool BMAspirate { get; set; }

        public bool BMBiopsy { get; set; }

        public bool MmunophenotypingAcute { get; set; }

        public bool MmunophenotypingChronic { get; set; }

        public bool CytogeneticsFISH { get; set; }

        public bool CytogeneticsKaryotyping { get; set; }

        public bool ProthrombinTime { get; set; }

        public bool PTT { get; set; }

        public bool BleedingTime { get; set; }

        public bool CottingTime { get; set; }

        public bool FDPS { get; set; }

        public bool FibrinogenLevel { get; set; }

        public bool DDimer { get; set; }

        public bool AntiThrombinIII { get; set; }

        public bool ProteinC { get; set; }

        public bool ProteinS { get; set; }

        public bool LupusAnticogulant { get; set; }

        public bool VWFRistocetinCofactorAct { get; set; }

        public bool FactorIIVVIIX { get; set; }

        public bool FactorVIIIXXI { get; set; }

        public bool ESR { get; set; }

        public bool ASOT { get; set; }

        public bool CRP { get; set; }

        public bool RheumatoidFactor { get; set; }

        public bool RoseWaaler { get; set; }

        public bool ANA { get; set; }

        public bool AntiMitochondrialAb { get; set; }

        public bool AntiDsDNA { get; set; }

        public bool ANCA { get; set; }

        public bool C3 { get; set; }

        public bool C4 { get; set; }

        public bool SerumIgM { get; set; }

        public bool SerumIgG { get; set; }

        public bool T3 { get; set; }

        public bool T4 { get; set; }

        public bool TSH { get; set; }

        public bool FreeT4 { get; set; }

        public bool FreeT3 { get; set; }

        public bool LH { get; set; }

        public bool FSH { get; set; }

        public bool Prolactin { get; set; }

        public bool TotalTestosterone { get; set; }

        public bool FeerTestosterone { get; set; }

        public bool ProgesteroneP4 { get; set; }

        public bool EstradiolE2 { get; set; }

        public bool BhCG { get; set; }

        public bool CortisolAM { get; set; }

        public bool CortisolPM { get; set; }

        public bool VMA { get; set; }

        public bool HBsAg { get; set; }

        public bool HBsAb { get; set; }

        public bool HCVAb { get; set; }

        public bool HIVAb { get; set; }

        public bool CMVIgm { get; set; }

        public bool RubellaIgM { get; set; }

        public bool RubellaIgG { get; set; }

        public bool Toxoplasma { get; set; }

        public bool TORCH { get; set; }

        public bool AFP { get; set; }

        public bool CEA { get; set; }

        public bool CA15 { get; set; }

        public bool TotalPSA { get; set; }

        public bool CA125 { get; set; }

        public bool CA19 { get; set; }

        public bool CSFAnlysis { get; set; }

        public bool AsciticFluid { get; set; }

        public bool Pleural { get; set; }

        public bool Miscellaneous { get; set; }

        public bool CompleteUrineAnalysis { get; set; }

        public bool ProtineIn24HrUrine { get; set; }

        public bool CreatinineClearance { get; set; }

        public int PateintID { get; set; }
        public bool IsDeleted { get; set; }


        public virtual Patient Patient { get; set; }
    }
}
