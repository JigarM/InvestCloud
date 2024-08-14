using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using InvestCloud.Interfaces;
using InvestCloud.Models;
using InvestCloud.Resources.Strings;

namespace InvestCloud.ViewModels;

public class SignInViewModel : BaseViewModel
{
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
    
    public string PopupTitle
    {
        get => m_PopupTitle;
        set => SetProperty(ref m_PopupTitle, value);
    }
    private string m_PopupTitle;
    
    public string PopupMessage
    {
        get => m_PopupMessage;
        set => SetProperty(ref m_PopupMessage, value);
    }
    private string m_PopupMessage;
    
    public SignInViewModel()
    {
    }
    
    public void InitializeAsync()
    {
    }
    
    public async Task<bool> AuthenticateUser()
    {
        m_PopupTitle = AppResources.Error;
        var record = await App.Database.GetUserByUsernameAsync(Username);
        
        if (record == null)
        {
            m_PopupMessage = AppResources.AccountNotExistsError;
            return false;
        }

        if (!record.Password.Equals(Password))
        {
            m_PopupMessage = AppResources.PasswordNotMatchError;
            return false;
        }
        
        m_PopupTitle = AppResources.Success;
        m_PopupMessage = AppResources.SuccessAuthenticationMessage;
        return true;
    }
}