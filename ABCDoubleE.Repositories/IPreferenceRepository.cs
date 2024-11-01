using ABCDoubleE.Models;
public interface IPreferenceRepository
{
    Task<Preference?> GetPreferenceByUserIdAsync(int userId);      
    Task<Preference?> GetPreferenceByIdAsync(int preferenceId);
    Task AddPreferenceAsync(Preference preference);            
    Task UpdatePreferenceAsync(Preference preference);           
    Task DeletePreferenceAsync(int preferenceId);
}
