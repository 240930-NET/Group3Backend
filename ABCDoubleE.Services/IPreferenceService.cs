namespace ABCDoubleE.Services;
using ABCDoubleE.DTOs;

public interface IPreferenceService
{
    Task<PreferenceDTO?> GetPreferenceByUserIdAsync(int userId);              
    Task<PreferenceDTO?> GetPreferenceByIdAsync(int preferenceId);          
    Task<PreferenceDTO> CreatePreferenceAsync(int userId, PreferenceCreateDTO preferenceCreateDto); 
    Task<PreferenceDTO?> UpdatePreferenceAsync(int userId, PreferenceUpdateDTO preferenceUpdateDto); 
}
