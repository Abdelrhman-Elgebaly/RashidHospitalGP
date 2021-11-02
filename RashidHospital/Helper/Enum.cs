using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RashidHospital.Helper
{
    public class Enum
    {
        public enum HistoryType {
            Medical=1,
            Surgical=2,
            Family=3,
            Allergy=4
        }
        public enum _Gender
        {

            Male = 1,

            Female = 2,
        }
        public enum MedicalHistory
        {
            [Display(Name = "Diabetes")]
            Diabetes ,
            [Display(Name = "High Blood Pressure")]
            HighBloodPressure,
            [Display(Name = "High Cholesterol")]
            HighCholesterol,
            [Display(Name = "Hypothyroidism")]
            Hypothyroidism,
            [Display(Name = "Goiter")]
            Goiter,
            [Display(Name = "Cancer")]
            Cancer,
            [Display(Name = "Leukemia")]
            Leukemia,
            [Display(Name = "Psoriasis")]
            Psoriasis,
            [Display(Name = "Angina")]
            Angina,
            [Display(Name = "Heart Problems")]
            HeartProblems,
            [Display(Name = "Heart Murmur")]
            HeartMurmur,
            [Display(Name = "Pneumonia")]
            Pneumonia,
            [Display(Name = "Pulmonary Embolism")]
            PulmonaryEmbolism,
            [Display(Name = "Asthma")]
            Asthma,
            [Display(Name = "Emphysema")]
            Emphysema,
            [Display(Name = "Stroke")]

            Stroke,
            [Display(Name = "Epilepsy Seizures")]

            EpilepsySeizures,
            [Display(Name = "Cataracts")]

            Cataracts,
            [Display(Name = "Kidney Disease")]

            KidneyDisease,
            [Display(Name = "Kidney Stones")]

            KidneyStones,
            [Display(Name = "Crohn Disease")]

            CrohnDisease,
            [Display(Name = "Colitis")]

            Colitis,
            [Display(Name = "Anemia")]

            Anemia,
            [Display(Name = "Jaundice")]

            Jaundice,
            [Display(Name = "Hepatitis")]

            Hepatitis,
            [Display(Name = "Stomach Orpeptic Ulcer")]

            StomachOrpepticUlcer,
            [Display(Name = "Rheumatic Fever")]

            RheumaticFever,
            [Display(Name = "Tuberculosis")]

            Tuberculosis,
            [Display(Name = "HIVAIDS")]

            HIVAIDS,
            [Display(Name = "Allergy")]

            Allergy,
            [Display(Name = "Family History")]

            FamilyHistory,
            [Display(Name = "Hypertension")]

            Hypertension,
            [Display(Name = "Diabetes Mellitus")]

            DiabetesMellitus,
            [Display(Name = "Others")]

            Others
        }
        public enum SurgicalHistory {
            [Display(Name = "Abdominal Aortic Aneurysm Repair")]

            AbdominalAorticAneurysmRepair,
            [Display(Name = "Limb Amputation")]

            LimbAmputation,
            [Display(Name = "Appendix Surgery")]

            AppendixSurgery,
            [Display(Name = "AV Shunt For Dialysis")]

            AVShuntForDialysis,
            [Display(Name = "Bile Duct Liver Or Pancreatic Surgery")]

            BileDuctLiverOrPancreaticSurgery,
            [Display(Name = "Breast Surgery")]

            BreastSurgery,
            [Display(Name = "Cardiac Surgery")]

            CardiacSurgery,
            [Display(Name = "Coronary Bypass With Chest Donor Incisions")]

            CoronaryBypassWithChestDonorIncisions,
            [Display(Name = "Coronary Bypassz Graf tWith Chest Incision")]

            CoronaryBypasszGraftWithChestIncision,
            [Display(Name = "Carotid Endarterectomy")]

            CarotidEndarterectomy,
            [Display(Name = "Gallbladder Surgery")]

            GallbladderSurgery,
            [Display(Name = "Colon Surgery")]

            ColonSurgery,
            [Display(Name = "Craniotomy")]

            Craniotomy,
            [Display(Name = "Cesarean Section")]

            CesareanSection,
            [Display(Name = "Spinal Fusion")]

            SpinalFusion,
            [Display(Name = "Open Reduction Of Fracture")]

            OpenReductionOfFracture,
            [Display(Name = "Gastric Surgery")]

            GastricSurgery,
            [Display(Name = "Herniorrhaphy")]

            Herniorrhaphy,
            [Display(Name = "Hip Prosthesis")]

            HipProsthesis,
            [Display(Name = "Heart Transplant")]

            HeartTransplant,
            [Display(Name = "Abdominal Hysterectomy")]

            AbdominalHysterectomy,
            [Display(Name = "Knee Prosthesis")]

            KneeProsthesis,
            [Display(Name = "Kidney Transplant")]

            KidneyTransplant,
            [Display(Name = "Laminectomy")]

            Laminectomy,
            [Display(Name = "Liver Transplant")]

            LiverTransplant,
            [Display(Name = "Neck Surgery")]

            NeckSurgery,
            [Display(Name = "Kidney Surgery")]

            KidneySurgery,
            [Display(Name = "Ovarian Surgery")]

            OvarianSurgery,
            [Display(Name = "Pacemaker Surgery")]

            PacemakerSurgery,
            [Display(Name = "Prostate Surgery")]

            ProstateSurgery,
            [Display(Name = "Peripheral Vascular Bypass Surgery")]

            PeripheralVascularBypassSurgery,
            [Display(Name = "Rectal Surgery")]

            RectalSurgery,
            [Display(Name = "Small Bowel Surgery")]

            SmallBowelSurgery,
            [Display(Name = "Spleen Surgery")]

            SpleenSurgery,
            [Display(Name = "Thoracic Surgery")]

            ThoracicSurgery,
            [Display(Name = "Thyroid And Or Parathyroid Surgery")]

            ThyroidAndOrParathyroidSurgery,
            [Display(Name = "Vaginal Hysterectomy")]

            VaginalHysterectomy,
            [Display(Name = "Ventricular Shunt")]

            VentricularShunt,
            [Display(Name = "Exploratory Laparotomy")]

            ExploratoryLaparotomy,

        }
        public enum RadiologyProcedureType {
            [Display(Name = "US")]

            US = 1,
            [Display(Name = "CT With Contrast")]

            CTWithContrast,
            [Display(Name = "CT Without Contrast")]

            CTWithoutContrast,
            [Display(Name = "Triphasic CT")]

            TriphasicCT,
            [Display(Name = "CT Enterocolography")]

            CTEnterocolography,
            [Display(Name = "MRI")]

            MRI,
            [Display(Name = "XRay")]

            XRay,
            [Display(Name = "PETCT")]

            PETCT,
            [Display(Name = "RAI Scan")]

            RAIScan,
            [Display(Name = "SPECT")]

            SPECT,
            [Display(Name = "Bone Scan")]

            BoneScan,

        }
        public enum RadiologySite {
            [Display(Name = "Abdomen")]

            Abdomen=1,

            [Display(Name = "Abdomen and Pelvis")]
            AbdomenandPelvis,
            [Display(Name = "Breast")]

            Breast,
            [Display(Name = "Chest")]

            Chest,
            [Display(Name = "Head and Neck")]

            HeadanfNeck,
            [Display(Name = "Cardiac")]

            Cardiac,
            [Display(Name = "Upper Extremity Non joint")]

            UpperExtremityNonjoint,
            [Display(Name = "Upper Extremity Any joint")]

            UpperExtremityAnyJoint,
            [Display(Name = "Lower Extremity")]

            LowerExtremity,
            [Display(Name = "TMJ")]

            TMJ,
            [Display(Name = "Orbit")]

            Orbit,
            [Display(Name = "Brain")]

            Brain,
            [Display(Name = "Pelvis")]

            Pelvis,
            [Display(Name = "Whole spine")]

            Wholespine,
            [Display(Name = "Cervical spine")]

            CervicalSpine,
            [Display(Name = "Thoracic spine")]

            ThoracicSpine,
            [Display(Name = "Lumbar spine")]

            LumbarSpine,
            [Display(Name = "Upper Extremity")]

            UpperExtremity,
            [Display(Name = "Sinus")]

            Sinus,
            [Display(Name = "Base Of Skull")]

            BaseOfSkull,
            [Display(Name = "Neck")]

            Neck,
            [Display(Name = "Whole Body")]

            WholeBody,
            [Display(Name = "NCAP")]

            NCAP,
            [Display(Name = "CAP")]

            CAP,
            [Display(Name = "Enterocolongraphy")]

            Enterocolongraphy,
            [Display(Name = "Colonscopy")]

            colonscopy,
            [Display(Name = "Mammogram")]

            mammogram,
            [Display(Name = "Thoracoscopy")]

            thoracoscopy,
            [Display(Name = "Upper GIT endoscopy")]

            UpperGITendoscopy,
            [Display(Name = "Cystoscopy")]

            Cystoscopy
        }
        public enum LabTyps {
            [Display(Name = "Total WBCs")]
            TotalWBCs =1,
            [Display(Name = "Platelet Count")]
            PlateletCount,
            [Display(Name = "Hemopglibin Conc PCV")]
            HemopglibinConcPCV,
            [Display(Name = "Urea")]
            Urea,
            [Display(Name = "Ceratinine")]
            Ceratinine,
            [Display(Name = "Direct Bilirubin")]
            DirectBilirubin,
            [Display(Name = "Total Bilirubin")]
            TotalBilirubin,
            [Display(Name = "SGOT")]
            SGOT,
            [Display(Name = "SGPT")]
            SGPT,
            [Display(Name = "Alkaline Phosphatase")]
            AlkalinePhosphatase,
            [Display(Name = "GGT")]
            GGT,
            [Display(Name = "Serum Albumin")]
            SerumAlbumin,
            [Display(Name = "Total Protiens")]
            TotalProtiens,
            [Display(Name = "Serum Na")]
            SerumNa,
            [Display(Name = "Serum K")]
            SerumK,
            [Display(Name = "Serum Uric Acid")]
            SerumUricAcid,
            [Display(Name = "Serum Total Calcium")]
            SerumTotalCalcium,
            [Display(Name = "Serum Phosphorus")]
            SerumPhosphorus,
            [Display(Name = "Serum Chloride")]
            SerumChloride,
            [Display(Name = "Lipid profile")]
            Lipidprofile,
            [Display(Name = "Cholesterol")]
            Cholesterol,
            [Display(Name = "Triglgycerides")]
            Triglgycerides,
            [Display(Name = "HDL Cholesterol")]
            HDLCholesterol,
            [Display(Name = "LDL Cholesterol")]
            LDLCholesterol,
            [Display(Name = "Cardiac")]
            Cardiac,
            [Display(Name = "CBK")]
            CBK,
            [Display(Name = "CKMB")]
            CKMB,
            [Display(Name = "TropininTest")]
            TropininTest,
            [Display(Name = "LDH")]
            LDH,
            [Display(Name = "Fasting Blood Sugar")]
            FastingBloodSugar,
            [Display(Name = "PP Blood Sugar")]
            PPBloodSugar,
            [Display(Name = "Random Blood Sugar")]
            RandomBloodSugar,
            [Display(Name = "Protin Electrophoresis")]
            ProtinElectrophoresis,
            [Display(Name = "Serum Iron")]
            SerumIron,
            [Display(Name = "TIBC")]
            TIBC,
            [Display(Name = "Ferritin")]
            Ferritin,
            [Display(Name = "Direct Coombas Test")]
            DirectCoombasTest,
            [Display(Name = "InDirect Coombas Test")]
            InDirectCoombasTest,
            [Display(Name = "BM Aspirate")]
            BMAspirate,
            [Display(Name = "BM Biopsy")]
            BMBiopsy,
            [Display(Name = "ESR")]
            ESR,
            [Display(Name = "ASOT")]
            ASOT,
            [Display(Name = "CRP")]
            CRP,
            [Display(Name = "ProthrombinTime")]
            ProthrombinTime,
            [Display(Name = "PTT")]
            PTT,
            [Display(Name = "BleedingTime")]
            BleedingTime,
            [Display(Name = "Clotting Time")]
            ClottingTime,
            [Display(Name = "FDPS")]
            FDPS,
            [Display(Name = "Fibrinogen Level")]
            FibrinogenLevel,
            [Display(Name = "D Dimer")]
            DDimer,
            [Display(Name = "Anti ThrombinIII")]
            AntiThrombinIII,
            [Display(Name = "Protein C")]
            ProteinC,
            [Display(Name = "Protein S")]
            ProteinS,
            [Display(Name = "HBsAb")]
            HBsAb,
            [Display(Name = "HCVAb")]
            HCVAb,
            [Display(Name = "HIVAb")]
            HIVAb,
            [Display(Name = "CMVIgm")]
            CMVIgm,
            [Display(Name = "RubellaIgM")]
            RubellaIgM,
            [Display(Name = "RubellaIgG")]
            RubellaIgG,
            [Display(Name = "Toxoplasma")]
            Toxoplasma,
            [Display(Name = "TORCH")]
            TORCH,
            [Display(Name = "AFP")]
            AFP,
            [Display(Name = "CEA")]
            CEA,
            [Display(Name = "CA15.3")]
            CA15_3,
            [Display(Name = "Total PSA")]
            TotalPSA,
            [Display(Name = "CA125")]
            CA125,
            [Display(Name = "CA19.9")]
            CA19_9,
            [Display(Name = "BhCG")]
            BhCG,
            [Display(Name = "Rheumatoid Factor")]
            RheumatoidFactor,
            [Display(Name = "Rose Waaler")]
            RoseWaaler,
            [Display(Name = "ANA")]
            ANA,
            [Display(Name = "Anti Mitochondrial Ab")]
            AntiMitochondrialAb,
            [Display(Name = "Anti Ds DNA")]
            AntiDsDNA,
            [Display(Name = "ANCA")]
            ANCA,
            [Display(Name = "C3")]
            C3,
            [Display(Name = "C4")]
            C4,
            [Display(Name = "Serum IgM")]
            SerumIgM,
            [Display(Name = "Serum IgG")]
            SerumIgG,
            [Display(Name = "T3")]
            T3,
            [Display(Name = "T4")]
            T4,
            [Display(Name = "TSH")]
            TSH,
            [Display(Name = "FreeT4")]
            FreeT4,
            [Display(Name = "FreeT3")]
            FreeT3,
            [Display(Name = "LH")]
            LH,
            [Display(Name = "FSH")]
            FSH,
            [Display(Name = "Prolactin")]
            Prolactin,
            [Display(Name = "Total Testosterone")]
            TotalTestosterone,
            [Display(Name = "Feer Testosterone")]
            FeeTestosterone,
            [Display(Name = "Progesterone P4")]
            ProgesteroneP4,
            [Display(Name = "Estradiol E2")]
            EstradiolE2,
            [Display(Name = "Cortisol AM")]
            CortisolAM,
            [Display(Name = "CortisolPM")]
            CortisolPM,
            [Display(Name = "VMA")]
            VMA,
            [Display(Name = "HBsAg")]
            HBsAg,
        }

        public enum TumorHistology {
            [Display(Name = "Histology 1")]

            Histology1 = 1,
            [Display(Name = "Histology 2")]

            Histology2,
            [Display(Name = "Histology 3")]

            Histology3,
            [Display(Name = "Histology 4")]

            Histology4,
            [Display(Name = "Histology 5")]

            Histology5
        }
        public enum TumorGrade {
            [Display(Name = "Well Differentiated")]

            Well = 1,
            [Display(Name = "Moderate Differentiated")]

            Moderate,
            [Display(Name = "Poorly Differentiated")]

            Poorly,
            [Display(Name = "Undifferentiated")]

            Undifferentiated

        }
        public enum TumorFocality {
            [Display(Name = "Single")]

            Single = 1,
            [Display(Name = "Multifocal")]

            Multifocal,
            [Display(Name = "Multicentric")]

            Multicentric,

        }
        public enum TumorMargins {
            [Display(Name = "Free")]

            Free = 1,
            [Display(Name = "Focally Involved")]

            FocallyInvolved,
            [Display(Name = "Involved")]

            Involved,

        }
        //public enum IHCType {
        //    [Display(Name = "ACTH")]

        //    ACTH = 1,
        //    [Display(Name = "ACTH")]

        //    ACTIN,
        //    [Display(Name = "ADENOVIRUS")]

        //    ADENOVIRUS,
        //    [Display(Name = "AFP")]

        //    AFP,
        //    [Display(Name = "ALK-1")]

        //    ALK_1,
        //    [Display(Name = "AMYLOIDA")]

        //    AMYLOIDA,
        //    [Display(Name = "ANDROGEN RECEPTOR")]

        //    ANDROGENRECEPTOR,
        //    [Display(Name = "ANNEXIN")]

        //    ANNEXIN,
        //    [Display(Name = "ARGINASE-1")]

        //    ARGINASE_1,
        //    [Display(Name = "BAP1")]

        //    BAP1,
        //    [Display(Name = "B-AMYLOID")]

        //    B_AMYLOID,
        //    [Display(Name = "BCL-1")]

        //    BCL_1,
        //    [Display(Name = "BCL-2")]

        //    BCL_2,
        //    [Display(Name = "BCL-6")]

        //    BCL_6,
        //    [Display(Name = "BEREP4")]

        //    BEREP4,
        //    [Display(Name = "Beta Catenin")]

        //    Beta_Catenin,
        //    [Display(Name = "BOB-1")]

        //    BOB1,
        //    [Display(Name = "BRACHYURY")]

        //    BRACHYURY,
        //    [Display(Name = "BRST-2")]

        //    BRST_2,
        //    [Display(Name = "C3d")]

        //    C3d,
        //    [Display(Name = "C4d")]

        //    C4d,
        //    [Display(Name = "CALCITONIN")]

        //    CALCITONIN,
        //    [Display(Name = "CALDESMON")]

        //    CALDESMON,
        //    [Display(Name = "CALPONIN")]

        //    CALPONIN,
        //    [Display(Name = "CALRETININ")]

        //    CALRETININ,
        //    [Display(Name = "CD14")]

        //    CD14,
        //    [Display(Name = "CD117")]

        //    CD117,
        //    [Display(Name = "CD117BM")]

        //    CD117_BM,
        //    [Display(Name = "CD123")]

        //    CD123,
        //    [Display(Name = "CD138")]

        //    CD138,
        //    [Display(Name = "CD15")]

        //    CD15,
        //    [Display(Name = "CD163")]

        //    CD163,
        //    [Display(Name = "CD1a")]

        //    CD1a,
        //    [Display(Name = "CD2")]

        //    CD2,
        //    [Display(Name = "CD20")]

        //    CD20,
        //    [Display(Name = "CD21")]

        //    CD21,
        //    [Display(Name = "CD23")]

        //    CD23,
        //    [Display(Name = "CD25")]

        //    CD25,
        //    [Display(Name = "CD3")]

        //    CD3,
        //    [Display(Name = "CD30")]

        //    CD30,
        //    [Display(Name = "CD31")]

        //    CD31,
        //    [Display(Name = "CD33")]

        //    CD33,
        //    [Display(Name = "CD34")]

        //    CD34,
        //    [Display(Name = "CD34BM")]

        //    CD34BM,
        //    [Display(Name = "CD38")]

        //    CD38,
        //    [Display(Name = "CD4")]

        //    CD4,
        //    [Display(Name = "CD43")]

        //    CD43,
        //    [Display(Name = "CD45RA")]

        //    CD45RA,
        //    [Display(Name = "CD45RB")]

        //    CD45RB,
        //    [Display(Name = "CD45RO")]

        //    CD45RO,
        //    [Display(Name = "CD5")]

        //    CD5,
        //    [Display(Name = "CD56")]

        //    CD56,
        //    [Display(Name = "CD57")]

        //    CD57,
        //    [Display(Name = "CD61")]

        //    CD61,
        //    [Display(Name = "CD68")]

        //    CD68,
        //    [Display(Name = "CD7")]

        //    CD7,
        //    [Display(Name = "CD79a")]

        //    CD79a,
        //    [Display(Name = "CD8")]

        //    CD8,
        //    [Display(Name = "CD99")]

        //    CD99,
        //    [Display(Name = "CDX2")]

        //    CDX2,
        //    [Display(Name = "CEAm")]

        //    CEAm,
        //    [Display(Name = "CEAp")]

        //    CEAp,
        //    [Display(Name = "Chromogranin")]

        //    Chromogranin,
        //    [Display(Name = "Chymotrypsin")]

        //    Chymotrypsin,
        //    [Display(Name = "CK MIX")]

        //    CKMIX,
        //    [Display(Name = "CK20")]

        //    CK20,
        //    [Display(Name = "CK34BE12")]

        //    CK34BE12,
        //    [Display(Name = "CK5-6")]

        //    CK5_6,
        //    [Display(Name = "CK7")]

        //    CK7,
        //    [Display(Name = "CK19")]

        //    CK19,
        //    [Display(Name = "CKAE1-AE3")]

        //    CKAE1_AE3,
        //    [Display(Name = "CKCAM5-2")]

        //    CKCAM5_2,
        //    [Display(Name = "CMV")]

        //    CMV,
        //    [Display(Name = "c-Myc")]

        //    c_Myc,
        //    [Display(Name = "CXCL 13")]

        //    CXCL13,
        //    [Display(Name = "CYCLIN D3")]

        //    CYCLIND3,
        //    [Display(Name = "D2-40")]

        //    D2_40,
        //    [Display(Name = "DBA-44")]

        //    DBA_44,
        //    [Display(Name = "Desmin")]

        //    Desmin,
        //    [Display(Name = "DOG-1")]

        //    DOG_1,
        //    [Display(Name = "EBV-LMP")]

        //    EBV_LMP,
        //    [Display(Name = "E-Cadherin")]

        //    E_Cadherin,
        //    [Display(Name = "EGFR")]

        //    EGFR,
        //    [Display(Name = "EMA")]

        //    EMA,
        //    [Display(Name = "EMA-Perineurioma")]

        //    EMA_Perineurioma,
        //    [Display(Name = "ER")]

        //    ER,
        //    [Display(Name = "ERG")]

        //    ERG,
        //    [Display(Name = "Factor-13a")]

        //    Factor_13a,
        //    [Display(Name = "Factor-8")]

        //    Factor_8,
        //    [Display(Name = "FOXP1")]

        //    FOXP1,
        //    [Display(Name = "FSH")]

        //    FSH,
        //    [Display(Name = "GALECTIN-3")]

        //    GALECTIN_3,
        //    [Display(Name = "Gastrin")]

        //    Gastrin,
        //    [Display(Name = "GATA-3")]

        //    GATA_3,
        //    [Display(Name = "GFAP")]

        //    GFAP,
        //    [Display(Name = "GH")]

        //    GH,
        //    [Display(Name = "Glucagon")]

        //    Glucagon,
        //    [Display(Name = "Glut1")]

        //    Glut1,
        //    [Display(Name = "Glutamine Synthetase")]

        //    Glutamine_Synthetase,
        //    [Display(Name = "Glypican-3")]

        //    Glypican_3,
        //    [Display(Name = "GPC")]

        //    GPC,
        //    [Display(Name = "GRANZYME")]

        //    GRANZYME,
        //    [Display(Name = "H-pylori")]

        //    H_pylori,
        //    [Display(Name = "HBC")]

        //    HBC,
        //    [Display(Name = "HBS")]

        //    HBS,
        //    [Display(Name = "hCG")]

        //    hCG,
        //    [Display(Name = "HepPar1")]

        //    HepPar1,
        //    [Display(Name = "Her2neu")]

        //    Her2neu,
        //    [Display(Name = "HGAL")]

        //    HGAL,
        //    [Display(Name = "HHV-8")]

        //    HHV_8,
        //    [Display(Name = "HMB-45")]

        //    HMB_45,
        //    [Display(Name = "HPL")]

        //    HPL,
        //    [Display(Name = "HSV")]

        //    HSV,
        //    [Display(Name = "IDH1")]

        //    IDH1,
        //    [Display(Name = "IgA")]

        //    IgA,
        //    [Display(Name = "IgD")]

        //    IgD,
        //    [Display(Name = "IgG")]

        //    IgG,
        //    [Display(Name = "IgG4")]

        //    IgG4,
        //    [Display(Name = "IgM")]

        //    IgM,
        //    [Display(Name = "Inhibin")]

        //    Inhibin,
        //    [Display(Name = "INI")]

        //    INI,
        //    [Display(Name = "Insulin")]

        //    Insulin,
        //    [Display(Name = "ISH EBV")]

        //    ISH_EBV,
        //    [Display(Name = "ISH KAPPA")]

        //    ISH_KAPPA,
        //    [Display(Name = "ISH LAMBDA")]

        //    ISH_LAMBDA,
        //    [Display(Name = "KAPPA")]

        //    KAPPA,
        //    [Display(Name = "KBA62")]

        //    KBA62,
        //    [Display(Name = "KI67")]

        //    KI67,
        //    [Display(Name = "Lambda")]

        //    Lambda,
        //    [Display(Name = "LANGERIN")]

        //    LANGERIN,
        //    [Display(Name = "LAT")]

        //    LAT,
        //    [Display(Name = "LEF1")]

        //    LEF1,
        //    [Display(Name = "LH")]

        //    LH,
        //    [Display(Name = "LM02")]

        //    LM02,
        //    [Display(Name = "LYSOZYME")]

        //    LYSOZYME,
        //    [Display(Name = "MAP-2")]

        //    MAP_2,
        //    [Display(Name = "MCT")]

        //    MCT,
        //    [Display(Name = "MELANA")]

        //    MELANA,
        //    [Display(Name = "MITF")]

        //    MITF,
        //    [Display(Name = "MLH1")]

        //    MLH1,
        //    [Display(Name = "MNDA")]

        //    MNDA,
        //    [Display(Name = "MOC-31")]

        //    MOC_31,
        //    [Display(Name = "MPO")]

        //    MPO,
        //    [Display(Name = "MSH2")]

        //    MSH2,
        //    [Display(Name = "MSH6")]

        //    MSH6,
        //    [Display(Name = "MUC2")]

        //    MUC2,
        //    [Display(Name = "MUC5AC")]

        //    MUC5AC,
        //    [Display(Name = "MUM1")]

        //    MUM1,
        //    [Display(Name = "Myogenin")]

        //    Myogenin,
        //    [Display(Name = "NapsinA")]

        //    NapsinA,
        //    [Display(Name = "NB84")]

        //    NB84,
        //    [Display(Name = "NEU N")]

        //    NEUN,
        //    [Display(Name = "Neurofilament")]

        //    Neurofilament,
        //    [Display(Name = "NKI/C3")]

        //    NKI_C3,
        //    [Display(Name = "NKX3.1")]

        //    NKX31,
        //    [Display(Name = "NPM")]

        //    NPM,
        //    [Display(Name = "NSE")]

        //    NSE,
        //    [Display(Name = "NUT")]

        //    NUT,
        //    [Display(Name = "Oct2")]

        //    Oct2,
        //    [Display(Name = "Oct3/4")]

        //    Oct3_4,
        //    [Display(Name = "p16")]

        //    p16,
        //    [Display(Name = "p53")]

        //    p53,
        //    [Display(Name = "p57")]

        //    p57,
        //    [Display(Name = "p63")]

        //    p63,
        //    [Display(Name = "Parvovirus")]

        //    Parvovirus,
        //    [Display(Name = "PAX-2")]

        //    PAX_2,
        //    [Display(Name = "PAX-5")]

        //    PAX_5,
        //    [Display(Name = "PAX8")]

        //    PAX8,
        //    [Display(Name = "PD1")]

        //    PD1,
        //    [Display(Name = "Perforin")]

        //    Perforin,
        //    [Display(Name = "PHH3")]

        //    PHH3,
        //    [Display(Name = "PHLDA1")]

        //    PHLDA1,
        //    [Display(Name = "PIN4")]

        //    PIN4,
        //    [Display(Name = "PLAP")]

        //    PLAP,
        //    [Display(Name = "PMS2")]

        //    PMS2,
        //    [Display(Name = "PR")]

        //    PR,
        //    [Display(Name = "PRAP")]

        //    PRAP,
        //    [Display(Name = "Prolactin")]

        //    Prolactin,
        //    [Display(Name = "Prox1")]

        //    Prox1,
        //    [Display(Name = "PSA")]

        //    PSA,
        //    [Display(Name = "RNA")]

        //    RNA,
        //    [Display(Name = "S100")]

        //    S100,
        //    [Display(Name = "S100P")]

        //    S100P,
        //    [Display(Name = "SALL4")]

        //    SALL4,
        //    [Display(Name = "SF-1")]

        //    SF1,
        //    [Display(Name = "SMA")]

        //    SMA,
        //    [Display(Name = "SMMS")]

        //    SMMS,
        //    [Display(Name = "Somatostatin")]

        //    Somatostatin,
        //    [Display(Name = "SOX-10")]

        //    SOX10,
        //    [Display(Name = "SOX-11")]

        //    SOX11,
        //    [Display(Name = "Spirochetes")]

        //    Spirochetes,
        //    [Display(Name = "STAT6")]

        //    STAT6,
        //    [Display(Name = "SV40")]

        //    SV40,
        //    [Display(Name = "SYNAP")]

        //    SYNAP,
        //    [Display(Name = "Tamm-Horsfall")]

        //    Tamm_Horsfall,
        //    [Display(Name = "Tbet")]

        //    Tbet,
        //    [Display(Name = "TCL-1")]

        //    TCL_1,
        //    [Display(Name = "TCR")]

        //    TCR,
        //    [Display(Name = "TCR-Gamma")]

        //    TCR_Gamma,
        //    [Display(Name = "TdT")]

        //    TdT,
        //    [Display(Name = "THYRO")]

        //    THYRO,
        //    [Display(Name = "TIA-1")]

        //    TIA_1,
        //    [Display(Name = "TOXO")]

        //    TOXO,
        //    [Display(Name = "Transthyretin")]

        //    Transthyretin,
        //    [Display(Name = "TRAP")]

        //    TRAP,
        //    [Display(Name = "TSH")]

        //    TSH,
        //    [Display(Name = "TTF1")]

        //    TTF1,
        //    [Display(Name = "Tyrosinase")]

        //    Tyrosinase,
        //    [Display(Name = "VIMENTIN")]

        //    VIMENTIN,
        //    [Display(Name = "WT1")]

        //    WT1,
        //    [Display(Name = "WT1 (C-19)")]

        //    WT1_C19,
        //    [Display(Name = "ZAP70")]

        //    ZAP70,

        //}
        public enum RadiologyValues {
            [Display(Name = "High")]
            High =1,
            [Display(Name = "Normal")]
            Normal,
            [Display(Name = "Low")]
            Low,
            [Display(Name = "Decline")]
            Decline,
            [Display(Name = "Rise")]
            Rise
        }
        public enum RadiologyRecist {
            [Display(Name = "CR (complete response)")]
            CR,
            [Display(Name = "PR (Partial response)")]
            PR,
            [Display(Name = "SD (stable disease)")]
            SD,
            [Display(Name = "PD (progressive disease)")]
            PD,
            [Display(Name = "U/N (not identified)")]
            UN,
        }
        public enum ToxictyGrade {
            [Display(Name = "Grade 1")]
            grade1=1,
            [Display(Name = "Grade 2")]
            grade2 = 2,
            [Display(Name = "Grade 3")]
            grade3 = 3,
            [Display(Name = "Grade 4")]
            grade4 = 4,
            [Display(Name = "Grade 5")]
            grade5 = 5
        }
        public enum ToxictyCondition {
            [Display(Name = "Improved")]
            Improved = 1,
            [Display(Name = "Stable")]
            Stable = 2,
            [Display(Name = "Worse")]
            Worse = 3
        }
        public enum RTXSite {
            [Display(Name = "Site 1")]
            Site1 = 1,
            [Display(Name = "Site 2")]
            Site2 = 2,
            [Display(Name = "Site 3")]
            Site3 = 3,
            [Display(Name = "Site 4")]
            Site4 = 4,

        }
        public enum RTXType
        {
            [Display(Name = "Definitive")]
            Definitive = 1,

            [Display(Name = "Adjuvant")]
            Adjuvant = 2,

            [Display(Name = "Pallitive")]
            Pallitive = 3,

            [Display(Name = "Concomittant")]
            Concomittant = 4,

        }
        public enum TypeOfTechnique {
            [Display(Name = "TwoD")]
            TwoD = 1,
            [Display(Name = "ThreeD")]
            ThreeD = 2,
            [Display(Name = "IMRT")]
            IMRT = 3,
            [Display(Name = "VMAT")]
            VMAT = 4,
            [Display(Name = "SRS")]
            SRS = 5,
            [Display(Name = "SBRT")]
            SBRT = 6

        }
    }
}