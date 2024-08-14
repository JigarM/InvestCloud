using InvestCloud.Interfaces;
using InvestCloud.Models;

namespace InvestCloud.ViewModels;

public class SignUpViewModel : BaseViewModel
{
    
    public bool IsValidFirstName { get; set; }
    public bool IsValidLastName { get; set; }
    public bool IsValidPassword { get; set; }
    public bool IsValidPhoneNumber { get; set; }
    public bool IsValidServiceStartDate { get; set; }
    
    public string FirstName
    {
        get => m_FirstName;
        set => SetProperty(ref m_FirstName, value);
    }
    private string m_FirstName;
    
    public string LastName
    {
        get => m_LastName;
        set => SetProperty(ref m_LastName, value);
    }
    private string m_LastName;
    
    public string Username
    {
        get => m_Username;
        set => SetProperty(ref m_Username, value);
    }
    private string m_Username;
    
    public string Password
    {
        get => m_Password;
        set => SetProperty(ref m_Password, value);
        
    }
    private string m_Password;
    
    public string PhoneNumber
    {
        get => m_PhoneNumber;
        set => SetProperty(ref m_PhoneNumber, value);
        
    }
    private string m_PhoneNumber;
    
    public DateTime MinimumDate
    {
        get => m_MinimumDate;
        set => SetProperty(ref m_MinimumDate, value);
        
    }
    private DateTime m_MinimumDate;
    
    public DateTime ServiceStartDate
    {
        get => m_ServiceStartDate;
        set => SetProperty(ref m_ServiceStartDate, value);
        
    }
    private DateTime m_ServiceStartDate;
    
    public SignUpViewModel()
    {
        MinimumDate = DateTime.Today;
    }

    public void InitializeAsync()
    {
    }
    
    public async Task SaveUserData()
    {
        //insert user into db
        var user = new User()
        {
            FirstName = FirstName,
            LastName = LastName,
            Username = Username,
            Password = Password,
            PhoneNumber = PhoneNumber,
            ServiceStartDate = ServiceStartDate
        };
        await App.Database.AddUserAsync(user);
    }
}