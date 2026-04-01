namespace TrackWizzSaaS.Ckyc.Test.CkycSearchAndDownload.QuickFetch.Helpers;

public class CkycSearchInputFieldProperties
{
    //Common Fields
    public string CkycNumber { get; set; }
    public string CkycReferenceId { get; set; }
    public string Pan { get; set; }

    //Individual Fields
    public string VoterId { get; set; }
    public string Passport { get; set; }
    public string DrivingLicense { get; set; }

    //Aadhaar Search Fields
    public string AadhaarLastFourDigits { get; set; }
    public string FullName { get; set; }
    public string DateOfBirth { get; set; }
    public string Gender { get; set; }

    //Legal Entity (LE) Fields
    public string CinNumber { get; set; }
    public string CrnNumber { get; set; }
}
